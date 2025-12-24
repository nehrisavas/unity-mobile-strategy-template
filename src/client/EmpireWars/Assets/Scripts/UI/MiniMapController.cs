using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EmpireWars.Core;
using EmpireWars.CameraSystem;

namespace EmpireWars.UI
{
    /// <summary>
    /// Dairesel Mini Harita Kontrolcusu
    /// Rise of Kingdoms / Mafia City tarzi radar minimap
    /// Zoom in/out ve konum takibi destekli
    /// </summary>
    public class MiniMapController : MonoBehaviour, IPointerClickHandler, IDragHandler, IScrollHandler
    {
        public static MiniMapController Instance { get; private set; }

        [Header("Render Ayarlari")]
        [SerializeField] private RenderTexture miniMapTexture;
        [SerializeField] private Camera miniMapCamera;
        [SerializeField] private RawImage miniMapImage;
        [SerializeField] private int textureSize = 256;

        [Header("UI Ayarlari")]
        [SerializeField] private float minimapUISize = 180f;
        [SerializeField] private Vector2 screenOffset = new Vector2(-20f, 100f); // Sag alt

        [Header("Dairesel Gosterim")]
        [SerializeField] private Material circularMaskMaterial;
        [SerializeField] private Image borderRing;
        [SerializeField] private Image playerDot;
        [SerializeField] private Color borderColor = new Color(0.6f, 0.5f, 0.3f, 1f);
        [SerializeField] private Color playerColor = Color.yellow;

        [Header("Zoom Ayarlari")]
        [SerializeField] private float zoomSpeed = 20f;
        [SerializeField] private float zoomSmoothing = 8f;

        [Header("Performans")]
        [SerializeField] private bool useGameConfig = true; // GameConfig'den ayarlari al

        // Internal state
        private float targetZoom;
        private float lastUpdateTime;
        private RectTransform rectTransform;
        private RectTransform canvasRect;
        private bool isInitialized = false;

        // GameConfig'den alinan degerler
        private float minZoom;
        private float maxZoom;
        private float updateInterval;
        private float worldMinX;
        private float worldMaxX;
        private float worldMinZ;
        private float worldMaxZ;

        #region Unity Lifecycle

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (!isInitialized) return;

            // Periyodik guncelleme (performans icin)
            if (Time.time - lastUpdateTime >= updateInterval)
            {
                UpdateMiniMapCamera();
                lastUpdateTime = Time.time;
            }

            // Zoom smooth gecis
            if (miniMapCamera != null && Mathf.Abs(miniMapCamera.orthographicSize - targetZoom) > 0.1f)
            {
                miniMapCamera.orthographicSize = Mathf.Lerp(
                    miniMapCamera.orthographicSize,
                    targetZoom,
                    Time.deltaTime * zoomSmoothing
                );
            }
        }

        private void OnDestroy()
        {
            if (miniMapTexture != null)
            {
                miniMapTexture.Release();
            }
        }

        #endregion

        #region Initialization

        public void Initialize()
        {
            if (isInitialized) return;

            // GameConfig'den ayarlari al
            if (useGameConfig)
            {
                GameConfig.Initialize();
                minZoom = GameConfig.MinimapMinZoom;
                maxZoom = GameConfig.MinimapMaxZoom;
                updateInterval = GameConfig.MinimapUpdateInterval;
                worldMinX = GameConfig.MapOffsetX;
                worldMaxX = GameConfig.MapOffsetX + GameConfig.WorldWidth;
                worldMinZ = GameConfig.MapOffsetZ;
                worldMaxZ = GameConfig.MapOffsetZ + GameConfig.WorldHeight;
                minimapUISize = GameConfig.MinimapSize;
            }
            else
            {
                // Fallback degerler
                minZoom = 30f;
                maxZoom = 200f;
                updateInterval = 0.1f;
                worldMinX = 0f;
                worldMaxX = 60f;
                worldMinZ = 0f;
                worldMaxZ = 60f;
            }

            CreateMiniMapCamera();
            SetupUI();

            targetZoom = (minZoom + maxZoom) / 3f;
            isInitialized = true;
            Debug.Log($"MiniMapController: Dairesel minimap olusturuldu - World({worldMinX}-{worldMaxX}, {worldMinZ}-{worldMaxZ})");
        }

