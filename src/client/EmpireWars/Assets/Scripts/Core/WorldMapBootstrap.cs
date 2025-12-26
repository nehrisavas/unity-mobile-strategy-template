using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EmpireWars.UI;
using EmpireWars.WorldMap;
using EmpireWars.WorldMap.Tiles;
using EmpireWars.CameraSystem;

namespace EmpireWars.Core
{
    // FORCE RECOMPILE: v2 - SimpleTileRenderer aktif
    /// <summary>
    /// WorldMap sahnesini otomatik baslatir
    /// Bu scripti Main Camera'ya veya bos bir objeye ekle
    /// Minimap, harita, bottom navigation ve HD ayarlarini otomatik kurar
    /// </summary>
    public class WorldMapBootstrap : MonoBehaviour
    {
        [Header("Databases (Inspector'dan ata)")]
        [SerializeField] private HexTilePrefabDatabase tilePrefabDatabase;
        [SerializeField] private TerrainDecorationDatabase decorationDatabase;
        [SerializeField] private BuildingDatabase buildingDatabase;

        [Header("Cloud Prefabs (Opsiyonel)")]
        [SerializeField] private GameObject cloudBigPrefab;
        [SerializeField] private GameObject cloudSmallPrefab;

        [Header("Ozellikler")]
        [SerializeField] private bool createMap = true;
        [SerializeField] private bool createMinimap = true;
        [SerializeField] private bool createBottomNav = true;
        [SerializeField] private bool createClouds = true;
        [SerializeField] private bool setupHDGraphics = true;

        private void Awake()
        {
            // ScreenManager'ı oluştur (safe area, DPI yönetimi)
            if (ScreenManager.Instance == null)
            {
                GameObject screenManagerObj = new GameObject("ScreenManager");
                screenManagerObj.AddComponent<ScreenManager>();
            }

            // MobilePerformanceManager'ı oluştur (GPU Instancing, Static Batching)
            if (MobilePerformanceManager.Instance == null)
            {
                GameObject perfManagerObj = new GameObject("MobilePerformanceManager");
                perfManagerObj.AddComponent<MobilePerformanceManager>();
            }
        }

        private void Start()
        {
            Debug.Log("=== WorldMap Bootstrap Baslatiliyor ===");

            // GameConfig'i baslat - TUM AYARLAR ORADAN OKUNUR
            GameConfig.Initialize();
            Debug.Log($"Harita: {GameConfig.MapWidth}x{GameConfig.MapHeight} ({GameConfig.WorldWidth:F0}x{GameConfig.WorldHeight:F0} units)");

            // Database'leri otomatik bul (eger atanmamissa)
            AutoFindDatabases();

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

            // Badge görünürlük ayarı değişikliklerini dinle
            GameConfig.OnBadgeVisibilityChanged += OnBadgeVisibilityChanged;

            Debug.Log("=== WorldMap Bootstrap Tamamlandi ===");
        }

        private void OnDestroy()
        {
            GameConfig.OnBadgeVisibilityChanged -= OnBadgeVisibilityChanged;
        }

        private void OnBadgeVisibilityChanged(bool show)
        {
            // Tüm HexTile'ların badge görünürlüğünü güncelle
            HexTile[] allTiles = FindObjectsByType<HexTile>(FindObjectsSortMode.None);
            foreach (var tile in allTiles)
            {
                tile.UpdateBadgeVisibility(show);
            }
            Debug.Log($"WorldMapBootstrap: {allTiles.Length} tile'ın badge görünürlüğü güncellendi - {(show ? "Açık" : "Kapalı")}");
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
            Debug.Log($"WorldMapBootstrap.CreateMap: UseSimpleRenderer={GameConfig.UseSimpleRenderer}");

            // ULTRA PERFORMANS: Basit renderer kullan (tek mesh, tek draw call)
            if (GameConfig.UseSimpleRenderer)
            {
                Debug.Log("WorldMapBootstrap: SimpleTileRenderer KULLANILIYOR!");
                CreateSimpleMap();
                return;
            }

            Debug.Log("WorldMapBootstrap: SimpleTileRenderer KULLANILMIYOR, ChunkedTileLoader'a geçiliyor");

            // GameConfig'e gore chunk-based loading kullanilmali mi?
            if (GameConfig.ShouldUseChunkedLoading())
            {
                CreateChunkedMap();
                return;
            }

            // Kucuk haritalar icin eski sistem
            CreateFullMap();
        }

