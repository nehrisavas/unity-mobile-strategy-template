using UnityEngine;
using EmpireWars.UI;
using EmpireWars.WorldMap;
using EmpireWars.WorldMap.Tiles;

namespace EmpireWars.Core
{
    /// <summary>
    /// WorldMap sahnesini otomatik baslatir
    /// Bu scripti Main Camera'ya veya bos bir objeye ekle
    /// Minimap, harita, bottom navigation ve HD ayarlarini otomatik kurar
    /// </summary>
    public class WorldMapBootstrap : MonoBehaviour
    {
        [Header("Harita Ayarlari")]
        [SerializeField] private int mapWidth = 60;
        [SerializeField] private int mapHeight = 60;

        [Header("Databases (Opsiyonel - Inspector'dan ata)")]
        [SerializeField] private HexTilePrefabDatabase tilePrefabDatabase;
        [SerializeField] private TerrainDecorationDatabase decorationDatabase;

        [Header("Cloud Prefabs (Opsiyonel)")]
        [SerializeField] private GameObject cloudBigPrefab;
        [SerializeField] private GameObject cloudSmallPrefab;
        [SerializeField] private int cloudCount = 15;

        [Header("Ozellikler")]
        [SerializeField] private bool createMap = true;
        [SerializeField] private bool createMinimap = true;
        [SerializeField] private bool createBottomNav = true;
        [SerializeField] private bool createClouds = true;
        [SerializeField] private bool setupHDGraphics = true;

        private void Awake()
        {
            // QHD cozunurluk (2560x1440) varsayilan olarak ayarla
            #if !UNITY_EDITOR
            Screen.SetResolution(2560, 1440, FullScreenMode.FullScreenWindow);
            #endif
        }

        private void Start()
        {
            Debug.Log("=== WorldMap Bootstrap Baslatiliyor ===");

            // HD grafik ayarlari
            if (setupHDGraphics)
            {
                SetupHDGraphics();
            }

            // Harita olustur
            if (createMap && tilePrefabDatabase != null)
            {
                CreateMap();
            }
            else if (createMap)
            {
                Debug.LogWarning("WorldMapBootstrap: tilePrefabDatabase atanmamis! Harita olusturulamadi.");
            }

            // Minimap olustur
            if (createMinimap)
            {
                CreateMinimap();
            }

            // Bottom Navigation olustur
            if (createBottomNav)
            {
                CreateBottomNavigation();
            }

            // Bulutlar olustur
            if (createClouds)
            {
                CreateClouds();
            }

            Debug.Log("=== WorldMap Bootstrap Tamamlandi ===");
        }

