using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

namespace EmpireWars.UI
{
    /// <summary>
    /// Mobil Alt Navigasyon Sistemi
    /// Rise of Kingdoms / Mafia City tarzi bottom bar
    /// 5 buton: Harita, Ittifak, Merkez FAB, Mesajlar, Menu
    /// </summary>
    public class MobileBottomNavigation : MonoBehaviour
    {
        [Header("Genel Ayarlar")]
        [SerializeField] private float barHeight = 80f;
        [SerializeField] private float fabSize = 100f;
        [SerializeField] private float fabOffset = 28f;
        [SerializeField] private Color barColor = new Color(0.1f, 0.12f, 0.15f, 0.95f);
        [SerializeField] private Color activeColor = new Color(1f, 0.8f, 0.2f, 1f);
        [SerializeField] private Color inactiveColor = new Color(0.6f, 0.6f, 0.6f, 1f);

        [Header("Badge Ayarlari")]
        [SerializeField] private Color badgeColor = new Color(0.9f, 0.2f, 0.2f, 1f);

        // UI References
        private Canvas canvas;
        private RectTransform barRect;
        private List<NavButton> navButtons = new List<NavButton>();
        private int activeIndex = 0;

        // Events
        public event Action<int> OnNavButtonClicked;
        public event Action OnFabClicked;
        public event Action OnMenuOpened;

        // Bottom Sheet
        private GameObject bottomSheet;
        private RectTransform bottomSheetRect;
        private bool isBottomSheetOpen = false;
        private Vector2 dragStartPos;

        private void Start()
        {
            CreateBottomNavigation();
        }

        public void CreateBottomNavigation()
        {
            // Canvas bul veya olustur
            canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("UI Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 100;
                canvasObj.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasObj.AddComponent<GraphicRaycaster>();
            }

            CreateBar();
            CreateNavButtons();
            CreateBottomSheet();

            Debug.Log("MobileBottomNavigation: Alt navigasyon olusturuldu");
        }

        private void CreateBar()
        {
            // Bar container
            GameObject barObj = new GameObject("Bottom Navigation Bar");
            barObj.transform.SetParent(canvas.transform, false);
            barRect = barObj.AddComponent<RectTransform>();

            // Alt ortada konumla
            barRect.anchorMin = new Vector2(0, 0);
            barRect.anchorMax = new Vector2(1, 0);
            barRect.pivot = new Vector2(0.5f, 0);
            barRect.anchoredPosition = Vector2.zero;
            barRect.sizeDelta = new Vector2(0, barHeight);

            // Arka plan
            Image barBg = barObj.AddComponent<Image>();
            barBg.color = barColor;

            // Gradient overlay (ust kisim icin)
            GameObject gradientObj = new GameObject("Gradient");
            gradientObj.transform.SetParent(barObj.transform, false);
            RectTransform gradientRect = gradientObj.AddComponent<RectTransform>();
            gradientRect.anchorMin = new Vector2(0, 1);
            gradientRect.anchorMax = new Vector2(1, 1);
            gradientRect.pivot = new Vector2(0.5f, 0);
            gradientRect.anchoredPosition = Vector2.zero;
            gradientRect.sizeDelta = new Vector2(0, 2);
            Image gradientImg = gradientObj.AddComponent<Image>();
            gradientImg.color = new Color(1f, 0.8f, 0.3f, 0.5f); // Altin cizgi
        }

        private void CreateNavButtons()
        {
            // 5 buton: Harita, Ittifak, FAB (Kale), Mesajlar, Menu
            string[] icons = { "M", "A", "+", "E", "=" }; // Placeholder ikonlar
            string[] labels = { "Harita", "Ittifak", "", "Mesaj", "Menu" };

            for (int i = 0; i < 5; i++)
            {
                bool isFab = (i == 2);
                CreateNavButton(i, icons[i], labels[i], isFab);
            }
        }

