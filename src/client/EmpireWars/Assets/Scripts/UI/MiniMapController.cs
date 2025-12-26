using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using EmpireWars.Core;
using EmpireWars.CameraSystem;
using EmpireWars.Data;
using EmpireWars.WorldMap;
using TMPro;
using System.Collections.Generic;

namespace EmpireWars.UI
{
    /// <summary>
    /// Dairesel Mini Harita Kontrolcusu
    /// Rise of Kingdoms / Mafia City tarzi radar minimap
    /// Zoom in/out ve konum takibi destekli
    /// Pre-rendered terrain texture ile tum haritayi gosterir (chunk bagimsiz)
    /// </summary>
    public class MiniMapController : MonoBehaviour, IPointerClickHandler, IDragHandler, IScrollHandler
    {
        public static MiniMapController Instance { get; private set; }

        [Header("Render Ayarlari")]
        [SerializeField] private RenderTexture miniMapTexture;
        [SerializeField] private Camera miniMapCamera;
        [SerializeField] private RawImage miniMapImage;
        [SerializeField] private int textureSize = 256;

        [Header("Terrain Texture Mode")]
        [SerializeField] private bool useTerrainTexture = true; // Pre-rendered terrain texture kullan
        [SerializeField] private Texture2D terrainPreviewTexture; // Tum haritanin onceden render edilmis hali
        [SerializeField] private int terrainTextureSize = 512; // Terrain texture boyutu

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

        [Header("Koordinat Arama")]
        [SerializeField] private bool showCoordinateSearch = true;

        // Koordinat arama UI
        private TMP_InputField inputX;
        private TMP_InputField inputY;
        private Button goButton;
        private TMP_Text currentCoordText;
        private GameObject coordSearchPanel;

        // Harita geçiş butonları
        private Button worldMapButton;
        private Button cityMapButton;
        private GameObject mapTogglePanel;

        // Mevcut harita modu
        public enum MapMode { World, City }
        public static MapMode CurrentMapMode { get; private set; } = MapMode.World;
        public static event System.Action<MapMode> OnMapModeChanged;

        // Viewport indicator (terrain texture modunda gorunen alan cercevesi)
        private RectTransform viewportIndicator;

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

            // Terrain texture modunda her frame güncelle (akıcı takip için)
            if (useTerrainTexture)
            {
                UpdateTerrainTextureMode();
            }
            else
            {
                // Kamera modunda periyodik güncelleme
                if (Time.time - lastUpdateTime >= updateInterval)
                {
                    UpdateMiniMapCamera();
                    lastUpdateTime = Time.time;
                }

                // Zoom smooth geçiş
                if (miniMapCamera != null && Mathf.Abs(miniMapCamera.orthographicSize - targetZoom) > 0.1f)
                {
                    miniMapCamera.orthographicSize = Mathf.Lerp(
                        miniMapCamera.orthographicSize,
                        targetZoom,
                        Time.deltaTime * zoomSmoothing
                    );
                }
            }

            // Koordinat göstergesini güncelle
            if (Time.time - lastUpdateTime >= 0.1f)
            {
                UpdateCurrentCoordinateDisplay();
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

            // KRITIK: EventSystem ve Canvas kontrolu
            EnsureEventSystem();
            EnsureCanvasHasRaycaster();

            // Terrain texture modu aktifse, ÖNCE pre-rendered harita olustur
            if (useTerrainTexture)
            {
                GenerateTerrainPreviewTexture();
            }

            CreateMiniMapCamera();
            SetupUI();

            targetZoom = (minZoom + maxZoom) / 3f;
            isInitialized = true;

            Debug.Log($"MiniMapController: Dairesel minimap olusturuldu - World({worldMinX}-{worldMaxX}, {worldMinZ}-{worldMaxZ}), TerrainTexture: {useTerrainTexture}");
        }

