using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using System;
using EmpireWars.Core;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace EmpireWars.InputSystem
{
    /// <summary>
    /// Unified Mobile Input Manager
    /// Tüm input işlemlerini merkezi olarak yönetir
    /// Desktop ve Mobile arasında soyutlama sağlar
    /// </summary>
    public class MobileInputManager : MonoBehaviour
    {
        public static MobileInputManager Instance { get; private set; }

        [Header("Input Ayarları")]
        [SerializeField] private bool enableGestures = true;
        [SerializeField] private bool enableVibration = true;
        [SerializeField] private float dragThreshold = 10f;

        [Header("Debug")]
        [SerializeField] private bool logInput = false;

        // Input state
        private Vector2 primaryPosition;
        private Vector2 primaryDelta;
        private bool isPrimaryDown;
        private bool isPrimaryDragging;
        private Vector2 dragStartPosition;

        // Pinch state
        private bool isPinching;
        private float pinchDistance;
        private float pinchDelta;
        private Vector2 pinchCenter;

        // Components
        private GestureDetector gestureDetector;

        // Events
        public static event Action<Vector2> OnPrimaryDown;
        public static event Action<Vector2> OnPrimaryUp;
        public static event Action<Vector2, Vector2> OnPrimaryDrag; // position, delta
        public static event Action<Vector2> OnPrimaryClick;

        public static event Action<float, Vector2> OnPinch; // delta, center
        public static event Action OnPinchStart;
        public static event Action OnPinchEnd;

        // Properties
        public Vector2 PrimaryPosition => primaryPosition;
        public Vector2 PrimaryDelta => primaryDelta;
        public bool IsPrimaryDown => isPrimaryDown;
        public bool IsDragging => isPrimaryDragging;
        public bool IsPinching => isPinching;
        public float PinchDelta => pinchDelta;
        public Vector2 PinchCenter => pinchCenter;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Initialize()
        {
            // GestureDetector oluştur
            if (enableGestures)
            {
                gestureDetector = gameObject.AddComponent<GestureDetector>();

                // DPI'ya göre ayarla
                if (ScreenManager.Instance != null)
                {
                    gestureDetector.AutoAdjustForDPI();
                }
            }

            // DPI'ya göre drag threshold ayarla
            if (Screen.dpi > 0)
            {
                dragThreshold = 10f * (Screen.dpi / 160f);
            }
        }

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }

        private void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }

        private void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            // Platform kontrolü - touch veya mouse
            if (IsTouchDevice())
            {
                ProcessTouchInput();
            }
            else
            {
                ProcessMouseInput();
            }
        }

        private bool IsTouchDevice()
        {
            return Touch.activeTouches.Count > 0 ||
                   Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        }

        #region Touch Input

        private void ProcessTouchInput()
        {
            var touches = Touch.activeTouches;

            if (touches.Count == 0)
            {
                HandleTouchEnd();
                return;
            }

            if (touches.Count == 1)
            {
                ProcessSingleTouch(touches[0]);
            }
            else if (touches.Count >= 2)
            {
                ProcessMultiTouch(touches[0], touches[1]);
            }
        }

        private void ProcessSingleTouch(Touch touch)
        {
            // Pinch bittiğinde
            if (isPinching)
            {
                isPinching = false;
                OnPinchEnd?.Invoke();
            }

            primaryPosition = touch.screenPosition;

            switch (touch.phase)
            {
                case UnityEngine.InputSystem.TouchPhase.Began:
                    HandlePrimaryDown(primaryPosition);
                    break;

                case UnityEngine.InputSystem.TouchPhase.Moved:
                    primaryDelta = touch.delta;
                    HandlePrimaryMove(primaryPosition, primaryDelta);
                    break;

                case UnityEngine.InputSystem.TouchPhase.Stationary:
                    primaryDelta = Vector2.zero;
                    break;

                case UnityEngine.InputSystem.TouchPhase.Ended:
                    HandlePrimaryUp(primaryPosition);
                    break;

                case UnityEngine.InputSystem.TouchPhase.Canceled:
                    HandleTouchEnd();
                    break;
            }
        }

        private void ProcessMultiTouch(Touch touch0, Touch touch1)
        {
            // Primary input iptal
            if (isPrimaryDown && !isPinching)
            {
                isPrimaryDown = false;
                isPrimaryDragging = false;
            }

            Vector2 pos0 = touch0.screenPosition;
            Vector2 pos1 = touch1.screenPosition;
            float currentDistance = Vector2.Distance(pos0, pos1);

            if (!isPinching)
            {
                // Pinch başladı
                isPinching = true;
                pinchDistance = currentDistance;
                pinchCenter = (pos0 + pos1) / 2f;
                OnPinchStart?.Invoke();

                if (logInput) Debug.Log("MobileInputManager: Pinch started");
            }
            else
            {
                // Pinch devam ediyor
                pinchDelta = currentDistance - pinchDistance;
                pinchDistance = currentDistance;
                pinchCenter = (pos0 + pos1) / 2f;

                OnPinch?.Invoke(pinchDelta, pinchCenter);
            }
        }

        private void HandleTouchEnd()
        {
            if (isPrimaryDown)
            {
                HandlePrimaryUp(primaryPosition);
            }

            if (isPinching)
            {
                isPinching = false;
                OnPinchEnd?.Invoke();
            }
        }

        #endregion

        #region Mouse Input

        private void ProcessMouseInput()
        {
            var mouse = Mouse.current;
            if (mouse == null) return;

            primaryPosition = mouse.position.ReadValue();

            // Sol tık
            if (mouse.leftButton.wasPressedThisFrame)
            {
                HandlePrimaryDown(primaryPosition);
            }
            else if (mouse.leftButton.wasReleasedThisFrame)
            {
                HandlePrimaryUp(primaryPosition);
            }
            else if (mouse.leftButton.isPressed)
            {
                primaryDelta = mouse.delta.ReadValue();
                HandlePrimaryMove(primaryPosition, primaryDelta);
            }

            // Scroll wheel -> Pinch simülasyonu
            float scrollDelta = mouse.scroll.ReadValue().y;
            if (Mathf.Abs(scrollDelta) > 0.01f)
            {
                if (!isPinching)
                {
                    isPinching = true;
                    OnPinchStart?.Invoke();
                }

                pinchDelta = scrollDelta * 10f; // Scale factor
                pinchCenter = primaryPosition;
                OnPinch?.Invoke(pinchDelta, pinchCenter);
            }
            else if (isPinching && !mouse.leftButton.isPressed)
            {
                isPinching = false;
                OnPinchEnd?.Invoke();
            }
        }

        #endregion

        #region Common Handlers

        private void HandlePrimaryDown(Vector2 position)
        {
            isPrimaryDown = true;
            isPrimaryDragging = false;
            dragStartPosition = position;
            primaryDelta = Vector2.zero;

            OnPrimaryDown?.Invoke(position);

            if (logInput) Debug.Log($"MobileInputManager: Primary down at {position}");
        }

        private void HandlePrimaryUp(Vector2 position)
        {
            bool wasDragging = isPrimaryDragging;
            isPrimaryDown = false;
            isPrimaryDragging = false;

            OnPrimaryUp?.Invoke(position);

            // Click kontrolü - drag değilse
            if (!wasDragging)
            {
                OnPrimaryClick?.Invoke(position);
                if (logInput) Debug.Log($"MobileInputManager: Click at {position}");
            }
        }

        private void HandlePrimaryMove(Vector2 position, Vector2 delta)
        {
            if (!isPrimaryDown) return;

            // Drag başladı mı?
            if (!isPrimaryDragging)
            {
                float moveDistance = Vector2.Distance(position, dragStartPosition);
                if (moveDistance >= dragThreshold)
                {
                    isPrimaryDragging = true;
                    if (logInput) Debug.Log("MobileInputManager: Drag started");
                }
            }

            if (isPrimaryDragging)
            {
                OnPrimaryDrag?.Invoke(position, delta);
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// Haptic feedback tetikle
        /// </summary>
        public void TriggerVibration()
        {
            if (!enableVibration) return;

#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }

        /// <summary>
        /// Ekran pozisyonunu world pozisyonuna çevir
        /// </summary>
        public Vector3 ScreenToWorldPosition(Vector2 screenPos, Camera cam = null)
        {
            if (cam == null) cam = Camera.main;
            if (cam == null) return Vector3.zero;

            // Orthographic kamera için
            if (cam.orthographic)
            {
                Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
                worldPos.y = 0;
                return worldPos;
            }

            // Perspective kamera için raycast
            Ray ray = cam.ScreenPointToRay(screenPos);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            if (groundPlane.Raycast(ray, out float distance))
            {
                return ray.GetPoint(distance);
            }

            return Vector3.zero;
        }

        /// <summary>
        /// UI üzerinde mi kontrol et
        /// </summary>
        public bool IsOverUI()
        {
            return UnityEngine.EventSystems.EventSystem.current != null &&
                   UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }

        /// <summary>
        /// Touch count
        /// </summary>
        public int TouchCount => Touch.activeTouches.Count;

        #endregion
    }
}