        private void CreateMiniMapCamera()
        {
            // RenderTexture olustur
            if (miniMapTexture == null)
            {
                miniMapTexture = new RenderTexture(textureSize, textureSize, 16);
                miniMapTexture.filterMode = FilterMode.Bilinear;
                miniMapTexture.Create();
            }

            // Kamera olustur
            if (miniMapCamera == null)
            {
                GameObject camObj = new GameObject("MiniMap_Camera");
                camObj.transform.SetParent(transform);
                miniMapCamera = camObj.AddComponent<Camera>();
            }

            // Kamera ayarlari
            miniMapCamera.targetTexture = miniMapTexture;
            miniMapCamera.orthographic = true;
            miniMapCamera.orthographicSize = (minZoom + maxZoom) / 3f;
            miniMapCamera.clearFlags = CameraClearFlags.SolidColor;
            miniMapCamera.backgroundColor = new Color(0.08f, 0.12f, 0.18f, 1f);
            miniMapCamera.cullingMask = ~0; // Tum layerlar
            miniMapCamera.depth = -100;

            // Yukari bak (top-down)
            miniMapCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            miniMapCamera.transform.position = new Vector3(30f, 200f, 30f);
        }

        private void SetupUI()
        {
            // Canvas bul
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                canvas = FindFirstObjectByType<Canvas>();
            }

            if (canvas == null)
            {
                Debug.LogWarning("MiniMapController: Canvas bulunamadi");
                return;
            }

            canvasRect = canvas.GetComponent<RectTransform>();

            // RectTransform ayarla (sag alt kose)
            if (rectTransform == null)
            {
                rectTransform = gameObject.AddComponent<RectTransform>();
            }

            rectTransform.anchorMin = new Vector2(1, 0);
            rectTransform.anchorMax = new Vector2(1, 0);
            rectTransform.pivot = new Vector2(1, 0);
            rectTransform.anchoredPosition = screenOffset;
            rectTransform.sizeDelta = new Vector2(minimapUISize, minimapUISize);

            // Circular material olustur veya bul
            if (circularMaskMaterial == null)
            {
                Shader shader = Shader.Find("EmpireWars/UI/CircularMask");
                if (shader != null)
                {
                    circularMaskMaterial = new Material(shader);
                    circularMaskMaterial.SetColor("_BorderColor", borderColor);
                    circularMaskMaterial.SetFloat("_BorderWidth", 0.025f);
                    circularMaskMaterial.SetFloat("_Softness", 0.01f);
                }
            }

            // RawImage olustur veya ayarla
            if (miniMapImage == null)
            {
                miniMapImage = GetComponentInChildren<RawImage>();
                if (miniMapImage == null)
                {
                    GameObject imageObj = new GameObject("MiniMapImage");
                    imageObj.transform.SetParent(transform);
                    miniMapImage = imageObj.AddComponent<RawImage>();

                    RectTransform imgRect = miniMapImage.GetComponent<RectTransform>();
                    imgRect.anchorMin = Vector2.zero;
                    imgRect.anchorMax = Vector2.one;
                    imgRect.offsetMin = Vector2.zero;
                    imgRect.offsetMax = Vector2.zero;
                }
            }

            miniMapImage.texture = miniMapTexture;
            if (circularMaskMaterial != null)
            {
                miniMapImage.material = circularMaskMaterial;
            }

            // Oyuncu gostergesi (sari nokta ortada)
            if (playerDot == null)
            {
                GameObject dotObj = new GameObject("PlayerDot");
                dotObj.transform.SetParent(transform);
                playerDot = dotObj.AddComponent<Image>();
                playerDot.color = playerColor;

                RectTransform dotRect = playerDot.GetComponent<RectTransform>();
                dotRect.anchorMin = new Vector2(0.5f, 0.5f);
                dotRect.anchorMax = new Vector2(0.5f, 0.5f);
                dotRect.sizeDelta = new Vector2(8f, 8f);
                dotRect.anchoredPosition = Vector2.zero;

                // Dairesel yapmak icin
                playerDot.raycastTarget = false;
            }

