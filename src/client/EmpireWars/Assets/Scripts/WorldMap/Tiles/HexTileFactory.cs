using UnityEngine;
using EmpireWars.Core;
using EmpireWars.Data;
using EmpireWars.WorldMap;

namespace EmpireWars.WorldMap.Tiles
{
    /// <summary>
    /// Hex tile'larini olusturan factory sinifi
    /// KayKit Medieval Hexagon Pack prefab'larini kullanir
    /// </summary>
    public class HexTileFactory : MonoBehaviour
    {
        [Header("Databases")]
        [SerializeField] private HexTilePrefabDatabase prefabDatabase;
        [SerializeField] private TerrainDecorationDatabase decorationDatabase;
        [SerializeField] private BuildingDatabase buildingDatabase;

        [Header("Settings")]
        [SerializeField] private Transform tilesParent;
        [SerializeField] private float tileScale = 1f;
        [SerializeField] private bool addDecorations = true;
        [SerializeField] private bool useKingdomMap = true;

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

            // Dekorasyon ekle (orman, dag, tepe vb.)
            if (addDecorations && decorationDatabase != null)
            {
                AddDecoration(tile, coords, terrainType);
            }

            return tile;
        }

        /// <summary>
        /// Terrain tipine gore uygun dekorasyon ekler
        /// Tile prefab'inda zaten gorsel varsa dekorasyon eklenmez
        /// </summary>
        private void AddDecoration(GameObject tile, HexCoordinates coords, TerrainType terrainType)
        {
            if (!decorationDatabase.RequiresDecoration(terrainType)) return;

            // Bu terrain tipleri zaten prefab'da gorsel iceriyor, dekorasyon ekleme
            if (TileHasBuiltInVisuals(terrainType)) return;

            // Koordinata gore tutarli seed (her tile ayni dekorasyonu alir)
            int seed = coords.Q * 1000 + coords.R;
            GameObject decorPrefab = decorationDatabase.GetRandomDecoration(terrainType, seed);

            if (decorPrefab == null) return;

            // Dekorasyonu tile'in child'i olarak ekle
            GameObject decor = Instantiate(decorPrefab, tile.transform);
            decor.name = $"Decor_{terrainType}";

            // Pozisyon ve olcek ayarla
            float yOffset = decorationDatabase.GetDecorationYOffset(terrainType);
            decor.transform.localPosition = new Vector3(0, yOffset, 0);

            float scale = decorationDatabase.GetDecorationScale(terrainType);
            decor.transform.localScale = Vector3.one * scale;

            // Seed'e gore rotasyon varyasyonu (dogal gorunum)
            float rotationY = (seed % 6) * 60f; // 0, 60, 120, 180, 240, 300 derece
            decor.transform.localRotation = Quaternion.Euler(0, rotationY, 0);
        }

