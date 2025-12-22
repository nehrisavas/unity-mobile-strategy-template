using UnityEngine;
using EmpireWars.Core;
using EmpireWars.Data;

namespace EmpireWars.Map
{
    /// <summary>
    /// Tek bir hex hucresi
    /// Dokuman referansi: docs/01-game-design/map/MAP-SYSTEM.md
    /// </summary>
    public class HexCell : MonoBehaviour
    {
        [Header("Koordinatlar")]
        [SerializeField] private HexCoordinates coordinates;

        [Header("Arazi")]
        [SerializeField] private TerrainType terrainType = TerrainType.Grass;

        [Header("Durum")]
        [SerializeField] private bool isExplored = false;
        [SerializeField] private bool isVisible = false;

        [Header("Sahiplik")]
        [SerializeField] private long occupantId = 0;
        [SerializeField] private OccupantType occupantType = OccupantType.Empty;

        [Header("Referanslar")]
        [SerializeField] private HexChunk parentChunk;
        [SerializeField] private GameObject terrainModel;
        [SerializeField] private GameObject occupantModel;
        [SerializeField] private GameObject fogOverlay;

        // Komsular
        private HexCell[] neighbors = new HexCell[6];

        #region Properties

        public HexCoordinates Coordinates
        {
            get => coordinates;
            set => coordinates = value;
        }

        public TerrainType TerrainType
        {
            get => terrainType;
            set
            {
                if (terrainType != value)
                {
                    terrainType = value;
                    UpdateTerrainVisual();
                }
            }
        }

        public bool IsExplored
        {
            get => isExplored;
            set
            {
                if (isExplored != value)
                {
                    isExplored = value;
                    UpdateFogVisual();
                }
            }
        }

        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    UpdateFogVisual();
                }
            }
        }

        public int Zone => coordinates.GetZone();

        public bool IsPassable => TerrainProperties.IsPassable(terrainType) && occupantType == OccupantType.Empty;

        public float MovementCost => TerrainProperties.GetMovementModifier(terrainType);

        public HexChunk ParentChunk
        {
            get => parentChunk;
            set => parentChunk = value;
        }

        #endregion

        #region Initialization

        public void Initialize(HexCoordinates coords, HexChunk chunk)
        {
            coordinates = coords;
            parentChunk = chunk;
            transform.localPosition = coords.ToWorldPosition();
            gameObject.name = $"Hex_{coords.Q}_{coords.R}";
        }

        #endregion

        #region Neighbors

        public HexCell GetNeighbor(int direction)
        {
            return neighbors[direction];
        }

        public void SetNeighbor(int direction, HexCell cell)
        {
            neighbors[direction] = cell;
            // Karsilikli baglanti
            if (cell != null)
            {
                cell.neighbors[(direction + 3) % 6] = this;
            }
        }

        public HexCell[] GetAllNeighbors()
        {
            return neighbors;
        }

        #endregion

        #region Occupant

        public void SetOccupant(OccupantType type, long id, GameObject model = null)
        {
            occupantType = type;
            occupantId = id;

            if (occupantModel != null)
            {
                Destroy(occupantModel);
            }

            if (model != null)
            {
                occupantModel = model;
                occupantModel.transform.SetParent(transform);
                occupantModel.transform.localPosition = Vector3.zero;
            }
        }

        public void ClearOccupant()
        {
            occupantType = OccupantType.Empty;
            occupantId = 0;

            if (occupantModel != null)
            {
                Destroy(occupantModel);
                occupantModel = null;
            }
        }

        public OccupantType GetOccupantType() => occupantType;
        public long GetOccupantId() => occupantId;

        #endregion

        #region Visuals

        public void SetTerrainModel(GameObject model)
        {
            if (terrainModel != null)
            {
                Destroy(terrainModel);
            }

            terrainModel = model;
            if (terrainModel != null)
            {
                terrainModel.transform.SetParent(transform);
                terrainModel.transform.localPosition = Vector3.zero;
                terrainModel.transform.localRotation = Quaternion.identity;
            }
        }

        private void UpdateTerrainVisual()
        {
            // WorldMapManager tarafindan cagrilacak
            if (parentChunk != null)
            {
                parentChunk.OnCellTerrainChanged(this);
            }
        }

        private void UpdateFogVisual()
        {
            if (fogOverlay != null)
            {
                if (!isExplored)
                {
                    // Tamamen sisli - siyah
                    fogOverlay.SetActive(true);
                    var renderer = fogOverlay.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = new Color(0, 0, 0, 1f);
                    }
                }
                else if (!isVisible)
                {
                    // Kesfedilmis ama gorunmuyor - gri
                    fogOverlay.SetActive(true);
                    var renderer = fogOverlay.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = new Color(0, 0, 0, 0.5f);
                    }
                }
                else
                {
                    // Gorunuyor - sis yok
                    fogOverlay.SetActive(false);
                }
            }
        }

        public void SetFogOverlay(GameObject fog)
        {
            fogOverlay = fog;
            if (fogOverlay != null)
            {
                fogOverlay.transform.SetParent(transform);
                fogOverlay.transform.localPosition = new Vector3(0, 0.1f, 0);
            }
            UpdateFogVisual();
        }

        #endregion

        #region Utility

        public int DistanceTo(HexCell other)
        {
            return coordinates.DistanceTo(other.coordinates);
        }

        public Vector3 GetWorldPosition()
        {
            return transform.position;
        }

        public override string ToString()
        {
            return $"HexCell {coordinates} - {terrainType}";
        }

        #endregion

        #region Selection (Editor/Debug)

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Vector3 center = transform.position;

            // Hex kenarlarini ciz
            for (int i = 0; i < 6; i++)
            {
                Vector3 corner1 = center + HexMetrics.GetCorner(i);
                Vector3 corner2 = center + HexMetrics.GetCorner(i + 1);
                Gizmos.DrawLine(corner1, corner2);
            }
        }

        #endregion
    }

    /// <summary>
    /// Hex uzerinde ne var
    /// </summary>
    public enum OccupantType : byte
    {
        Empty = 0,
        PlayerCity = 1,
        AllianceHQ = 2,
        AllianceTower = 3,
        AllianceFlag = 4,
        ResourceNode = 5,
        BarbarianCamp = 6,
        BarbarianFortress = 7,
        Monster = 8,
        HolySite = 9,
        KingdomCastle = 10,
        Army = 11
    }
}