        /// <summary>
        /// Tum haritanin terrain renklerini iceren bir texture olustur
        /// Bu sayede chunk yuklenmeden bile tum harita gorunur
        /// </summary>
        private void GenerateTerrainPreviewTexture()
        {
            // Texture olustur
            terrainPreviewTexture = new Texture2D(terrainTextureSize, terrainTextureSize, TextureFormat.RGB24, false);
            terrainPreviewTexture.filterMode = FilterMode.Bilinear;
            terrainPreviewTexture.wrapMode = TextureWrapMode.Clamp;

            // Harita boyutlari
            int mapWidth = GameConfig.MapWidth;
            int mapHeight = GameConfig.MapHeight;

            // Her pixel icin terrain rengi belirle
            Color[] pixels = new Color[terrainTextureSize * terrainTextureSize];

            for (int y = 0; y < terrainTextureSize; y++)
            {
                for (int x = 0; x < terrainTextureSize; x++)
                {
                    // Texture koordinatini harita koordinatina cevir
                    int mapQ = Mathf.FloorToInt((float)x / terrainTextureSize * mapWidth);
                    int mapR = Mathf.FloorToInt((float)y / terrainTextureSize * mapHeight);

                    // Tile verisini al
                    var tileData = KingdomMapGenerator.GetTileAt(mapQ, mapR);
                    TerrainType terrain = tileData.Terrain;

                    // Terrain rengini al
                    Color terrainColor = TerrainProperties.GetTerrainColor(terrain);

                    // Maden varsa ozel renk
                    if (tileData.MineLevel > 0)
                    {
                        terrainColor = KingdomMapGenerator.GetMineTypeColor(tileData.MineType);
                        // Seviyeye gore parlaklik
                        float brightness = 0.7f + (tileData.MineLevel / 7f) * 0.3f;
                        terrainColor *= brightness;
                    }

                    // Bina varsa koyu tonlama
                    if (tileData.HasBuilding)
                    {
                        terrainColor = Color.Lerp(terrainColor, Color.black, 0.3f);
                    }

                    // Pixel'e ata
                    int pixelIndex = y * terrainTextureSize + x;
                    pixels[pixelIndex] = terrainColor;
                }
            }

            terrainPreviewTexture.SetPixels(pixels);
            terrainPreviewTexture.Apply();

            Debug.Log($"MiniMap: Terrain preview texture olusturuldu ({terrainTextureSize}x{terrainTextureSize})");
        }

        /// <summary>
        /// Kamera gorunum alanini gosteren cerceve olustur
        /// </summary>
        private void CreateViewportIndicator()
        {
            GameObject viewportObj = new GameObject("ViewportIndicator");
            viewportObj.transform.SetParent(transform);

            // Çerçeve görüntüsü - yarı saydam beyaz arka plan
            Image viewportImg = viewportObj.AddComponent<Image>();
            viewportImg.color = new Color(1f, 1f, 1f, 0.15f); // Hafif görünür

            viewportIndicator = viewportObj.GetComponent<RectTransform>();
            viewportIndicator.anchorMin = new Vector2(0.5f, 0.5f);
            viewportIndicator.anchorMax = new Vector2(0.5f, 0.5f);
            viewportIndicator.sizeDelta = new Vector2(40f, 30f);

            // Outline ekle (parlak çerçeve)
            Outline outline = viewportObj.AddComponent<Outline>();
            outline.effectColor = new Color(1f, 0.9f, 0.3f, 0.9f); // Altın sarısı
            outline.effectDistance = new Vector2(2f, 2f);

            // Raycast'i devre dışı bırak (tıklama engellemesin)
            viewportImg.raycastTarget = false;

            viewportObj.SetActive(true);
        }

        /// <summary>
        /// Terrain texture modunda player dot ve viewport pozisyonlarini guncelle
        /// </summary>
        private void UpdateTerrainTextureMode()
        {
            if (!useTerrainTexture || playerDot == null) return;

            Vector3 camPos = Vector3.zero;
            float camZoom = GameConfig.DefaultZoom;

            // Kamera pozisyonunu al - öncelik sırası
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                camPos = mainCam.transform.position;
                if (mainCam.orthographic)
                    camZoom = mainCam.orthographicSize;
            }

            // MapCameraController varsa ondan al (daha doğru)
            if (MapCameraController.Instance != null)
            {
                camPos = MapCameraController.Instance.transform.position;
                camZoom = MapCameraController.Instance.GetCurrentZoom();
            }

            // Hiçbiri yoksa fallback
            if (mainCam == null && MapCameraController.Instance == null)
            {
                camPos = new Vector3(GameConfig.WorldWidth / 2f, 0, GameConfig.WorldHeight / 2f);
            }

            // World pozisyonunu normalize et (0-1 arası)
            float normalizedX = Mathf.Clamp01(camPos.x / GameConfig.WorldWidth);
            float normalizedZ = Mathf.Clamp01(camPos.z / GameConfig.WorldHeight);

            // Minimap boyutu (dairesel, radius = size/2)
            float mapRadius = minimapUISize / 2f;

