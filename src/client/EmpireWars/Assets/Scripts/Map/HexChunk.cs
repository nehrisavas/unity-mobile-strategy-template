using UnityEngine;
using EmpireWars.Core;
using System.Collections.Generic;

namespace EmpireWars.Map
{
    /// <summary>
    /// Hex chunk sistemi - Performans icin haritayi parcalara boler
    /// Her chunk 20x20 hex icerir
    /// Dokuman referansi: docs/01-game-design/map/MAP-SYSTEM.md
    /// </summary>
    public class HexChunk : MonoBehaviour
    {
        [Header("Chunk Bilgileri")]
        [SerializeField] private int chunkX;
        [SerializeField] private int chunkZ;
        [SerializeField] private bool isLoaded = false;
        [SerializeField] private bool isVisible = false;

        [Header("Hucreler")]
        private HexCell[,] cells;
        private List<HexCell> allCells = new List<HexCell>();

        [Header("Referanslar")]
        [SerializeField] private WorldMapManager worldMapManager;

        // Events
        public System.Action<HexChunk> OnChunkLoaded;
        public System.Action<HexChunk> OnChunkUnloaded;

        #region Properties

        public int ChunkX => chunkX;
        public int ChunkZ => chunkZ;
        public bool IsLoaded => isLoaded;
        public bool IsVisible => isVisible;
        public int CellCount => allCells.Count;

        #endregion

        #region Initialization

        public void Initialize(int x, int z, WorldMapManager manager)
        {
            chunkX = x;
            chunkZ = z;
            worldMapManager = manager;

            cells = new HexCell[HexMetrics.ChunkSizeX, HexMetrics.ChunkSizeZ];

            gameObject.name = $"Chunk_{x}_{z}";

            // Chunk pozisyonunu ayarla
            float worldX = (x * HexMetrics.ChunkSizeX - HexMetrics.MapWidth / 2) * HexMetrics.InnerRadius * 2f;
            float worldZ = (z * HexMetrics.ChunkSizeZ - HexMetrics.MapHeight / 2) * HexMetrics.OuterRadius * 1.5f;
            transform.position = new Vector3(worldX, 0, worldZ);
        }

        #endregion

        #region Cell Management

        public void AddCell(int localX, int localZ, HexCell cell)
        {
            cells[localX, localZ] = cell;
            allCells.Add(cell);
            cell.ParentChunk = this;
            cell.transform.SetParent(transform);
        }

        public HexCell GetCell(int localX, int localZ)
        {
            if (localX >= 0 && localX < HexMetrics.ChunkSizeX &&
                localZ >= 0 && localZ < HexMetrics.ChunkSizeZ)
            {
                return cells[localX, localZ];
            }
            return null;
        }

        public HexCell GetCell(HexCoordinates coords)
        {
            var (localX, localZ) = HexMetrics.GetLocalIndex(coords);
            return GetCell(localX, localZ);
        }

        public List<HexCell> GetAllCells()
        {
            return allCells;
        }

        #endregion

        #region Loading/Unloading

        public void Load()
        {
            if (isLoaded) return;

            isLoaded = true;
            gameObject.SetActive(true);

            // Tum hucreleri aktif et
            foreach (var cell in allCells)
            {
                if (cell != null)
                {
                    cell.gameObject.SetActive(true);
                }
            }

            OnChunkLoaded?.Invoke(this);
        }

        public void Unload()
        {
            if (!isLoaded) return;

            isLoaded = false;
            gameObject.SetActive(false);

            // Tum hucreleri deaktif et
            foreach (var cell in allCells)
            {
                if (cell != null)
                {
                    cell.gameObject.SetActive(false);
                }
            }

            OnChunkUnloaded?.Invoke(this);
        }

        public void SetVisibility(bool visible)
        {
            if (isVisible == visible) return;

            isVisible = visible;

            if (visible)
            {
                Load();
            }
            else
            {
                // Gorunmez ama yakin ise yuklu kalabilir
                // Unload sadece cok uzak chunk'lar icin
            }
        }

        #endregion

        #region Visual Updates

        public void OnCellTerrainChanged(HexCell cell)
        {
            // WorldMapManager'a bildir
            if (worldMapManager != null)
            {
                worldMapManager.OnCellTerrainChanged(cell);
            }
        }

        public void RefreshAllCells()
        {
            foreach (var cell in allCells)
            {
                if (cell != null)
                {
                    OnCellTerrainChanged(cell);
                }
            }
        }

        #endregion

        #region Utility

        public Vector3 GetCenterWorldPosition()
        {
            float centerX = transform.position.x + (HexMetrics.ChunkSizeX * HexMetrics.InnerRadius);
            float centerZ = transform.position.z + (HexMetrics.ChunkSizeZ * HexMetrics.OuterRadius * 0.75f);
            return new Vector3(centerX, 0, centerZ);
        }

        public Bounds GetBounds()
        {
            float width = HexMetrics.ChunkSizeX * HexMetrics.InnerRadius * 2f;
            float height = HexMetrics.ChunkSizeZ * HexMetrics.OuterRadius * 1.5f;
            Vector3 center = GetCenterWorldPosition();
            return new Bounds(center, new Vector3(width, 10f, height));
        }

        public bool IsInViewFrustum(UnityEngine.Camera camera)
        {
            if (camera == null) return false;

            Bounds bounds = GetBounds();
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, bounds);
        }

        #endregion

        #region Debug

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Bounds bounds = GetBounds();
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }

        #endregion
    }
}
