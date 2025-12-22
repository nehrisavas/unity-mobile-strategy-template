using UnityEngine;
using EmpireWars.Core;
using EmpireWars.Data;

namespace EmpireWars.WorldMap.Tiles
{
    /// <summary>
    /// Her hex tile uzerindeki component
    /// Highlight icin ayri overlay objesi kullanir (material instance olusturmaz)
    /// Kaynak: https://discussions.unity.com/t/hex-tile-selection-highlight-marker-implementation/864966
    /// </summary>
    public class HexTile : MonoBehaviour
    {
        [Header("Tile Data")]
        [SerializeField] private int q;
        [SerializeField] private int r;
        [SerializeField] private TerrainType terrainType;

        [Header("State")]
        [SerializeField] private bool isExplored = true;
        [SerializeField] private bool isVisible = true;
        [SerializeField] private bool isSelected;
        [SerializeField] private bool isHighlighted;

        [Header("Ownership")]
        [SerializeField] private int ownerPlayerId = -1;
        [SerializeField] private int ownerNationId = -1;

        [Header("Resources")]
        [SerializeField] private bool hasResource;
        [SerializeField] private ResourceType resourceType;

        [Header("Building")]
        [SerializeField] private bool hasBuilding;
        [SerializeField] private GameObject buildingObject;

        [Header("Highlight Overlay")]
        [SerializeField] private GameObject highlightOverlay;
        [SerializeField] private GameObject selectionOverlay;

        // Properties
        public HexCoordinates Coordinates => new HexCoordinates(q, r);
        public TerrainType Terrain => terrainType;
        public bool IsExplored => isExplored;
        public bool IsVisible => isVisible;
        public bool IsSelected => isSelected;
        public int OwnerPlayerId => ownerPlayerId;
        public int OwnerNationId => ownerNationId;
        public bool HasResource => hasResource;
        public ResourceType Resource => resourceType;
        public bool HasBuilding => hasBuilding;

        // Fog of War icin renderer cache
        private Renderer[] renderers;

        // Highlight renkleri
        private static readonly Color HighlightColor = new Color(0.3f, 0.6f, 1f, 0.4f);
        private static readonly Color SelectionColor = new Color(1f, 0.8f, 0.2f, 0.5f);

        private void Awake()
        {
            CacheRenderers();
            CreateOverlays();
        }

        /// <summary>
        /// Tile'i koordinat ve terrain tipi ile initialize eder
        /// </summary>
        public void Initialize(HexCoordinates coords, TerrainType terrain)
        {
            q = coords.Q;
            r = coords.R;
            terrainType = terrain;
            isExplored = true;
            isVisible = true;

            CacheRenderers();

            if (highlightOverlay == null || selectionOverlay == null)
            {
                CreateOverlays();
            }

            UpdateOverlays();
        }

        private void CacheRenderers()
        {
            renderers = GetComponentsInChildren<Renderer>();
        }

        /// <summary>
        /// Highlight ve selection icin overlay objelerini olustur
        /// </summary>
        private void CreateOverlays()
        {
            // Highlight overlay (hover)
            if (highlightOverlay == null)
            {
                highlightOverlay = CreateOverlayQuad("HighlightOverlay", HighlightColor);
            }

            // Selection overlay
            if (selectionOverlay == null)
            {
                selectionOverlay = CreateOverlayQuad("SelectionOverlay", SelectionColor);
            }

            UpdateOverlays();
        }

        private GameObject CreateOverlayQuad(string name, Color color)
        {
            GameObject overlay = GameObject.CreatePrimitive(PrimitiveType.Quad);
            overlay.name = name;
            overlay.transform.SetParent(transform);
            overlay.transform.localPosition = new Vector3(0, 0.05f, 0);
            overlay.transform.localRotation = Quaternion.Euler(90, 0, 0);
            overlay.transform.localScale = new Vector3(HexMetrics.InnerRadius * 1.8f, HexMetrics.InnerRadius * 1.8f, 1f);

            // Collider kaldir (raycast engellemez)
            Collider col = overlay.GetComponent<Collider>();
            if (col != null)
            {
                DestroyImmediate(col);
            }

            // Material ayarla
            Renderer rend = overlay.GetComponent<Renderer>();
            if (rend != null)
            {
                // Transparent material olustur
                Material mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
                if (mat != null)
                {
                    mat.SetFloat("_Surface", 1); // Transparent
                    mat.SetFloat("_Blend", 0);   // Alpha
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.renderQueue = 3000;
                    mat.color = color;
                }
                rend.material = mat;
                rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                rend.receiveShadows = false;
            }

            overlay.SetActive(false);
            return overlay;
        }

