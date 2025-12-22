using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EmpireWars.Map;
using EmpireWars.Data;

namespace EmpireWars.UI
{
    /// <summary>
    /// Hex tile bilgi paneli
    /// Tile tiklandiginda bilgileri gosterir
    /// Dokuman referansi: docs/01-game-design/map/MAP-SYSTEM.md - Bolum 5
    /// </summary>
    public class TileInfoPanel : MonoBehaviour
    {
        public static TileInfoPanel Instance { get; private set; }

        [Header("Panel")]
        [SerializeField] private GameObject panelRoot;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Temel Bilgiler")]
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI coordinatesText;
        [SerializeField] private TextMeshProUGUI zoneText;
        [SerializeField] private TextMeshProUGUI terrainText;

        [Header("Arazi Ozellikleri")]
        [SerializeField] private TextMeshProUGUI movementText;
        [SerializeField] private TextMeshProUGUI defenseText;
        [SerializeField] private TextMeshProUGUI passableText;

        [Header("Sahiplik Bilgileri")]
        [SerializeField] private GameObject occupantSection;
        [SerializeField] private TextMeshProUGUI occupantTypeText;
        [SerializeField] private TextMeshProUGUI occupantNameText;

        [Header("Kaynak Bilgileri")]
        [SerializeField] private GameObject resourceSection;
        [SerializeField] private TextMeshProUGUI resourceTypeText;
        [SerializeField] private TextMeshProUGUI resourceAmountText;
        [SerializeField] private Slider resourceSlider;

        [Header("Butonlar")]
        [SerializeField] private Button closeButton;
        [SerializeField] private Button actionButton;
        [SerializeField] private TextMeshProUGUI actionButtonText;

        [Header("Animasyon")]
        [SerializeField] private float fadeSpeed = 5f;

        // Mevcut secili cell
        private HexCell selectedCell;
        private bool isShowing = false;

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
        }

        private void Start()
        {
            // Event dinle
            if (WorldMapManager.Instance != null)
            {
                WorldMapManager.Instance.OnCellClicked += ShowForCell;
            }

            // Buton eventleri
            if (closeButton != null)
            {
                closeButton.onClick.AddListener(Hide);
            }

            if (actionButton != null)
            {
                actionButton.onClick.AddListener(OnActionButtonClicked);
            }

            // Baslangicta gizle
            Hide();
        }

        private void Update()
        {
            // Fade animasyonu
            if (canvasGroup != null)
            {
                float targetAlpha = isShowing ? 1f : 0f;
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);

                if (!isShowing && canvasGroup.alpha < 0.01f && panelRoot != null)
                {
                    panelRoot.SetActive(false);
                }
            }