        /// <summary>
        /// Bu terrain tipi prefab'inda zaten gorsel icerir mi?
        /// Coast, River, Road gibi tile'lar kendi gorselleriyle gelir
        /// </summary>
        private bool TileHasBuiltInVisuals(TerrainType terrainType)
        {
            return terrainType switch
            {
                TerrainType.Coast => true,      // Coast tile'lari zaten su iceriyor
                TerrainType.River => true,      // River tile'lari su iceriyor
                TerrainType.Bridge => true,     // Bridge tile'lari kopru iceriyor
                TerrainType.Road => true,       // Road tile'lari yol iceriyor
                TerrainType.Water => true,      // Water tile'lari zaten su
                TerrainType.GrassSlopedHigh => true, // Sloped tile'lar kendi gorunumunde
                TerrainType.GrassSlopedLow => true,
                _ => false
            };
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
        /// Sabit harita olusturur - her seferinde ayni harita
        /// Tum terrain tiplerinden en az 2 tane bulunur
        /// </summary>
        public void GenerateTestGrid(int width, int height)
        {
            ClearAllTiles();

            if (useKingdomMap)
            {
                GenerateKingdomMap();
                return;
            }

            // Eski sabit harita verisini kullan
            var mapTiles = FixedMapData.GetMapTiles();
            var (mapWidth, mapHeight) = FixedMapData.GetMapSize();

            int actualWidth = Mathf.Min(width, mapWidth);
            int actualHeight = Mathf.Min(height, mapHeight);

            int tileCount = 0;
            foreach (var tile in mapTiles)
            {
                if (tile.q < actualWidth && tile.r < actualHeight)
                {
                    HexCoordinates coords = new HexCoordinates(tile.q, tile.r);
                    CreateTile(coords, tile.type);
                    tileCount++;
                }
            }

            var counts = FixedMapData.GetTerrainCounts();
            string countLog = "Terrain Sayilari:\n";
            foreach (var kvp in counts)
            {
                countLog += $"  {kvp.Key}: {kvp.Value}\n";
            }
            Debug.Log($"HexTileFactory: {tileCount} tile olusturuldu (Sabit Harita).\n{countLog}");
        }

        /// <summary>
        /// Kingdom haritasi olusturur - merkez kale, bataklik, kuleler, madenler
        /// </summary>
        public void GenerateKingdomMap()
        {
            ClearAllTiles();

            var mapTiles = KingdomMapGenerator.GenerateMap();
            var (mapWidth, mapHeight) = KingdomMapGenerator.GetMapSize();

            int tileCount = 0;
            int buildingCount = 0;
            int mineCount = 0;

            var terrainCounts = new System.Collections.Generic.Dictionary<TerrainType, int>();

            foreach (var tileData in mapTiles)
            {
                HexCoordinates coords = new HexCoordinates(tileData.Q, tileData.R);
                var tileObj = CreateTile(coords, tileData.Terrain);

                // Terrain sayisini kaydet
                if (!terrainCounts.ContainsKey(tileData.Terrain))
                    terrainCounts[tileData.Terrain] = 0;
                terrainCounts[tileData.Terrain]++;

                // Maden seviyesini kaydet
                if (tileData.MineLevel > 0)
                {
                    var hexTile = tileObj.GetComponent<HexTile>();
                    if (hexTile != null)
                    {
                        // Mine level'i HexTile'a kaydet (ileride UI'da gosterilecek)
                        tileObj.name = $"Hex_{tileData.Q}_{tileData.R}_{tileData.Terrain}_Lv{tileData.MineLevel}";
                    }
                    mineCount++;
                }

                // Bina yerleştir
                if (tileData.HasBuilding && buildingDatabase != null)
                {
                    PlaceBuilding(tileObj, tileData.BuildingType);
                    buildingCount++;
                }

                tileCount++;
            }

            // Log
            string countLog = "=== KINGDOM MAP GENERATED ===\n";
            countLog += $"Toplam Tile: {tileCount}\n";
            countLog += $"Binalar: {buildingCount}\n";
            countLog += $"Madenler: {mineCount}\n";
            countLog += "Terrain Dagilimi:\n";
            foreach (var kvp in terrainCounts)
            {
                countLog += $"  {kvp.Key}: {kvp.Value}\n";
            }
            Debug.Log(countLog);
        }

        /// <summary>
        /// Tile üzerine bina yerleştirir
        /// </summary>
        private void PlaceBuilding(GameObject tile, string buildingType)
        {
            if (buildingDatabase == null) return;

            GameObject buildingPrefab = buildingDatabase.GetBuildingPrefab(buildingType);
            if (buildingPrefab == null)
            {
                Debug.LogWarning($"HexTileFactory: Bina bulunamadi: {buildingType}");
                return;
            }

            GameObject building = Instantiate(buildingPrefab, tile.transform);
            building.name = $"Building_{buildingType}";
            building.transform.localPosition = new Vector3(0, 0.1f, 0);
            building.transform.localScale = Vector3.one * 0.8f;
        }

        /// <summary>
        /// Rastgele harita olusturur (test amacli)
        /// </summary>
        public void GenerateRandomGrid(int width, int height)
        {
            ClearAllTiles();

            for (int r = 0; r < height; r++)
            {
                for (int q = 0; q < width; q++)
                {
                    HexCoordinates coords = new HexCoordinates(q, r);
                    TerrainType terrain = GetRandomTerrainForTest(q, r);
                    CreateTile(coords, terrain);
                }
            }

            Debug.Log($"HexTileFactory: {width}x{height} = {width * height} rastgele tile olusturuldu.");
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