        /// <summary>
        /// Overlay gorunurlugunu guncelle
        /// </summary>
        private void UpdateOverlays()
        {
            if (highlightOverlay != null)
            {
                highlightOverlay.SetActive(isHighlighted && !isSelected && isExplored && isVisible);
            }

            if (selectionOverlay != null)
            {
                selectionOverlay.SetActive(isSelected && isExplored && isVisible);
            }

            // Fog of War
            UpdateFogOfWar();
        }

        private void UpdateFogOfWar()
        {
            if (renderers == null) return;

            foreach (var renderer in renderers)
            {
                if (renderer == null) continue;
                if (renderer.gameObject == highlightOverlay || renderer.gameObject == selectionOverlay)
                    continue;

                renderer.enabled = isExplored;

                // Gorunurluk icin karartma (sis etkisi)
                // MaterialPropertyBlock kullan - instance olusturmaz
                if (isExplored && !isVisible)
                {
                    // Fog tint uygula
                    MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                    renderer.GetPropertyBlock(propBlock);
                    propBlock.SetColor("_BaseColor", new Color(0.5f, 0.5f, 0.5f, 1f));
                    propBlock.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f, 1f));
                    renderer.SetPropertyBlock(propBlock);
                }
                else if (isExplored && isVisible)
                {
                    // Normal rengi geri yukle
                    renderer.SetPropertyBlock(null);
                }
            }
        }

        #region State Setters

        public void SetExplored(bool explored)
        {
            isExplored = explored;
            UpdateOverlays();
        }

        public void SetVisible(bool visible)
        {
            isVisible = visible;
            UpdateOverlays();
        }

        public void SetSelected(bool selected)
        {
            isSelected = selected;
            UpdateOverlays();
        }

        public void SetHighlighted(bool highlighted)
        {
            isHighlighted = highlighted;
            UpdateOverlays();
        }

        public void SetOwner(int playerId, int nationId)
        {
            ownerPlayerId = playerId;
            ownerNationId = nationId;
        }

        public void SetResource(ResourceType type)
        {
            hasResource = true;
            resourceType = type;
        }

        public void SetBuilding(GameObject building)
        {
            if (buildingObject != null)
            {
                Destroy(buildingObject);
            }

            hasBuilding = building != null;
            buildingObject = building;

            if (buildingObject != null)
            {
                buildingObject.transform.SetParent(transform);
                buildingObject.transform.localPosition = Vector3.zero;
            }
        }

        #endregion

        #region Properties

        public float GetMovementCost()
        {
            return TerrainProperties.GetMovementModifier(terrainType);
        }

        public float GetDefenseBonus()
        {
            return TerrainProperties.GetDefenseBonus(terrainType);
        }

        public bool IsPassable()
        {
            return TerrainProperties.IsPassable(terrainType);
        }

        #endregion

        #region Mouse Events

        private void OnMouseEnter()
        {
            if (isExplored && isVisible)
            {
                SetHighlighted(true);
            }
        }

        private void OnMouseExit()
        {
            SetHighlighted(false);
        }

        private void OnMouseUp()
        {
            if (!isExplored || !isVisible) return;

            // Kamera surukleniyorsa tile secme
            var cameraController = EmpireWars.CameraSystem.MapCameraController.Instance;
            if (cameraController != null && cameraController.IsDragging) return;

            // Selection event'i tetikle
            OnTileClicked?.Invoke(this);
            SetSelected(true);
        }

        #endregion

        // Static event - tile tiklandiginda tetiklenir
        public static event System.Action<HexTile> OnTileClicked;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.5f);

            // Komsulari goster
            for (int i = 0; i < 6; i++)
            {
                HexCoordinates neighbor = Coordinates.GetNeighbor(i);
                Vector3 neighborPos = neighbor.ToWorldPosition();
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, neighborPos);
            }
        }
#endif
    }

    /// <summary>
    /// Kaynak tipleri
    /// </summary>
    public enum ResourceType
    {
        None,
        Gold,
        Iron,
        Wood,
        Stone,
        Food,
        Gems
    }
}