        private void CreateSimpleMap()
        {
            Debug.Log("WorldMapBootstrap: SimpleTileRenderer kullaniliyor (TEK MESH, TEK DRAW CALL)");

            GameObject rendererObj = new GameObject("SimpleTileRenderer");
            SimpleTileRenderer renderer = rendererObj.AddComponent<SimpleTileRenderer>();

            // View radius - chunk size'a benzer
            var rendererType = typeof(SimpleTileRenderer);
            var flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
            var viewRadiusField = rendererType.GetField("viewRadius", flags);
            if (viewRadiusField != null)
            {
                viewRadiusField.SetValue(renderer, GameConfig.ChunkSize / 2);
            }

            Debug.Log($"SimpleTileRenderer: ViewRadius = {GameConfig.ChunkSize / 2}");
        }

        private void CreateChunkedMap()
        {
            GameObject loaderObj = new GameObject("ChunkedTileLoader");
            ChunkedTileLoader loader = loaderObj.AddComponent<ChunkedTileLoader>();

            // Database'leri ata (reflection ile)
            var loaderType = typeof(ChunkedTileLoader);
            var flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;

            var prefabDbField = loaderType.GetField("prefabDatabase", flags);
            if (prefabDbField != null)
            {
                prefabDbField.SetValue(loader, tilePrefabDatabase);
            }

            var decorDbField = loaderType.GetField("decorationDatabase", flags);
            if (decorDbField != null && decorationDatabase != null)
            {
                decorDbField.SetValue(loader, decorationDatabase);
            }

            var buildingDbField = loaderType.GetField("buildingDatabase", flags);
            if (buildingDbField != null)
            {
                if (buildingDatabase != null)
                {
                    buildingDbField.SetValue(loader, buildingDatabase);
                    Debug.Log("WorldMapBootstrap: BuildingDatabase atandi");
                }
                else
                {
                    Debug.LogError("WorldMapBootstrap: BuildingDatabase NULL! Inspector'da atayin. Binalar gorunmeyecek!");
                }
            }

            // Chunk sistemi baslatma - bir frame bekle
            StartCoroutine(InitializeChunkedLoader(loader));
        }

        private IEnumerator InitializeChunkedLoader(ChunkedTileLoader loader)
        {
            yield return null; // Bir frame bekle

            // GameConfig'den okuyarak baslat
            loader.Initialize();
            Debug.Log($"WorldMapBootstrap: {GameConfig.MapWidth}x{GameConfig.MapHeight} chunk-based harita baslatildi");
        }

        private void CreateFullMap()
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

            // Harita boyutunu ayarla - GameConfig'den oku
            KingdomMapGenerator.SetMapSize(Mathf.Max(GameConfig.MapWidth, GameConfig.MapHeight));

            // Haritayi olustur
            tileFactory.GenerateTestGrid(GameConfig.MapWidth, GameConfig.MapHeight);
            Debug.Log($"WorldMapBootstrap: {GameConfig.MapWidth}x{GameConfig.MapHeight} harita olusturuldu");
        }