            // Player dot pozisyonu - merkeze göre offset (-radius to +radius)
            float dotX = (normalizedX - 0.5f) * minimapUISize;
            float dotZ = (normalizedZ - 0.5f) * minimapUISize;

            // Daire sınırları içinde tut
            Vector2 dotPos = new Vector2(dotX, dotZ);
            if (dotPos.magnitude > mapRadius * 0.9f)
            {
                dotPos = dotPos.normalized * mapRadius * 0.9f;
            }

            RectTransform dotRect = playerDot.GetComponent<RectTransform>();
            dotRect.anchoredPosition = dotPos;

            // Viewport indicator pozisyonu ve boyutu (zoom'a göre)
            if (viewportIndicator != null)
            {
                viewportIndicator.anchoredPosition = dotPos;

                // Görünüm alanının minimap üzerindeki boyutunu hesapla
                // camZoom = orthographicSize, görünüm yüksekliği = camZoom * 2
                float viewWorldWidth = camZoom * 2f * 1.77f; // 16:9 aspect ratio
                float viewWorldHeight = camZoom * 2f;

                float viewWidth = (viewWorldWidth / GameConfig.WorldWidth) * minimapUISize;
                float viewHeight = (viewWorldHeight / GameConfig.WorldHeight) * minimapUISize;

                viewportIndicator.sizeDelta = new Vector2(
                    Mathf.Clamp(viewWidth, 8f, minimapUISize * 0.7f),
                    Mathf.Clamp(viewHeight, 6f, minimapUISize * 0.5f)
                );
            }
        }

        /// <summary>
        /// EventSystem yoksa olusturur - UI input icin KRITIK
        /// Unity 6 Input System icin InputSystemUIInputModule kullanilmali
        /// </summary>
        private void EnsureEventSystem()
        {
            EventSystem eventSystem = FindFirstObjectByType<EventSystem>();
            if (eventSystem == null)
            {
                Debug.Log("MiniMap: EventSystem bulunamadi, olusturuluyor...");
                GameObject eventSystemObj = new GameObject("EventSystem");
                eventSystem = eventSystemObj.AddComponent<EventSystem>();
                // Unity 6 yeni Input System icin InputSystemUIInputModule kullan
                eventSystemObj.AddComponent<InputSystemUIInputModule>();
                Debug.Log("MiniMap: EventSystem + InputSystemUIInputModule olusturuldu");
            }
            else
            {
                Debug.Log($"MiniMap: EventSystem mevcut - {eventSystem.gameObject.name}");
            }

            // Unity 6 Input System kontrolu - InputSystemUIInputModule gerekli
            InputSystemUIInputModule inputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
            if (inputModule == null)
            {
                // Eski StandaloneInputModule varsa kaldir
                StandaloneInputModule oldModule = eventSystem.GetComponent<StandaloneInputModule>();
                if (oldModule != null)
                {
                    Debug.Log("MiniMap: Eski StandaloneInputModule kaldiriliyor...");
                    DestroyImmediate(oldModule);
                }

                inputModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
                Debug.Log("MiniMap: InputSystemUIInputModule eklendi (Unity 6 Input System)");
            }
            else
            {
                Debug.Log("MiniMap: InputSystemUIInputModule mevcut");
            }
        }

        /// <summary>
        /// Canvas'ta GraphicRaycaster yoksa ekler - UI click icin KRITIK
        /// </summary>
        private void EnsureCanvasHasRaycaster()
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
            {
                canvas = FindFirstObjectByType<Canvas>();
            }

