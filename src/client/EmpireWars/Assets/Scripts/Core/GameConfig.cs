using UnityEngine;

namespace EmpireWars.Core
{
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
        private const int DEFAULT_LOAD_RADIUS = 2;
        private const float DEFAULT_CHUNK_UPDATE_INTERVAL = 0.5f;
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
        }

        /// <summary>
        /// Oyun baslarken cagrilir
        /// WorldSettings varsa ondan okur, yoksa varsayilanlari kullanir
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

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

            Debug.Log($"GameConfig: World={WorldWidth:F0}x{WorldHeight:F0}, Zoom={MinZoom}-{MaxZoom}, Chunk={ChunkSize}");
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

        // ╔════════════════════════════════════════════════════════════╗
        // ║                        EVENTS                               ║
        // ╚════════════════════════════════════════════════════════════╝

        public static event System.Action<int, int> OnMapSizeChanged;

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
