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
        [SerializeField] private int chunkSize = 8; // Her chunk 8x8 tile
        [SerializeField] private int loadRadius = 3; // Merkez chunk + 3 chunk yaricap
        [SerializeField] private float updateInterval = 0.5f;

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
        private Queue<Vector2Int> chunksToLoad;
        private Queue<Vector2Int> chunksToUnload;

        // Object pooling
        private Queue<GameObject> tilePool;
        private const int POOL_INITIAL_SIZE = 256;

        // State
        private Vector2Int lastCameraChunk;
        private float lastUpdateTime;
        private Transform tilesParent;
        private int mapWidth;
        private int mapHeight;
        private bool isInitialized = false;

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
            chunksToLoad = new Queue<Vector2Int>();
            chunksToUnload = new Queue<Vector2Int>();
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
            ProcessChunkQueue(2);
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Harita verilerini generate et ve chunk sistemini baslat
        /// </summary>
        public void Initialize(int width, int height)
        {
            mapWidth = width;
            mapHeight = height;

            // Map size'i ayarla
            KingdomMapGenerator.SetMapSize(Mathf.Max(width, height));

            // Tum tile verilerini pre-generate et (sadece data, GameObject yok)
            GenerateMapData();

            // Kamera sinirlarini ayarla
            SetupCameraBounds();

            isInitialized = true;
            lastCameraChunk = GetCameraChunk();

            // Ilk yuklemeleri baslat
            UpdateVisibleChunks();

            Debug.Log($"ChunkedTileLoader: {width}x{height} harita icin chunk sistemi baslatildi");
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

        private void SetupCameraBounds()
        {
            if (MapCameraController.Instance != null)
            {
                float worldWidth = mapWidth * HexMetrics.InnerRadius * 2f;
                float worldHeight = mapHeight * HexMetrics.OuterRadius * 1.5f;
                MapCameraController.Instance.SetMapBounds(worldWidth, worldHeight, 0, 0);
            }
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

            // Kamera ayni chunk'ta, kontrol gereksiz
            if (currentChunk == lastCameraChunk && loadedChunks.Count > 0)
                return;

            lastCameraChunk = currentChunk;

            // Gorunur olmasi gereken chunk'lari hesapla
            HashSet<Vector2Int> visibleChunks = new HashSet<Vector2Int>();
            for (int dx = -loadRadius; dx <= loadRadius; dx++)
            {
                for (int dy = -loadRadius; dy <= loadRadius; dy++)
                {
                    Vector2Int chunkCoord = new Vector2Int(currentChunk.x + dx, currentChunk.y + dy);

                    // Harita sinirlari icinde mi?
                    if (IsChunkInBounds(chunkCoord))
                    {
                        visibleChunks.Add(chunkCoord);
                    }
                }
            }

            // Yuklenmesi gereken chunk'lari queue'ya ekle
            foreach (var chunk in visibleChunks)
            {
                if (!loadedChunks.ContainsKey(chunk) && !chunksToLoad.Contains(chunk))
                {
                    chunksToLoad.Enqueue(chunk);
                }
            }

            // Bosaltilmasi gereken chunk'lari queue'ya ekle
            List<Vector2Int> toUnload = new List<Vector2Int>();
            foreach (var kvp in loadedChunks)
            {
                if (!visibleChunks.Contains(kvp.Key))
                {
                    toUnload.Add(kvp.Key);
                }
            }
            foreach (var chunk in toUnload)
            {
                if (!chunksToUnload.Contains(chunk))
                {
                    chunksToUnload.Enqueue(chunk);
                }
            }
        }

        private void ProcessChunkQueue(int maxPerFrame)
        {
            int processed = 0;

            // Oncelik: unload (bellek tasarrufu)
            while (chunksToUnload.Count > 0 && processed < maxPerFrame)
            {
                Vector2Int chunk = chunksToUnload.Dequeue();
                UnloadChunk(chunk);
                processed++;
            }

            // Sonra load
            while (chunksToLoad.Count > 0 && processed < maxPerFrame)
            {
                Vector2Int chunk = chunksToLoad.Dequeue();
                LoadChunk(chunk);
                processed++;
            }
        }

        private bool IsChunkInBounds(Vector2Int chunkCoord)
        {
            int maxChunkX = Mathf.CeilToInt((float)mapWidth / chunkSize);
            int maxChunkY = Mathf.CeilToInt((float)mapHeight / chunkSize);

            return chunkCoord.x >= 0 && chunkCoord.x < maxChunkX &&
                   chunkCoord.y >= 0 && chunkCoord.y < maxChunkY;
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
            if (prefabDatabase == null) return null;

            HexCoordinates coords = new HexCoordinates(tileData.Q, tileData.R);
            Vector3 worldPos = coords.ToWorldPosition();

            GameObject prefab = prefabDatabase.GetTilePrefab(tileData.Terrain);
            if (prefab == null) return null;

            GameObject tile = Instantiate(prefab, worldPos, Quaternion.identity);
            tile.name = $"Hex_{tileData.Q}_{tileData.R}";
            tile.transform.SetParent(tilesParent);

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
            if (tileData.HasBuilding && buildingDatabase != null)
            {
                PlaceBuilding(tile, tileData.BuildingType);
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

        private void PlaceBuilding(GameObject tile, string buildingType)
        {
            GameObject buildingPrefab = buildingDatabase.GetBuildingPrefab(buildingType);
            if (buildingPrefab == null) return;

            GameObject building = Instantiate(buildingPrefab, tile.transform);
            building.name = $"Building_{buildingType}";
            building.transform.localPosition = new Vector3(0, 0.1f, 0);
            building.transform.localScale = Vector3.one * 0.8f;
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
