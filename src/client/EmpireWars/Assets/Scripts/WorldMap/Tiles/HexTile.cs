using UnityEngine;
using EmpireWars.Core;
using EmpireWars.Data;
using EmpireWars.WorldMap;
using TMPro;

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

        [Header("Mine Info")]
        [SerializeField] private int mineLevel;
        [SerializeField] private KingdomMapGenerator.MineType mineType;
        [SerializeField] private GameObject mineLevelIndicator;

        [Header("Building")]
        [SerializeField] private bool hasBuilding;
        [SerializeField] private GameObject buildingObject;
        [SerializeField] private int buildingLevel;
        [SerializeField] private string buildingType;
        [SerializeField] private GameObject buildingLevelIndicator;

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
        public int MineLevel => mineLevel;
        public KingdomMapGenerator.MineType MineType => mineType;
        public bool IsMine => mineLevel > 0;
        public int BuildingLevel => buildingLevel;
        public string BuildingType => buildingType;

        // Fog of War icin renderer cache
        private Renderer[] renderers;

        // Highlight renkleri - daha parlak ve gorunur
        private static readonly Color HighlightColor = new Color(0.2f, 0.7f, 1f, 0.6f);   // Mavi hover
        private static readonly Color SelectionColor = new Color(1f, 0.85f, 0.1f, 0.7f);  // Sari secim

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

            // Her zaman yeni hexagonal overlay olustur
            CreateOverlays();
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
            // Eski overlay'leri temizle (yeni hexagonal olanlar icin)
            if (highlightOverlay != null)
            {
                DestroyImmediate(highlightOverlay);
                highlightOverlay = null;
            }
            if (selectionOverlay != null)
            {
                DestroyImmediate(selectionOverlay);
                selectionOverlay = null;
            }

            // Highlight overlay (hover) - HEXAGONAL
            highlightOverlay = CreateOverlayQuad("HighlightOverlay", HighlightColor);

            // Selection overlay - HEXAGONAL
            selectionOverlay = CreateOverlayQuad("SelectionOverlay", SelectionColor);

            UpdateOverlays();
        }

        private GameObject CreateOverlayQuad(string name, Color color)
        {
            // Hexagonal mesh olustur (kare degil!)
            GameObject overlay = new GameObject(name);
            overlay.transform.SetParent(transform);
            overlay.transform.localPosition = new Vector3(0, 0.1f, 0); // Daha yukarda
            overlay.transform.localRotation = Quaternion.identity;

            // Mesh Filter ve Renderer ekle
            MeshFilter meshFilter = overlay.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = overlay.AddComponent<MeshRenderer>();

            // Hexagonal mesh olustur
            meshFilter.mesh = CreateHexMesh();

            // Transparent material olustur - Unlit/Color daha guvenilir
            Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
            if (shader == null)
            {
                shader = Shader.Find("Unlit/Color");
            }
            if (shader == null)
            {
                shader = Shader.Find("Sprites/Default");
            }

            Material mat = new Material(shader);
            mat.color = color;
            mat.SetFloat("_Cull", 0); // Cift tarafli (Off)
            mat.SetFloat("_Surface", 1); // Transparent
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.renderQueue = 3000;
            mat.EnableKeyword("_ALPHABLEND_ON");

            meshRenderer.material = mat;
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            meshRenderer.receiveShadows = false;

            overlay.SetActive(false);
            return overlay;
        }

        /// <summary>
        /// Hexagonal mesh olusturur (flat-top hexagon)
        /// </summary>
        private Mesh CreateHexMesh()
        {
            Mesh mesh = new Mesh();
            mesh.name = "HexOverlay";

            float radius = HexMetrics.OuterRadius * 0.98f; // Tile boyutuna yakin

            // 7 vertex: merkez + 6 kose
            Vector3[] vertices = new Vector3[7];
            vertices[0] = Vector3.zero; // Merkez

            // 6 kose (flat-top hexagon: 30 derece offset ile basla)
            for (int i = 0; i < 6; i++)
            {
                float angle = 60f * i - 30f; // Flat-top icin -30 derece offset
                float rad = Mathf.Deg2Rad * angle;
                vertices[i + 1] = new Vector3(
                    radius * Mathf.Cos(rad),
                    0f,
                    radius * Mathf.Sin(rad)
                );
            }

            // 6 ucgen (fan seklinde) - TERS winding order (yukaridan bakinca saat yonunun tersine)
            int[] triangles = new int[18];
            for (int i = 0; i < 6; i++)
            {
                triangles[i * 3] = 0;                       // Merkez
                triangles[i * 3 + 1] = (i + 1) % 6 + 1;     // Sonraki kose (TERS)
                triangles[i * 3 + 2] = i + 1;               // Mevcut kose (TERS)
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
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

        /// <summary>
        /// Maden bilgisini ayarla ve görsel gösterge oluştur
        /// </summary>
        public void SetMineInfo(int level, KingdomMapGenerator.MineType type)
        {
            mineLevel = level;
            mineType = type;

            if (level > 0)
            {
                CreateMineLevelIndicator();
            }
        }

        /// <summary>
        /// Maden seviyesi ve türünü gösteren 3D text oluştur
        /// </summary>
        private void CreateMineLevelIndicator()
        {
            if (mineLevelIndicator != null)
            {
                DestroyImmediate(mineLevelIndicator);
            }

            // 3D Text GameObject oluştur
            mineLevelIndicator = new GameObject("MineLevelIndicator");
            mineLevelIndicator.transform.SetParent(transform);
            mineLevelIndicator.transform.localPosition = new Vector3(0, 1.2f, 0);
            mineLevelIndicator.transform.localRotation = Quaternion.Euler(90f, 0, 0); // Yukarı bak

            // TextMeshPro component ekle
            TextMeshPro tmp = mineLevelIndicator.AddComponent<TextMeshPro>();

            // Maden türü adı + seviye
            string typeLabel = KingdomMapGenerator.GetMineTypeName(mineType);
            tmp.text = $"<size=80%>{typeLabel}</size>\n<b>Lv{mineLevel}</b>";

            // Maden türüne göre renk
            tmp.color = KingdomMapGenerator.GetMineTypeColor(mineType);
            tmp.fontSize = 2.5f;
            tmp.fontStyle = FontStyles.Bold;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.sortingOrder = 100;

            // Arka plan için outline - daha koyu
            tmp.outlineWidth = 0.25f;
            tmp.outlineColor = new Color32(0, 0, 0, 255);

            // Glow efekti
            tmp.fontMaterial.EnableKeyword("GLOW_ON");

            // RectTransform ayarla
            RectTransform rect = mineLevelIndicator.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.sizeDelta = new Vector2(2.5f, 2f);
            }
        }

        /// <summary>
        /// Bina bilgisini ayarla ve seviye göstergesi oluştur
        /// </summary>
        public void SetBuildingInfo(string type, int level)
        {
            buildingType = type;
            buildingLevel = level;
            hasBuilding = !string.IsNullOrEmpty(type);

            if (level > 0 && hasBuilding)
            {
                CreateBuildingLevelIndicator();
            }
        }

        /// <summary>
        /// Bina seviyesini gösteren 3D text oluştur
        /// </summary>
        private void CreateBuildingLevelIndicator()
        {
            if (buildingLevelIndicator != null)
            {
                DestroyImmediate(buildingLevelIndicator);
            }

            // 3D Text GameObject oluştur
            buildingLevelIndicator = new GameObject("BuildingLevelIndicator");
            buildingLevelIndicator.transform.SetParent(transform);
            buildingLevelIndicator.transform.localPosition = new Vector3(0, 2.0f, 0); // Binanın üstünde
            buildingLevelIndicator.transform.localRotation = Quaternion.Euler(90f, 0, 0);

            // TextMeshPro component ekle
            TextMeshPro tmp = buildingLevelIndicator.AddComponent<TextMeshPro>();

            // Bina adı + seviye
            string displayName = GetBuildingDisplayName(buildingType);
            tmp.text = $"<size=70%>{displayName}</size>\n<b>Lv.{buildingLevel}</b>";

            // Seviyeye göre renk (düşük=gri, yüksek=altın)
            tmp.color = GetBuildingLevelColor(buildingLevel);
            tmp.fontSize = 2.2f;
            tmp.fontStyle = FontStyles.Bold;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.sortingOrder = 100;

            // Outline
            tmp.outlineWidth = 0.2f;
            tmp.outlineColor = new Color32(0, 0, 0, 200);

            // RectTransform
            RectTransform rect = buildingLevelIndicator.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.sizeDelta = new Vector2(3f, 2f);
            }
        }

        /// <summary>
        /// Bina türüne göre Türkçe ad döndür
        /// </summary>
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
                "tower_cannon" => "Top Kulesi",
                "tower_catapult" => "Mancınık",
                "watchtower" => "Gözcü",
                "tower_a" or "tower_b" => "Kule",
                _ => baseName
            };
        }

        /// <summary>
        /// Seviyeye göre renk döndür (1-30)
        /// </summary>
        private static Color GetBuildingLevelColor(int level)
        {
            if (level >= 25) return new Color(1f, 0.84f, 0f);      // Altın (25-30)
            if (level >= 20) return new Color(0.8f, 0.5f, 1f);     // Mor (20-24)
            if (level >= 15) return new Color(0.2f, 0.6f, 1f);     // Mavi (15-19)
            if (level >= 10) return new Color(0.3f, 0.9f, 0.3f);   // Yeşil (10-14)
            if (level >= 5) return new Color(1f, 1f, 1f);          // Beyaz (5-9)
            return new Color(0.7f, 0.7f, 0.7f);                     // Gri (1-4)
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
