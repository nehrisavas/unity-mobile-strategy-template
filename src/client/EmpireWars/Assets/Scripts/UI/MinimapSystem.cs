using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EmpireWars.Core;

namespace EmpireWars.UI
{
    /// <summary>
    /// Profesyonel Minimap Sistemi
    /// Mafia City / Rise of Kingdoms tarzinda sag alt kosede radar
    /// </summary>
    public class MinimapSystem : MonoBehaviour, IPointerClickHandler, IDragHandler
    {
        [Header("References")]
        [SerializeField] private Camera minimapCamera;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private RawImage minimapImage;
        [SerializeField] private RectTransform playerIndicator;
        [SerializeField] private RectTransform viewAreaIndicator;

        [Header("Minimap Settings")]
        [SerializeField] private float minimapSize = 200f;
        [SerializeField] private float cameraHeight = 100f;
        [SerializeField] private float cameraOrthoSize = 50f;
        [SerializeField] private int renderTextureSize = 512;

        [Header("World Bounds")]
        [SerializeField] private float worldMinX = -50f;
        [SerializeField] private float worldMaxX = 50f;
        [SerializeField] private float worldMinZ = -50f;
        [SerializeField] private float worldMaxZ = 50f;

        [Header("Visual Settings")]
        [SerializeField] private Color playerColor = Color.yellow;
        [SerializeField] private Color viewAreaColor = new Color(1f, 1f, 1f, 0.3f);

        private RenderTexture minimapRenderTexture;
        private RectTransform minimapRect;
        private bool isInitialized = false;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (isInitialized) return;

            // Main camera bul
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            // Minimap kamerasini olustur
            CreateMinimapCamera();

            // UI olustur
            CreateMinimapUI();

            isInitialized = true;
            Debug.Log("MinimapSystem: Basariyla olusturuldu.");
        }

        private void CreateMinimapCamera()
        {
            if (minimapCamera != null) return;

            // Minimap kamerasi olustur
            GameObject camObj = new GameObject("Minimap Camera");
            camObj.transform.SetParent(transform);
            minimapCamera = camObj.AddComponent<Camera>();

            // Kamera ayarlari
            minimapCamera.orthographic = true;
            minimapCamera.orthographicSize = cameraOrthoSize;
            minimapCamera.transform.position = new Vector3(0, cameraHeight, 0);
            minimapCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            minimapCamera.clearFlags = CameraClearFlags.SolidColor;
            minimapCamera.backgroundColor = new Color(0.1f, 0.15f, 0.2f, 1f);
            minimapCamera.cullingMask = ~0; // Tum layerlari goster
            minimapCamera.depth = -10;

            // Render texture olustur
            minimapRenderTexture = new RenderTexture(renderTextureSize, renderTextureSize, 16);
            minimapRenderTexture.antiAliasing = 2;
            minimapCamera.targetTexture = minimapRenderTexture;
        }

        private void CreateMinimapUI()
        {
            // Canvas bul veya olustur
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("UI Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
            }

            // Minimap container
            GameObject minimapContainer = new GameObject("Minimap Container");
            minimapContainer.transform.SetParent(canvas.transform);
            minimapRect = minimapContainer.AddComponent<RectTransform>();

            // Sag alt kose pozisyonu (bottom nav bar'in ustunde)
            minimapRect.anchorMin = new Vector2(1, 0);
            minimapRect.anchorMax = new Vector2(1, 0);
            minimapRect.pivot = new Vector2(1, 0);
            minimapRect.anchoredPosition = new Vector2(-20, 100); // Bottom nav bar ustunde
            minimapRect.sizeDelta = new Vector2(minimapSize, minimapSize);

            // Arka plan (cember cerceve)
            GameObject bgObj = new GameObject("Background");
            bgObj.transform.SetParent(minimapContainer.transform);
            Image bgImage = bgObj.AddComponent<Image>();
            bgImage.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);
            RectTransform bgRect = bgObj.GetComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.offsetMin = new Vector2(-5, -5);
            bgRect.offsetMax = new Vector2(5, 5);

            // Mask (dairesel minimap icin)
            GameObject maskObj = new GameObject("Mask");
            maskObj.transform.SetParent(minimapContainer.transform);
            Image maskImage = maskObj.AddComponent<Image>();
            maskImage.color = Color.white;
            Mask mask = maskObj.AddComponent<Mask>();
            mask.showMaskGraphic = false;
            RectTransform maskRect = maskObj.GetComponent<RectTransform>();
            maskRect.anchorMin = Vector2.zero;
            maskRect.anchorMax = Vector2.one;
            maskRect.offsetMin = Vector2.zero;
            maskRect.offsetMax = Vector2.zero;

            // Minimap goruntu
            GameObject imageObj = new GameObject("Minimap Image");
            imageObj.transform.SetParent(maskObj.transform);
            minimapImage = imageObj.AddComponent<RawImage>();
            minimapImage.texture = minimapRenderTexture;
            RectTransform imageRect = imageObj.GetComponent<RectTransform>();
            imageRect.anchorMin = Vector2.zero;
            imageRect.anchorMax = Vector2.one;
            imageRect.offsetMin = Vector2.zero;
            imageRect.offsetMax = Vector2.zero;

            // Oyuncu gostergesi (sari nokta)
            GameObject playerObj = new GameObject("Player Indicator");
            playerObj.transform.SetParent(minimapContainer.transform);
            Image playerImage = playerObj.AddComponent<Image>();
            playerImage.color = playerColor;
            playerIndicator = playerObj.GetComponent<RectTransform>();
            playerIndicator.sizeDelta = new Vector2(10, 10);
            playerIndicator.anchorMin = new Vector2(0.5f, 0.5f);
            playerIndicator.anchorMax = new Vector2(0.5f, 0.5f);