            // ESC ile kapat
            if (isShowing && Input.GetKeyDown(KeyCode.Escape))
            {
                Hide();
            }
        }

        private void OnDestroy()
        {
            if (WorldMapManager.Instance != null)
            {
                WorldMapManager.Instance.OnCellClicked -= ShowForCell;
            }
        }

        #endregion

        #region Show/Hide

        public void ShowForCell(HexCell cell)
        {
            if (cell == null)
            {
                Hide();
                return;
            }

            selectedCell = cell;
            UpdateDisplay();
            Show();
        }

        public void Show()
        {
            // UI henuz kurulmamissa sessizce cik
            if (panelRoot == null && canvasGroup == null)
            {
                Debug.LogWarning("TileInfoPanel: UI referanslari atanmamis. Inspector'dan atayin.");
                return;
            }

            isShowing = true;

            if (panelRoot != null)
                panelRoot.SetActive(true);

            if (canvasGroup != null)
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }

        public void Hide()
        {
            isShowing = false;
            selectedCell = null;

            if (panelRoot != null)
                panelRoot.SetActive(false);

            if (canvasGroup != null)
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }

        #endregion

        #region Display Update

        private void UpdateDisplay()
        {
            if (selectedCell == null) return;

            // Temel bilgiler
            UpdateBasicInfo();

            // Arazi ozellikleri
            UpdateTerrainProperties();

            // Sahiplik
            UpdateOccupantInfo();

            // Kaynak bilgileri
            UpdateResourceInfo();

            // Aksiyon butonu
            UpdateActionButton();
        }

        private void UpdateBasicInfo()
        {
            TerrainType terrain = selectedCell.TerrainType;
            string terrainName = TerrainProperties.GetDisplayName(terrain);

            if (titleText != null)
            {
                titleText.text = terrainName;
            }

            if (coordinatesText != null)
            {
                coordinatesText.text = $"Konum: {selectedCell.Coordinates}";
            }

            if (zoneText != null)
            {
                int zone = selectedCell.Zone;
                string zoneName = zone switch
                {
                    1 => "Merkez Bolge",
                    2 => "Ileri Bolge",
                    3 => "Orta Bolge",
                    4 => "Dis Bolge",
                    _ => "Bilinmeyen"
                };
                zoneText.text = $"Bolge: {zoneName} ({zone})";
            }

            if (terrainText != null)
            {
                terrainText.text = $"Arazi: {terrainName}";
            }
        }

        private void UpdateTerrainProperties()
        {
            TerrainType terrain = selectedCell.TerrainType;

            if (movementText != null)
            {
                float moveMod = TerrainProperties.GetMovementModifier(terrain);
                if (moveMod > 0)
                {
                    movementText.text = $"Hareket: {moveMod:P0}";
                }
                else
                {
                    movementText.text = "Hareket: Gecilmez";
                }
            }

            if (defenseText != null)
            {
                float defBonus = TerrainProperties.GetDefenseBonus(terrain);
                if (defBonus > 0)
                {
                    defenseText.text = $"Savunma: +{defBonus:P0}";
                }
                else
                {
                    defenseText.text = "Savunma: -";
                }
            }

            if (passableText != null)
            {
                bool passable = TerrainProperties.IsPassable(terrain);
                passableText.text = passable ? "Gecis: Evet" : "Gecis: Hayir";
                passableText.color = passable ? Color.green : Color.red;
            }
        }

        private void UpdateOccupantInfo()
        {
            OccupantType occupant = selectedCell.GetOccupantType();

            if (occupantSection != null)
            {
                occupantSection.SetActive(occupant != OccupantType.Empty);
            }

            if (occupant != OccupantType.Empty)
            {
                if (occupantTypeText != null)
                {
                    occupantTypeText.text = GetOccupantTypeName(occupant);
                }

                if (occupantNameText != null)
                {
                    // TODO: Gercek sahiplik bilgisi backend'den gelecek
                    occupantNameText.text = $"ID: {selectedCell.GetOccupantId()}";
                }
            }
        }

        private void UpdateResourceInfo()
        {
            TerrainType terrain = selectedCell.TerrainType;
            bool isResource = terrain == TerrainType.Farm ||
                              terrain == TerrainType.Mine ||
                              terrain == TerrainType.Quarry ||
                              terrain == TerrainType.GoldMine ||
                              terrain == TerrainType.GemMine;

            if (resourceSection != null)
            {
                resourceSection.SetActive(isResource);
            }

            if (isResource)
            {
                if (resourceTypeText != null)
                {
                    resourceTypeText.text = GetResourceTypeName(terrain);
                }

                // TODO: Gercek kaynak miktari backend'den gelecek
                if (resourceAmountText != null)
                {
                    resourceAmountText.text = "45,000 / 50,000";
                }

                if (resourceSlider != null)
                {
                    resourceSlider.value = 0.9f;
                }
            }
        }

        private void UpdateActionButton()
        {
            if (actionButton == null) return;

            OccupantType occupant = selectedCell.GetOccupantType();
            TerrainType terrain = selectedCell.TerrainType;

            // Aksiyon belirleme
            string action = DetermineAction(terrain, occupant);

            if (string.IsNullOrEmpty(action))
            {
                actionButton.gameObject.SetActive(false);
            }
            else
            {
                actionButton.gameObject.SetActive(true);
                if (actionButtonText != null)
                {
                    actionButtonText.text = action;
                }
            }
        }

        private string DetermineAction(TerrainType terrain, OccupantType occupant)
        {
            // Kaynak toplama
            if (terrain == TerrainType.Farm ||
                terrain == TerrainType.Mine ||
                terrain == TerrainType.Quarry ||
                terrain == TerrainType.GoldMine ||
                terrain == TerrainType.GemMine)
            {
                return "Topla";
            }

            // Barbar kampi
            if (occupant == OccupantType.BarbarianCamp ||
                occupant == OccupantType.BarbarianFortress)
            {
                return "Saldir";
            }

            // Bos arazi
            if (occupant == OccupantType.Empty && TerrainProperties.IsPassable(terrain))
            {
                return "Git";
            }

            return null;
        }

        #endregion

        #region Helpers

        private string GetOccupantTypeName(OccupantType type)
        {
            return type switch
            {
                OccupantType.PlayerCity => "Oyuncu Sehri",
                OccupantType.AllianceHQ => "Ittifak Merkezi",
                OccupantType.AllianceTower => "Ittifak Kulesi",
                OccupantType.AllianceFlag => "Ittifak Bayragi",
                OccupantType.ResourceNode => "Kaynak Noktasi",
                OccupantType.BarbarianCamp => "Barbar Kampi",
                OccupantType.BarbarianFortress => "Barbar Kalesi",
                OccupantType.Monster => "Canavar",
                OccupantType.HolySite => "Kutsal Mekan",
                OccupantType.KingdomCastle => "Krallik Kalesi",
                OccupantType.Army => "Ordu",
                _ => "Bos"
            };
        }

        private string GetResourceTypeName(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Farm => "Ciftlik - Yiyecek",
                TerrainType.Mine => "Maden - Demir",
                TerrainType.Quarry => "Ocak - Tas",
                TerrainType.GoldMine => "Altin Madeni",
                TerrainType.GemMine => "Mucevher Madeni",
                _ => "Kaynak"
            };
        }

        #endregion

        #region Button Actions

        private void OnActionButtonClicked()
        {
            if (selectedCell == null) return;

            string action = actionButtonText?.text;

            switch (action)
            {
                case "Topla":
                    StartGathering();
                    break;
                case "Saldir":
                    StartAttack();
                    break;
                case "Git":
                    MoveTo();
                    break;
            }
        }

        private void StartGathering()
        {
            Debug.Log($"Kaynak toplama baslatildi: {selectedCell.Coordinates}");
            // TODO: Kaynak toplama sistemi
        }

        private void StartAttack()
        {
            Debug.Log($"Saldiri baslatildi: {selectedCell.Coordinates}");
            // TODO: Savas sistemi
        }

        private void MoveTo()
        {
            Debug.Log($"Hareket baslatildi: {selectedCell.Coordinates}");
            // TODO: Ordu hareket sistemi
        }

        #endregion
    }
}
