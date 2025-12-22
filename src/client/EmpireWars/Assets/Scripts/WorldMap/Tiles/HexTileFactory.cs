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
        /// Tum terrain tiplerinden en az 2 tane olur
        /// </summary>
        public void GenerateTestGrid(int width, int height)
        {
            ClearAllTiles();

            // Garantili tile listesi - her tipten en az 2 tane
            var guaranteedTiles = new System.Collections.Generic.List<(int q, int r, TerrainType type)>
            {
                // Grass (4 tane - en yaygin)
                (0, 0, TerrainType.Grass),
                (1, 0, TerrainType.Grass),
                (2, 0, TerrainType.Grass),
                (3, 0, TerrainType.Grass),

                // Water (2 tane)
                (0, 1, TerrainType.Water),
                (1, 1, TerrainType.Water),

                // Forest (2 tane)
                (2, 1, TerrainType.Forest),
                (3, 1, TerrainType.Forest),

                // Hill (2 tane)
                (4, 0, TerrainType.Hill),
                (4, 1, TerrainType.Hill),

                // Mountain (2 tane)
                (5, 0, TerrainType.Mountain),
                (5, 1, TerrainType.Mountain),

                // Desert (2 tane)
                (6, 0, TerrainType.Desert),
                (6, 1, TerrainType.Desert),

                // Snow (2 tane)
                (7, 0, TerrainType.Snow),
                (7, 1, TerrainType.Snow),

                // Swamp (2 tane)
                (8, 0, TerrainType.Swamp),
                (8, 1, TerrainType.Swamp),

                // Road (2 tane)
                (0, 2, TerrainType.Road),
                (1, 2, TerrainType.Road),

                // Coast (2 tane)
                (2, 2, TerrainType.Coast),
                (3, 2, TerrainType.Coast),
            };

            // Garantili tile'lari olustur
            var usedCoords = new System.Collections.Generic.HashSet<(int, int)>();
            foreach (var tile in guaranteedTiles)
            {
                if (tile.q < width && tile.r < height)
                {
                    HexCoordinates coords = new HexCoordinates(tile.q, tile.r);
                    CreateTile(coords, tile.type);
                    usedCoords.Add((tile.q, tile.r));
                }
            }

            // Geri kalan tile'lari rastgele doldur
            for (int r = 0; r < height; r++)
            {
                for (int q = 0; q < width; q++)
                {
                    if (usedCoords.Contains((q, r))) continue;

                    HexCoordinates coords = new HexCoordinates(q, r);
                    TerrainType terrain = GetRandomTerrainForTest(q, r);
                    CreateTile(coords, terrain);
                }
            }

            Debug.Log($"HexTileFactory: {width}x{height} = {width * height} tile olusturuldu. Tum terrain tipleri dahil.");
        }

        private TerrainType GetRandomTerrainForTest(int q, int r)
        {
            // Perlin noise ile dogal gorunum
            float noise = Mathf.PerlinNoise(q * 0.25f + 0.5f, r * 0.25f + 0.5f);
            float noise2 = Mathf.PerlinNoise(q * 0.4f + 10f, r * 0.4f + 10f);

            // Cogunlukla grass olsun
            if (noise < 0.15f) return TerrainType.Water;
            if (noise < 0.25f) return TerrainType.Coast;
            if (noise < 0.4f) return TerrainType.Forest;
            if (noise > 0.85f) return TerrainType.Mountain;
            if (noise > 0.75f) return TerrainType.Hill;
            if (noise2 > 0.8f) return TerrainType.Desert;
            if (noise2 < 0.15f) return TerrainType.Swamp;
            if (noise2 > 0.7f && noise > 0.6f) return TerrainType.Snow;

            return TerrainType.Grass;
        }
    }
}