        private void SetupHDGraphics()
        {
            HDSceneSetup hdSetup = FindFirstObjectByType<HDSceneSetup>();
            if (hdSetup == null)
            {
                hdSetup = gameObject.AddComponent<HDSceneSetup>();
            }

            // Minimap'i HDSceneSetup'tan devre disi birak, biz kendimiz olusturacagiz
            var setupMinimapField = typeof(HDSceneSetup).GetField("setupMinimap",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (setupMinimapField != null)
            {
                setupMinimapField.SetValue(hdSetup, false);
            }

            hdSetup.SetupHDScene();
            Debug.Log("WorldMapBootstrap: HD grafik ayarlari yapildi");
        }

        private void CreateMap()
        {
            // HexGrid parent
            GameObject hexGridObj = new GameObject("HexGrid");

            // HexTileFactory
            GameObject factoryObj = new GameObject("HexTileFactory");
            HexTileFactory tileFactory = factoryObj.AddComponent<HexTileFactory>();

            if (tileFactory == null)
            {
                Debug.LogError("WorldMapBootstrap: HexTileFactory olusturulamadi!");
                return;
            }

            try
            {
                // Database'leri ata (reflection ile)
                var factoryType = typeof(HexTileFactory);
                var flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;

                var prefabDbField = factoryType.GetField("prefabDatabase", flags);
                if (prefabDbField != null)
                {
                    prefabDbField.SetValue(tileFactory, tilePrefabDatabase);
                }

                var decorDbField = factoryType.GetField("decorationDatabase", flags);
                if (decorDbField != null && decorationDatabase != null)
                {
                    decorDbField.SetValue(tileFactory, decorationDatabase);
                }

                var tilesParentField = factoryType.GetField("tilesParent", flags);
                if (tilesParentField != null)
                {
                    tilesParentField.SetValue(tileFactory, hexGridObj.transform);
                }

                var addDecorField = factoryType.GetField("addDecorations", flags);
                if (addDecorField != null)
                {
                    addDecorField.SetValue(tileFactory, decorationDatabase != null);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"WorldMapBootstrap: Reflection hatasi - {ex.Message}");
                return;
            }

            // Haritayi olustur
            tileFactory.GenerateTestGrid(mapWidth, mapHeight);
            Debug.Log($"WorldMapBootstrap: {mapWidth}x{mapHeight} harita olusturuldu");
        }

        private void CreateMinimap()
        {
            MinimapSystem minimapSystem = FindFirstObjectByType<MinimapSystem>();
            if (minimapSystem == null)
            {
                GameObject minimapObj = new GameObject("Minimap System");
                minimapSystem = minimapObj.AddComponent<MinimapSystem>();
            }
            minimapSystem.Initialize();
            Debug.Log("WorldMapBootstrap: Minimap olusturuldu (sag alt kose)");
        }

        private void CreateBottomNavigation()
        {
            MobileBottomNavigation bottomNav = FindFirstObjectByType<MobileBottomNavigation>();
            if (bottomNav == null)
            {
                GameObject navObj = new GameObject("Bottom Navigation");
                bottomNav = navObj.AddComponent<MobileBottomNavigation>();
            }
            Debug.Log("WorldMapBootstrap: Bottom Navigation olusturuldu");
        }

        private void CreateClouds()
        {
            CloudManager cloudManager = FindFirstObjectByType<CloudManager>();
            if (cloudManager == null)
            {
                GameObject cloudObj = new GameObject("Cloud Manager");
                cloudManager = cloudObj.AddComponent<CloudManager>();
            }

            // Cloud prefab'larini ata
            if (cloudBigPrefab != null || cloudSmallPrefab != null)
            {
                cloudManager.SetCloudPrefabs(cloudBigPrefab, cloudSmallPrefab);
            }
            else
            {
                // Prefab'lari otomatik bul
                TryAutoAssignCloudPrefabs(cloudManager);
            }

            // Cloud sayisini ayarla
            var cloudCountField = typeof(CloudManager).GetField("cloudCount",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (cloudCountField != null)
            {
                cloudCountField.SetValue(cloudManager, cloudCount);
            }

            // Harita merkezine gore alan ayarla
            var areaCenterField = typeof(CloudManager).GetField("areaCenter",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (areaCenterField != null)
            {
                areaCenterField.SetValue(cloudManager, new Vector3(mapWidth / 2f, 0, mapHeight / 2f));
            }

            Debug.Log($"WorldMapBootstrap: {cloudCount} bulut ile CloudManager olusturuldu");
        }

        private void TryAutoAssignCloudPrefabs(CloudManager cloudManager)
        {
            // KayKit cloud prefab'larini bul
            string cloudBigPath = "Assets/KayKit_Medieval_Hexagon/decoration/nature/cloud_big.fbx";
            string cloudSmallPath = "Assets/KayKit_Medieval_Hexagon/decoration/nature/cloud_small.fbx";

            #if UNITY_EDITOR
            var bigCloud = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(cloudBigPath);
            var smallCloud = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(cloudSmallPath);

            if (bigCloud != null || smallCloud != null)
            {
                cloudManager.SetCloudPrefabs(bigCloud, smallCloud);
                Debug.Log("WorldMapBootstrap: Cloud prefab'lari otomatik atandi");
            }
            else
            {
                Debug.LogWarning("WorldMapBootstrap: Cloud prefab'lari bulunamadi. Inspector'dan atayin.");
            }
            #else
            Debug.LogWarning("WorldMapBootstrap: Cloud prefab'larini Inspector'dan atayin.");
            #endif
        }
    }
}
