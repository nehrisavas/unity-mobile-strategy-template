using UnityEngine;

namespace EmpireWars.Core
{
    // FORCE RECOMPILE: v3 - SimpleTileRenderer aktif, ChunkSize=8
    /// <summary>
    /// GLOBAL OYUN AYARLARI - TEK KAYNAK
    /// Tum degerler buradan okunur, baska yerde hardcoded deger OLMAMALI
    ///
    /// Degistirmek icin:
    /// 1. Bu dosyadaki VARSAYILAN DEGERLER bolumunu duzenle
    /// 2. VEYA WorldSettings ScriptableObject olustur (Assets/Resources/WorldSettings)
    /// </summary>
    public static class GameConfig
    {
        // ╔════════════════════════════════════════════════════════════╗
        // ║                    VARSAYILAN DEGERLER                      ║
        // ║         BURADAKI DEGERLERI DEGISTIR = HER YER GUNCELLENIR  ║
        // ╚════════════════════════════════════════════════════════════╝

        #region === HARITA AYARLARI ===

        private const int DEFAULT_MAP_WIDTH = 2000;
        private const int DEFAULT_MAP_HEIGHT = 2000;

        #endregion

        #region === KAMERA AYARLARI ===

        private const float DEFAULT_MIN_ZOOM = 10f;
        private const float DEFAULT_MAX_ZOOM = 70f;  // Sinirlandi - lag onlemek icin (2000x2000 harita)
        private const float DEFAULT_ZOOM = 35f;

        #endregion

        #region === CHUNK AYARLARI ===

        private const int DEFAULT_CHUNK_SIZE = 32;
        private const int MOBILE_CHUNK_SIZE = 16; // Mobil için chunk (16x16=256 tile)
        private const int DEFAULT_LOAD_RADIUS = 2;
        private const int MOBILE_LOAD_RADIUS = 1; // Mobil için (3x3 chunk = ~2304 tile)
        private const float DEFAULT_CHUNK_UPDATE_INTERVAL = 0.5f;
        private const float MOBILE_CHUNK_UPDATE_INTERVAL = 0.3f; // Mobil için daha sık
        private const int DEFAULT_CHUNK_THRESHOLD = 100;

        #endregion

        #region === MINIMAP AYARLARI ===

        private const float DEFAULT_MINIMAP_SIZE = 220f;  // Buyutuldu: 200 -> 220
        private const float DEFAULT_MINIMAP_MIN_ZOOM = 100f;  // Daha genis gorunum: 50 -> 100
        private const float DEFAULT_MINIMAP_MAX_ZOOM = 500f;  // Buyutuldu: 400 -> 500
        private const float DEFAULT_MINIMAP_UPDATE_INTERVAL = 0.1f;

        #endregion

        #region === BULUT AYARLARI ===

        private const int DEFAULT_CLOUD_COUNT = 50;
        private const float DEFAULT_CLOUD_UPDATE_INTERVAL = 0.1f;

        #endregion

        #region === UI AYARLARI ===

        private const bool DEFAULT_SHOW_BADGES = true;

        #endregion

        #region === MOBİL PERFORMANS AYARLARI ===

        private const bool DEFAULT_SHOW_DECORATIONS = true;
        private const bool DEFAULT_SHOW_BUILDINGS = true;
        private const bool DEFAULT_USE_SHADOWS = true;

        // DEBUG: Editor'da mobil modu test etmek için true yapın
        public const bool FORCE_MOBILE_MODE = true; // TEST İÇİN TRUE

        // Basit renderer kullan - TEK MESH, TEK DRAW CALL
        public static bool UseSimpleRenderer { get; private set; } = false;

        #endregion

        // ╔════════════════════════════════════════════════════════════╗
        // ║                    RUNTIME PROPERTIES                       ║
        // ║              (Yukaridaki varsayilanlardan okunur)          ║
        // ╚════════════════════════════════════════════════════════════╝

        // Harita
        public static int MapWidth { get; private set; } = DEFAULT_MAP_WIDTH;
        public static int MapHeight { get; private set; } = DEFAULT_MAP_HEIGHT;
        public static float MapOffsetX { get; private set; } = 0f;
        public static float MapOffsetZ { get; private set; } = 0f;

        // Hesaplanan world degerleri
        public static float WorldWidth => MapWidth * HexMetrics.InnerRadius * 2f;
        public static float WorldHeight => MapHeight * HexMetrics.OuterRadius * 1.5f;
        public static Vector3 WorldCenter => new Vector3(MapOffsetX + WorldWidth / 2f, 0f, MapOffsetZ + WorldHeight / 2f);

