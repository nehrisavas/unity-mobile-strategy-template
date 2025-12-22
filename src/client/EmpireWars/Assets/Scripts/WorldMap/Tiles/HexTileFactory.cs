using UnityEngine;
using EmpireWars.Core;
using EmpireWars.Data;

namespace EmpireWars.WorldMap.Tiles
{
    /// <summary>
    /// Hex tile'larini olusturan factory sinifi
    /// KayKit Medieval Hexagon Pack prefab'larini kullanir
    /// </summary>
    public class HexTileFactory : MonoBehaviour
    {
        [Header("Database")]
        [SerializeField] private HexTilePrefabDatabase prefabDatabase;

        [Header("Settings")]
        [SerializeField] private Transform tilesParent;
        [SerializeField] private float tileScale = 1f;

        private static HexTileFactory _instance;
        public static HexTileFactory Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        /// <summary>
        /// Belirtilen koordinatlara hex tile olusturur
        /// </summary>
        public GameObject CreateTile(HexCoordinates coords, TerrainType terrainType)
        {
            if (prefabDatabase == null)
            {
                Debug.LogError("HexTileFactory: PrefabDatabase atanmamis!");
                return CreateFallbackTile(coords);
            }

            GameObject prefab = prefabDatabase.GetTilePrefab(terrainType);
            if (prefab == null)
            {
                Debug.LogWarning($"HexTileFactory: {terrainType} icin prefab bulunamadi, fallback kullaniliyor.");
                return CreateFallbackTile(coords);
            }

            return InstantiateTile(prefab, coords, terrainType);
        }

        /// <summary>
        /// Yol tile'i olusturur
        /// </summary>
        public GameObject CreateRoadTile(HexCoordinates coords, int connectionMask)
        {
            if (prefabDatabase == null) return null;

            GameObject prefab = prefabDatabase.GetRoadTile(connectionMask);
            if (prefab == null) return null;

            return InstantiateTile(prefab, coords, TerrainType.Road, $"Road_{coords.Q}_{coords.R}");
        }

        /// <summary>
        /// Nehir tile'i olusturur
        /// </summary>
        public GameObject CreateRiverTile(HexCoordinates coords, int connectionMask)
        {
            if (prefabDatabase == null) return null;

            GameObject prefab = prefabDatabase.GetRiverTile(connectionMask);
            if (prefab == null) return null;

            return InstantiateTile(prefab, coords, TerrainType.Water, $"River_{coords.Q}_{coords.R}");
        }

        private GameObject InstantiateTile(GameObject prefab, HexCoordinates coords, TerrainType terrainType, string customName = null)
        {
            Vector3 worldPos = coords.ToWorldPosition();

            GameObject tile = Instantiate(prefab, worldPos, Quaternion.identity);
            tile.name = customName ?? $"Hex_{coords.Q}_{coords.R}_{terrainType}";

            if (tileScale != 1f)
            {
                tile.transform.localScale = Vector3.one * tileScale;
            }

            if (tilesParent != null)
            {
                tile.transform.SetParent(tilesParent);
            }

            // Collider ekle (mouse events icin gerekli)
            EnsureCollider(tile);

            // HexTile component ekle
            HexTile hexTile = tile.GetComponent<HexTile>();
            if (hexTile == null)
            {
                hexTile = tile.AddComponent<HexTile>();
            }
            hexTile.Initialize(coords, terrainType);

            return tile;
        }

        /// <summary>
        /// Tile'in collider'i oldugundan emin ol
        /// </summary>
        private void EnsureCollider(GameObject tile)
        {
            Collider existingCollider = tile.GetComponent<Collider>();
            if (existingCollider != null) return;

            // Child'larda collider var mi?
            existingCollider = tile.GetComponentInChildren<Collider>();
            if (existingCollider != null) return;

            // MeshFilter varsa MeshCollider ekle
            MeshFilter meshFilter = tile.GetComponent<MeshFilter>();
            if (meshFilter == null)
            {
                meshFilter = tile.GetComponentInChildren<MeshFilter>();
            }

            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                MeshCollider meshCollider = tile.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = meshFilter.sharedMesh;
                meshCollider.convex = true;
            }
            else
            {
                // Fallback: BoxCollider ekle
                BoxCollider boxCollider = tile.AddComponent<BoxCollider>();
                boxCollider.size = new Vector3(HexMetrics.InnerRadius * 2f, 0.5f, HexMetrics.InnerRadius * 2f);
            }
        }

        /// <summary>
        /// Prefab yoksa basit cylinder ile fallback tile olusturur
        /// </summary>
        private GameObject CreateFallbackTile(HexCoordinates coords)
        {
            Vector3 worldPos = coords.ToWorldPosition();

            GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            tile.name = $"Hex_{coords.Q}_{coords.R}_Fallback";
            tile.transform.position = worldPos;
            tile.transform.localScale = new Vector3(
                HexMetrics.InnerRadius * 2f * 0.95f,
                0.1f,
                HexMetrics.InnerRadius * 2f * 0.95f
            );

            if (tilesParent != null)
            {
                tile.transform.SetParent(tilesParent);
            }

            // HexTile component ekle
            HexTile hexTile = tile.AddComponent<HexTile>();
            hexTile.Initialize(coords, TerrainType.Grass);

            return tile;
        }

        /// <summary>
        /// Tum tile'lari temizler
        /// </summary>
        public void ClearAllTiles()
        {
            if (tilesParent != null)
            {
                for (int i = tilesParent.childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(tilesParent.GetChild(i).gameObject);
                }
            }
        }

        /// <summary>
        /// Test icin basit bir grid olusturur
        /// </summary>
        public void GenerateTestGrid(int width, int height)
        {
            ClearAllTiles();

            for (int r = 0; r < height; r++)
            {
                for (int q = 0; q < width; q++)
                {
                    HexCoordinates coords = new HexCoordinates(q, r);

                    // Rastgele terrain tipi ata (test icin)
                    TerrainType terrain = GetRandomTerrainForTest(q, r);

                    CreateTile(coords, terrain);
                }
            }

            Debug.Log($"HexTileFactory: {width}x{height} = {width * height} tile olusturuldu.");
        }

        private TerrainType GetRandomTerrainForTest(int q, int r)
        {
            // Basit noise benzeri pattern
            float noise = Mathf.PerlinNoise(q * 0.3f, r * 0.3f);

            if (noise < 0.2f) return TerrainType.Water;
            if (noise < 0.35f) return TerrainType.Hill;
            if (noise < 0.5f) return TerrainType.Grass;
            if (noise < 0.65f) return TerrainType.Forest;
            if (noise < 0.8f) return TerrainType.Mountain;
            return TerrainType.Snow;
        }
    }
}
