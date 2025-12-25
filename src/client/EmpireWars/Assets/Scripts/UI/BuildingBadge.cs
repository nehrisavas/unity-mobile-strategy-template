using UnityEngine;
using TMPro;

namespace EmpireWars.UI
{
    /// <summary>
    /// Bina ve maden bilgisini gösteren modern badge UI
    /// Billboard efekti ile her zaman kameraya bakar
    /// </summary>
    public class BuildingBadge : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshPro labelText;
        [SerializeField] private TextMeshPro levelText;
        [SerializeField] private SpriteRenderer backgroundSprite;
        [SerializeField] private SpriteRenderer iconSprite;

        [Header("Settings")]
        [SerializeField] private bool billboardEnabled = true;
        [SerializeField] private float minVisibleDistance = 3f;
        [SerializeField] private float maxVisibleDistance = 35f;
        [SerializeField] private float fadeStartDistance = 25f;

        private Transform cameraTransform;
        private CanvasGroup canvasGroup;
        private string buildingType;
        private int level;
        private bool isMine;

        // Renk paletleri
        private static readonly Color[] LevelColors = new Color[]
        {
            new Color(0.6f, 0.6f, 0.6f),     // 1-4: Gri
            new Color(0.9f, 0.9f, 0.9f),     // 5-9: Beyaz
            new Color(0.3f, 0.9f, 0.4f),     // 10-14: Yeşil
            new Color(0.3f, 0.6f, 1f),       // 15-19: Mavi
            new Color(0.7f, 0.4f, 1f),       // 20-24: Mor
            new Color(1f, 0.8f, 0.2f),       // 25-30: Altın
        };

        private static readonly Color BackgroundDark = new Color(0.1f, 0.1f, 0.15f, 0.85f);
        private static readonly Color BackgroundLight = new Color(0.2f, 0.2f, 0.25f, 0.8f);