        private void CreateMinimap()
        {
            // Canvas bul veya olustur
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("UI Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                // CanvasScaler - mobil uyumlu ayarlar
                var scaler = canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = ScreenManager.Instance != null
                    ? ScreenManager.Instance.ReferenceResolution
                    : new Vector2(1920, 1080);
                scaler.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                scaler.matchWidthOrHeight = 0.5f; // Ortada dengeli

                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();

                // SafeAreaPanel ekle - tüm UI safe area içinde
                GameObject safeAreaObj = new GameObject("SafeArea");
                safeAreaObj.transform.SetParent(canvasObj.transform);
                RectTransform safeRect = safeAreaObj.AddComponent<RectTransform>();
                safeRect.anchorMin = Vector2.zero;
                safeRect.anchorMax = Vector2.one;
                safeRect.offsetMin = Vector2.zero;
                safeRect.offsetMax = Vector2.zero;
                safeAreaObj.AddComponent<UI.SafeAreaPanel>();
            }

            MiniMapController miniMap = FindFirstObjectByType<MiniMapController>();
            if (miniMap == null)
            {
                GameObject minimapObj = new GameObject("MiniMap Controller");
                minimapObj.transform.SetParent(canvas.transform);
                miniMap = minimapObj.AddComponent<MiniMapController>();
            }

            // GameConfig'den okuyor, ayri ayar gereksiz
            miniMap.Initialize();
            Debug.Log($"WorldMapBootstrap: Dairesel minimap olusturuldu ({GameConfig.WorldWidth:F0}x{GameConfig.WorldHeight:F0})");
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

            // CloudManager GameConfig'den okuyor, ekstra ayar gereksiz
            Debug.Log($"WorldMapBootstrap: CloudManager olusturuldu (GameConfig'den {GameConfig.CloudCount} bulut)");
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

        private void AutoFindDatabases()
        {
            // Runtime: Resources.Load kullan
            if (tilePrefabDatabase == null)
            {
                tilePrefabDatabase = Resources.Load<HexTilePrefabDatabase>("HexTilePrefabDatabase");
                if (tilePrefabDatabase != null)
                    Debug.Log("WorldMapBootstrap: TilePrefabDatabase Resources'dan yuklendi");
            }

            if (decorationDatabase == null)
            {
                decorationDatabase = Resources.Load<TerrainDecorationDatabase>("TerrainDecorationDatabase");
                if (decorationDatabase != null)
                    Debug.Log("WorldMapBootstrap: DecorationDatabase Resources'dan yuklendi");
            }

            if (buildingDatabase == null)
            {
                buildingDatabase = Resources.Load<BuildingDatabase>("BuildingDatabase");
                if (buildingDatabase != null)
                    Debug.Log("WorldMapBootstrap: BuildingDatabase Resources'dan yuklendi");
            }

            #if UNITY_EDITOR
            // Editor: AssetDatabase kullan (fallback)
            if (tilePrefabDatabase == null)
            {
                string[] guids = UnityEditor.AssetDatabase.FindAssets("t:HexTilePrefabDatabase");
                if (guids.Length > 0)
                {
                    string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                    tilePrefabDatabase = UnityEditor.AssetDatabase.LoadAssetAtPath<HexTilePrefabDatabase>(path);
                    if (tilePrefabDatabase != null)
                        Debug.Log($"WorldMapBootstrap: TilePrefabDatabase otomatik bulundu: {path}");
                }
            }

            if (decorationDatabase == null)
            {
                string[] guids = UnityEditor.AssetDatabase.FindAssets("t:TerrainDecorationDatabase");
                if (guids.Length > 0)
                {
                    string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                    decorationDatabase = UnityEditor.AssetDatabase.LoadAssetAtPath<TerrainDecorationDatabase>(path);
                    if (decorationDatabase != null)
                        Debug.Log($"WorldMapBootstrap: DecorationDatabase otomatik bulundu: {path}");
                }
            }

            // BuildingDatabase
            if (buildingDatabase == null)
            {
                string[] guids = UnityEditor.AssetDatabase.FindAssets("t:BuildingDatabase");
                if (guids.Length > 0)
                {
                    string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                    buildingDatabase = UnityEditor.AssetDatabase.LoadAssetAtPath<BuildingDatabase>(path);
                    if (buildingDatabase != null)
                        Debug.Log($"WorldMapBootstrap: BuildingDatabase otomatik bulundu: {path}");
                }
                else
                {
                    Debug.LogWarning("WorldMapBootstrap: BuildingDatabase bulunamadi! Tools > EmpireWars > Setup Building Database calistirin.");
                }
            }
            #endif
        }
    }
}