            // Gorunur alan gostergesi
            GameObject viewObj = new GameObject("View Area");
            viewObj.transform.SetParent(minimapContainer.transform);
            Image viewImage = viewObj.AddComponent<Image>();
            viewImage.color = viewAreaColor;
            viewAreaIndicator = viewObj.GetComponent<RectTransform>();
            viewAreaIndicator.sizeDelta = new Vector2(40, 30);
            viewAreaIndicator.anchorMin = new Vector2(0.5f, 0.5f);
            viewAreaIndicator.anchorMax = new Vector2(0.5f, 0.5f);

            // Cerceve (border)
            GameObject borderObj = new GameObject("Border");
            borderObj.transform.SetParent(minimapContainer.transform);
            Image borderImage = borderObj.AddComponent<Image>();
            borderImage.color = new Color(0.6f, 0.5f, 0.3f, 1f); // Altin rengi
            borderImage.raycastTarget = false;
            Outline outline = borderObj.AddComponent<Outline>();
            outline.effectColor = new Color(0.3f, 0.25f, 0.15f, 1f);
            outline.effectDistance = new Vector2(2, 2);
            RectTransform borderRect = borderObj.GetComponent<RectTransform>();
            borderRect.anchorMin = Vector2.zero;
            borderRect.anchorMax = Vector2.one;
            borderRect.offsetMin = new Vector2(-3, -3);
            borderRect.offsetMax = new Vector2(3, 3);
            // Border sadece cerceve olsun, ici bos
            borderImage.fillCenter = false;
            borderImage.type = Image.Type.Sliced;
        }

        private void LateUpdate()
        {
            if (!isInitialized || mainCamera == null) return;

            UpdateMinimapCamera();
            UpdateIndicators();
        }

        private void UpdateMinimapCamera()
        {
            // Minimap kamerasini ana kamera pozisyonuna gore guncelle
            Vector3 mainPos = mainCamera.transform.position;
            minimapCamera.transform.position = new Vector3(mainPos.x, cameraHeight, mainPos.z);
        }

        private void UpdateIndicators()
        {
            if (mainCamera == null) return;

            // Oyuncu/kamera pozisyonunu minimap uzerinde goster
            Vector3 worldPos = mainCamera.transform.position;

            // World pozisyonunu minimap pozisyonuna cevir
            float normalizedX = (worldPos.x - worldMinX) / (worldMaxX - worldMinX);
            float normalizedZ = (worldPos.z - worldMinZ) / (worldMaxZ - worldMinZ);

            // -0.5 ile 0.5 arasina normalize et (merkez = 0)
            float minimapX = (normalizedX - 0.5f) * minimapSize;
            float minimapY = (normalizedZ - 0.5f) * minimapSize;

            // Sinirlar icinde tut
            minimapX = Mathf.Clamp(minimapX, -minimapSize / 2f + 10f, minimapSize / 2f - 10f);
            minimapY = Mathf.Clamp(minimapY, -minimapSize / 2f + 10f, minimapSize / 2f - 10f);

            // Gostergeleri guncelle
            if (playerIndicator != null)
            {
                playerIndicator.anchoredPosition = new Vector2(minimapX, minimapY);
            }

            if (viewAreaIndicator != null)
            {
                viewAreaIndicator.anchoredPosition = new Vector2(minimapX, minimapY);

                // Kamera rotasyonuna gore don
                float rotY = mainCamera.transform.eulerAngles.y;
                viewAreaIndicator.localRotation = Quaternion.Euler(0, 0, -rotY);
            }
        }

        // Minimap'e tiklandiginda o konuma git
        public void OnPointerClick(PointerEventData eventData)
        {
            MoveToClickPosition(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveToClickPosition(eventData.position);
        }

        private void MoveToClickPosition(Vector2 screenPosition)
        {
            if (minimapRect == null || mainCamera == null) return;

            // Screen pozisyonunu minimap lokal pozisyonuna cevir
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                minimapRect, screenPosition, null, out localPoint))
            {
                // Lokal pozisyonu world pozisyonuna cevir
                float normalizedX = (localPoint.x / minimapSize) + 0.5f;
                float normalizedZ = (localPoint.y / minimapSize) + 0.5f;

                float worldX = Mathf.Lerp(worldMinX, worldMaxX, normalizedX);
                float worldZ = Mathf.Lerp(worldMinZ, worldMaxZ, normalizedZ);

                // Kamerayi o konuma tasi (MapCameraController varsa)
                var cameraController = EmpireWars.CameraSystem.MapCameraController.Instance;
                if (cameraController != null)
                {
                    Vector3 newTarget = new Vector3(worldX, 0f, worldZ);
                    cameraController.FocusOnPosition(newTarget);
                }
            }
        }

        private void OnDestroy()
        {
            if (minimapRenderTexture != null)
            {
                minimapRenderTexture.Release();
                Destroy(minimapRenderTexture);
            }
        }

        /// <summary>
        /// Dunya sinirlarini ayarla
        /// </summary>
        public void SetWorldBounds(float minX, float maxX, float minZ, float maxZ)
        {
            worldMinX = minX;
            worldMaxX = maxX;
            worldMinZ = minZ;
            worldMaxZ = maxZ;
        }
    }
}
