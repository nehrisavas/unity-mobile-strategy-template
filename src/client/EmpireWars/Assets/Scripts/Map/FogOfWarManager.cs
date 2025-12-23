using UnityEngine;
using EmpireWars.Core;
using System.Collections.Generic;

namespace EmpireWars.Map
{
    /// <summary>
    /// Fog of War (Savas Sisi) sistemi
    /// Oyuncunun gorebildigi alanlari yonetir
    /// Dokuman referansi: docs/01-game-design/map/MAP-SYSTEM.md - Bolum 5
    ///
    /// Uc durum:
    /// 1. Kesfedilmemis (Siyah) - Hic gorulmemis
    /// 2. Kesfedilmis (Gri/Sisli) - Daha once gorulmus ama su an gorunmuyor
    /// 3. Gorunen (Acik) - Su an goruluyor
    /// </summary>
    public class FogOfWarManager : MonoBehaviour
    {
        public static FogOfWarManager Instance { get; private set; }

        [Header("Ayarlar")]
        [SerializeField] private bool fogEnabled = true;
        [SerializeField] private int defaultVisionRange = 3;
        [SerializeField] private float updateInterval = 0.5f;

        [Header("Gorseller")]
        [SerializeField] private GameObject fogTilePrefab;
        [SerializeField] private Material unexploredMaterial;
        [SerializeField] private Material exploredMaterial;
        [SerializeField] private Color unexploredColor = new Color(0, 0, 0, 1f);
        [SerializeField] private Color exploredColor = new Color(0, 0, 0, 0.5f);

        [Header("Debug")]
        [SerializeField] private bool showVisionRanges = false;
        [SerializeField] private int exploredCellCount = 0;
        [SerializeField] private int visibleCellCount = 0;

        // Gorus kaynaklari (sehirler, ordular, kuleler vb.)
        private List<VisionSource> visionSources = new List<VisionSource>();

        // Hucre durumu cache
        private HashSet<long> exploredCells = new HashSet<long>();
        private HashSet<long> visibleCells = new HashSet<long>();

        // Guncelleme zamanlama
        private float lastUpdateTime = 0f;
        private bool needsUpdate = true;

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
            // Baslangicta tum haritayi sisli yap
            if (fogEnabled)
            {
                InitializeFog();
            }
        }