        // Kamera
        public static float MinZoom { get; private set; } = DEFAULT_MIN_ZOOM;
        public static float MaxZoom { get; private set; } = DEFAULT_MAX_ZOOM;
        public static float DefaultZoom { get; private set; } = DEFAULT_ZOOM;

        // Chunk
        public static int ChunkSize { get; private set; } = DEFAULT_CHUNK_SIZE;
        public static int LoadRadius { get; private set; } = DEFAULT_LOAD_RADIUS;
        public static float ChunkUpdateInterval { get; private set; } = DEFAULT_CHUNK_UPDATE_INTERVAL;
        public static int ChunkLoadingThreshold { get; private set; } = DEFAULT_CHUNK_THRESHOLD;

        // Minimap
        public static float MinimapSize { get; private set; } = DEFAULT_MINIMAP_SIZE;
        public static float MinimapMinZoom { get; private set; } = DEFAULT_MINIMAP_MIN_ZOOM;
        public static float MinimapMaxZoom { get; private set; } = DEFAULT_MINIMAP_MAX_ZOOM;
        public static float MinimapUpdateInterval { get; private set; } = DEFAULT_MINIMAP_UPDATE_INTERVAL;

        // Bulut
        public static int CloudCount { get; private set; } = DEFAULT_CLOUD_COUNT;
        public static float CloudUpdateInterval { get; private set; } = DEFAULT_CLOUD_UPDATE_INTERVAL;

        // UI
        public static bool ShowBadges { get; private set; } = DEFAULT_SHOW_BADGES;

        // Mobil Performans
        public static bool ShowDecorations { get; private set; } = DEFAULT_SHOW_DECORATIONS;
        public static bool ShowBuildings { get; private set; } = DEFAULT_SHOW_BUILDINGS;
        public static bool UseShadows { get; private set; } = DEFAULT_USE_SHADOWS;

        // ╔════════════════════════════════════════════════════════════╗
        // ║                      INITIALIZATION                         ║
        // ╚════════════════════════════════════════════════════════════╝

        private static bool _initialized = false;

        /// <summary>
        /// Editor'da domain reload sirasinda reset
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _initialized = false;
            // Varsayilanlara don
            MapWidth = DEFAULT_MAP_WIDTH;
            MapHeight = DEFAULT_MAP_HEIGHT;
            MinZoom = DEFAULT_MIN_ZOOM;
            MaxZoom = DEFAULT_MAX_ZOOM;
            DefaultZoom = DEFAULT_ZOOM;
            ChunkSize = DEFAULT_CHUNK_SIZE;
            LoadRadius = DEFAULT_LOAD_RADIUS;
            ChunkUpdateInterval = DEFAULT_CHUNK_UPDATE_INTERVAL;
            MinimapSize = DEFAULT_MINIMAP_SIZE;
            MinimapMinZoom = DEFAULT_MINIMAP_MIN_ZOOM;
            MinimapMaxZoom = DEFAULT_MINIMAP_MAX_ZOOM;
            CloudCount = DEFAULT_CLOUD_COUNT;
            CloudUpdateInterval = DEFAULT_CLOUD_UPDATE_INTERVAL;
            ShowBadges = DEFAULT_SHOW_BADGES;
            ShowDecorations = DEFAULT_SHOW_DECORATIONS;
            ShowBuildings = DEFAULT_SHOW_BUILDINGS;
            UseShadows = DEFAULT_USE_SHADOWS;
        }

        /// <summary>
        /// Oyun baslarken cagrilir
        /// WorldSettings varsa ondan okur, yoksa varsayilanlari kullanir
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            Debug.Log("GameConfig: Initialize() başladı");

            // WorldSettings varsa ondan oku
            var settings = Resources.Load<WorldSettings>("WorldSettings");
            if (settings != null)
            {
                LoadFromWorldSettings(settings);
                Debug.Log($"GameConfig: WorldSettings'den yuklendi - {MapWidth}x{MapHeight}");
            }
            else
            {
                Debug.Log($"GameConfig: Varsayilanlar kullaniliyor - {MapWidth}x{MapHeight}");
            }

            // Mobil optimizasyonlar
            ApplyMobileOptimizations();

