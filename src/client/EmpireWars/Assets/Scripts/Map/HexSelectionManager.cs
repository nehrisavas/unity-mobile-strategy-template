using UnityEngine;
using EmpireWars.Core;
using EmpireWars.UI;
using EmpireWars.CameraSystem;

namespace EmpireWars.Map
{
    /// <summary>
    /// Hex secim yoneticisi
    /// Harita uzerinde hucre secimi, hover ve etkilesimleri yonetir
    /// </summary>
    public class HexSelectionManager : MonoBehaviour
    {
        public static HexSelectionManager Instance { get; private set; }

        [Header("Secim Ayarlari")]
        [SerializeField] private LayerMask hexLayerMask;
        [SerializeField] private bool allowMultiSelect = false;

        [Header("Gorseller")]
        [SerializeField] private GameObject selectionIndicatorPrefab;
        [SerializeField] private GameObject hoverIndicatorPrefab;
        [SerializeField] private Color selectionColor = new Color(0.2f, 0.6f, 1f, 0.5f);
        [SerializeField] private Color hoverColor = new Color(1f, 1f, 1f, 0.3f);

        [Header("Cursor")]
        [SerializeField] private Texture2D normalCursor;
        [SerializeField] private Texture2D attackCursor;
        [SerializeField] private Texture2D gatherCursor;
        [SerializeField] private Texture2D moveCursor;

        // Internal
        private HexCell selectedCell;
        private HexCell hoveredCell;
        private GameObject selectionIndicator;
        private GameObject hoverIndicator;
        private UnityEngine.Camera mainCamera;

        // Events
        public System.Action<HexCell> OnCellSelected;
        public System.Action<HexCell> OnCellDeselected;
        public System.Action<HexCell> OnCellHovered;
        public System.Action OnSelectionCleared;

        #region Properties

        public HexCell SelectedCell => selectedCell;
        public HexCell HoveredCell => hoveredCell;
        public bool HasSelection => selectedCell != null;

        #endregion

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
            mainCamera = UnityEngine.Camera.main;
            CreateIndicators();
        }

        private void Update()
        {
            HandleHover();
            HandleInput();
            UpdateCursor();
        }

        #endregion

        #region Initialization

        private void CreateIndicators()
        {
            // Secim gostergesi
            if (selectionIndicatorPrefab != null)
            {
                selectionIndicator = Instantiate(selectionIndicatorPrefab, transform);
                selectionIndicator.SetActive(false);
            }
            else
            {
                selectionIndicator = CreateDefaultIndicator("SelectionIndicator", selectionColor);
            }

            // Hover gostergesi
            if (hoverIndicatorPrefab != null)
            {
                hoverIndicator = Instantiate(hoverIndicatorPrefab, transform);
                hoverIndicator.SetActive(false);
            }
            else
            {
                hoverIndicator = CreateDefaultIndicator("HoverIndicator", hoverColor);
            }
        }

        private GameObject CreateDefaultIndicator(string name, Color color)
        {
            GameObject indicator = new GameObject(name);
            indicator.transform.SetParent(transform);

            // Basit bir halka mesh olustur
            MeshFilter meshFilter = indicator.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = indicator.AddComponent<MeshRenderer>();

            // Hex outline mesh
            meshFilter.mesh = CreateHexOutlineMesh();

            // Material
            Material mat = new Material(Shader.Find("Sprites/Default"));
            mat.color = color;
            meshRenderer.material = mat;

            indicator.SetActive(false);
            return indicator;
        }