        private void Update()
        {
            if (!fogEnabled) return;

            // Periyodik guncelleme
            if (needsUpdate || Time.time - lastUpdateTime >= updateInterval)
            {
                UpdateVisibility();
                lastUpdateTime = Time.time;
                needsUpdate = false;
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Fog of War sistemini baslatir
        /// Tum hucreleri kesfedilmemis olarak isaretle
        /// </summary>
        private void InitializeFog()
        {
            // Cache'leri temizle
            exploredCells.Clear();
            visibleCells.Clear();

            // WorldMapManager varsa, mevcut hucreleri sisli yap
            if (WorldMapManager.Instance != null)
            {
                // WorldMapManager'in hazir olmasini bekle
                StartCoroutine(InitializeFogDelayed());
            }
            else
            {
                Debug.Log("FogOfWarManager: WorldMapManager bulunamadi, fog baslangic durumu atandi.");
            }
        }

        /// <summary>
        /// WorldMapManager hazir oldugunda fog'u baslat
        /// </summary>
        private System.Collections.IEnumerator InitializeFogDelayed()
        {
            // WorldMapManager'in chunk'lari yuklemesini bekle
            yield return new WaitForSeconds(0.5f);

            if (WorldMapManager.Instance == null) yield break;

            // Varsayilan bir gorus kaynagi olustur (oyuncu baslangic noktasi)
            // Bu normalde oyuncu spawn oldugunda yapilir
            Debug.Log("FogOfWarManager: Fog of War sistemi baslatildi.");

            needsUpdate = true;
        }

        /// <summary>
        /// Yeni bir oyuncu gorus kaynagi olusturur
        /// </summary>
        public VisionSource CreatePlayerVisionSource(Vector3 position, int range = 0)
        {
            if (range <= 0) range = defaultVisionRange;

            var source = new VisionSource(VisionSourceType.City, 1, position, range);
            RegisterVisionSource(source);
            return source;
        }

        #endregion

        #region Vision Sources

        public void RegisterVisionSource(VisionSource source)
        {
            if (!visionSources.Contains(source))
            {
                visionSources.Add(source);
                needsUpdate = true;
                Debug.Log($"Vision source eklendi: {source.SourceType} at {source.Position} (Range: {source.VisionRange})");
            }
        }

        public void UnregisterVisionSource(VisionSource source)
        {
            if (visionSources.Remove(source))
            {
                needsUpdate = true;
                Debug.Log($"Vision source cikarildi: {source.SourceType}");
            }
        }

        public void UpdateVisionSource(VisionSource source)
        {
            needsUpdate = true;
        }

        #endregion

        #region Visibility Calculation

        private void UpdateVisibility()
        {
            // Onceki gorunur hucreleri temizle (ama kesfedilmis olarak kalsin)
            HashSet<long> previousVisible = new HashSet<long>(visibleCells);
            visibleCells.Clear();

            // Her gorus kaynagi icin gorunur hucreleri hesapla
            foreach (var source in visionSources)
            {
                if (source == null || !source.IsActive) continue;

                HexCoordinates sourceCoords = HexCoordinates.FromPosition(source.Position);
                int range = source.VisionRange;

                // Hex spiral ile tum hucreleri kontrol et
                var cellsInRange = GetCellsInVisionRange(sourceCoords, range);

                foreach (var coords in cellsInRange)
                {
                    long cellId = coords.ToUniqueId();

                    // Gorus hattini kontrol et (engel var mi?)
                    if (HasLineOfSight(sourceCoords, coords, source.CanSeeOverObstacles))
                    {
                        visibleCells.Add(cellId);
                        exploredCells.Add(cellId);
                    }
                }
            }

            // Degisen hucreleri guncelle
            UpdateChangedCells(previousVisible);

            // Debug istatistikleri
            exploredCellCount = exploredCells.Count;
            visibleCellCount = visibleCells.Count;
        }

        private List<HexCoordinates> GetCellsInVisionRange(HexCoordinates center, int range)
        {
            List<HexCoordinates> cells = new List<HexCoordinates>();

            for (int q = -range; q <= range; q++)
            {
                for (int r = Mathf.Max(-range, -q - range); r <= Mathf.Min(range, -q + range); r++)
                {
                    cells.Add(center + new HexCoordinates(q, r));
                }
            }

            return cells;
        }

        private bool HasLineOfSight(HexCoordinates from, HexCoordinates to, bool canSeeOverObstacles)
        {
            if (canSeeOverObstacles)
            {
                return true;
            }

            // Iki nokta arasindaki hucreleri kontrol et
            int distance = from.DistanceTo(to);
            if (distance <= 1)
            {
                return true;
            }

            // Lerp ile ara hucreleri kontrol et
            for (int i = 1; i < distance; i++)
            {
                float t = (float)i / distance;
                HexCoordinates midpoint = LerpHexCoordinates(from, to, t);

                HexCell cell = WorldMapManager.Instance?.GetCell(midpoint);
                if (cell != null && BlocksVision(cell.TerrainType))
                {
                    return false;
                }
            }

            return true;
        }

        private HexCoordinates LerpHexCoordinates(HexCoordinates a, HexCoordinates b, float t)
        {
            // Cube koordinatlara cevir
            float ax = a.Q;
            float ay = a.R;
            float az = -a.Q - a.R;

            float bx = b.Q;
            float by = b.R;
            float bz = -b.Q - b.R;

            // Lerp
            float x = Mathf.Lerp(ax, bx, t);
            float y = Mathf.Lerp(ay, by, t);
            float z = Mathf.Lerp(az, bz, t);

            // Round to nearest hex
            int rx = Mathf.RoundToInt(x);
            int ry = Mathf.RoundToInt(y);
            int rz = Mathf.RoundToInt(z);

            float xDiff = Mathf.Abs(rx - x);
            float yDiff = Mathf.Abs(ry - y);
            float zDiff = Mathf.Abs(rz - z);

            if (xDiff > yDiff && xDiff > zDiff)
            {
                rx = -ry - rz;
            }
            else if (yDiff > zDiff)
            {
                ry = -rx - rz;
            }

            return new HexCoordinates(rx, ry);
        }

        private bool BlocksVision(Data.TerrainType terrain)
        {
            return terrain == Data.TerrainType.Mountain ||
                   terrain == Data.TerrainType.Forest;
        }

        private void UpdateChangedCells(HashSet<long> previousVisible)
        {
            if (WorldMapManager.Instance == null) return;

            // Artik gorunmeyen hucreleri guncelle (kesfedilmis ama sisli)
            foreach (long cellId in previousVisible)
            {
                if (!visibleCells.Contains(cellId))
                {
                    // Bu hucre artik gorunmuyor
                    HexCoordinates coords = HexCoordinates.FromUniqueId(cellId);
                    HexCell cell = WorldMapManager.Instance.GetCell(coords);
                    if (cell != null)
                    {
                        cell.IsVisible = false;
                        cell.IsExplored = true;
                    }
                }
            }

            // Yeni gorunen hucreleri guncelle
            foreach (long cellId in visibleCells)
            {
                if (!previousVisible.Contains(cellId))
                {
                    // Bu hucre yeni gorunuyor
                    HexCoordinates coords = HexCoordinates.FromUniqueId(cellId);
                    HexCell cell = WorldMapManager.Instance.GetCell(coords);
                    if (cell != null)
                    {
                        cell.IsVisible = true;
                        cell.IsExplored = true;
                    }
                }
            }
        }

        #endregion

        #region Query Methods

        public bool IsCellVisible(HexCoordinates coords)
        {
            return visibleCells.Contains(coords.ToUniqueId());
        }

        public bool IsCellExplored(HexCoordinates coords)
        {
            return exploredCells.Contains(coords.ToUniqueId());
        }

        public FogState GetCellFogState(HexCoordinates coords)
        {
            long cellId = coords.ToUniqueId();

            if (visibleCells.Contains(cellId))
            {
                return FogState.Visible;
            }
            else if (exploredCells.Contains(cellId))
            {
                return FogState.Explored;
            }
            else
            {
                return FogState.Unexplored;
            }
        }

        public int GetVisibleCellCount()
        {
            return visibleCells.Count;
        }

        public int GetExploredCellCount()
        {
            return exploredCells.Count;
        }

        #endregion

        #region Manual Exploration

        public void ExploreCell(HexCoordinates coords)
        {
            long cellId = coords.ToUniqueId();
            if (exploredCells.Add(cellId))
            {
                HexCell cell = WorldMapManager.Instance?.GetCell(coords);
                if (cell != null)
                {
                    cell.IsExplored = true;
                }
            }
        }

        public void ExploreCellsInRange(HexCoordinates center, int range)
        {
            var cells = GetCellsInVisionRange(center, range);
            foreach (var coords in cells)
            {
                ExploreCell(coords);
            }
        }

        public void RevealAll()
        {
            fogEnabled = false;

            if (WorldMapManager.Instance == null) return;

            // Tum hucreleri goster
            int halfWidth = HexMetrics.MapWidth / 2;
            int halfHeight = HexMetrics.MapHeight / 2;

            for (int r = -halfHeight; r < halfHeight; r++)
            {
                for (int q = -halfWidth; q < halfWidth; q++)
                {
                    HexCoordinates coords = new HexCoordinates(q, r);
                    HexCell cell = WorldMapManager.Instance.GetCell(coords);
                    if (cell != null)
                    {
                        cell.IsExplored = true;
                        cell.IsVisible = true;
                        exploredCells.Add(coords.ToUniqueId());
                        visibleCells.Add(coords.ToUniqueId());
                    }
                }
            }
        }

        public void HideAll()
        {
            fogEnabled = true;
            exploredCells.Clear();
            visibleCells.Clear();
            needsUpdate = true;
        }

        #endregion

        #region Persistence

        public FogOfWarData ToData()
        {
            return new FogOfWarData
            {
                ExploredCellIds = new List<long>(exploredCells)
            };
        }

        public void FromData(FogOfWarData data)
        {
            exploredCells.Clear();
            if (data.ExploredCellIds != null)
            {
                foreach (long id in data.ExploredCellIds)
                {
                    exploredCells.Add(id);
                }
            }

            // Hucreleri guncelle
            if (WorldMapManager.Instance != null)
            {
                foreach (long cellId in exploredCells)
                {
                    HexCoordinates coords = HexCoordinates.FromUniqueId(cellId);
                    HexCell cell = WorldMapManager.Instance.GetCell(coords);
                    if (cell != null)
                    {
                        cell.IsExplored = true;
                    }
                }
            }

            needsUpdate = true;
        }

        #endregion

        #region Debug

        private void OnDrawGizmos()
        {
            if (!showVisionRanges) return;

            foreach (var source in visionSources)
            {
                if (source == null || !source.IsActive) continue;

                Gizmos.color = new Color(1, 1, 0, 0.3f);
                Gizmos.DrawWireSphere(source.Position, source.VisionRange * HexMetrics.OuterRadius * 2f);
            }
        }

        #endregion
    }

    /// <summary>
    /// Sis durumu
    /// </summary>
    public enum FogState : byte
    {
        Unexplored = 0,  // Siyah - hic gorulmemis
        Explored = 1,    // Gri - daha once gorulmus
        Visible = 2      // Acik - su an goruluyor
    }

    /// <summary>
    /// Gorus kaynagi (sehir, ordu, kule vb.)
    /// </summary>
    [System.Serializable]
    public class VisionSource
    {
        public VisionSourceType SourceType;
        public long SourceId;
        public Vector3 Position;
        public int VisionRange;
        public bool IsActive = true;
        public bool CanSeeOverObstacles = false;

        public VisionSource(VisionSourceType type, long id, Vector3 pos, int range)
        {
            SourceType = type;
            SourceId = id;
            Position = pos;
            VisionRange = range;
        }

        public void UpdatePosition(Vector3 newPosition)
        {
            Position = newPosition;
            FogOfWarManager.Instance?.UpdateVisionSource(this);
        }
    }

    /// <summary>
    /// Gorus kaynagi tipleri
    /// </summary>
    public enum VisionSourceType : byte
    {
        City = 0,
        Army = 1,
        Scout = 2,
        Tower = 3,
        AllianceFlag = 4,
        Watchtower = 5
    }

    /// <summary>
    /// Fog of War veri yapisi (kayit icin)
    /// </summary>
    [System.Serializable]
    public class FogOfWarData
    {
        public List<long> ExploredCellIds;
    }
}
