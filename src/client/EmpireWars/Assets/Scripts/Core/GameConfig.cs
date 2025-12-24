using UnityEngine;

namespace EmpireWars.Core
{
    /// <summary>
    /// Oyun genelinde kullanilan merkezi konfigürasyon
    /// Tum sistemler bu degerlerden okur
    /// </summary>
    public static class GameConfig
    {
        // ============================================
        // HARITA AYARLARI
        // ============================================

        /// <summary>
        /// Harita genisligi (hex tile sayisi)
        /// MAP-SYSTEM.md: 2000x2000 hedef
        /// </summary>
        public static int MapWidth { get; private set; } = 200;

        /// <summary>
        /// Harita yuksekligi (hex tile sayisi)
        /// </summary>
        public static int MapHeight { get; private set; } = 200;

        /// <summary>
        /// Harita baslangic X koordinati (world space)
        /// </summary>
        public static float MapOffsetX { get; private set; } = 0f;

        /// <summary>
        /// Harita baslangic Z koordinati (world space)
        /// </summary>
        public static float MapOffsetZ { get; private set; } = 0f;

        // ============================================
        // HESAPLANAN DEGERLER (World Space)
        // ============================================

        /// <summary>
        /// Harita genisligi (world units)
        /// </summary>
        public static float WorldWidth => MapWidth * HexMetrics.InnerRadius * 2f;

        /// <summary>
        /// Harita yuksekligi (world units)
        /// </summary>
        public static float WorldHeight => MapHeight * HexMetrics.OuterRadius * 1.5f;

        /// <summary>
        /// Harita merkezi (world space)
        /// </summary>
        public static Vector3 WorldCenter => new Vector3(
            MapOffsetX + WorldWidth / 2f,
            0f,
            MapOffsetZ + WorldHeight / 2f
        );

        // ============================================
        // CHUNK AYARLARI
        // ============================================

        /// <summary>
        /// Her chunk'taki tile sayisi (NxN)
        /// </summary>
        public static int ChunkSize { get; private set; } = 16;

        /// <summary>
        /// Yukleme yaricapi (chunk sayisi)
        /// Merkez chunk + bu kadar yaricap yuklenir
        /// </summary>
        public static int LoadRadius { get; private set; } = 4;

        /// <summary>
        /// Chunk yukleme icin minimum harita boyutu
        /// Bu boyutun altindaki haritalar tek seferde yuklenir
        /// </summary>
        public static int ChunkLoadingThreshold { get; private set; } = 100;

        // ============================================
        // KAMERA AYARLARI
        // ============================================

        /// <summary>
        /// Minimum zoom (yakin)
        /// </summary>
        public static float MinZoom { get; private set; } = 5f;

        /// <summary>
        /// Maksimum zoom (uzak)
        /// </summary>
        public static float MaxZoom { get; private set; } = 80f;

        /// <summary>
        /// Baslangic zoom degeri
        /// </summary>
        public static float DefaultZoom { get; private set; } = 25f;

        // ============================================
        // MINIMAP AYARLARI
        // ============================================

        /// <summary>
        /// Minimap UI boyutu (piksel)
        /// </summary>
        public static float MinimapSize { get; private set; } = 180f;

        /// <summary>
        /// Minimap minimum zoom
        /// </summary>
        public static float MinimapMinZoom { get; private set; } = 30f;

        /// <summary>
        /// Minimap maksimum zoom
        /// </summary>
        public static float MinimapMaxZoom { get; private set; } = 300f;

        // ============================================
        // PERFORMANS AYARLARI
        // ============================================

        /// <summary>
        /// Chunk guncelleme araligi (saniye)
        /// </summary>
        public static float ChunkUpdateInterval { get; private set; } = 0.3f;

        /// <summary>
        /// Minimap guncelleme araligi (saniye)
        /// </summary>
        public static float MinimapUpdateInterval { get; private set; } = 0.1f;

        /// <summary>
        /// Bulut guncelleme araligi (saniye)
        /// </summary>
        public static float CloudUpdateInterval { get; private set; } = 0.1f;

        /// <summary>
        /// Bulut sayisi
        /// </summary>
        public static int CloudCount { get; private set; } = 12;

