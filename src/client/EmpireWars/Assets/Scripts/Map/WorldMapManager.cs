using UnityEngine;
using EmpireWars.Core;
using EmpireWars.Data;
using System.Collections.Generic;

namespace EmpireWars.Map
{
    /// <summary>
    /// Dunya haritasi yoneticisi
    /// 2000x2000 hex haritayi yonetir
    /// Dokuman referansi: docs/01-game-design/map/MAP-SYSTEM.md
    /// </summary>
    public class WorldMapManager : MonoBehaviour
    {
        public static WorldMapManager Instance { get; private set; }

        [Header("Prefablar")]
        [SerializeField] private GameObject hexCellPrefab;
        [SerializeField] private GameObject chunkPrefab;
        [SerializeField] private GameObject fogPrefab;

        [Header("Arazi Modelleri")]
        [SerializeField] private GameObject grassTilePrefab;
        [SerializeField] private GameObject waterTilePrefab;
        [SerializeField] private GameObject mountainPrefab;
        [SerializeField] private GameObject forestPrefab;
        [SerializeField] private GameObject hillPrefab;
        [SerializeField] private GameObject desertTilePrefab;
        [SerializeField] private GameObject snowTilePrefab;
        [SerializeField] private GameObject swampTilePrefab;

        [Header("Ayarlar")]
        [SerializeField] private int viewDistanceChunks = 5; // Kameradan kac chunk uzakliga kadar yukle
        [SerializeField] private int unloadDistanceChunks = 8; // Kac chunk uzaklikta unload et
        [SerializeField] private bool generateOnStart = true;

        [Header("Debug")]
        [SerializeField] private bool showChunkBounds = false;
        [SerializeField] private int loadedChunkCount = 0;

        // Veri yapilari
        private HexChunk[,] chunks;
        private Dictionary<long, HexCell> cellLookup = new Dictionary<long, HexCell>();
        private List<HexChunk> loadedChunks = new List<HexChunk>();

        // Kamera referansi
        private UnityEngine.Camera mainCamera;
        private HexCoordinates lastCameraChunkCoords;

        // Events
        public System.Action<HexCell> OnCellClicked;
        public System.Action<HexCell> OnCellHovered;
        public System.Action OnMapGenerated;

        #region Unity Lifecycle

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            chunks = new HexChunk[HexMetrics.ChunksX, HexMetrics.ChunksZ];
        }

        private void Start()
        {
            mainCamera = UnityEngine.Camera.main;

            if (generateOnStart)
            {
                GenerateMap();
            }
        }

        private void Update()
        {
            UpdateVisibleChunks();
            HandleInput();
        }

        #endregion

        #region Map Generation

        public void GenerateMap()
        {
            Debug.Log($"Harita olusturuluyor: {HexMetrics.MapWidth}x{HexMetrics.MapHeight} ({HexMetrics.TotalTiles:N0} hex)");

            // Chunk'lari olustur
            CreateChunks();

            // Hucreleri olustur
            CreateCells();

            // Komsu baglantilarini kur
            SetupNeighbors();

            // Arazi olustur (prosedural veya preset)
            GenerateTerrain();

            Debug.Log("Harita olusturuldu!");
            OnMapGenerated?.Invoke();
        }

        private void CreateChunks()
        {
            Transform chunksParent = new GameObject("Chunks").transform;
            chunksParent.SetParent(transform);

            for (int z = 0; z < HexMetrics.ChunksZ; z++)
            {
                for (int x = 0; x < HexMetrics.ChunksX; x++)
                {
                    HexChunk chunk = CreateChunk(x, z);
                    chunk.transform.SetParent(chunksParent);
                    chunks[x, z] = chunk;
                }
            }
        }

        private HexChunk CreateChunk(int x, int z)
        {
            GameObject chunkObj;
            if (chunkPrefab != null)
            {
                chunkObj = Instantiate(chunkPrefab);
            }
            else
            {
                chunkObj = new GameObject();
            }

            HexChunk chunk = chunkObj.GetComponent<HexChunk>();
            if (chunk == null)
            {
                chunk = chunkObj.AddComponent<HexChunk>();
            }

            chunk.Initialize(x, z, this);
            chunk.gameObject.SetActive(false); // Baslangicta deaktif

            return chunk;
        }

        private void CreateCells()
        {
            int halfWidth = HexMetrics.MapWidth / 2;
            int halfHeight = HexMetrics.MapHeight / 2;

            for (int r = -halfHeight; r < halfHeight; r++)
            {
                for (int q = -halfWidth; q < halfWidth; q++)
                {
                    CreateCell(new HexCoordinates(q, r));
                }
            }
        }

