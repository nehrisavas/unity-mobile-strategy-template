using UnityEngine;
using System.Collections.Generic;
using EmpireWars.Core;
using EmpireWars.Data;
using EmpireWars.CameraSystem;
using EmpireWars.WorldMap.Tiles;

namespace EmpireWars.WorldMap
{
    /// <summary>
    /// Chunk-based tile loading sistemi
    /// Sadece gorunur alana yakin tile'lari yukler, uzaktakileri bosaltir
    /// Buyuk haritalar icin performans optimizasyonu
    /// </summary>
    public class ChunkedTileLoader : MonoBehaviour
    {
        public static ChunkedTileLoader Instance { get; private set; }

        [Header("Chunk Settings")]
        [SerializeField] private bool useGameConfig = true; // GameConfig'den ayarlari al

        // GameConfig'den alinan degerler
        private int chunkSize;
        private int loadRadius;
        private float updateInterval;

        [Header("References")]
        [SerializeField] private HexTilePrefabDatabase prefabDatabase;
        [SerializeField] private TerrainDecorationDatabase decorationDatabase;
        [SerializeField] private BuildingDatabase buildingDatabase;

        [Header("Debug")]
        [SerializeField] private bool showDebugGizmos = false;

        // Harita verileri (static, pre-generated)
        private Dictionary<Vector2Int, KingdomMapGenerator.TileData> tileDataMap;

        // Yuklenmis chunk'lar ve tile'lar
        private Dictionary<Vector2Int, ChunkData> loadedChunks;
        private List<Vector2Int> chunksToLoad;  // List for priority sorting
        private List<Vector2Int> chunksToUnload;

        // Object pooling
        private Queue<GameObject> tilePool;
        private const int POOL_INITIAL_SIZE = 256;

        // State
        private Vector2Int lastCameraChunk;
        private float lastUpdateTime;
        private float lastZoom;
        private Transform tilesParent;
        private int mapWidth;
        private int mapHeight;
        private bool isInitialized = false;

        // Cached bounds (performans icin)
        private int cachedMaxChunkX;
        private int cachedMaxChunkY;

        // Dinamik chunk loading
        private int dynamicLoadRadius;
        private const float BUFFER_RATIO = 0.5f;  // Gorunur alanin %50'si buffer

        private class ChunkData
        {
            public Vector2Int ChunkCoord;
            public List<GameObject> Tiles = new List<GameObject>();
            public bool IsLoaded;
        }

        #region Unity Lifecycle

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            loadedChunks = new Dictionary<Vector2Int, ChunkData>();
            chunksToLoad = new List<Vector2Int>();
            chunksToUnload = new List<Vector2Int>();
            tilePool = new Queue<GameObject>();
            tileDataMap = new Dictionary<Vector2Int, KingdomMapGenerator.TileData>();
        }

        private void Start()
        {
            // Tiles parent olustur
            GameObject parent = new GameObject("ChunkedTiles");
            tilesParent = parent.transform;
            tilesParent.SetParent(transform);

            InitializePool();
        }

