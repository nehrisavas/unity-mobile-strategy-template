using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using EmpireWars.Core;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace EmpireWars.CameraSystem
{
    /// <summary>
    /// Harita kamera kontrolu - Rise of Kingdoms / Mafia City tarzi
    /// World plane raycasting ile profesyonel drag sistemi
    /// Kaynak: https://onewheelstudio.com/blog/2022/1/14/strategy-game-camera-unitys-new-input-system
    /// </summary>
    public class MapCameraController : MonoBehaviour
    {
        public static MapCameraController Instance { get; private set; }

        [Header("Kamera Referansi")]
        [SerializeField] private Camera targetCamera;

        [Header("Hareket Ayarlari")]
        [SerializeField] private float panSmoothing = 10f;
        [SerializeField] private float keyboardPanSpeed = 20f;

        [Header("Zoom Ayarlari")]
        [SerializeField] private float zoomSpeed = 5f;    // Her scroll icin 5 birim zoom
        [SerializeField] private float minZoom = 3f;      // Daha yakin zoom
        [SerializeField] private float maxZoom = 60f;     // Daha uzak zoom
        [SerializeField] private float zoomSmoothing = 20f; // Hizli gecis
        [SerializeField] private float pinchZoomSpeed = 0.15f;

        [Header("Sinirlar")]
        [SerializeField] private bool useBounds = true;
        [SerializeField] private Vector2 mapSize = new Vector2(100f, 100f);

        [Header("Drag Ayarlari")]
        [SerializeField] private float dragThreshold = 5f;

        // State
        private Vector3 targetPosition;
        private float targetZoom;
        private bool isDragging = false;
        private float totalDragDistance = 0f;
        private bool rightButtonDown = false;
        private Vector2 dragStartScreenPos;

        // World plane drag
        private Plane groundPlane;
        private Vector3 dragStartWorldPos;

        // Pinch state
        private float lastPinchDistance;

        // Input actions
        private InputAction pointerPositionAction;
        private InputAction rightClickAction;
        private InputAction scrollAction;
        private InputAction moveAction;

        public bool IsDragging => isDragging;

        #region Unity Lifecycle

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            // XZ duzleminde ground plane olustur (Y=0)
            groundPlane = new Plane(Vector3.up, Vector3.zero);

            SetupInputActions();
        }

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            EnableInputActions();
        }

        private void OnDisable()
        {
            DisableInputActions();
            EnhancedTouchSupport.Disable();
        }

        private void Start()
        {
            if (targetCamera == null)
            {
                targetCamera = GetComponent<Camera>();
                if (targetCamera == null)
                    targetCamera = Camera.main;
            }

            targetPosition = transform.position;
            targetZoom = targetCamera != null && targetCamera.orthographic
                ? targetCamera.orthographicSize
                : transform.position.y;
        }

        private void Update()
        {
            HandleTouchInput();
            HandleKeyboardInput();
            HandleMouseScroll();
            ApplyMovement();
            ClampToBounds();
        }

        #endregion

        #region Input Setup

        private void SetupInputActions()
        {
            pointerPositionAction = new InputAction("PointerPosition", binding: "<Pointer>/position");
            rightClickAction = new InputAction("RightClick", binding: "<Mouse>/rightButton");
            scrollAction = new InputAction("Scroll", binding: "<Mouse>/scroll/y");
            moveAction = new InputAction("Move", binding: "<Gamepad>/leftStick");

            // WASD bindings
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");

            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow")
                .With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Right", "<Keyboard>/rightArrow");
        }

        private void EnableInputActions()
        {
            pointerPositionAction?.Enable();
            rightClickAction?.Enable();
            scrollAction?.Enable();
            moveAction?.Enable();

            if (rightClickAction != null)
            {
                rightClickAction.started += OnRightButtonDown;
                rightClickAction.canceled += OnRightButtonUp;
            }
        }

        private void DisableInputActions()
        {
            if (rightClickAction != null)
            {
                rightClickAction.started -= OnRightButtonDown;
                rightClickAction.canceled -= OnRightButtonUp;
            }

            pointerPositionAction?.Disable();
            rightClickAction?.Disable();
            scrollAction?.Disable();
            moveAction?.Disable();
        }

        #endregion

        #region Input Handlers

        private void OnRightButtonDown(InputAction.CallbackContext ctx)
        {
            rightButtonDown = true;
            Vector2 screenPos = pointerPositionAction.ReadValue<Vector2>();
            dragStartScreenPos = screenPos;
            totalDragDistance = 0f;
            isDragging = false;

            // Baslangic world pozisyonunu kaydet
            dragStartWorldPos = GetWorldPositionOnPlane(screenPos);
        }

        private void OnRightButtonUp(InputAction.CallbackContext ctx)
        {
            rightButtonDown = false;
            isDragging = false;
        }

        /// <summary>
        /// Ekran pozisyonundan world pozisyonunu hesapla (XZ plane uzerinde)
        /// </summary>
        private Vector3 GetWorldPositionOnPlane(Vector2 screenPos)
        {
            if (targetCamera == null) return Vector3.zero;

            Ray ray = targetCamera.ScreenPointToRay(screenPos);
            if (groundPlane.Raycast(ray, out float distance))
            {
                return ray.GetPoint(distance);
            }
            return Vector3.zero;
        }

        private void HandleTouchInput()
        {
            // Multi-touch (pinch zoom)
            if (Touch.activeTouches.Count >= 2)
            {
                HandlePinchZoom();
                isDragging = true;
                return;
            }

            // Sag tik surukleme - World plane raycasting
            if (rightButtonDown)
            {
                Vector2 currentScreenPos = pointerPositionAction.ReadValue<Vector2>();
                totalDragDistance += Vector2.Distance(currentScreenPos, dragStartScreenPos);

                if (totalDragDistance > dragThreshold)
                {
                    isDragging = true;

                    // Mevcut world pozisyonunu hesapla
                    Vector3 currentWorldPos = GetWorldPositionOnPlane(currentScreenPos);

                    // Fark vektorunu hesapla ve kamerayi tasi
                    Vector3 diff = dragStartWorldPos - currentWorldPos;
                    targetPosition += diff;

                    // Yeni baslangic noktasini guncelle (smooth hareket icin)
                    dragStartWorldPos = GetWorldPositionOnPlane(currentScreenPos);
                }
            }
            else
            {
                isDragging = false;
            }
        }

        private void HandlePinchZoom()
        {
            if (Touch.activeTouches.Count < 2) return;

            var touch0 = Touch.activeTouches[0];
            var touch1 = Touch.activeTouches[1];

            float currentDistance = Vector2.Distance(touch0.screenPosition, touch1.screenPosition);

            if (touch1.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                lastPinchDistance = currentDistance;
            }
            else
            {
                float deltaPinch = currentDistance - lastPinchDistance;
                targetZoom -= deltaPinch * pinchZoomSpeed;
                targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
                lastPinchDistance = currentDistance;

                // Pinch sirasinda pan
                Vector2 center = (touch0.screenPosition + touch1.screenPosition) / 2f;
                Vector3 worldCenter = GetWorldPositionOnPlane(center);

                Vector2 lastCenter = ((touch0.screenPosition - touch0.delta) + (touch1.screenPosition - touch1.delta)) / 2f;
                Vector3 lastWorldCenter = GetWorldPositionOnPlane(lastCenter);

                targetPosition += lastWorldCenter - worldCenter;
            }
        }

        private void HandleKeyboardInput()
        {
            Vector2 move = moveAction.ReadValue<Vector2>();
            if (move.sqrMagnitude > 0.01f)
            {
                float zoomFactor = targetZoom / maxZoom;
                float speed = keyboardPanSpeed * (0.5f + zoomFactor);
                targetPosition += new Vector3(move.x, 0, move.y) * speed * Time.deltaTime;
            }
        }

        private void HandleMouseScroll()
        {
            float scroll = scrollAction.ReadValue<float>();
            if (Mathf.Abs(scroll) > 0.1f)
            {
                // Scroll degeri anlik - her notch icin zoomSpeed kadar zoom
                // scroll > 0 = yukari = zoom in (kucult), scroll < 0 = asagi = zoom out (buyut)
                targetZoom -= Mathf.Sign(scroll) * zoomSpeed;
                targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
            }
        }

        #endregion

        #region Movement

        private void ApplyMovement()
        {
            // Position - smooth lerp
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * panSmoothing);

            // Zoom
            if (targetCamera != null)
            {
                if (targetCamera.orthographic)
                {
                    targetCamera.orthographicSize = Mathf.Lerp(
                        targetCamera.orthographicSize,
                        targetZoom,
                        Time.deltaTime * zoomSmoothing
                    );
                }
                else
                {
                    Vector3 pos = transform.position;
                    pos.y = Mathf.Lerp(pos.y, targetZoom, Time.deltaTime * zoomSmoothing);
                    transform.position = pos;
                    targetPosition.y = pos.y;
                }
            }
        }

        private void ClampToBounds()
        {
            if (!useBounds) return;

            float halfWidth = mapSize.x / 2f;
            float halfHeight = mapSize.y / 2f;

            targetPosition.x = Mathf.Clamp(targetPosition.x, -halfWidth, halfWidth);
            targetPosition.z = Mathf.Clamp(targetPosition.z, -halfHeight, halfHeight);

            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -halfWidth, halfWidth);
            pos.z = Mathf.Clamp(pos.z, -halfHeight, halfHeight);
            transform.position = pos;
        }

        #endregion

        #region Public Methods

        public void FocusOnPosition(Vector3 worldPosition)
        {
            targetPosition = new Vector3(worldPosition.x, transform.position.y, worldPosition.z);
        }

        public void FocusOnCell(HexCoordinates coords)
        {
            Vector3 pos = coords.ToWorldPosition();
            FocusOnPosition(pos);
        }

        public void SetZoom(float zoom)
        {
            targetZoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        }

        public float GetCurrentZoom()
        {
            return targetCamera != null && targetCamera.orthographic
                ? targetCamera.orthographicSize
                : transform.position.y;
        }

        #endregion
    }
}