        private HexCell CreateCell(HexCoordinates coords)
        {
            // Hangi chunk'a ait
            var (chunkX, chunkZ) = HexMetrics.GetChunkIndex(coords);
            var (localX, localZ) = HexMetrics.GetLocalIndex(coords);

            if (chunkX < 0 || chunkX >= HexMetrics.ChunksX ||
                chunkZ < 0 || chunkZ >= HexMetrics.ChunksZ)
            {
                return null;
            }

            HexChunk chunk = chunks[chunkX, chunkZ];

            // Cell olustur
            GameObject cellObj;
            if (hexCellPrefab != null)
            {
                cellObj = Instantiate(hexCellPrefab);
            }
            else
            {
                cellObj = new GameObject();
            }

            HexCell cell = cellObj.GetComponent<HexCell>();
            if (cell == null)
            {
                cell = cellObj.AddComponent<HexCell>();
            }

            cell.Initialize(coords, chunk);
            chunk.AddCell(localX, localZ, cell);

            // Lookup'a ekle
            cellLookup[coords.ToUniqueId()] = cell;

            return cell;
        }

        private void SetupNeighbors()
        {
            foreach (var cell in cellLookup.Values)
            {
                HexCoordinates coords = cell.Coordinates;

                for (int i = 0; i < 6; i++)
                {
                    HexCoordinates neighborCoords = coords.GetNeighbor(i);
                    HexCell neighbor = GetCell(neighborCoords);

                    if (neighbor != null)
                    {
                        cell.SetNeighbor(i, neighbor);
                    }
                }
            }
        }

        #endregion

        #region Terrain Generation

        private void GenerateTerrain()
        {
            // Prosedural arazi olusturma
            // Perlin noise ile dogal gorunum

            float scale = 0.02f;
            float mountainThreshold = 0.75f;
            float waterThreshold = 0.25f;
            float forestThreshold = 0.55f;

            foreach (var cell in cellLookup.Values)
            {
                HexCoordinates coords = cell.Coordinates;

                // Perlin noise ile yukseklik
                float noiseValue = Mathf.PerlinNoise(
                    (coords.Q + 1000) * scale,
                    (coords.R + 1000) * scale
                );

                // Ikinci noise katmani (detay)
                float detailNoise = Mathf.PerlinNoise(
                    (coords.Q + 5000) * scale * 2f,
                    (coords.R + 5000) * scale * 2f
                );

                float combinedNoise = (noiseValue * 0.7f) + (detailNoise * 0.3f);

                // Arazi tipi belirle
                TerrainType terrain;

                if (combinedNoise > mountainThreshold)
                {
                    terrain = TerrainType.Mountain;
                }
                else if (combinedNoise < waterThreshold)
                {
                    terrain = TerrainType.Water;
                }
                else if (combinedNoise > forestThreshold)
                {
                    terrain = TerrainType.Forest;
                }
                else if (combinedNoise > 0.45f && combinedNoise < 0.55f)
                {
                    terrain = TerrainType.Hill;
                }
                else
                {
                    terrain = TerrainType.Grass;
                }

                // Bolgeye gore ozel araziler
                int zone = coords.GetZone();
                if (zone == 1 && combinedNoise > 0.3f && combinedNoise < 0.5f)
                {
                    // Merkez bolgede daha fazla kaynak alani
                    if (Random.value < 0.1f)
                    {
                        terrain = TerrainType.GoldMine;
                    }
                }

                // Kar bolgeleri (kuzey)
                if (coords.R < -HexMetrics.MapHeight / 3 && terrain == TerrainType.Grass)
                {
                    if (Random.value < 0.3f)
                    {
                        terrain = TerrainType.Snow;
                    }
                }

                // Col bolgeleri (guney)
                if (coords.R > HexMetrics.MapHeight / 3 && terrain == TerrainType.Grass)
                {
                    if (Random.value < 0.3f)
                    {
                        terrain = TerrainType.Desert;
                    }
                }

                cell.TerrainType = terrain;
            }
        }

        #endregion

        #region Chunk Visibility

