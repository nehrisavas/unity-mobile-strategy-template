using UnityEngine;
using EmpireWars.UI;
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

        [Header("Ozellikler")]
        [SerializeField] private bool createMap = true;
        [SerializeField] private bool createMinimap = true;
        [SerializeField] private bool createBottomNav = true;
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
    }
}