            Debug.Log($"GameConfig: UseSimpleRenderer={UseSimpleRenderer}, ChunkSize={ChunkSize}, LoadRadius={LoadRadius}");
            Debug.Log($"GameConfig: World={WorldWidth:F0}x{WorldHeight:F0}, Zoom={MinZoom}-{MaxZoom}, Chunk={ChunkSize}, LoadRadius={LoadRadius}");
        }

        /// <summary>
        /// Mobil cihazlar icin performans optimizasyonlari
        /// </summary>
        private static void ApplyMobileOptimizations()
        {
            bool isMobile = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;

            // Debug modu - Editor'da mobil optimizasyonları test et
            if (FORCE_MOBILE_MODE)
            {
                isMobile = true;
                Debug.Log("GameConfig: FORCE_MOBILE_MODE aktif - Editor'da mobil optimizasyonlar uygulanıyor");
            }

            if (isMobile)
            {
                // Chunk boyutunu ve yarıçapını azalt
                ChunkSize = MOBILE_CHUNK_SIZE; // 8 (64 tile per chunk)
                LoadRadius = MOBILE_LOAD_RADIUS; // 0 = sadece 1 chunk
                ChunkUpdateInterval = MOBILE_CHUNK_UPDATE_INTERVAL;

                // Bulut sayısını azalt veya kapat
                CloudCount = 0; // Bulutları tamamen kapat

                // Badge'leri kapat
                ShowBadges = false;

                // Dekorasyonları ve binaları göster
                ShowDecorations = true;
                ShowBuildings = true;

                // SimpleTileRenderer kapalı - 3D prefab'lar için ChunkedTileLoader kullan
                UseSimpleRenderer = false;

                // Gölgeleri kapat
                UseShadows = false;
                QualitySettings.shadows = ShadowQuality.Disable;
                QualitySettings.shadowResolution = ShadowResolution.Low;

                // Diğer kalite ayarları
                QualitySettings.antiAliasing = 0;
                QualitySettings.softParticles = false;
                QualitySettings.realtimeReflectionProbes = false;
                QualitySettings.billboardsFaceCameraPosition = false;
                QualitySettings.lodBias = 0.5f; // Daha agresif LOD
                QualitySettings.maximumLODLevel = 1;

                int totalTiles = (LoadRadius * 2 + 1) * (LoadRadius * 2 + 1) * ChunkSize * ChunkSize;
                Debug.Log($"GameConfig: MOBİL OPTİMİZASYON - ChunkSize={ChunkSize}, LoadRadius={LoadRadius}, TotalTiles=~{totalTiles}");
            }
        }

        private static void LoadFromWorldSettings(WorldSettings settings)
        {
            MapWidth = settings.mapWidth;
            MapHeight = settings.mapHeight;
            MinZoom = settings.minZoom;
            MaxZoom = settings.maxZoom;
            DefaultZoom = settings.defaultZoom;
            ChunkSize = settings.chunkSize;
            LoadRadius = settings.loadRadius;
            ChunkUpdateInterval = settings.chunkUpdateInterval;
            MinimapSize = settings.minimapSize;
            MinimapMinZoom = settings.minimapMinZoom;
            MinimapMaxZoom = settings.minimapMaxZoom;
            CloudCount = settings.cloudCount;
            CloudUpdateInterval = settings.cloudUpdateInterval;
            ShowBadges = settings.showBadges;
        }

        /// <summary>
        /// Runtime'da harita boyutunu degistir (WorldSettings olmadan)
        /// </summary>
        public static void SetMapSize(int width, int height)
        {
            MapWidth = Mathf.Max(20, width);
            MapHeight = Mathf.Max(20, height);
            OnMapSizeChanged?.Invoke(MapWidth, MapHeight);
            Debug.Log($"GameConfig: Harita boyutu degistirildi - {MapWidth}x{MapHeight}");
        }

        /// <summary>
        /// Runtime'da badge görünürlüğünü değiştir
        /// </summary>
        public static void SetShowBadges(bool show)
        {
            if (ShowBadges == show) return;
            ShowBadges = show;
            OnBadgeVisibilityChanged?.Invoke(show);
            Debug.Log($"GameConfig: Badge görünürlüğü değiştirildi - {(show ? "Açık" : "Kapalı")}");
        }

        // ╔════════════════════════════════════════════════════════════╗
        // ║                        EVENTS                               ║
        // ╚════════════════════════════════════════════════════════════╝

        public static event System.Action<int, int> OnMapSizeChanged;
        public static event System.Action<bool> OnBadgeVisibilityChanged;

        // ╔════════════════════════════════════════════════════════════╗
        // ║                    UTILITY METHODS                          ║
        // ╚════════════════════════════════════════════════════════════╝

        public static bool IsPositionInMap(Vector3 worldPos)
        {
            return worldPos.x >= MapOffsetX && worldPos.x <= MapOffsetX + WorldWidth &&
                   worldPos.z >= MapOffsetZ && worldPos.z <= MapOffsetZ + WorldHeight;
        }

        public static bool IsCoordInMap(int q, int r)
        {
            return q >= 0 && q < MapWidth && r >= 0 && r < MapHeight;
        }

        public static bool ShouldUseChunkedLoading()
        {
            return MapWidth > ChunkLoadingThreshold || MapHeight > ChunkLoadingThreshold;
        }
    }
}
