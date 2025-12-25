using UnityEngine;
using System;

namespace EmpireWars.Core
{
    /// <summary>
    /// Ekran yönetimi - Safe area, DPI, çözünürlük
    /// Mobil cihazlar için notch/punch-hole desteği
    /// </summary>
    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager Instance { get; private set; }

        [Header("Referans Çözünürlük")]
        [SerializeField] private Vector2 referenceResolution = new Vector2(1920, 1080);
        [SerializeField] private float referenceAspectRatio = 16f / 9f;

        [Header("Safe Area")]
        [SerializeField] private bool applySafeArea = true;
        [SerializeField] private bool debugSafeArea = false;

        [Header("DPI Ayarları")]
        [SerializeField] private float baseDPI = 160f; // Android mdpi baseline
        [SerializeField] private float minUIScale = 0.75f;
        [SerializeField] private float maxUIScale = 1.5f;

        // Cached values
        private Rect lastSafeArea;
        private ScreenOrientation lastOrientation;
        private Vector2Int lastScreenSize;

        // Properties
        public Rect SafeArea => Screen.safeArea;
        public float DPI => Screen.dpi > 0 ? Screen.dpi : baseDPI;
        public float UIScale => Mathf.Clamp(DPI / baseDPI, minUIScale, maxUIScale);
        public bool IsPortrait => Screen.height > Screen.width;
        public bool IsLandscape => Screen.width >= Screen.height;
        public float AspectRatio => (float)Screen.width / Screen.height;
        public Vector2 ScreenSize => new Vector2(Screen.width, Screen.height);
        public Vector2 ReferenceResolution => referenceResolution;

        // Events
        public static event Action<Rect> OnSafeAreaChanged;
        public static event Action<ScreenOrientation> OnOrientationChanged;
        public static event Action<Vector2Int> OnResolutionChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Initialize()
        {
            lastSafeArea = Screen.safeArea;
            lastOrientation = Screen.orientation;
            lastScreenSize = new Vector2Int(Screen.width, Screen.height);

            // Platform-specific ayarlar
            ApplyPlatformSettings();

            Debug.Log($"ScreenManager: {Screen.width}x{Screen.height}, DPI:{DPI:F0}, Scale:{UIScale:F2}, SafeArea:{SafeArea}");
        }

        private void ApplyPlatformSettings()
        {
#if UNITY_ANDROID || UNITY_IOS
            // Mobilde native çözünürlük kullan
            Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);

            // Frame rate ayarı
            Application.targetFrameRate = 60;