        private void CreateNavButton(int index, string icon, string label, bool isFab)
        {
            GameObject btnObj = new GameObject($"NavButton_{index}");
            btnObj.transform.SetParent(barRect, false);
            RectTransform btnRect = btnObj.AddComponent<RectTransform>();

            if (isFab)
            {
                // Ortadaki FAB buton - yukari tassin
                btnRect.anchorMin = new Vector2(0.5f, 1);
                btnRect.anchorMax = new Vector2(0.5f, 1);
                btnRect.pivot = new Vector2(0.5f, 0.5f);
                btnRect.anchoredPosition = new Vector2(0, fabOffset);
                btnRect.sizeDelta = new Vector2(fabSize, fabSize);

                // FAB arka plan (daire)
                Image fabBg = btnObj.AddComponent<Image>();
                fabBg.color = activeColor;

                // FAB cerceve
                GameObject fabBorder = new GameObject("Border");
                fabBorder.transform.SetParent(btnObj.transform, false);
                RectTransform borderRect = fabBorder.AddComponent<RectTransform>();
                borderRect.anchorMin = Vector2.zero;
                borderRect.anchorMax = Vector2.one;
                borderRect.sizeDelta = new Vector2(6, 6);
                Image borderImg = fabBorder.AddComponent<Image>();
                borderImg.color = new Color(0.8f, 0.6f, 0.1f, 1f);
                borderImg.raycastTarget = false;

                // FAB ikon
                GameObject iconObj = new GameObject("Icon");
                iconObj.transform.SetParent(btnObj.transform, false);
                RectTransform iconRect = iconObj.AddComponent<RectTransform>();
                iconRect.anchorMin = Vector2.zero;
                iconRect.anchorMax = Vector2.one;
                iconRect.sizeDelta = Vector2.zero;
                Text iconText = iconObj.AddComponent<Text>();
                iconText.text = icon;
                iconText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                iconText.fontSize = 40;
                iconText.alignment = TextAnchor.MiddleCenter;
                iconText.color = Color.white;
                iconText.raycastTarget = false;

                // FAB buton
                Button fabBtn = btnObj.AddComponent<Button>();
                fabBtn.transition = Selectable.Transition.ColorTint;
                int idx = index;
                fabBtn.onClick.AddListener(() => OnButtonClick(idx));

                // Scale animasyon
                NavButtonAnimator animator = btnObj.AddComponent<NavButtonAnimator>();
                animator.isFab = true;
            }
            else
            {
                // Anchor-based layout (5 esit parca: 0-20%, 20-40%, 40-60%, 60-80%, 80-100%)
                float anchorStart = index * 0.2f;
                float anchorEnd = (index + 1) * 0.2f;

                btnRect.anchorMin = new Vector2(anchorStart, 0);
                btnRect.anchorMax = new Vector2(anchorEnd, 1);
                btnRect.offsetMin = Vector2.zero;
                btnRect.offsetMax = Vector2.zero;

                // Buton arka plan (seffaf)
                Image btnBg = btnObj.AddComponent<Image>();
                btnBg.color = Color.clear;

                // Ikon container
                GameObject iconContainer = new GameObject("IconContainer");
                iconContainer.transform.SetParent(btnObj.transform, false);
                RectTransform iconContRect = iconContainer.AddComponent<RectTransform>();
                iconContRect.anchorMin = new Vector2(0.5f, 0.5f);
                iconContRect.anchorMax = new Vector2(0.5f, 0.5f);
                iconContRect.anchoredPosition = new Vector2(0, 8);
                iconContRect.sizeDelta = new Vector2(30, 30);

                // Ikon (placeholder)
                Image iconImg = iconContainer.AddComponent<Image>();
                iconImg.color = index == activeIndex ? activeColor : inactiveColor;
                iconImg.raycastTarget = false;

                // Label
                GameObject labelObj = new GameObject("Label");
                labelObj.transform.SetParent(btnObj.transform, false);
                RectTransform labelRect = labelObj.AddComponent<RectTransform>();
                labelRect.anchorMin = new Vector2(0, 0);
                labelRect.anchorMax = new Vector2(1, 0);
                labelRect.pivot = new Vector2(0.5f, 0);
                labelRect.anchoredPosition = new Vector2(0, 8);
                labelRect.sizeDelta = new Vector2(0, 20);
                Text labelText = labelObj.AddComponent<Text>();
                labelText.text = label;
                labelText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                labelText.fontSize = 12;
                labelText.alignment = TextAnchor.MiddleCenter;
                labelText.color = index == activeIndex ? activeColor : inactiveColor;
                labelText.raycastTarget = false;

                // Badge (bildirim sayisi)
                GameObject badgeObj = new GameObject("Badge");
                badgeObj.transform.SetParent(iconContainer.transform, false);
                RectTransform badgeRect = badgeObj.AddComponent<RectTransform>();
                badgeRect.anchorMin = new Vector2(1, 1);
                badgeRect.anchorMax = new Vector2(1, 1);
                badgeRect.pivot = new Vector2(0.5f, 0.5f);
                badgeRect.anchoredPosition = new Vector2(5, 5);
                badgeRect.sizeDelta = new Vector2(18, 18);
                Image badgeBg = badgeObj.AddComponent<Image>();
                badgeBg.color = badgeColor;
                badgeObj.SetActive(false);

                // Badge text
                GameObject badgeTextObj = new GameObject("BadgeText");
                badgeTextObj.transform.SetParent(badgeObj.transform, false);
                RectTransform badgeTextRect = badgeTextObj.AddComponent<RectTransform>();
                badgeTextRect.anchorMin = Vector2.zero;
                badgeTextRect.anchorMax = Vector2.one;
                badgeTextRect.sizeDelta = Vector2.zero;
                Text badgeText = badgeTextObj.AddComponent<Text>();
                badgeText.text = "0";
                badgeText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                badgeText.fontSize = 10;
                badgeText.alignment = TextAnchor.MiddleCenter;
                badgeText.color = Color.white;

                // Buton
                Button btn = btnObj.AddComponent<Button>();
                btn.transition = Selectable.Transition.None;
                int idx = index;
                btn.onClick.AddListener(() => OnButtonClick(idx));

                // Animator
                NavButtonAnimator animator = btnObj.AddComponent<NavButtonAnimator>();
                animator.iconImage = iconImg;
                animator.labelText = labelText;
                animator.activeColor = activeColor;
                animator.inactiveColor = inactiveColor;

                // NavButton kaydet
                navButtons.Add(new NavButton
                {
                    index = index,
                    button = btn,
                    iconImage = iconImg,
                    labelText = labelText,
                    badge = badgeObj,
                    badgeText = badgeText,
                    animator = animator
                });
            }
        }