        private void Start()
        {
            if (Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
        }

        private void LateUpdate()
        {
            if (cameraTransform == null)
            {
                if (Camera.main != null)
                    cameraTransform = Camera.main.transform;
                return;
            }

            // Billboard efekti - kameraya bak
            if (billboardEnabled)
            {
                transform.rotation = Quaternion.LookRotation(
                    transform.position - cameraTransform.position,
                    Vector3.up
                );
            }

            // Mesafeye göre görünürlük
            float distance = Vector3.Distance(transform.position, cameraTransform.position);

            if (distance < minVisibleDistance || distance > maxVisibleDistance)
            {
                SetVisible(false);
            }
            else
            {
                SetVisible(true);

                // Fade efekti
                if (distance > fadeStartDistance)
                {
                    float alpha = 1f - ((distance - fadeStartDistance) / (maxVisibleDistance - fadeStartDistance));
                    SetAlpha(alpha);
                }
                else
                {
                    SetAlpha(1f);
                }
            }
        }

        private void SetVisible(bool visible)
        {
            if (labelText != null) labelText.enabled = visible;
            if (levelText != null) levelText.enabled = visible;
            if (backgroundSprite != null) backgroundSprite.enabled = visible;
            if (iconSprite != null) iconSprite.enabled = visible;
        }

        private void SetAlpha(float alpha)
        {
            if (labelText != null)
            {
                Color c = labelText.color;
                c.a = alpha;
                labelText.color = c;
            }
            if (levelText != null)
            {
                Color c = levelText.color;
                c.a = alpha;
                levelText.color = c;
            }
            if (backgroundSprite != null)
            {
                Color c = backgroundSprite.color;
                c.a = BackgroundDark.a * alpha;
                backgroundSprite.color = c;
            }
        }

        /// <summary>
        /// Badge'i bina bilgisiyle ayarla
        /// </summary>
        public void SetupBuilding(string type, int buildingLevel)
        {
            buildingType = type;
            level = buildingLevel;
            isMine = false;

            CreateBadgeElements();
            UpdateDisplay();
        }

        /// <summary>
        /// Badge'i maden bilgisiyle ayarla
        /// </summary>
        public void SetupMine(WorldMap.KingdomMapGenerator.MineType mineType, int mineLevel)
        {
            buildingType = mineType.ToString();
            level = mineLevel;
            isMine = true;

            CreateBadgeElements();
            UpdateMineDisplay(mineType);
        }

        private void CreateBadgeElements()
        {
            // Arka plan sprite - kompakt boyut
            if (backgroundSprite == null)
            {
                GameObject bgObj = new GameObject("Background");
                bgObj.transform.SetParent(transform);
                bgObj.transform.localPosition = Vector3.zero;
                bgObj.transform.localScale = new Vector3(0.9f, 0.4f, 1f);

                backgroundSprite = bgObj.AddComponent<SpriteRenderer>();
                backgroundSprite.sprite = CreateRoundedRectSprite();
                backgroundSprite.color = BackgroundDark;
                backgroundSprite.sortingOrder = 99;
            }

            // Tek satır text - ikon + isim + seviye birlikte
            if (labelText == null)
            {
                GameObject labelObj = new GameObject("LabelText");
                labelObj.transform.SetParent(transform);
                labelObj.transform.localPosition = new Vector3(0, 0, -0.01f);

                labelText = labelObj.AddComponent<TextMeshPro>();
                labelText.fontSize = 2.2f;
                labelText.alignment = TextAlignmentOptions.Center;
                labelText.sortingOrder = 101;
                labelText.color = Color.white;

                RectTransform rect = labelObj.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(2.5f, 0.8f);
            }

            // LevelText artık kullanılmıyor - tek satırda birleştirildi
            if (levelText != null)
            {
                DestroyImmediate(levelText.gameObject);
                levelText = null;
            }
        }

        private void UpdateDisplay()
        {
            string displayName = GetBuildingDisplayName(buildingType);
            string icon = GetBuildingIcon(buildingType);
            Color lvlColor = GetLevelColor(level);

            // Tek satırda: ikon isim seviye
            if (labelText != null)
            {
                string hexColor = ColorUtility.ToHtmlStringRGB(lvlColor);
                labelText.text = $"{icon}{displayName}<color=#{hexColor}>{level}</color>";
            }

            // Seviyeye göre arka plan rengi
            if (backgroundSprite != null)
            {
                if (level >= 25)
                    backgroundSprite.color = new Color(0.3f, 0.25f, 0.1f, 0.9f);
                else if (level >= 20)
                    backgroundSprite.color = new Color(0.2f, 0.15f, 0.3f, 0.9f);
                else
                    backgroundSprite.color = BackgroundDark;
            }
        }

        private void UpdateMineDisplay(WorldMap.KingdomMapGenerator.MineType mineType)
        {
            string typeName = WorldMap.KingdomMapGenerator.GetMineTypeName(mineType);
            string icon = WorldMap.KingdomMapGenerator.GetMineTypeIcon(mineType);
            Color typeColor = WorldMap.KingdomMapGenerator.GetMineTypeColor(mineType);

            // Tek satırda: ikon isim seviye
            if (labelText != null)
            {
                string hexColor = ColorUtility.ToHtmlStringRGB(typeColor);
                labelText.text = $"{icon}{typeName}<color=#{hexColor}>Lv{level}</color>";
                labelText.color = Color.white;
            }

            if (backgroundSprite != null)
            {
                Color bgColor = typeColor * 0.3f;
                bgColor.a = 0.9f;
                backgroundSprite.color = bgColor;
            }
        }

        private Color GetLevelColor(int lvl)
        {
            int index = Mathf.Clamp((lvl - 1) / 5, 0, LevelColors.Length - 1);
            return LevelColors[index];
        }

        private static string GetBuildingDisplayName(string buildingType)
        {
            if (string.IsNullOrEmpty(buildingType)) return "";

            string baseName = buildingType.Replace("_green", "").Replace("_blue", "").Replace("_red", "").Replace("_yellow", "");

            return baseName switch
            {
                "castle" => "Kale",
                "townhall" => "Belediye",
                "market" => "Pazar",
                "blacksmith" => "Demirci",
                "workshop" => "Atölye",
                "barracks" => "Kışla",
                "archeryrange" => "Okçu",
                "stables" => "Ahır",
                "mine" => "Maden",
                "lumbermill" => "Kereste",
                "windmill" => "Değirmen",
                "watermill" => "Su Değ.",
                "home_a" or "home_b" => "Ev",
                "tavern" => "Taverna",
                "tent" => "Çadır",
                "church" => "Kilise",
                "shrine" => "Tapınak",
                "shipyard" => "Tersane",
                "docks" => "Rıhtım",
                "ship" => "Gemi",
                "boat" => "Tekne",
                "boatrack" => "Tekne Rafı",
                "tower_cannon" => "Top Kulesi",
                "tower_catapult" => "Mancınık",
                "watchtower" => "Gözcü",
                "tower_a" or "tower_b" => "Kule",
                "wall_straight" => "Sur",
                _ => baseName
            };
        }

        private static string GetBuildingIcon(string buildingType)
        {
            if (string.IsNullOrEmpty(buildingType)) return "";

            string baseName = buildingType.Replace("_green", "").Replace("_blue", "").Replace("_red", "").Replace("_yellow", "");

            return baseName switch
            {
                "castle" => "🏰",
                "townhall" => "🏛",
                "market" => "🏪",
                "blacksmith" => "⚒",
                "workshop" => "🔧",
                "barracks" => "⚔",
                "archeryrange" => "🏹",
                "stables" => "🐎",
                "mine" => "⛏",
                "lumbermill" => "🪓",
                "windmill" => "🌾",
                "watermill" => "💧",
                "home_a" or "home_b" => "🏠",
                "tavern" => "🍺",
                "tent" => "⛺",
                "church" => "⛪",
                "shrine" => "🕌",
                "shipyard" => "🚢",
                "docks" => "⚓",
                "ship" => "⛵",
                "boat" => "🚣",
                "boatrack" => "🛶",
                "tower_cannon" => "💣",
                "tower_catapult" => "🎯",
                "watchtower" => "👁",
                "tower_a" or "tower_b" => "🗼",
                "wall_straight" => "🧱",
                _ => "📍"
            };
        }

        /// <summary>
        /// Basit yuvarlak köşeli dikdörtgen sprite oluştur
        /// </summary>
        private Sprite CreateRoundedRectSprite()
        {
            int width = 64;
            int height = 32;
            Texture2D tex = new Texture2D(width, height);
            tex.filterMode = FilterMode.Bilinear;

            Color fillColor = Color.white;
            Color transparent = new Color(1, 1, 1, 0);
            int cornerRadius = 8;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Köşe kontrolü
                    bool inCorner = false;
                    float dist = 0;

                    // Sol alt köşe
                    if (x < cornerRadius && y < cornerRadius)
                    {
                        dist = Vector2.Distance(new Vector2(x, y), new Vector2(cornerRadius, cornerRadius));
                        inCorner = true;
                    }
                    // Sağ alt köşe
                    else if (x >= width - cornerRadius && y < cornerRadius)
                    {
                        dist = Vector2.Distance(new Vector2(x, y), new Vector2(width - cornerRadius - 1, cornerRadius));
                        inCorner = true;
                    }
                    // Sol üst köşe
                    else if (x < cornerRadius && y >= height - cornerRadius)
                    {
                        dist = Vector2.Distance(new Vector2(x, y), new Vector2(cornerRadius, height - cornerRadius - 1));
                        inCorner = true;
                    }
                    // Sağ üst köşe
                    else if (x >= width - cornerRadius && y >= height - cornerRadius)
                    {
                        dist = Vector2.Distance(new Vector2(x, y), new Vector2(width - cornerRadius - 1, height - cornerRadius - 1));
                        inCorner = true;
                    }

                    if (inCorner && dist > cornerRadius)
                    {
                        tex.SetPixel(x, y, transparent);
                    }
                    else
                    {
                        tex.SetPixel(x, y, fillColor);
                    }
                }
            }

            tex.Apply();

            return Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 32f);
        }

        /// <summary>
        /// Badge factory - bina için oluştur
        /// </summary>
        public static BuildingBadge CreateForBuilding(Transform parent, string buildingType, int level, float yOffset = 2f)
        {
            GameObject badgeObj = new GameObject($"Badge_{buildingType}");
            badgeObj.transform.SetParent(parent);
            badgeObj.transform.localPosition = new Vector3(0, yOffset, 0);

            BuildingBadge badge = badgeObj.AddComponent<BuildingBadge>();
            badge.SetupBuilding(buildingType, level);

            return badge;
        }

        /// <summary>
        /// Badge factory - maden için oluştur
        /// </summary>
        public static BuildingBadge CreateForMine(Transform parent, WorldMap.KingdomMapGenerator.MineType mineType, int level, float yOffset = 1.5f)
        {
            GameObject badgeObj = new GameObject($"Badge_Mine_{mineType}");
            badgeObj.transform.SetParent(parent);
            badgeObj.transform.localPosition = new Vector3(0, yOffset, 0);

            BuildingBadge badge = badgeObj.AddComponent<BuildingBadge>();
            badge.SetupMine(mineType, level);

            return badge;
        }
    }
}
