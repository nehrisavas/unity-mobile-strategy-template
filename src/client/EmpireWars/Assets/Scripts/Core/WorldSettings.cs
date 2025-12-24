using UnityEngine;

namespace EmpireWars.Core
{
    /// <summary>
    /// Global oyun ayarlari - Unity Editor'dan degistirilebilir
    /// Assets > Create > EmpireWars > World Settings ile olustur
    /// </summary>
    [CreateAssetMenu(fileName = "WorldSettings", menuName = "EmpireWars/World Settings", order = 1)]
    public class WorldSettings : ScriptableObject
    {
        private static WorldSettings _instance;
        public static WorldSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<WorldSettings>("WorldSettings");
                    if (_instance == null)
                    {
                        Debug.LogWarning("WorldSettings not found in Resources. Using defaults.");
                    }
                }
                return _instance;
            }
        }

        // ============================================
        // HARITA AYARLARI
        // ============================================
        [Header("HARITA AYARLARI")]
        [Tooltip("Harita genisligi (hex tile sayisi)")]
        [Range(100, 5000)]
        public int mapWidth = 2000;

        [Tooltip("Harita yuksekligi (hex tile sayisi)")]
        [Range(100, 5000)]
        public int mapHeight = 2000;

        // ============================================
        // KAMERA AYARLARI
        // ============================================
        [Header("KAMERA AYARLARI")]
        [Tooltip("Minimum zoom (yakin gorunum)")]
        [Range(5f, 50f)]
        public float minZoom = 10f;

        [Tooltip("Maksimum zoom (uzak gorunum)")]
        [Range(50f, 500f)]
        public float maxZoom = 300f;

        [Tooltip("Baslangic zoom degeri")]
        [Range(10f, 200f)]
        public float defaultZoom = 50f;

        // ============================================
        // CHUNK AYARLARI
        // ============================================
        [Header("CHUNK AYARLARI")]
        [Tooltip("Her chunk'taki tile sayisi (NxN)")]
        [Range(8, 64)]
        public int chunkSize = 32;

        [Tooltip("Yukleme yaricapi (chunk sayisi)")]
        [Range(1, 8)]
        public int loadRadius = 2;

        [Tooltip("Chunk guncelleme araligi (saniye)")]
        [Range(0.1f, 1f)]
        public float chunkUpdateInterval = 0.5f;

        // ============================================
        // MINIMAP AYARLARI
        // ============================================
        [Header("MINIMAP AYARLARI")]
        [Tooltip("Minimap UI boyutu (piksel)")]
        [Range(100f, 300f)]
        public float minimapSize = 200f;

        [Tooltip("Minimap minimum zoom")]
        [Range(20f, 100f)]
        public float minimapMinZoom = 40f;

        [Tooltip("Minimap maksimum zoom")]
        [Range(100f, 1000f)]
        public float minimapMaxZoom = 600f;

        // ============================================
        // BULUT AYARLARI
        // ============================================
        [Header("BULUT AYARLARI")]
        [Tooltip("Bulut sayisi")]
        [Range(10, 100)]
        public int cloudCount = 50;

        [Tooltip("Bulut guncelleme araligi (saniye)")]
        [Range(0.05f, 0.5f)]
        public float cloudUpdateInterval = 0.1f;

        // ============================================
        // HESAPLANAN DEGERLER (Sadece okunur)
        // ============================================
        [Header("HESAPLANAN DEGERLER (Salt Okunur)")]
        [SerializeField, Tooltip("World genisligi (birim)")]
        private float _worldWidth;

        [SerializeField, Tooltip("World yuksekligi (birim)")]
        private float _worldHeight;

        public float WorldWidth => mapWidth * HexMetrics.InnerRadius * 2f;
        public float WorldHeight => mapHeight * HexMetrics.OuterRadius * 1.5f;
        public Vector3 WorldCenter => new Vector3(WorldWidth / 2f, 0f, WorldHeight / 2f);

        /// <summary>
        /// GameConfig'i bu ayarlarla guncelle
        /// </summary>
        public void ApplyToGameConfig()
        {
            GameConfig.SetMapSize(mapWidth, mapHeight);
            Debug.Log($"WorldSettings: GameConfig'e uygulandi - {mapWidth}x{mapHeight}");
        }

        private void OnValidate()
        {
            // Editor'da degisiklikleri goster
            _worldWidth = WorldWidth;
            _worldHeight = WorldHeight;
        }
    }
}