            // VSync kapalı (mobilde performans için)
            QualitySettings.vSyncCount = 0;
#elif UNITY_STANDALONE
            // PC'de referans çözünürlük veya mevcut
            // Hardcoded değer kullanma, mevcut çözünürlüğü koru
            Application.targetFrameRate = -1; // VSync'e bırak
#endif
        }

        private void Update()
        {
            CheckForChanges();
        }

        private void CheckForChanges()
        {
            // Safe area değişikliği
            if (lastSafeArea != Screen.safeArea)
            {
                lastSafeArea = Screen.safeArea;
                OnSafeAreaChanged?.Invoke(lastSafeArea);

                if (debugSafeArea)
                {
                    Debug.Log($"Safe area changed: {lastSafeArea}");
                }
            }

            // Oryantasyon değişikliği
            if (lastOrientation != Screen.orientation)
            {
                lastOrientation = Screen.orientation;
                OnOrientationChanged?.Invoke(lastOrientation);
                Debug.Log($"Orientation changed: {lastOrientation}");
            }

            // Çözünürlük değişikliği
            Vector2Int currentSize = new Vector2Int(Screen.width, Screen.height);
            if (lastScreenSize != currentSize)
            {
                lastScreenSize = currentSize;
                OnResolutionChanged?.Invoke(currentSize);
                Debug.Log($"Resolution changed: {currentSize}");
            }
        }

        #region Safe Area Helpers

        /// <summary>
        /// Safe area'yı normalize edilmiş koordinatlarda döndür (0-1 arası)
        /// </summary>
        public Rect GetNormalizedSafeArea()
        {
            Rect safeArea = Screen.safeArea;
            return new Rect(
                safeArea.x / Screen.width,
                safeArea.y / Screen.height,
                safeArea.width / Screen.width,
                safeArea.height / Screen.height
            );
        }

        /// <summary>
        /// Safe area kenar boşluklarını döndür (piksel)
        /// </summary>
        public Vector4 GetSafeAreaMargins()
        {
            Rect safeArea = Screen.safeArea;
            return new Vector4(
                safeArea.x,                                    // Left
                Screen.width - (safeArea.x + safeArea.width),  // Right
                safeArea.y,                                    // Bottom
                Screen.height - (safeArea.y + safeArea.height) // Top
            );
        }

        /// <summary>
        /// Notch var mı kontrol et
        /// </summary>
        public bool HasNotch()
        {
            Rect safeArea = Screen.safeArea;
            return safeArea.x > 0 || safeArea.y > 0 ||
                   safeArea.width < Screen.width ||
                   safeArea.height < Screen.height;
        }

        #endregion

        #region UI Scaling Helpers

        /// <summary>
        /// DPI'ya göre ölçeklenmiş piksel değeri döndür
        /// </summary>
        public float ScaleByDPI(float basePixels)
        {
            return basePixels * UIScale;
        }

        /// <summary>
        /// Referans çözünürlüğe göre ölçeklenmiş değer
        /// </summary>
        public float ScaleByResolution(float baseValue)
        {
            float currentDiagonal = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
            float referenceDiagonal = Mathf.Sqrt(referenceResolution.x * referenceResolution.x + referenceResolution.y * referenceResolution.y);
            return baseValue * (currentDiagonal / referenceDiagonal);
        }

        /// <summary>
        /// Touch-friendly minimum button boyutu (48dp Android guideline)
        /// </summary>
        public float GetMinTouchTargetSize()
        {
            // Android: 48dp minimum
            // iOS: 44pt minimum
            float baseSizeDP = 48f;
            return ScaleByDPI(baseSizeDP);
        }

        #endregion

        #region Platform Detection

        public bool IsMobile()
        {
#if UNITY_ANDROID || UNITY_IOS
            return true;
#else
            return false;
#endif
        }

        public bool IsTablet()
        {
            if (!IsMobile()) return false;

            // 7 inch ve üzeri = tablet
            float diagonalInches = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height) / DPI;
            return diagonalInches >= 7f;
        }

        public bool IsPhone()
        {
            return IsMobile() && !IsTablet();
        }

        #endregion

        #region Debug

        private void OnGUI()
        {
            if (!debugSafeArea) return;

            // Safe area sınırlarını çiz
            Rect safeArea = Screen.safeArea;

            // Dış çerçeve (ekran)
            GUI.color = new Color(1, 0, 0, 0.3f);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);

            // Safe area (güvenli bölge)
            GUI.color = new Color(0, 1, 0, 0.3f);
            // Unity GUI Y ekseni ters, düzelt
            float yFlipped = Screen.height - safeArea.y - safeArea.height;
            GUI.DrawTexture(new Rect(safeArea.x, yFlipped, safeArea.width, safeArea.height), Texture2D.whiteTexture);

            // Bilgi metni
            GUI.color = Color.white;
            string info = $"Screen: {Screen.width}x{Screen.height}\n" +
                          $"SafeArea: {safeArea}\n" +
                          $"DPI: {DPI:F0}, Scale: {UIScale:F2}\n" +
                          $"Platform: {(IsPhone() ? "Phone" : IsTablet() ? "Tablet" : "Desktop")}";
            GUI.Label(new Rect(10, 10, 300, 100), info);
        }

        #endregion
    }
}
