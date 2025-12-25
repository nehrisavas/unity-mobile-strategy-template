using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using System;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace EmpireWars.InputSystem
{
    /// <summary>
    /// Touch Gesture Detector
    /// Long press, double tap, swipe algılama
    /// New Input System (EnhancedTouch) kullanır
    /// </summary>
    public class GestureDetector : MonoBehaviour
    {
        public static GestureDetector Instance { get; private set; }

        [Header("Long Press Ayarları")]
        [SerializeField] private float longPressThreshold = 0.5f;
        [SerializeField] private float longPressMoveThreshold = 20f;

        [Header("Double Tap Ayarları")]
        [SerializeField] private float doubleTapThreshold = 0.3f;
        [SerializeField] private float doubleTapDistanceThreshold = 50f;

        [Header("Swipe Ayarları")]
        [SerializeField] private float swipeMinDistance = 100f;
        [SerializeField] private float swipeMaxTime = 0.5f;

        [Header("Debug")]
        [SerializeField] private bool logGestures = false;

        // Long press state
        private bool isLongPressTracking;
        private Vector2 longPressStartPos;
        private float longPressStartTime;
        private bool longPressTriggered;

        // Double tap state
        private float lastTapTime;
        private Vector2 lastTapPosition;

        // Swipe state
        private Vector2 swipeStartPos;
        private float swipeStartTime;

        // Events
        public static event Action<Vector2> OnLongPress;
        public static event Action<Vector2> OnDoubleTap;
        public static event Action<Vector2> OnTap;
        public static event Action<SwipeDirection, Vector2, float> OnSwipe;

        public enum SwipeDirection
        {
            None,
            Up,
            Down,
            Left,
            Right
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
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
            ProcessTouches();
        }

        private void ProcessTouches()
        {
            var touches = Touch.activeTouches;

            if (touches.Count == 0)
            {
                // Dokunma bitti
                if (isLongPressTracking && !longPressTriggered)
                {
                    // Long press değildi, normal tap
                    ProcessTap(longPressStartPos);
                }
                ResetLongPress();
                return;
            }

            // Tek parmak dokunma
            if (touches.Count == 1)
            {
                var touch = touches[0];
                ProcessSingleTouch(touch);
            }
            else
            {
                // Çoklu dokunma - gesture'ları iptal et
                ResetLongPress();
            }
        }

        private void ProcessSingleTouch(Touch touch)
        {
            Vector2 position = touch.screenPosition;

            switch (touch.phase)
            {
                case UnityEngine.InputSystem.TouchPhase.Began:
                    StartGestureTracking(position);
                    break;

                case UnityEngine.InputSystem.TouchPhase.Moved:
                case UnityEngine.InputSystem.TouchPhase.Stationary:
                    UpdateGestureTracking(position);
                    break;

                case UnityEngine.InputSystem.TouchPhase.Ended:
                    EndGestureTracking(position);
                    break;

                case UnityEngine.InputSystem.TouchPhase.Canceled:
                    ResetLongPress();
                    break;
            }
        }

        private void StartGestureTracking(Vector2 position)
        {
            // Long press tracking başlat
            isLongPressTracking = true;
            longPressStartPos = position;
            longPressStartTime = Time.time;
            longPressTriggered = false;

            // Swipe tracking başlat
            swipeStartPos = position;
            swipeStartTime = Time.time;
        }

        private void UpdateGestureTracking(Vector2 position)
        {
            if (!isLongPressTracking) return;

            // Hareket kontrolü - çok hareket ettiyse long press iptal
            float moveDistance = Vector2.Distance(position, longPressStartPos);
            if (moveDistance > longPressMoveThreshold)
            {
                // Long press iptal, swipe olabilir
                isLongPressTracking = false;
                return;
            }

            // Long press kontrolü
            if (!longPressTriggered && Time.time - longPressStartTime >= longPressThreshold)
            {
                longPressTriggered = true;
                TriggerLongPress(position);
            }
        }

        private void EndGestureTracking(Vector2 position)
        {
            float touchDuration = Time.time - longPressStartTime;
            float moveDistance = Vector2.Distance(position, swipeStartPos);

            // Swipe kontrolü
            if (touchDuration <= swipeMaxTime && moveDistance >= swipeMinDistance)
            {
                SwipeDirection direction = GetSwipeDirection(swipeStartPos, position);
                TriggerSwipe(direction, position, moveDistance);
            }
            // Long press zaten tetiklendi, başka bir şey yapma
            else if (longPressTriggered)
            {
                // Long press bitti
            }
            // Normal tap
            else if (moveDistance < longPressMoveThreshold)
            {
                ProcessTap(position);
            }

            ResetLongPress();
        }

        private void ProcessTap(Vector2 position)
        {
            float timeSinceLastTap = Time.time - lastTapTime;
            float distanceFromLastTap = Vector2.Distance(position, lastTapPosition);

            // Double tap kontrolü
            if (timeSinceLastTap <= doubleTapThreshold &&
                distanceFromLastTap <= doubleTapDistanceThreshold)
            {
                TriggerDoubleTap(position);
                lastTapTime = 0; // Üçlü tap'i önle
            }
            else
            {
                TriggerTap(position);
                lastTapTime = Time.time;
                lastTapPosition = position;
            }
        }

        private SwipeDirection GetSwipeDirection(Vector2 start, Vector2 end)
        {
            Vector2 delta = end - start;
            float absX = Mathf.Abs(delta.x);
            float absY = Mathf.Abs(delta.y);

            if (absX > absY)
            {
                return delta.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
            }
            else
            {
                return delta.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            }
        }

        private void ResetLongPress()
        {
            isLongPressTracking = false;
            longPressTriggered = false;
        }

        #region Event Triggers

        private void TriggerLongPress(Vector2 position)
        {
            if (logGestures) Debug.Log($"GestureDetector: Long Press at {position}");
            OnLongPress?.Invoke(position);

            // Haptic feedback
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }

        private void TriggerDoubleTap(Vector2 position)
        {
            if (logGestures) Debug.Log($"GestureDetector: Double Tap at {position}");
            OnDoubleTap?.Invoke(position);
        }

        private void TriggerTap(Vector2 position)
        {
            if (logGestures) Debug.Log($"GestureDetector: Tap at {position}");
            OnTap?.Invoke(position);
        }

        private void TriggerSwipe(SwipeDirection direction, Vector2 endPosition, float distance)
        {
            if (logGestures) Debug.Log($"GestureDetector: Swipe {direction} ({distance:F0}px)");
            OnSwipe?.Invoke(direction, endPosition, distance);
        }

        #endregion

        #region Public API

        /// <summary>
        /// Long press eşik süresini ayarla
        /// </summary>
        public void SetLongPressThreshold(float seconds)
        {
            longPressThreshold = Mathf.Max(0.1f, seconds);
        }

        /// <summary>
        /// Double tap eşik süresini ayarla
        /// </summary>
        public void SetDoubleTapThreshold(float seconds)
        {
            doubleTapThreshold = Mathf.Max(0.1f, seconds);
        }

        /// <summary>
        /// Swipe minimum mesafesini ayarla
        /// </summary>
        public void SetSwipeMinDistance(float pixels)
        {
            swipeMinDistance = Mathf.Max(20f, pixels);
        }

        /// <summary>
        /// DPI'ya göre threshold'ları otomatik ayarla
        /// </summary>
        public void AutoAdjustForDPI()
        {
            float dpiScale = Screen.dpi / 160f; // Android mdpi baseline
            if (dpiScale <= 0) dpiScale = 1f;

            longPressMoveThreshold = 20f * dpiScale;
            doubleTapDistanceThreshold = 50f * dpiScale;
            swipeMinDistance = 100f * dpiScale;

            Debug.Log($"GestureDetector: Thresholds adjusted for DPI {Screen.dpi:F0} (scale: {dpiScale:F2})");
        }

        #endregion
    }
}