        private void OnButtonClick(int index)
        {
            // Haptic feedback (mobilde)
            #if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
            #endif

            if (index == 2)
            {
                // FAB - Kaleye git
                OnFabClicked?.Invoke();
                Debug.Log("FAB clicked - Go to Castle");
            }
            else if (index == 4)
            {
                // Menu - Bottom sheet ac
                ToggleBottomSheet();
            }
            else
            {
                // Normal navigasyon
                SetActiveButton(index);
                OnNavButtonClicked?.Invoke(index);
                Debug.Log($"Nav button clicked: {index}");
            }
        }

        public void SetActiveButton(int index)
        {
            activeIndex = index;

            foreach (var navBtn in navButtons)
            {
                bool isActive = navBtn.index == index;
                if (navBtn.animator != null)
                {
                    navBtn.animator.SetActive(isActive);
                }
            }
        }

        public void SetBadge(int buttonIndex, int count)
        {
            var navBtn = navButtons.Find(b => b.index == buttonIndex);
            if (navBtn != null && navBtn.badge != null)
            {
                navBtn.badge.SetActive(count > 0);
                if (navBtn.badgeText != null)
                {
                    navBtn.badgeText.text = count > 99 ? "99+" : count.ToString();
                }
            }
        }

        #region Bottom Sheet