        private Mesh CreateHexOutlineMesh()
        {
            Mesh mesh = new Mesh();

            // Hex koselerini hesapla (ic ve dis)
            Vector3[] vertices = new Vector3[12];
            float innerRadius = HexMetrics.InnerRadius * 0.9f;
            float outerRadius = HexMetrics.InnerRadius * 1.1f;

            for (int i = 0; i < 6; i++)
            {
                float angle = (60f * i - 30f) * Mathf.Deg2Rad;
                vertices[i] = new Vector3(
                    Mathf.Cos(angle) * innerRadius,
                    0.1f,
                    Mathf.Sin(angle) * innerRadius
                );
                vertices[i + 6] = new Vector3(
                    Mathf.Cos(angle) * outerRadius,
                    0.1f,
                    Mathf.Sin(angle) * outerRadius
                );
            }

            // Ucgenler
            int[] triangles = new int[36];
            for (int i = 0; i < 6; i++)
            {
                int nextI = (i + 1) % 6;
                int baseIndex = i * 6;

                triangles[baseIndex] = i;
                triangles[baseIndex + 1] = i + 6;
                triangles[baseIndex + 2] = nextI;

                triangles[baseIndex + 3] = nextI;
                triangles[baseIndex + 4] = i + 6;
                triangles[baseIndex + 5] = nextI + 6;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            return mesh;
        }

        #endregion

        #region Input Handling

        private void HandleHover()
        {
            if (mainCamera == null) return;

            Ray ray = mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, hexLayerMask))
            {
                HexCell cell = hit.collider.GetComponentInParent<HexCell>();
                if (cell != hoveredCell)
                {
                    hoveredCell = cell;
                    OnCellHovered?.Invoke(cell);
                    UpdateHoverIndicator();
                }
            }
            else
            {
                if (hoveredCell != null)
                {
                    hoveredCell = null;
                    OnCellHovered?.Invoke(null);
                    UpdateHoverIndicator();
                }
            }
        }

        private void HandleInput()
        {
            // Kamera surukleniyorsa secim yapma
            if (MapCameraController.Instance != null && MapCameraController.Instance.IsDragging)
            {
                return;
            }

            // Sol tik - secim
            if (UnityEngine.Input.GetMouseButtonDown(0) && !IsPointerOverUI())
            {
                if (hoveredCell != null)
                {
                    SelectCell(hoveredCell);
                }
                else
                {
                    ClearSelection();
                }
            }

            // ESC - secimi temizle
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                ClearSelection();
            }
        }

        private bool IsPointerOverUI()
        {
            return UnityEngine.EventSystems.EventSystem.current != null &&
                   UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }

        #endregion

        #region Selection

        public void SelectCell(HexCell cell)
        {
            if (cell == null) return;

            // Ayni hucre seciliyse bile tekrar tetikle
            if (selectedCell != null && selectedCell != cell)
            {
                OnCellDeselected?.Invoke(selectedCell);
            }

            selectedCell = cell;
            UpdateSelectionIndicator();
            OnCellSelected?.Invoke(cell);

            // Tile info panelini goster
            TileInfoPanel.Instance?.ShowForCell(cell);

            Debug.Log($"Hucre secildi: {cell.Coordinates} - {cell.TerrainType}");
        }

        public void ClearSelection()
        {
            if (selectedCell != null)
            {
                OnCellDeselected?.Invoke(selectedCell);
                selectedCell = null;
            }

            UpdateSelectionIndicator();
            OnSelectionCleared?.Invoke();

            // Tile info panelini gizle
            TileInfoPanel.Instance?.Hide();

            // Yol gorselini temizle
            PathVisualizer.Instance?.Hide();
        }

        public void SelectCellByCoordinates(HexCoordinates coords)
        {
            if (WorldMapManager.Instance == null) return;

            HexCell cell = WorldMapManager.Instance.GetCell(coords);
            if (cell != null)
            {
                SelectCell(cell);
            }
        }

        #endregion

        #region Indicators

        private void UpdateSelectionIndicator()
        {
            if (selectionIndicator == null) return;

            if (selectedCell != null)
            {
                selectionIndicator.transform.position = selectedCell.GetWorldPosition() + Vector3.up * 0.1f;
                selectionIndicator.SetActive(true);
            }
            else
            {
                selectionIndicator.SetActive(false);
            }
        }

        private void UpdateHoverIndicator()
        {
            if (hoverIndicator == null) return;

            if (hoveredCell != null && hoveredCell != selectedCell)
            {
                hoverIndicator.transform.position = hoveredCell.GetWorldPosition() + Vector3.up * 0.1f;
                hoverIndicator.SetActive(true);
            }
            else
            {
                hoverIndicator.SetActive(false);
            }
        }

        #endregion

        #region Cursor

        private void UpdateCursor()
        {
            if (hoveredCell == null)
            {
                SetCursor(normalCursor);
                return;
            }

            // Hovered cell'e gore cursor degistir
            OccupantType occupant = hoveredCell.GetOccupantType();
            Data.TerrainType terrain = hoveredCell.TerrainType;

            // Dusuman - saldiri cursor
            if (occupant == OccupantType.BarbarianCamp ||
                occupant == OccupantType.BarbarianFortress ||
                occupant == OccupantType.Monster)
            {
                SetCursor(attackCursor);
            }
            // Kaynak - toplama cursor
            else if (terrain == Data.TerrainType.Farm ||
                     terrain == Data.TerrainType.Mine ||
                     terrain == Data.TerrainType.Quarry ||
                     terrain == Data.TerrainType.GoldMine ||
                     terrain == Data.TerrainType.GemMine)
            {
                SetCursor(gatherCursor);
            }
            // Bos alan - hareket cursor
            else if (occupant == OccupantType.Empty && Data.TerrainProperties.IsPassable(terrain))
            {
                SetCursor(moveCursor);
            }
            else
            {
                SetCursor(normalCursor);
            }
        }

        private void SetCursor(Texture2D cursor)
        {
            if (cursor != null)
            {
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }

        #endregion

        #region Pathfinding Integration

        public void ShowPathToHovered()
        {
            if (selectedCell == null || hoveredCell == null) return;
            if (selectedCell == hoveredCell) return;
            if (HexPathfinder.Instance == null) return;

            PathResult result = HexPathfinder.Instance.FindPath(
                selectedCell.Coordinates,
                hoveredCell.Coordinates
            );

            PathVisualizer.Instance?.ShowPathFromResult(result);
        }

        public void ShowMovementRange(int range)
        {
            if (selectedCell == null) return;
            PathVisualizer.Instance?.ShowMovementRange(selectedCell.Coordinates, range);
        }

        public void ShowAttackRange(int range)
        {
            if (selectedCell == null) return;
            PathVisualizer.Instance?.ShowAttackRange(selectedCell.Coordinates, range);
        }

        #endregion
    }
}