        // ============================================
        // INITIALIZATION
        // ============================================

        private static bool _initialized = false;

        /// <summary>
        /// Oyun baslarken cagrilir - varsayilan degerler
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            // Harita boyutuna gore dinamik ayarlamalar
            AdjustSettingsForMapSize();

            Debug.Log($"GameConfig: Initialized - Map: {MapWidth}x{MapHeight}, " +
                     $"World: {WorldWidth:F0}x{WorldHeight:F0}, " +
                     $"Chunk: {ChunkSize}x{ChunkSize}, LoadRadius: {LoadRadius}");
        }

        /// <summary>
        /// Harita boyutunu degistir (runtime'da)
        /// </summary>
        public static void SetMapSize(int width, int height)
        {
            MapWidth = Mathf.Max(20, width);
            MapHeight = Mathf.Max(20, height);
            AdjustSettingsForMapSize();

            // Event trigger
            OnMapSizeChanged?.Invoke(MapWidth, MapHeight);

            Debug.Log($"GameConfig: Map size changed to {MapWidth}x{MapHeight}");
        }

        /// <summary>
        /// Harita boyutuna gore diger ayarlari otomatik ayarla
        /// </summary>
        private static void AdjustSettingsForMapSize()
        {
            int maxDimension = Mathf.Max(MapWidth, MapHeight);

            // Chunk boyutu - buyuk haritalarda buyuk chunk
            if (maxDimension >= 1000)
            {
                ChunkSize = 32;
                LoadRadius = 3;
            }
            else if (maxDimension >= 500)
            {
                ChunkSize = 24;
                LoadRadius = 3;
            }
            else if (maxDimension >= 200)
            {
                ChunkSize = 16;
                LoadRadius = 4;
            }
            else
            {
                ChunkSize = 8;
                LoadRadius = 5;
            }

            // Kamera zoom - harita boyutuna gore
            MinZoom = Mathf.Max(3f, maxDimension * 0.01f);
            MaxZoom = Mathf.Min(150f, maxDimension * 0.3f);
            DefaultZoom = (MinZoom + MaxZoom) / 3f;

            // Minimap zoom - harita boyutuna gore
            MinimapMinZoom = Mathf.Max(20f, maxDimension * 0.03f);
            MinimapMaxZoom = Mathf.Min(500f, maxDimension * 0.4f);

            // Bulut sayisi - harita boyutuna gore
            CloudCount = Mathf.Clamp(maxDimension / 15, 8, 30);
        }

        // ============================================
        // EVENTS
        // ============================================

        /// <summary>
        /// Harita boyutu degistiginde tetiklenir
        /// </summary>
        public static event System.Action<int, int> OnMapSizeChanged;

        // ============================================
        // UTILITY METHODS
        // ============================================

        /// <summary>
        /// World pozisyonunun harita icinde olup olmadigini kontrol et
        /// </summary>
        public static bool IsPositionInMap(Vector3 worldPos)
        {
            return worldPos.x >= MapOffsetX && worldPos.x <= MapOffsetX + WorldWidth &&
                   worldPos.z >= MapOffsetZ && worldPos.z <= MapOffsetZ + WorldHeight;
        }

        /// <summary>
        /// Hex koordinatinin harita icinde olup olmadigini kontrol et
        /// </summary>
        public static bool IsCoordInMap(int q, int r)
        {
            return q >= 0 && q < MapWidth && r >= 0 && r < MapHeight;
        }

        /// <summary>
        /// Chunk koordinatinin gecerli olup olmadigini kontrol et
        /// </summary>
        public static bool IsChunkValid(int chunkX, int chunkY)
        {
            int maxChunkX = Mathf.CeilToInt((float)MapWidth / ChunkSize);
            int maxChunkY = Mathf.CeilToInt((float)MapHeight / ChunkSize);
            return chunkX >= 0 && chunkX < maxChunkX && chunkY >= 0 && chunkY < maxChunkY;
        }

        /// <summary>
        /// Chunk-based loading kullanilmali mi?
        /// </summary>
        public static bool ShouldUseChunkedLoading()
        {
            return MapWidth > ChunkLoadingThreshold || MapHeight > ChunkLoadingThreshold;
        }
    }
}