        private void Update()
        {
            if (!isInitialized) return;

            // Belirli araliklarla chunk kontrolu yap
            if (Time.time - lastUpdateTime >= updateInterval)
            {
                lastUpdateTime = Time.time;
                UpdateVisibleChunks();
            }

            // Her frame birkac chunk isle (stutter onleme)
            // Buyuk haritalar icin 4 chunk/frame
            ProcessChunkQueue(4);
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Harita verilerini generate et ve chunk sistemini baslat
        /// GameConfig'den harita boyutu alinir
        /// </summary>
        public void Initialize()
        {
            // GameConfig'den ayarlari al
            GameConfig.Initialize();

            if (useGameConfig)
            {
                mapWidth = GameConfig.MapWidth;
                mapHeight = GameConfig.MapHeight;
                chunkSize = GameConfig.ChunkSize;
                loadRadius = GameConfig.LoadRadius;
                updateInterval = GameConfig.ChunkUpdateInterval;
            }
            else
            {
                // Fallback degerler
                mapWidth = 200;
                mapHeight = 200;
                chunkSize = 16;
                loadRadius = 4;
                updateInterval = 0.3f;
            }

            // Map generator'i ayarla
            KingdomMapGenerator.SetMapSize(Mathf.Max(mapWidth, mapHeight));

            // Tum tile verilerini pre-generate et (sadece data, GameObject yok)
            GenerateMapData();

            // Chunk bounds'larini cache'le (performans)
            cachedMaxChunkX = Mathf.CeilToInt((float)mapWidth / chunkSize);
            cachedMaxChunkY = Mathf.CeilToInt((float)mapHeight / chunkSize);

            isInitialized = true;
            lastCameraChunk = GetCameraChunk();

            // Ilk yuklemeleri baslat
            UpdateVisibleChunks();

            Debug.Log($"ChunkedTileLoader: {mapWidth}x{mapHeight} harita, chunk:{chunkSize}, radius:{loadRadius}");
            Debug.Log($"ChunkedTileLoader: BuildingDatabase = {(buildingDatabase != null ? "ATANDI" : "NULL!")}");
            Debug.Log($"ChunkedTileLoader: PrefabDatabase = {(prefabDatabase != null ? "ATANDI" : "NULL!")}");
        }

        /// <summary>
        /// Eski API uyumlulugu - GameConfig kullanilmaz
        /// </summary>
        public void Initialize(int width, int height)
        {
            useGameConfig = false;
            mapWidth = width;
            mapHeight = height;
            chunkSize = 16;
            loadRadius = 4;
            updateInterval = 0.3f;

            KingdomMapGenerator.SetMapSize(Mathf.Max(width, height));
            GenerateMapData();

            isInitialized = true;
            lastCameraChunk = GetCameraChunk();
            UpdateVisibleChunks();

            Debug.Log($"ChunkedTileLoader: {width}x{height} harita icin chunk sistemi baslatildi (legacy)");
        }

        private void GenerateMapData()
        {
            tileDataMap.Clear();

            var tiles = KingdomMapGenerator.GenerateMap();
            foreach (var tile in tiles)
            {
                Vector2Int coord = new Vector2Int(tile.Q, tile.R);
                tileDataMap[coord] = tile;
            }

            Debug.Log($"ChunkedTileLoader: {tileDataMap.Count} tile verisi olusturuldu");
        }

        private void InitializePool()
        {
            for (int i = 0; i < POOL_INITIAL_SIZE; i++)
            {
                GameObject poolObj = CreatePooledTile();
                poolObj.SetActive(false);
                tilePool.Enqueue(poolObj);
            }
        }

        private GameObject CreatePooledTile()
        {
            // Basit placeholder object - gercek tile instantiate sirasinda degistirilecek
            GameObject obj = new GameObject("PooledTile");
            obj.transform.SetParent(tilesParent);
            return obj;
        }

        #endregion

        #region Chunk Management

        private Vector2Int GetCameraChunk()
        {
            Vector3 camPos = Vector3.zero;

            if (MapCameraController.Instance != null)
            {
                camPos = MapCameraController.Instance.transform.position;
            }
            else if (Camera.main != null)
            {
                camPos = Camera.main.transform.position;
            }

            // World pozisyonundan hex koordinatina cevir
            int q = Mathf.RoundToInt(camPos.x / (HexMetrics.InnerRadius * 2f));
            int r = Mathf.RoundToInt(camPos.z / (HexMetrics.OuterRadius * 1.5f));

            // Chunk koordinatina cevir
            return new Vector2Int(q / chunkSize, r / chunkSize);
        }

        private void UpdateVisibleChunks()
        {
            Vector2Int currentChunk = GetCameraChunk();
            float currentZoom = GetCurrentCameraZoom();

            // Kamera ayni chunk'ta VE zoom degismedi, kontrol gereksiz
            if (currentChunk == lastCameraChunk &&
                Mathf.Abs(currentZoom - lastZoom) < 5f &&
                loadedChunks.Count > 0)
                return;

            lastCameraChunk = currentChunk;
            lastZoom = currentZoom;

            // Dinamik load radius hesapla (kamera zoom'una gore)
            CalculateDynamicLoadRadius(currentZoom);

            // Gorunur olmasi gereken chunk'lari hesapla
            HashSet<Vector2Int> visibleChunks = new HashSet<Vector2Int>();
            for (int dx = -dynamicLoadRadius; dx <= dynamicLoadRadius; dx++)
            {
                for (int dy = -dynamicLoadRadius; dy <= dynamicLoadRadius; dy++)
                {
                    Vector2Int chunkCoord = new Vector2Int(currentChunk.x + dx, currentChunk.y + dy);

                    // Harita sinirlari icinde mi?
                    if (IsChunkInBounds(chunkCoord))
                    {
                        visibleChunks.Add(chunkCoord);
                    }
                }
            }

            // Yuklenmesi gereken chunk'lari listeye ekle
            chunksToLoad.Clear();
            foreach (var chunk in visibleChunks)
            {
                if (!loadedChunks.ContainsKey(chunk))
                {
                    chunksToLoad.Add(chunk);
                }
            }

            // Yakin chunk'lar once yuklensin (Google Maps tarzi)
            chunksToLoad.Sort((a, b) =>
            {
                float distA = Vector2Int.Distance(a, currentChunk);
                float distB = Vector2Int.Distance(b, currentChunk);
                return distA.CompareTo(distB);
            });

            // Bosaltilmasi gereken chunk'lari listeye ekle
            chunksToUnload.Clear();
            foreach (var kvp in loadedChunks)
            {
                if (!visibleChunks.Contains(kvp.Key))
                {
                    chunksToUnload.Add(kvp.Key);
                }
            }

            // Uzak chunk'lar once bosaltilsin
            chunksToUnload.Sort((a, b) =>
            {
                float distA = Vector2Int.Distance(a, currentChunk);
                float distB = Vector2Int.Distance(b, currentChunk);
                return distB.CompareTo(distA);  // Ters siralama
            });
        }

        private void ProcessChunkQueue(int maxPerFrame)
        {
            int processed = 0;

            // Oncelik 1: Uzak chunk'lari hizla bosalt (bellek tasarrufu)
            int unloadCount = Mathf.Min(chunksToUnload.Count, maxPerFrame * 2);
            for (int i = 0; i < unloadCount && chunksToUnload.Count > 0; i++)
            {
                Vector2Int chunk = chunksToUnload[0];
                chunksToUnload.RemoveAt(0);
                UnloadChunk(chunk);
            }

            // Oncelik 2: Yakin chunk'lari yukle
            while (chunksToLoad.Count > 0 && processed < maxPerFrame)
            {
                Vector2Int chunk = chunksToLoad[0];
                chunksToLoad.RemoveAt(0);
                LoadChunk(chunk);
                processed++;
            }
        }

        private bool IsChunkInBounds(Vector2Int chunkCoord)
        {
            // Cached degerler kullan (Initialize'da hesaplandi)
            return chunkCoord.x >= 0 && chunkCoord.x < cachedMaxChunkX &&
                   chunkCoord.y >= 0 && chunkCoord.y < cachedMaxChunkY;
        }

        /// <summary>
        /// Kameranin mevcut zoom degerini al
        /// </summary>
        private float GetCurrentCameraZoom()
        {
            if (MapCameraController.Instance != null)
            {
                return MapCameraController.Instance.GetCurrentZoom();
            }
            if (Camera.main != null && Camera.main.orthographic)
            {
                return Camera.main.orthographicSize;
            }
            return GameConfig.DefaultZoom;
        }

        /// <summary>
        /// Kamera zoom'una gore dinamik load radius hesapla
        /// Zoom out = daha fazla chunk yukle
        /// Zoom in = daha az chunk yukle
        ///
        /// Formul:
        /// - Gorunur alan (chunk): kamera gorunum / chunk boyutu
        /// - Buffer: gorunur alanin %50'si
        /// - Toplam radius: gorunur + buffer
        /// </summary>
        private void CalculateDynamicLoadRadius(float cameraZoom)
        {
            // Kamera gorunum alani (world units)
            float viewHeight = cameraZoom * 2f;  // orthographicSize * 2
            float viewWidth = viewHeight * 1.77f;  // 16:9 aspect ratio

            // Chunk boyutu (world units)
            float chunkWorldSize = chunkSize * HexMetrics.InnerRadius * 2f;

            // Gorunur chunk sayisi (tek yonde)
            int visibleChunksX = Mathf.CeilToInt(viewWidth / chunkWorldSize / 2f);
            int visibleChunksY = Mathf.CeilToInt(viewHeight / chunkWorldSize / 2f);
            int visibleRadius = Mathf.Max(visibleChunksX, visibleChunksY);

            // Buffer ekle (%50 daha fazla)
            int bufferChunks = Mathf.CeilToInt(visibleRadius * BUFFER_RATIO);

            // Toplam radius (minimum 2, maximum ayarlanabilir)
            dynamicLoadRadius = Mathf.Clamp(visibleRadius + bufferChunks, 2, 20);
        }

        #endregion

        #region Loading/Unloading

        private void LoadChunk(Vector2Int chunkCoord)
        {
            if (loadedChunks.ContainsKey(chunkCoord)) return;

            ChunkData chunkData = new ChunkData
            {
                ChunkCoord = chunkCoord,
                IsLoaded = true
            };

            int startQ = chunkCoord.x * chunkSize;
            int startR = chunkCoord.y * chunkSize;

            for (int dq = 0; dq < chunkSize; dq++)
            {
                for (int dr = 0; dr < chunkSize; dr++)
                {
                    int q = startQ + dq;
                    int r = startR + dr;

                    // Harita sinirlari icinde mi?
                    if (q >= mapWidth || r >= mapHeight) continue;

                    Vector2Int coord = new Vector2Int(q, r);
                    if (tileDataMap.TryGetValue(coord, out var tileData))
                    {
                        GameObject tile = CreateTileFromData(tileData);
                        if (tile != null)
                        {
                            chunkData.Tiles.Add(tile);
                        }
                    }
                }
            }

            loadedChunks[chunkCoord] = chunkData;
        }

        private void UnloadChunk(Vector2Int chunkCoord)
        {
            if (!loadedChunks.TryGetValue(chunkCoord, out var chunkData))
                return;

            foreach (var tile in chunkData.Tiles)
            {
                if (tile != null)
                {
                    // Pool'a geri koy yerine destroy (memory management)
                    Destroy(tile);
                }
            }

            chunkData.Tiles.Clear();
            loadedChunks.Remove(chunkCoord);
        }

        private GameObject CreateTileFromData(KingdomMapGenerator.TileData tileData)
        {
            if (prefabDatabase == null)
            {
                Debug.LogError($"ChunkedTileLoader: PrefabDatabase NULL! Tile olusturulamiyor: ({tileData.Q}, {tileData.R})");
                return null;
            }

            HexCoordinates coords = new HexCoordinates(tileData.Q, tileData.R);
            Vector3 worldPos = coords.ToWorldPosition();

            GameObject prefab = prefabDatabase.GetTilePrefab(tileData.Terrain);
            if (prefab == null)
            {
                Debug.LogWarning($"ChunkedTileLoader: Terrain prefab bulunamadi: {tileData.Terrain} at ({tileData.Q}, {tileData.R})");
                return null;
            }

            GameObject tile = Instantiate(prefab, worldPos, Quaternion.identity);
            tile.name = $"Hex_{tileData.Q}_{tileData.R}";
            tile.transform.SetParent(tilesParent);

            // Yol ve köprü tile'ları için rotasyon uygula
            if ((tileData.Terrain == TerrainType.Road || tileData.Terrain == TerrainType.Bridge) &&
                tileData.BuildingRotation != 0)
            {
                tile.transform.rotation = Quaternion.Euler(0, tileData.BuildingRotation, 0);
            }

            // HexTile component
            HexTile hexTile = tile.GetComponent<HexTile>();
            if (hexTile == null)
            {
                hexTile = tile.AddComponent<HexTile>();
            }
            hexTile.Initialize(coords, tileData.Terrain);

            // Dekorasyon
            if (decorationDatabase != null && decorationDatabase.RequiresDecoration(tileData.Terrain))
            {
                AddDecoration(tile, coords, tileData.Terrain);
            }

            // Bina
            if (tileData.HasBuilding)
            {
                if (buildingDatabase == null)
                {
                    Debug.LogError($"ChunkedTileLoader: BuildingDatabase NULL! Bina: {tileData.BuildingType} at ({tileData.Q}, {tileData.R})");
                }
                else
                {
                    PlaceBuilding(tile, tileData.BuildingType, tileData.BuildingRotation);
                    // Bina seviyesi göstergesi ekle
                    if (tileData.BuildingLevel > 0)
                    {
                        hexTile.SetBuildingInfo(tileData.BuildingType, tileData.BuildingLevel);
                        tile.name = $"Hex_{tileData.Q}_{tileData.R}_{tileData.BuildingType}_Lv{tileData.BuildingLevel}";
                    }
                }
            }

            // Maden bilgisi
            if (tileData.MineLevel > 0)
            {
                hexTile.SetMineInfo(tileData.MineLevel, tileData.MineType);
                tile.name = $"Hex_{tileData.Q}_{tileData.R}_{tileData.MineType}_Lv{tileData.MineLevel}";
            }

            return tile;
        }

        private void AddDecoration(GameObject tile, HexCoordinates coords, TerrainType terrainType)
        {
            // Coast, River, Road vb. zaten gorsel iceriyor
            if (TileHasBuiltInVisuals(terrainType)) return;

            int seed = coords.Q * 1000 + coords.R;
            GameObject decorPrefab = decorationDatabase.GetRandomDecoration(terrainType, seed);
            if (decorPrefab == null) return;

            GameObject decor = Instantiate(decorPrefab, tile.transform);
            decor.name = $"Decor_{terrainType}";

            float yOffset = decorationDatabase.GetDecorationYOffset(terrainType);
            decor.transform.localPosition = new Vector3(0, yOffset, 0);

            float scale = decorationDatabase.GetDecorationScale(terrainType);
            decor.transform.localScale = Vector3.one * scale;

            float rotationY = (seed % 6) * 60f;
            decor.transform.localRotation = Quaternion.Euler(0, rotationY, 0);
        }

        private bool TileHasBuiltInVisuals(TerrainType terrainType)
        {
            return terrainType switch
            {
                TerrainType.Coast => true,
                TerrainType.River => true,
                TerrainType.Bridge => true,
                TerrainType.Road => true,
                TerrainType.Water => true,
                TerrainType.GrassSlopedHigh => true,
                TerrainType.GrassSlopedLow => true,
                _ => false
            };
        }

        private void PlaceBuilding(GameObject tile, string buildingType, float customRotation = 0f)
        {
            if (buildingDatabase == null)
            {
                Debug.LogError($"ChunkedTileLoader: BuildingDatabase NULL! Bina yerlestirilemiyor: {buildingType}");
                return;
            }

            GameObject buildingPrefab = buildingDatabase.GetBuildingPrefab(buildingType);
            if (buildingPrefab == null)
            {
                Debug.LogWarning($"ChunkedTileLoader: Bina prefab bulunamadi: {buildingType}");
                return;
            }

            GameObject building = Instantiate(buildingPrefab, tile.transform);
            building.name = $"Building_{buildingType}";
            building.transform.localPosition = new Vector3(0, 0.1f, 0);
            building.transform.localScale = Vector3.one * 0.8f;

            // Rotasyon ayarla - TileData'dan gelen değer varsa onu kullan
            // Yoksa varsayılan rotasyon mantığını uygula
            float rotation = customRotation != 0f ? customRotation : GetDefaultBuildingRotation(buildingType);
            if (rotation != 0)
            {
                building.transform.localRotation = Quaternion.Euler(0, rotation, 0);
            }
        }

        /// <summary>
        /// TileData'da rotasyon belirtilmemişse varsayılan rotasyon
        /// </summary>
        private float GetDefaultBuildingRotation(string buildingType)
        {
            if (string.IsNullOrEmpty(buildingType)) return 0;

            string lowerType = buildingType.ToLower();

            // Köşe duvarları - pozisyona göre farklı açılar
            if (lowerType.Contains("wall_corner"))
            {
                return 0f;
            }

            // Çitler varsayılan 90°
            if (lowerType.Contains("fence"))
            {
                return 90f;
            }

            return 0f;
        }

        #endregion

        #region Public API

        public int GetLoadedTileCount()
        {
            int count = 0;
            foreach (var chunk in loadedChunks.Values)
            {
                count += chunk.Tiles.Count;
            }
            return count;
        }

        public int GetLoadedChunkCount()
        {
            return loadedChunks.Count;
        }

        public void ForceReload()
        {
            // Tum chunk'lari bosalt
            foreach (var chunk in loadedChunks.Values)
            {
                foreach (var tile in chunk.Tiles)
                {
                    if (tile != null) Destroy(tile);
                }
            }
            loadedChunks.Clear();

            // Yeniden yukle
            lastCameraChunk = new Vector2Int(-999, -999);
            UpdateVisibleChunks();
        }

        #endregion

        #region Debug

        private void OnDrawGizmos()
        {
            if (!showDebugGizmos || !isInitialized) return;

            // Loaded chunk'lari goster
            foreach (var kvp in loadedChunks)
            {
                Vector2Int chunk = kvp.Key;
                Vector3 center = new Vector3(
                    (chunk.x * chunkSize + chunkSize / 2f) * HexMetrics.InnerRadius * 2f,
                    1f,
                    (chunk.y * chunkSize + chunkSize / 2f) * HexMetrics.OuterRadius * 1.5f
                );

                float size = chunkSize * HexMetrics.InnerRadius * 2f;

                Gizmos.color = new Color(0, 1, 0, 0.3f);
                Gizmos.DrawCube(center, new Vector3(size, 0.5f, size));
            }

            // Kamera chunk'ini goster
            Vector2Int camChunk = GetCameraChunk();
            Vector3 camCenter = new Vector3(
                (camChunk.x * chunkSize + chunkSize / 2f) * HexMetrics.InnerRadius * 2f,
                2f,
                (camChunk.y * chunkSize + chunkSize / 2f) * HexMetrics.OuterRadius * 1.5f
            );
            float camSize = chunkSize * HexMetrics.InnerRadius * 2f;

            Gizmos.color = new Color(1, 1, 0, 0.5f);
            Gizmos.DrawWireCube(camCenter, new Vector3(camSize, 1f, camSize));
        }

        #endregion
    }
}