        private void CreateBottomSheet()
        {
            // Bottom sheet container
            bottomSheet = new GameObject("Bottom Sheet");
            bottomSheet.transform.SetParent(canvas.transform, false);
            bottomSheetRect = bottomSheet.AddComponent<RectTransform>();

            // Tam ekran overlay
            bottomSheetRect.anchorMin = Vector2.zero;
            bottomSheetRect.anchorMax = Vector2.one;
            bottomSheetRect.sizeDelta = Vector2.zero;

            // Karanlik arka plan
            GameObject overlay = new GameObject("Overlay");
            overlay.transform.SetParent(bottomSheet.transform, false);
            RectTransform overlayRect = overlay.AddComponent<RectTransform>();
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.sizeDelta = Vector2.zero;
            Image overlayImg = overlay.AddComponent<Image>();
            overlayImg.color = new Color(0, 0, 0, 0.5f);

            // Overlay'e tiklayinca kapat
            Button overlayBtn = overlay.AddComponent<Button>();
            overlayBtn.transition = Selectable.Transition.None;
            overlayBtn.onClick.AddListener(CloseBottomSheet);

            // Sheet panel
            GameObject sheet = new GameObject("Sheet Panel");
            sheet.transform.SetParent(bottomSheet.transform, false);
            RectTransform sheetRect = sheet.AddComponent<RectTransform>();
            sheetRect.anchorMin = new Vector2(0, 0);
            sheetRect.anchorMax = new Vector2(1, 0);
            sheetRect.pivot = new Vector2(0.5f, 0);
            sheetRect.anchoredPosition = Vector2.zero;
            sheetRect.sizeDelta = new Vector2(0, 300);

            Image sheetBg = sheet.AddComponent<Image>();
            sheetBg.color = new Color(0.15f, 0.17f, 0.2f, 1f);

            // Handle bar (surukleme icin)
            GameObject handle = new GameObject("Handle");
            handle.transform.SetParent(sheet.transform, false);
            RectTransform handleRect = handle.AddComponent<RectTransform>();
            handleRect.anchorMin = new Vector2(0.5f, 1);
            handleRect.anchorMax = new Vector2(0.5f, 1);
            handleRect.pivot = new Vector2(0.5f, 1);
            handleRect.anchoredPosition = new Vector2(0, -10);
            handleRect.sizeDelta = new Vector2(40, 5);
            Image handleImg = handle.AddComponent<Image>();
            handleImg.color = new Color(0.4f, 0.4f, 0.4f, 1f);

            // Menu items
            CreateBottomSheetItems(sheet.transform);

            // Drag handler
            BottomSheetDragHandler dragHandler = sheet.AddComponent<BottomSheetDragHandler>();
            dragHandler.onDragDown = CloseBottomSheet;

            bottomSheet.SetActive(false);
        }

