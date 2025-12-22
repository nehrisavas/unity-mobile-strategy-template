using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EmpireWars.Core;
using EmpireWars.Map;
using EmpireWars.CameraSystem;

namespace EmpireWars.UI
{
    /// <summary>
    /// Mini harita kontrolcusu
    /// Haritanin kucuk bir onizlemesini gosterir
    /// Dokuman referansi: docs/01-game-design/map/MAP-SYSTEM.md
    /// </summary>
    public class MiniMapController : MonoBehaviour, IPointerClickHandler, IDragHandler
    {
        public static MiniMapController Instance { get; private set; }

        [Header("Render Ayarlari")]
        [SerializeField] private RenderTexture miniMapTexture;
        [SerializeField] private UnityEngine.Camera miniMapCamera;
        [SerializeField] private RawImage miniMapImage;
        [SerializeField] private int textureSize = 256;

        [Header("Gorus Alani")]
        [SerializeField] private RectTransform viewportIndicator;
        [SerializeField] private Color viewportColor = new Color(1, 1, 1, 0.5f);

        [Header("Isaretler")]
        [SerializeField] private GameObject playerMarkerPrefab;
        [SerializeField] private GameObject allyMarkerPrefab;
        [SerializeField] private GameObject enemyMarkerPrefab;
        [SerializeField] private GameObject resourceMarkerPrefab;
        [SerializeField] private Transform markersContainer;

        [Header("Zoom")]
        [SerializeField] private float minZoom = 100f;
        [SerializeField] private float maxZoom = 1000f;
        [SerializeField] private float currentZoom = 500f;
        [SerializeField] private float zoomSpeed = 50f;

        [Header("Guncelleme")]
        [SerializeField] private float updateInterval = 0.5f;

        // Internal
        private float lastUpdateTime;
        private RectTransform rectTransform;
        private Vector2 mapSize;

        #region Unity Lifecycle

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            InitializeMiniMap();
        }