        private void UpdateVisibleChunks()
        {
            if (mainCamera == null) return;

            // Kamera pozisyonundan chunk koordinati hesapla
            Vector3 cameraPos = mainCamera.transform.position;
            HexCoordinates cameraCoords = HexCoordinates.FromPosition(cameraPos);
            var (centerChunkX, centerChunkZ) = HexMetrics.GetChunkIndex(cameraCoords);

            // Kamera chunk degisti mi?
            if (cameraCoords == lastCameraChunkCoords) return;
            lastCameraChunkCoords = cameraCoords;

            // Gorunur chunk'lari guncelle
            List<HexChunk> newLoadedChunks = new List<HexChunk>();

            for (int z = centerChunkZ - viewDistanceChunks; z <= centerChunkZ + viewDistanceChunks; z++)
            {
                for (int x = centerChunkX - viewDistanceChunks; x <= centerChunkX + viewDistanceChunks; x++)
                {
                    if (x >= 0 && x < HexMetrics.ChunksX && z >= 0 && z < HexMetrics.ChunksZ)
                    {
                        HexChunk chunk = chunks[x, z];
                        chunk.Load();
                        newLoadedChunks.Add(chunk);
                    }
                }
            }

            // Uzak chunk'lari unload et
            foreach (var chunk in loadedChunks)
            {
                if (!newLoadedChunks.Contains(chunk))
                {
                    int distance = Mathf.Abs(chunk.ChunkX - centerChunkX) + Mathf.Abs(chunk.ChunkZ - centerChunkZ);
                    if (distance > unloadDistanceChunks)
                    {
                        chunk.Unload();
                    }
                }
            }

            loadedChunks = newLoadedChunks;
            loadedChunkCount = loadedChunks.Count;
        }

        #endregion

        #region Cell Access

        public HexCell GetCell(HexCoordinates coords)
        {
            if (cellLookup.TryGetValue(coords.ToUniqueId(), out HexCell cell))
            {
                return cell;
            }
            return null;
        }

        public HexCell GetCell(Vector3 worldPosition)
        {
            HexCoordinates coords = HexCoordinates.FromPosition(worldPosition);
            return GetCell(coords);
        }

        public HexCell GetCell(int q, int r)
        {
            return GetCell(new HexCoordinates(q, r));
        }

        public List<HexCell> GetCellsInRange(HexCoordinates center, int range)
        {
            List<HexCell> cells = new List<HexCell>();

            for (int q = -range; q <= range; q++)
            {
                for (int r = Mathf.Max(-range, -q - range); r <= Mathf.Min(range, -q + range); r++)
                {
                    HexCoordinates coords = center + new HexCoordinates(q, r);
                    HexCell cell = GetCell(coords);
                    if (cell != null)
                    {
                        cells.Add(cell);
                    }
                }
            }

            return cells;
        }

        #endregion

        #region Input Handling

        private void HandleInput()
        {
            // Mouse tiklamasi
            if (Input.GetMouseButtonDown(0))
            {
                HandleClick();
            }

            // Mouse hover (opsiyonel, performans icin throttle edilebilir)
            // HandleHover();
        }

        private void HandleClick()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                HexCell cell = hit.collider.GetComponentInParent<HexCell>();
                if (cell != null)
                {
                    OnCellClicked?.Invoke(cell);
                    Debug.Log($"Cell tiklandi: {cell.Coordinates} - {cell.TerrainType}");
                }
            }
        }

        #endregion

        #region Visual Updates

        public void OnCellTerrainChanged(HexCell cell)
        {
            // Arazi modeli guncelle
            UpdateCellTerrainVisual(cell);
        }

        private void UpdateCellTerrainVisual(HexCell cell)
        {
            GameObject prefab = GetTerrainPrefab(cell.TerrainType);
            if (prefab != null)
            {
                GameObject model = Instantiate(prefab);
                cell.SetTerrainModel(model);
            }
        }

        private GameObject GetTerrainPrefab(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Grass => grassTilePrefab,
                TerrainType.Water => waterTilePrefab,
                TerrainType.Mountain => mountainPrefab,
                TerrainType.Forest => forestPrefab,
                TerrainType.Hill => hillPrefab,
                TerrainType.Desert => desertTilePrefab,
                TerrainType.Snow => snowTilePrefab,
                TerrainType.Swamp => swampTilePrefab,
                _ => grassTilePrefab
            };
        }

        #endregion

        #region Debug

        private void OnDrawGizmos()
        {
            if (!showChunkBounds || chunks == null) return;

            Gizmos.color = Color.green;
            foreach (var chunk in chunks)
            {
                if (chunk != null && chunk.IsLoaded)
                {
                    Bounds bounds = chunk.GetBounds();
                    Gizmos.DrawWireCube(bounds.center, bounds.size);
                }
            }
        }

        #endregion
    }
}