            // Zoom butonlari
            CreateZoomButtons();
        }

        private void CreateZoomButtons()
        {
            // Zoom In butonu (+)
            GameObject zoomInObj = new GameObject("ZoomIn");
            zoomInObj.transform.SetParent(transform);
            Image zoomInBg = zoomInObj.AddComponent<Image>();
            zoomInBg.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
            Button zoomInBtn = zoomInObj.AddComponent<Button>();
            zoomInBtn.onClick.AddListener(ZoomIn);

            RectTransform zoomInRect = zoomInObj.GetComponent<RectTransform>();
            zoomInRect.anchorMin = new Vector2(1, 1);
            zoomInRect.anchorMax = new Vector2(1, 1);
            zoomInRect.pivot = new Vector2(1, 1);
            zoomInRect.sizeDelta = new Vector2(28f, 28f);
            zoomInRect.anchoredPosition = new Vector2(-5f, -5f);

            // + text
            GameObject plusText = new GameObject("Text");
            plusText.transform.SetParent(zoomInObj.transform);
            Text plusTxt = plusText.AddComponent<Text>();
            plusTxt.text = "+";
            plusTxt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            plusTxt.fontSize = 20;
            plusTxt.alignment = TextAnchor.MiddleCenter;
            plusTxt.color = Color.white;
            RectTransform plusRect = plusText.GetComponent<RectTransform>();
            plusRect.anchorMin = Vector2.zero;
            plusRect.anchorMax = Vector2.one;
            plusRect.offsetMin = Vector2.zero;
            plusRect.offsetMax = Vector2.zero;

            // Zoom Out butonu (-)
            GameObject zoomOutObj = new GameObject("ZoomOut");
            zoomOutObj.transform.SetParent(transform);
            Image zoomOutBg = zoomOutObj.AddComponent<Image>();
            zoomOutBg.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
            Button zoomOutBtn = zoomOutObj.AddComponent<Button>();
            zoomOutBtn.onClick.AddListener(ZoomOut);

            RectTransform zoomOutRect = zoomOutObj.GetComponent<RectTransform>();
            zoomOutRect.anchorMin = new Vector2(1, 1);
            zoomOutRect.anchorMax = new Vector2(1, 1);
            zoomOutRect.pivot = new Vector2(1, 1);
            zoomOutRect.sizeDelta = new Vector2(28f, 28f);
            zoomOutRect.anchoredPosition = new Vector2(-5f, -38f);

            // - text
            GameObject minusText = new GameObject("Text");
            minusText.transform.SetParent(zoomOutObj.transform);
            Text minusTxt = minusText.AddComponent<Text>();
            minusTxt.text = "-";
            minusTxt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            minusTxt.fontSize = 24;
            minusTxt.alignment = TextAnchor.MiddleCenter;
            minusTxt.color = Color.white;
            RectTransform minusRect = minusText.GetComponent<RectTransform>();
            minusRect.anchorMin = Vector2.zero;
            minusRect.anchorMax = Vector2.one;
            minusRect.offsetMin = Vector2.zero;
            minusRect.offsetMax = Vector2.zero;
        }

        #endregion

        #region Update

        private void UpdateMiniMapCamera()
        {
            if (miniMapCamera == null) return;

            // Ana kamera pozisyonunu takip et
            Camera mainCam = Camera.main;
            Vector3 camPos = miniMapCamera.transform.position;

            if (MapCameraController.Instance != null)
            {
                Vector3 mainCamPos = MapCameraController.Instance.transform.position;
                camPos.x = mainCamPos.x;
                camPos.z = mainCamPos.z;
            }
            else if (mainCam != null)
            {
                camPos.x = mainCam.transform.position.x;
                camPos.z = mainCam.transform.position.z;
            }

            // Sinirlari kontrol et
            camPos.x = Mathf.Clamp(camPos.x, worldMinX + targetZoom, worldMaxX - targetZoom);
            camPos.z = Mathf.Clamp(camPos.z, worldMinZ + targetZoom, worldMaxZ - targetZoom);
            camPos.y = 200f;

            miniMapCamera.transform.position = camPos;
        }

        #endregion

        #region Interaction

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                MoveToClickedPosition(eventData.position);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveToClickedPosition(eventData.position);
        }

        public void OnScroll(PointerEventData eventData)
        {
            // Mouse scroll ile zoom
            float scrollDelta = eventData.scrollDelta.y;
            if (scrollDelta > 0)
            {
                ZoomIn();
            }
            else if (scrollDelta < 0)
            {
                ZoomOut();
            }
        }

        private void MoveToClickedPosition(Vector2 screenPosition)
        {
            if (MapCameraController.Instance == null || rectTransform == null) return;

            // Screen pozisyonunu lokal pozisyona cevir
            Vector2 localPoint;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, screenPosition, null, out localPoint))
            {
                return;
            }

            // Normalize et (-0.5 to 0.5)
            Vector2 normalized = new Vector2(
                localPoint.x / rectTransform.rect.width,
                localPoint.y / rectTransform.rect.height
            );

            // Daire icinde mi kontrol et
            if (normalized.magnitude > 0.5f) return;

            // Minimap kamera pozisyonuna gore world pozisyonu hesapla
            Vector3 miniCamPos = miniMapCamera.transform.position;
            float currentOrthoSize = miniMapCamera.orthographicSize;

            Vector3 worldPos = new Vector3(
                miniCamPos.x + normalized.x * currentOrthoSize * 2f,
                0f,
                miniCamPos.z + normalized.y * currentOrthoSize * 2f
            );

            // Ana kamerayi hareket ettir
            MapCameraController.Instance.FocusOnPosition(worldPos);
        }

        #endregion

        #region Zoom

        public void ZoomIn()
        {
            targetZoom = Mathf.Max(targetZoom - zoomSpeed, minZoom);
        }

        public void ZoomOut()
        {
            targetZoom = Mathf.Min(targetZoom + zoomSpeed, maxZoom);
        }

        public void SetZoom(float zoom)
        {
            targetZoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        }

        public float GetCurrentZoom()
        {
            return miniMapCamera != null ? miniMapCamera.orthographicSize : targetZoom;
        }

        #endregion

        #region Public API

        /// <summary>
        /// [DEPRECATED] GameConfig kullanin. Eski API uyumlulugu icin tutuldu.
        /// </summary>
        public void SetWorldBounds(float minX, float maxX, float minZ, float maxZ)
        {
            // GameConfig kullaniliyorsa bu metod etkisiz
            if (useGameConfig)
            {
                Debug.LogWarning("MiniMapController: SetWorldBounds cagrildi ama useGameConfig=true, GameConfig kullaniliyor");
                return;
            }

            worldMinX = minX;
            worldMaxX = maxX;
            worldMinZ = minZ;
            worldMaxZ = maxZ;

            float mapWidth = maxX - minX;
            float mapHeight = maxZ - minZ;
            float mapSize = Mathf.Min(mapWidth, mapHeight);

            minZoom = Mathf.Max(20f, mapSize * 0.05f);
            maxZoom = Mathf.Min(500f, mapSize * 0.5f);
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }

        /// <summary>
        /// Belirli bir dunya pozisyonunu minimap uzerinde goster
        /// </summary>
        public Vector2 WorldToMiniMapPosition(Vector3 worldPos)
        {
            if (miniMapCamera == null || rectTransform == null)
                return Vector2.zero;

            Vector3 miniCamPos = miniMapCamera.transform.position;
            float orthoSize = miniMapCamera.orthographicSize;

            // Normalize et
            float normalizedX = (worldPos.x - miniCamPos.x) / (orthoSize * 2f);
            float normalizedZ = (worldPos.z - miniCamPos.z) / (orthoSize * 2f);

            // UI pozisyonuna cevir
            return new Vector2(
                normalizedX * rectTransform.rect.width,
                normalizedZ * rectTransform.rect.height
            );
        }

        /// <summary>
        /// Minimap'i goster/gizle
        /// </summary>
        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
            if (miniMapCamera != null)
            {
                miniMapCamera.enabled = visible;
            }
        }

        #endregion
    }

    /// <summary>
    /// Marker tipleri
    /// </summary>
    public enum MarkerType
    {
        Player,
        Ally,
        Enemy,
        Resource,
        City,
        Army,
        Event
    }
}