            if (canvas != null)
            {
                GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
                if (raycaster == null)
                {
                    raycaster = canvas.gameObject.AddComponent<GraphicRaycaster>();
                    Debug.Log("MiniMap: GraphicRaycaster Canvas'a eklendi");
                }
                else
                {
                    Debug.Log("MiniMap: GraphicRaycaster mevcut");
                }
            }
            else
            {
                Debug.LogWarning("MiniMap: Canvas bulunamadi!");
            }
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
                // Canvas'tan bagimsiz, world space'te
                miniMapCamera = camObj.AddComponent<Camera>();
            }

            // Kamera ayarlari
            miniMapCamera.targetTexture = miniMapTexture;
            miniMapCamera.orthographic = true;
            miniMapCamera.orthographicSize = 150f; // Genis gorunum
            miniMapCamera.clearFlags = CameraClearFlags.SolidColor;
            miniMapCamera.backgroundColor = new Color(0.05f, 0.08f, 0.12f, 1f);
            miniMapCamera.nearClipPlane = 0.1f;
            miniMapCamera.farClipPlane = 500f; // Yeterli derinlik

            // Clouds layer'i haric tut (minimap'te bulut gorunmemeli)
            // KRITIK: -1 = tum layer'lari render et, ~0 YANLIS cunku hicbir sey render etmez
            int cloudsLayer = LayerMask.NameToLayer("Clouds");
            if (cloudsLayer >= 0)
            {
                miniMapCamera.cullingMask = -1 & ~(1 << cloudsLayer); // Tum layer'lar EKSI clouds
            }
            else
            {
                miniMapCamera.cullingMask = -1; // Fallback: TUM layer'lari render et
            }
            miniMapCamera.depth = -100;
            miniMapCamera.useOcclusionCulling = false; // Oclusion sorunlarini onle

            // Yukari bak (top-down) - harita merkezinde baslat
            miniMapCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            float centerX = GameConfig.WorldWidth / 2f;
            float centerZ = GameConfig.WorldHeight / 2f;
            miniMapCamera.transform.position = new Vector3(centerX, 300f, centerZ);

            Debug.Log($"MiniMapCamera: Pozisyon ({centerX}, 300, {centerZ}), OrthoSize: 150");
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

            // Terrain texture veya camera texture kullan
            if (useTerrainTexture && terrainPreviewTexture != null)
            {
                miniMapImage.texture = terrainPreviewTexture;
                Debug.Log("MiniMap: Terrain preview texture kullaniliyor");
            }
            else
            {
                miniMapImage.texture = miniMapTexture;
                Debug.Log("MiniMap: Camera render texture kullaniliyor");
            }

            if (circularMaskMaterial != null)
            {
                miniMapImage.material = circularMaskMaterial;
            }

            // Oyuncu gostergesi (sari nokta) - EN ÜSTTE olmalı
            if (playerDot == null)
            {
                GameObject dotObj = new GameObject("PlayerDot");
                dotObj.transform.SetParent(transform);
                playerDot = dotObj.AddComponent<Image>();
                playerDot.color = playerColor;

                RectTransform dotRect = playerDot.GetComponent<RectTransform>();
                dotRect.anchorMin = new Vector2(0.5f, 0.5f);
                dotRect.anchorMax = new Vector2(0.5f, 0.5f);
                dotRect.sizeDelta = new Vector2(14f, 14f); // Daha büyük
                dotRect.anchoredPosition = Vector2.zero;

                // Outline ekle (görünürlük için)
                Outline dotOutline = dotObj.AddComponent<Outline>();
                dotOutline.effectColor = Color.black;
                dotOutline.effectDistance = new Vector2(1.5f, -1.5f);

                playerDot.raycastTarget = false;

                // En üste çıkar
                dotObj.transform.SetAsLastSibling();
            }

            // Görüş alanı çerçevesi (terrain texture modunda)
            if (useTerrainTexture)
            {
                CreateViewportIndicator();
            }

            // Zoom butonlari
            CreateZoomButtons();

            // Koordinat arama
            if (showCoordinateSearch)
            {
                CreateCoordinateSearch();
            }

            // Harita geçiş butonları
            CreateMapToggleButtons(canvas.transform);
        }

        /// <summary>
        /// Şehir/Dünya harita geçiş butonlarını oluştur
        /// </summary>
        private void CreateMapToggleButtons(Transform canvasTransform)
        {
            // Sağ üst köşede butonlar
            mapTogglePanel = new GameObject("MapTogglePanel");
            mapTogglePanel.transform.SetParent(canvasTransform);

            RectTransform panelRect = mapTogglePanel.AddComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(1, 1); // Sağ üst
            panelRect.anchorMax = new Vector2(1, 1);
            panelRect.pivot = new Vector2(1, 1);
            panelRect.anchoredPosition = new Vector2(-15f, -15f);
            panelRect.sizeDelta = new Vector2(200f, 50f);

            // Arka plan
            Image panelBg = mapTogglePanel.AddComponent<Image>();
            panelBg.color = new Color(0.05f, 0.05f, 0.08f, 0.9f);
            panelBg.raycastTarget = false;

            // Dünya butonu
            worldMapButton = CreateMapButton(mapTogglePanel.transform, "Dünya", 10f, true);
            worldMapButton.onClick.AddListener(() => SetMapMode(MapMode.World));

            // Şehir butonu
            cityMapButton = CreateMapButton(mapTogglePanel.transform, "Şehir", 105f, false);
            cityMapButton.onClick.AddListener(() => SetMapMode(MapMode.City));

            UpdateMapToggleButtons();
        }

        private Button CreateMapButton(Transform parent, string text, float xPos, bool isActive)
        {
            GameObject btnObj = new GameObject($"Btn_{text}");
            btnObj.transform.SetParent(parent);

            Image btnBg = btnObj.AddComponent<Image>();
            btnBg.color = isActive ? new Color(0.3f, 0.5f, 0.3f, 1f) : new Color(0.2f, 0.2f, 0.25f, 1f);
            btnBg.raycastTarget = true;

            Button btn = btnObj.AddComponent<Button>();
            btn.targetGraphic = btnBg;

            RectTransform btnRect = btnObj.GetComponent<RectTransform>();
            btnRect.anchorMin = new Vector2(0, 0.5f);
            btnRect.anchorMax = new Vector2(0, 0.5f);
            btnRect.pivot = new Vector2(0, 0.5f);
            btnRect.anchoredPosition = new Vector2(xPos, 0);
            btnRect.sizeDelta = new Vector2(85f, 36f);

            // Text
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(btnObj.transform);

            TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = 16;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = Color.white;
            tmp.alignment = TextAlignmentOptions.Center;

            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            return btn;
        }

        /// <summary>
        /// Harita modunu değiştir
        /// </summary>
        public void SetMapMode(MapMode mode)
        {
            if (CurrentMapMode == mode) return;

            CurrentMapMode = mode;
            UpdateMapToggleButtons();
            OnMapModeChanged?.Invoke(mode);

            Debug.Log($"MiniMap: Harita modu değişti -> {mode}");

            // TODO: Şehir haritası implementasyonu
            if (mode == MapMode.City)
            {
                Debug.Log("Şehir haritasına geçiş yapılıyor... (Henüz implemente edilmedi)");
            }
            else
            {
                Debug.Log("Dünya haritasına geçiş yapılıyor...");
            }
        }

        private void UpdateMapToggleButtons()
        {
            if (worldMapButton != null)
            {
                var img = worldMapButton.GetComponent<Image>();
                img.color = CurrentMapMode == MapMode.World
                    ? new Color(0.3f, 0.6f, 0.3f, 1f)
                    : new Color(0.2f, 0.2f, 0.25f, 1f);
            }

            if (cityMapButton != null)
            {
                var img = cityMapButton.GetComponent<Image>();
                img.color = CurrentMapMode == MapMode.City
                    ? new Color(0.3f, 0.5f, 0.7f, 1f)
                    : new Color(0.2f, 0.2f, 0.25f, 1f);
            }
        }

        private void CreateZoomButtons()
        {
            // Zoom butonları kaldırıldı - minimap artık sadece görüntüleme amaçlı
        }

        private GameObject CreateStyledButton(string name, string text, Vector2 anchor, Vector2 position, Vector2 size, Color bgColor)
        {
            GameObject btnObj = new GameObject(name);
            btnObj.transform.SetParent(transform);

            // Arkaplan - yuvarlatilmis gorunum icin
            Image bg = btnObj.AddComponent<Image>();
            bg.color = bgColor;

            // Outline efekti
            Outline outline = btnObj.AddComponent<Outline>();
            outline.effectColor = new Color(1f, 1f, 1f, 0.3f);
            outline.effectDistance = new Vector2(1, -1);

            Button btn = btnObj.AddComponent<Button>();
            ColorBlock colors = btn.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(1.2f, 1.2f, 1.2f, 1f);
            colors.pressedColor = new Color(0.8f, 0.8f, 0.8f, 1f);
            btn.colors = colors;

            RectTransform rect = btnObj.GetComponent<RectTransform>();
            rect.anchorMin = anchor;
            rect.anchorMax = anchor;
            rect.pivot = new Vector2(1, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;

            // Text
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(btnObj.transform);
            TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = 22;
            tmp.fontStyle = FontStyles.Bold;
            tmp.color = Color.white;
            tmp.alignment = TextAlignmentOptions.Center;

            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            return btnObj;
        }

        private void CreateCoordinateSearch()
        {
            // Canvas'i bul - koordinat arama ekranin ALT ORTASINDA olacak
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas == null) canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null) return;

            // ═══════════════════════════════════════════════════════════
            // YATAY ARAMA CUBUGU - Ekranin alt ortasinda
            // Layout: [X: [____]] [Y: [____]] [GIT]
            // ═══════════════════════════════════════════════════════════
            coordSearchPanel = new GameObject("CoordSearchBar");
            coordSearchPanel.transform.SetParent(canvas.transform);

            Image panelBg = coordSearchPanel.AddComponent<Image>();
            panelBg.color = new Color(0.08f, 0.08f, 0.12f, 0.92f);
            panelBg.raycastTarget = true;

            Outline panelOutline = coordSearchPanel.AddComponent<Outline>();
            panelOutline.effectColor = new Color(0.5f, 0.45f, 0.3f, 0.8f);
            panelOutline.effectDistance = new Vector2(1.5f, -1.5f);

            RectTransform panelRect = coordSearchPanel.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.5f, 1); // UST orta
            panelRect.anchorMax = new Vector2(0.5f, 1);
            panelRect.pivot = new Vector2(0.5f, 1);
            panelRect.sizeDelta = new Vector2(340f, 50f); // Yatay cubuk
            panelRect.anchoredPosition = new Vector2(0, -15f); // Ekranin 15px asagisinda (ustten)

            // Yatay layout icin pozisyonlar
            float currentX = 12f;

            // X Label
            CreateInlineLabel(coordSearchPanel.transform, "X:", currentX, 24f);
            currentX += 28f;

            // X Input
            inputX = CreateInlineInput(coordSearchPanel.transform, "InputX", currentX, 80f);
            currentX += 88f;

            // Y Label
            CreateInlineLabel(coordSearchPanel.transform, "Y:", currentX, 24f);
            currentX += 28f;

            // Y Input
            inputY = CreateInlineInput(coordSearchPanel.transform, "InputY", currentX, 80f);
            currentX += 92f;

            // GIT Butonu
            GameObject goBtnObj = new GameObject("GoButton");
            goBtnObj.transform.SetParent(coordSearchPanel.transform);

            Image goBtnBg = goBtnObj.AddComponent<Image>();
            goBtnBg.color = new Color(0.2f, 0.55f, 0.25f, 1f);
            goBtnBg.raycastTarget = true;

            goButton = goBtnObj.AddComponent<Button>();
            goButton.targetGraphic = goBtnBg;
            goButton.onClick.AddListener(OnGoButtonClicked);

            ColorBlock btnColors = goButton.colors;
            btnColors.normalColor = Color.white;
            btnColors.highlightedColor = new Color(1.15f, 1.25f, 1.15f, 1f);
            btnColors.pressedColor = new Color(0.7f, 0.9f, 0.7f, 1f);
            goButton.colors = btnColors;

            RectTransform goBtnRect = goBtnObj.GetComponent<RectTransform>();
            goBtnRect.anchorMin = new Vector2(0, 0.5f);
            goBtnRect.anchorMax = new Vector2(0, 0.5f);
            goBtnRect.pivot = new Vector2(0, 0.5f);
            goBtnRect.anchoredPosition = new Vector2(currentX, 0);
            goBtnRect.sizeDelta = new Vector2(70f, 36f);

            // GIT text
            GameObject goTextObj = new GameObject("Text");
            goTextObj.transform.SetParent(goBtnObj.transform);
            TextMeshProUGUI goText = goTextObj.AddComponent<TextMeshProUGUI>();
            goText.text = "GIT";
            goText.fontSize = 18;
            goText.fontStyle = FontStyles.Bold;
            goText.color = Color.white;
            goText.alignment = TextAlignmentOptions.Center;

            RectTransform goTextRect = goTextObj.GetComponent<RectTransform>();
            goTextRect.anchorMin = Vector2.zero;
            goTextRect.anchorMax = Vector2.one;
            goTextRect.offsetMin = Vector2.zero;
            goTextRect.offsetMax = Vector2.zero;

            // ═══════════════════════════════════════════════════════════
            // MEVCUT KONUM - Sol alt kosede AYRI
            // ═══════════════════════════════════════════════════════════
            CreateCurrentLocationDisplay(canvas.transform);

            Debug.Log("MiniMap: Yatay koordinat arama cubugu olusturuldu");
        }

        private void CreateInlineLabel(Transform parent, string text, float xPos, float width)
        {
            GameObject labelObj = new GameObject($"Label_{text}");
            labelObj.transform.SetParent(parent);

            TextMeshProUGUI label = labelObj.AddComponent<TextMeshProUGUI>();
            label.text = text;
            label.fontSize = 20;
            label.fontStyle = FontStyles.Bold;
            label.color = new Color(0.85f, 0.85f, 0.85f, 1f);
            label.alignment = TextAlignmentOptions.Left;

            RectTransform rect = labelObj.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0.5f);
            rect.anchorMax = new Vector2(0, 0.5f);
            rect.pivot = new Vector2(0, 0.5f);
            rect.anchoredPosition = new Vector2(xPos, 0);
            rect.sizeDelta = new Vector2(width, 30f);
        }

        private TMP_InputField CreateInlineInput(Transform parent, string name, float xPos, float width)
        {
            GameObject inputObj = new GameObject(name);
            inputObj.transform.SetParent(parent);

            Image inputBg = inputObj.AddComponent<Image>();
            inputBg.color = new Color(0.15f, 0.15f, 0.2f, 1f);
            inputBg.raycastTarget = true;

            RectTransform inputRect = inputObj.GetComponent<RectTransform>();
            inputRect.anchorMin = new Vector2(0, 0.5f);
            inputRect.anchorMax = new Vector2(0, 0.5f);
            inputRect.pivot = new Vector2(0, 0.5f);
            inputRect.anchoredPosition = new Vector2(xPos, 0);
            inputRect.sizeDelta = new Vector2(width, 36f);

            // Text Area
            GameObject textAreaObj = new GameObject("Text Area");
            textAreaObj.transform.SetParent(inputObj.transform);
            RectTransform textAreaRect = textAreaObj.AddComponent<RectTransform>();
            textAreaRect.anchorMin = Vector2.zero;
            textAreaRect.anchorMax = Vector2.one;
            textAreaRect.offsetMin = new Vector2(6f, 4f);
            textAreaRect.offsetMax = new Vector2(-6f, -4f);
            textAreaObj.AddComponent<RectMask2D>();

            // Input Text
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(textAreaObj.transform);
            TextMeshProUGUI inputText = textObj.AddComponent<TextMeshProUGUI>();
            inputText.fontSize = 18;
            inputText.color = Color.white;
            inputText.alignment = TextAlignmentOptions.Center;
            inputText.textWrappingMode = TextWrappingModes.NoWrap;
            inputText.overflowMode = TextOverflowModes.Overflow;

            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            // Placeholder
            GameObject placeholderObj = new GameObject("Placeholder");
            placeholderObj.transform.SetParent(textAreaObj.transform);
            TextMeshProUGUI placeholder = placeholderObj.AddComponent<TextMeshProUGUI>();
            placeholder.text = "0";
            placeholder.fontSize = 18;
            placeholder.color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
            placeholder.alignment = TextAlignmentOptions.Center;
            placeholder.textWrappingMode = TextWrappingModes.NoWrap;

            RectTransform placeholderRect = placeholderObj.GetComponent<RectTransform>();
            placeholderRect.anchorMin = Vector2.zero;
            placeholderRect.anchorMax = Vector2.one;
            placeholderRect.offsetMin = Vector2.zero;
            placeholderRect.offsetMax = Vector2.zero;

            // TMP_InputField
            TMP_InputField inputField = inputObj.AddComponent<TMP_InputField>();
            inputField.textViewport = textAreaRect;
            inputField.textComponent = inputText;
            inputField.placeholder = placeholder;
            inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
            inputField.characterLimit = 5;
            inputField.text = "";
            inputField.targetGraphic = inputBg;
            inputField.interactable = true;
            inputField.caretBlinkRate = 0.85f;
            inputField.caretWidth = 2;
            inputField.customCaretColor = true;
            inputField.caretColor = new Color(1f, 0.9f, 0.5f, 1f);
            inputField.selectionColor = new Color(0.3f, 0.5f, 0.8f, 0.4f);

            return inputField;
        }

        private void CreateCurrentLocationDisplay(Transform canvasTransform)
        {
            // Sol UST kosede konum gostergesi
            GameObject locationPanel = new GameObject("CurrentLocationPanel");
            locationPanel.transform.SetParent(canvasTransform);

            Image panelBg = locationPanel.AddComponent<Image>();
            panelBg.color = new Color(0.05f, 0.05f, 0.08f, 0.85f);
            panelBg.raycastTarget = false;

            RectTransform panelRect = locationPanel.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0, 1); // Sol UST
            panelRect.anchorMax = new Vector2(0, 1);
            panelRect.pivot = new Vector2(0, 1);
            panelRect.anchoredPosition = new Vector2(15f, -15f);
            panelRect.sizeDelta = new Vector2(180f, 45f);

            // Konum text
            GameObject textObj = new GameObject("LocationText");
            textObj.transform.SetParent(locationPanel.transform);

            currentCoordText = textObj.AddComponent<TextMeshProUGUI>();
            currentCoordText.text = "Konum: (0, 0)";
            currentCoordText.fontSize = 20;
            currentCoordText.fontStyle = FontStyles.Bold;
            currentCoordText.color = new Color(1f, 0.92f, 0.5f, 1f); // Altin rengi
            currentCoordText.alignment = TextAlignmentOptions.Center;

            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = new Vector2(8f, 5f);
            textRect.offsetMax = new Vector2(-8f, -5f);
        }

        private void OnGoButtonClicked()
        {
            Debug.Log("MiniMap: Git butonuna basildi");

            if (MapCameraController.Instance == null)
            {
                Debug.LogError("MiniMap: MapCameraController.Instance bulunamadi!");
                return;
            }

            int x = 0, y = 0;

            // InputX kontrolu
            if (inputX != null)
            {
                string textX = inputX.text;
                Debug.Log($"MiniMap: inputX.text = '{textX}'");
                if (!string.IsNullOrEmpty(textX))
                {
                    if (!int.TryParse(textX, out x))
                    {
                        Debug.LogWarning($"MiniMap: X degeri parse edilemedi: '{textX}'");
                    }
                }
            }
            else
            {
                Debug.LogError("MiniMap: inputX null!");
            }

            // InputY kontrolu
            if (inputY != null)
            {
                string textY = inputY.text;
                Debug.Log($"MiniMap: inputY.text = '{textY}'");
                if (!string.IsNullOrEmpty(textY))
                {
                    if (!int.TryParse(textY, out y))
                    {
                        Debug.LogWarning($"MiniMap: Y degeri parse edilemedi: '{textY}'");
                    }
                }
            }
            else
            {
                Debug.LogError("MiniMap: inputY null!");
            }

            Debug.Log($"MiniMap: Koordinata gidiliyor X={x}, Y={y}");
            MapCameraController.Instance.GoToCoordinate(x, y);
        }

        private void UpdateCurrentCoordinateDisplay()
        {
            if (currentCoordText == null || MapCameraController.Instance == null) return;

            Vector2Int coord = MapCameraController.Instance.GetCurrentCoordinate();
            currentCoordText.text = $"Konum: ({coord.x}, {coord.y})";
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

                // Dinamik minimap zoom - ana kamera zoom'una bagla
                // Ana kamera yakin zoom = minimap genis gorunum (daha fazla alan goster)
                // Ana kamera uzak zoom = minimap dar gorunum (daha az alan goster)
                float mainZoom = MapCameraController.Instance.GetCurrentZoom();
                float mainMinZoom = GameConfig.MinZoom;
                float mainMaxZoom = GameConfig.MaxZoom;

                // Ana kamera zoom'unu 0-1 arasina normalize et
                float normalizedZoom = Mathf.InverseLerp(mainMinZoom, mainMaxZoom, mainZoom);

                // Minimap zoom'unu hesapla (ters orantili)
                // Ana kamera yakin (normalizedZoom=0) -> minimap genis (maxZoom)
                // Ana kamera uzak (normalizedZoom=1) -> minimap dar (minZoom)
                targetZoom = Mathf.Lerp(maxZoom, minZoom, normalizedZoom);
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

            Vector3 worldPos;

            // Terrain texture modunda tum harita gorunur
            if (useTerrainTexture)
            {
                // Normalized pozisyonu (0-1) world pozisyonuna cevir
                float worldX = (normalized.x + 0.5f) * GameConfig.WorldWidth;
                float worldZ = (normalized.y + 0.5f) * GameConfig.WorldHeight;

                worldPos = new Vector3(worldX, 0f, worldZ);
            }
            else
            {
                // Minimap kamera pozisyonuna gore world pozisyonu hesapla
                Vector3 miniCamPos = miniMapCamera.transform.position;
                float currentOrthoSize = miniMapCamera.orthographicSize;

                worldPos = new Vector3(
                    miniCamPos.x + normalized.x * currentOrthoSize * 2f,
                    0f,
                    miniCamPos.z + normalized.y * currentOrthoSize * 2f
                );
            }

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