        private void CreateBottomSheetItems(Transform parent)
        {
            string[] menuItems = { "Ayarlar", "Etkinlikler", "Siralama", "Destek", "Hesap" };
            string[] menuIcons = { "S", "E", "R", "?", "U" };

            float itemHeight = 50f;
            float startY = -40f;

            for (int i = 0; i < menuItems.Length; i++)
            {
                GameObject item = new GameObject($"MenuItem_{i}");
                item.transform.SetParent(parent, false);
                RectTransform itemRect = item.AddComponent<RectTransform>();
                itemRect.anchorMin = new Vector2(0, 1);
                itemRect.anchorMax = new Vector2(1, 1);
                itemRect.pivot = new Vector2(0.5f, 1);
                itemRect.anchoredPosition = new Vector2(0, startY - i * itemHeight);
                itemRect.sizeDelta = new Vector2(-40, itemHeight);

                // Arka plan
                Image itemBg = item.AddComponent<Image>();
                itemBg.color = Color.clear;

                // Ikon placeholder
                GameObject iconObj = new GameObject("Icon");
                iconObj.transform.SetParent(item.transform, false);
                RectTransform iconRect = iconObj.AddComponent<RectTransform>();
                iconRect.anchorMin = new Vector2(0, 0.5f);
                iconRect.anchorMax = new Vector2(0, 0.5f);
                iconRect.pivot = new Vector2(0, 0.5f);
                iconRect.anchoredPosition = new Vector2(20, 0);
                iconRect.sizeDelta = new Vector2(30, 30);
                Image iconImg = iconObj.AddComponent<Image>();
                iconImg.color = activeColor;

                // Label
                GameObject labelObj = new GameObject("Label");
                labelObj.transform.SetParent(item.transform, false);
                RectTransform labelRect = labelObj.AddComponent<RectTransform>();
                labelRect.anchorMin = new Vector2(0, 0);
                labelRect.anchorMax = new Vector2(1, 1);
                labelRect.offsetMin = new Vector2(70, 0);
                labelRect.offsetMax = new Vector2(-20, 0);
                Text labelText = labelObj.AddComponent<Text>();
                labelText.text = menuItems[i];
                labelText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                labelText.fontSize = 18;
                labelText.alignment = TextAnchor.MiddleLeft;
                labelText.color = Color.white;

                // Buton
                Button itemBtn = item.AddComponent<Button>();
                ColorBlock colors = itemBtn.colors;
                colors.highlightedColor = new Color(1, 1, 1, 0.1f);
                colors.pressedColor = new Color(1, 1, 1, 0.2f);
                itemBtn.colors = colors;
                int idx = i;
                itemBtn.onClick.AddListener(() => OnMenuItemClick(idx));
            }
        }

        private void OnMenuItemClick(int index)
        {
            Debug.Log($"Menu item clicked: {index}");
            CloseBottomSheet();
        }

        public void ToggleBottomSheet()
        {
            if (isBottomSheetOpen)
            {
                CloseBottomSheet();
            }
            else
            {
                OpenBottomSheet();
            }
        }

        public void OpenBottomSheet()
        {
            bottomSheet.SetActive(true);
            isBottomSheetOpen = true;
            OnMenuOpened?.Invoke();
        }

        public void CloseBottomSheet()
        {
            bottomSheet.SetActive(false);
            isBottomSheetOpen = false;
        }

        #endregion

        [Serializable]
        private class NavButton
        {
            public int index;
            public Button button;
            public Image iconImage;
            public Text labelText;
            public GameObject badge;
            public Text badgeText;
            public NavButtonAnimator animator;
        }
    }

    /// <summary>
    /// Nav buton animasyonlari
    /// </summary>
    public class NavButtonAnimator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Image iconImage;
        public Text labelText;
        public Color activeColor = new Color(1f, 0.8f, 0.2f, 1f);
        public Color inactiveColor = new Color(0.6f, 0.6f, 0.6f, 1f);
        public bool isFab = false;

        private bool isActive = false;
        private Vector3 originalScale;

        private void Awake()
        {
            originalScale = transform.localScale;
        }

        public void SetActive(bool active)
        {
            isActive = active;
            UpdateColors();
        }

        private void UpdateColors()
        {
            Color targetColor = isActive ? activeColor : inactiveColor;

            if (iconImage != null)
            {
                iconImage.color = targetColor;
            }

            if (labelText != null)
            {
                labelText.color = targetColor;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            float scale = isFab ? 0.9f : 0.95f;
            transform.localScale = originalScale * scale;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = originalScale;
        }
    }

    /// <summary>
    /// Bottom sheet surukleme handler
    /// </summary>
    public class BottomSheetDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Action onDragDown;

        private Vector2 dragStartPos;
        private float dragThreshold = 50f;

        public void OnBeginDrag(PointerEventData eventData)
        {
            dragStartPos = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Gorsel geri bildirim (opsiyonel)
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            float dragDistance = eventData.position.y - dragStartPos.y;

            // Asagi surukleme - kapat
            if (dragDistance < -dragThreshold)
            {
                onDragDown?.Invoke();
            }
        }
    }
}