        private void Update()
        {
            // Periyodik guncelleme
            if (Time.time - lastUpdateTime >= updateInterval)
            {
                UpdateMiniMap();
                lastUpdateTime = Time.time;
            }

            // Viewport guncelle
            UpdateViewportIndicator();

            // Mouse tekerlek zoom
            if (IsMouseOverMiniMap())
            {
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0)
                {
                    ZoomMiniMap(-scroll * zoomSpeed);
                }
            }
        }

        #endregion

        #region Initialization

        private void InitializeMiniMap()
        {
            // Harita boyutu
            mapSize = new Vector2(
                HexMetrics.MapWidth * HexMetrics.InnerRadius * 2f,
                HexMetrics.MapHeight * HexMetrics.OuterRadius * 1.5f
            );

            // RenderTexture olustur
            if (miniMapTexture == null)
            {
                miniMapTexture = new RenderTexture(textureSize, textureSize, 16);
                miniMapTexture.Create();
            }

            // Mini harita kamerasini ayarla
            if (miniMapCamera == null)
            {
                CreateMiniMapCamera();
            }
            else
            {
                ConfigureMiniMapCamera();
            }

            // RawImage'a texture ata
            if (miniMapImage != null)
            {
                miniMapImage.texture = miniMapTexture;
            }
        }

        private void CreateMiniMapCamera()
        {
            GameObject camObj = new GameObject("MiniMapCamera");
            camObj.transform.SetParent(transform);

            miniMapCamera = camObj.AddComponent<UnityEngine.Camera>();
            ConfigureMiniMapCamera();
        }

        private void ConfigureMiniMapCamera()
        {
            miniMapCamera.targetTexture = miniMapTexture;
            miniMapCamera.orthographic = true;
            miniMapCamera.orthographicSize = currentZoom;
            miniMapCamera.clearFlags = CameraClearFlags.SolidColor;
            miniMapCamera.backgroundColor = new Color(0.1f, 0.15f, 0.2f, 1f);
            miniMapCamera.cullingMask = LayerMask.GetMask("MiniMap", "Terrain", "Default");
            miniMapCamera.depth = -10;

            // Yukari bak
            miniMapCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }

        #endregion

        #region Update

        private void UpdateMiniMap()
        {
            if (miniMapCamera == null) return;

            // Kamera pozisyonunu guncelle (merkeze veya oyuncuya odakla)
            Vector3 cameraPos = miniMapCamera.transform.position;

            if (MapCameraController.Instance != null)
            {
                Vector3 mainCamPos = MapCameraController.Instance.transform.position;
                cameraPos.x = mainCamPos.x;
                cameraPos.z = mainCamPos.z;
            }

            cameraPos.y = 500f; // Yuksekten bak
            miniMapCamera.transform.position = cameraPos;
            miniMapCamera.orthographicSize = currentZoom;

            // Markerlari guncelle
            UpdateMarkers();
        }

        private void UpdateViewportIndicator()
        {
            if (viewportIndicator == null || MapCameraController.Instance == null) return;

            // Ana kameranin goruntuledigi alani hesapla
            UnityEngine.Camera mainCam = UnityEngine.Camera.main;
            if (mainCam == null) return;

            float mainCamSize = mainCam.orthographicSize;
            float aspect = mainCam.aspect;

            // Mini harita uzerinde viewport boyutu
            float viewWidth = (mainCamSize * 2f * aspect) / (currentZoom * 2f);
            float viewHeight = (mainCamSize * 2f) / (currentZoom * 2f);

            Vector2 miniMapSize = rectTransform.rect.size;
            viewportIndicator.sizeDelta = new Vector2(
                miniMapSize.x * viewWidth,
                miniMapSize.y * viewHeight
            );

            // Viewport pozisyonu
            Vector3 mainCamPos = MapCameraController.Instance.transform.position;
            Vector3 miniCamPos = miniMapCamera.transform.position;

            float offsetX = (mainCamPos.x - miniCamPos.x) / (currentZoom * 2f);
            float offsetY = (mainCamPos.z - miniCamPos.z) / (currentZoom * 2f);

            viewportIndicator.anchoredPosition = new Vector2(
                offsetX * miniMapSize.x,
                offsetY * miniMapSize.y
            );
        }

        private void UpdateMarkers()
        {
            // TODO: Oyuncu, muttefik ve dusman markerlarini guncelle
            // Bu islem backend'den gelen veriyle yapilacak
        }

        #endregion

        #region Interaction

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                MoveToClickedPosition(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveToClickedPosition(eventData);
        }

        private void MoveToClickedPosition(PointerEventData eventData)
        {
            if (MapCameraController.Instance == null) return;

            // Tiklanilan pozisyonu dunya koordinatina cevir
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint
            );

            // Normalize et (-0.5 to 0.5)
            Vector2 normalized = new Vector2(
                localPoint.x / rectTransform.rect.width,
                localPoint.y / rectTransform.rect.height
            );

            // Dunya pozisyonuna cevir
            Vector3 miniCamPos = miniMapCamera.transform.position;
            Vector3 worldPos = new Vector3(
                miniCamPos.x + normalized.x * currentZoom * 2f,
                0f,
                miniCamPos.z + normalized.y * currentZoom * 2f
            );

            // Ana kamerayi hareket ettir
            MapCameraController.Instance.FocusOnPosition(worldPos);
        }

        private bool IsMouseOverMiniMap()
        {
            return RectTransformUtility.RectangleContainsScreenPoint(
                rectTransform,
                Input.mousePosition,
                null
            );
        }

        #endregion

        #region Zoom

        public void ZoomMiniMap(float delta)
        {
            currentZoom = Mathf.Clamp(currentZoom + delta, minZoom, maxZoom);
            if (miniMapCamera != null)
            {
                miniMapCamera.orthographicSize = currentZoom;
            }
        }

        public void ZoomIn()
        {
            ZoomMiniMap(-zoomSpeed);
        }

        public void ZoomOut()
        {
            ZoomMiniMap(zoomSpeed);
        }

        public void ResetZoom()
        {
            currentZoom = (minZoom + maxZoom) / 2f;
            if (miniMapCamera != null)
            {
                miniMapCamera.orthographicSize = currentZoom;
            }
        }

        #endregion

        #region Markers

        public void AddPlayerMarker(long playerId, Vector3 position, bool isAlly)
        {
            if (markersContainer == null) return;

            GameObject prefab = isAlly ? allyMarkerPrefab : enemyMarkerPrefab;
            if (prefab == null) return;

            GameObject marker = Instantiate(prefab, markersContainer);
            MiniMapMarker markerComponent = marker.GetComponent<MiniMapMarker>();
            if (markerComponent == null)
            {
                markerComponent = marker.AddComponent<MiniMapMarker>();
            }

            markerComponent.Initialize(playerId, position, isAlly ? MarkerType.Ally : MarkerType.Enemy);
        }

        public void AddResourceMarker(Vector3 position, ResourceType resourceType)
        {
            if (markersContainer == null || resourceMarkerPrefab == null) return;

            GameObject marker = Instantiate(resourceMarkerPrefab, markersContainer);
            MiniMapMarker markerComponent = marker.GetComponent<MiniMapMarker>();
            if (markerComponent == null)
            {
                markerComponent = marker.AddComponent<MiniMapMarker>();
            }

            markerComponent.Initialize(0, position, MarkerType.Resource);
        }

        public void ClearAllMarkers()
        {
            if (markersContainer == null) return;

            foreach (Transform child in markersContainer)
            {
                Destroy(child.gameObject);
            }
        }

        #endregion

        #region Utility

        public Vector3 WorldToMiniMapPosition(Vector3 worldPos)
        {
            if (miniMapCamera == null) return Vector3.zero;

            Vector3 miniCamPos = miniMapCamera.transform.position;
            Vector2 offset = new Vector2(
                (worldPos.x - miniCamPos.x) / (currentZoom * 2f),
                (worldPos.z - miniCamPos.z) / (currentZoom * 2f)
            );

            Vector2 miniMapSize = rectTransform.rect.size;
            return new Vector3(
                offset.x * miniMapSize.x,
                offset.y * miniMapSize.y,
                0f
            );
        }

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
    /// Mini harita marker bileşeni
    /// </summary>
    public class MiniMapMarker : MonoBehaviour
    {
        public long OwnerId { get; private set; }
        public MarkerType Type { get; private set; }
        public Vector3 WorldPosition { get; private set; }

        public void Initialize(long ownerId, Vector3 worldPos, MarkerType type)
        {
            OwnerId = ownerId;
            WorldPosition = worldPos;
            Type = type;
        }

        public void UpdatePosition(Vector3 newWorldPos)
        {
            WorldPosition = newWorldPos;

            // Mini harita pozisyonunu guncelle
            if (MiniMapController.Instance != null)
            {
                Vector3 miniMapPos = MiniMapController.Instance.WorldToMiniMapPosition(newWorldPos);
                transform.localPosition = miniMapPos;
            }
        }
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
