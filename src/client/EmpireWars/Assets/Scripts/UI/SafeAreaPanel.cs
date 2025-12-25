using UnityEngine;
using EmpireWars.Core;

namespace EmpireWars.UI
{
    /// <summary>
    /// Safe Area Panel - RectTransform'u safe area içinde tutar
    /// Notch, punch-hole, rounded corners için UI koruma
    /// Bu componenti UI parent objelerine ekleyin
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaPanel : MonoBehaviour
    {
        [Header("Ayarlar")]
        [Tooltip("Safe area uygulanacak kenarlar")]
        [SerializeField] private bool applyLeft = true;
        [SerializeField] private bool applyRight = true;
        [SerializeField] private bool applyTop = true;
        [SerializeField] private bool applyBottom = true;

        [Header("Ekstra Padding")]
        [SerializeField] private float extraPaddingLeft = 0f;
        [SerializeField] private float extraPaddingRight = 0f;
        [SerializeField] private float extraPaddingTop = 0f;
        [SerializeField] private float extraPaddingBottom = 0f;

        [Header("Debug")]
        [SerializeField] private bool logChanges = false;

        private RectTransform rectTransform;
        private Rect lastSafeArea;
        private Vector2Int lastScreenSize;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            lastSafeArea = Screen.safeArea;
            lastScreenSize = new Vector2Int(Screen.width, Screen.height);
        }

        private void Start()
        {
            ApplySafeArea();

            // Event'e subscribe ol
            ScreenManager.OnSafeAreaChanged += OnSafeAreaChanged;
            ScreenManager.OnResolutionChanged += OnResolutionChanged;
        }

        private void OnDestroy()
        {
            ScreenManager.OnSafeAreaChanged -= OnSafeAreaChanged;
            ScreenManager.OnResolutionChanged -= OnResolutionChanged;
        }

        private void OnSafeAreaChanged(Rect newSafeArea)
        {
            ApplySafeArea();
        }

        private void OnResolutionChanged(Vector2Int newSize)
        {
            ApplySafeArea();
        }

        private void OnRectTransformDimensionsChange()
        {
            // Canvas boyutu değiştiğinde yeniden uygula
            if (rectTransform != null)
            {
                ApplySafeArea();
            }
        }

        /// <summary>
        /// Safe area'yı RectTransform'a uygula
        /// </summary>
        public void ApplySafeArea()
        {
            if (rectTransform == null) return;

            Rect safeArea = Screen.safeArea;

            // Değişiklik yoksa çık
            if (safeArea == lastSafeArea &&
                Screen.width == lastScreenSize.x &&
                Screen.height == lastScreenSize.y)
            {
                return;
            }

            lastSafeArea = safeArea;
            lastScreenSize = new Vector2Int(Screen.width, Screen.height);

            // Normalize edilmiş anchor değerleri hesapla (0-1 arası)
            Vector2 anchorMin = new Vector2(
                applyLeft ? (safeArea.x + extraPaddingLeft) / Screen.width : 0f,
                applyBottom ? (safeArea.y + extraPaddingBottom) / Screen.height : 0f
            );

            Vector2 anchorMax = new Vector2(
                applyRight ? (safeArea.x + safeArea.width - extraPaddingRight) / Screen.width : 1f,
                applyTop ? (safeArea.y + safeArea.height - extraPaddingTop) / Screen.height : 1f
            );

            // Anchor'ları uygula
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;

            // Offset'leri sıfırla (anchor'lar tüm işi yapıyor)
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            if (logChanges)
            {
                Debug.Log($"SafeAreaPanel [{gameObject.name}]: Applied safe area. " +
                          $"AnchorMin:{anchorMin}, AnchorMax:{anchorMax}, SafeArea:{safeArea}");
            }
        }

        /// <summary>
        /// Safe area margin'lerini piksel olarak döndür
        /// </summary>
        public Vector4 GetAppliedMargins()
        {
            Rect safeArea = Screen.safeArea;
            return new Vector4(
                applyLeft ? safeArea.x + extraPaddingLeft : 0f,
                applyRight ? Screen.width - (safeArea.x + safeArea.width) + extraPaddingRight : 0f,
                applyBottom ? safeArea.y + extraPaddingBottom : 0f,
                applyTop ? Screen.height - (safeArea.y + safeArea.height) + extraPaddingTop : 0f
            );
        }

        #region Editor

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isPlaying && rectTransform != null)
            {
                ApplySafeArea();
            }
        }

        /// <summary>
        /// Editor'da safe area simülasyonu için
        /// </summary>
        [ContextMenu("Simulate iPhone X Safe Area")]
        private void SimulateIPhoneX()
        {
            // iPhone X safe area insets (portrait): top:44, bottom:34
            extraPaddingTop = 44f;
            extraPaddingBottom = 34f;
            extraPaddingLeft = 0f;
            extraPaddingRight = 0f;
            ApplySafeArea();
        }

        [ContextMenu("Simulate Android Notch")]
        private void SimulateAndroidNotch()
        {
            // Typical Android notch: top:24-48dp
            extraPaddingTop = 32f;
            extraPaddingBottom = 0f;
            extraPaddingLeft = 0f;
            extraPaddingRight = 0f;
            ApplySafeArea();
        }

        [ContextMenu("Reset Extra Padding")]
        private void ResetPadding()
        {
            extraPaddingTop = 0f;
            extraPaddingBottom = 0f;
            extraPaddingLeft = 0f;
            extraPaddingRight = 0f;
            ApplySafeArea();
        }
#endif

        #endregion
    }
}
