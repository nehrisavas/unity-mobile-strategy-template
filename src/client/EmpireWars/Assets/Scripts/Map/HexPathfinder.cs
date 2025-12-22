using UnityEngine;
using EmpireWars.Core;
using EmpireWars.Data;
using System.Collections.Generic;

namespace EmpireWars.Map
{
    /// <summary>
    /// Hex grid icin A* Pathfinding sistemi
    /// Dokuman referansi: docs/01-game-design/map/MAP-SYSTEM.md - Bolum 5
    /// </summary>
    public class HexPathfinder : MonoBehaviour
    {
        public static HexPathfinder Instance { get; private set; }

        [Header("Ayarlar")]
        [SerializeField] private int maxSearchDistance = 500;
        [SerializeField] private int maxIterations = 10000;
        [SerializeField] private bool allowDiagonalMovement = true;

        [Header("Debug")]
        [SerializeField] private bool showPathGizmos = false;
        [SerializeField] private Color pathColor = Color.green;
        [SerializeField] private Color searchedColor = Color.yellow;

        // Son hesaplanan yol (debug icin)
        private List<HexCoordinates> lastPath = new List<HexCoordinates>();
        private HashSet<HexCoordinates> lastSearched = new HashSet<HexCoordinates>();

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
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Iki hex arasi en kisa yolu bul
        /// </summary>
        public PathResult FindPath(HexCoordinates start, HexCoordinates end, PathOptions options = null)
        {
            options ??= new PathOptions();

            lastPath.Clear();
            lastSearched.Clear();

            // Gecerlilik kontrolleri
            if (!IsValidCoordinate(start) || !IsValidCoordinate(end))
            {
                return new PathResult(PathStatus.InvalidCoordinates);
            }

            if (start == end)
            {
                return new PathResult(PathStatus.AlreadyAtDestination);
            }

            // Hedef gecilmez mi?
            HexCell endCell = WorldMapManager.Instance?.GetCell(end);
            if (endCell != null && !options.IgnorePassability && !TerrainProperties.IsPassable(endCell.TerrainType))
            {
                return new PathResult(PathStatus.DestinationBlocked);
            }

            // Mesafe cok uzak mi?
            int directDistance = start.DistanceTo(end);
            if (directDistance > maxSearchDistance)
            {
                return new PathResult(PathStatus.DestinationTooFar);
            }

            // A* algoritmasini baslat
            var path = CalculateAStarPath(start, end, options);

            if (path != null && path.Count > 0)
            {
                lastPath = path;
                return new PathResult(PathStatus.Success, path, CalculatePathCost(path));
            }

            return new PathResult(PathStatus.NoPathFound);
        }

        /// <summary>
        /// Belirli bir menzildeki tum erisebilir hucreleri bul
        /// </summary>
        public List<HexCoordinates> GetReachableCells(HexCoordinates start, int maxRange, PathOptions options = null)
        {
            options ??= new PathOptions();

            List<HexCoordinates> reachable = new List<HexCoordinates>();
            Dictionary<HexCoordinates, float> costSoFar = new Dictionary<HexCoordinates, float>();

            PriorityQueue<HexCoordinates> frontier = new PriorityQueue<HexCoordinates>();
            frontier.Enqueue(start, 0);
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                HexCoordinates current = frontier.Dequeue();

                foreach (HexCoordinates neighbor in current.GetAllNeighbors())
                {
                    if (!IsValidCoordinate(neighbor)) continue;

                    HexCell neighborCell = WorldMapManager.Instance?.GetCell(neighbor);
                    if (neighborCell == null) continue;

                    if (!options.IgnorePassability && !TerrainProperties.IsPassable(neighborCell.TerrainType))
                    {
                        continue;
                    }

                    float moveCost = GetMovementCost(neighborCell, options);
                    float newCost = costSoFar[current] + moveCost;

                    if (newCost <= maxRange && (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor]))
                    {
                        costSoFar[neighbor] = newCost;
                        frontier.Enqueue(neighbor, newCost);
                        reachable.Add(neighbor);
                    }
                }
            }

            return reachable;
        }

        /// <summary>
        /// Yolun toplam hareket maliyetini hesapla
        /// </summary>
        public float CalculatePathCost(List<HexCoordinates> path, PathOptions options = null)
        {
            if (path == null || path.Count == 0) return 0f;

            options ??= new PathOptions();
            float totalCost = 0f;

            for (int i = 1; i < path.Count; i++)
            {
                HexCell cell = WorldMapManager.Instance?.GetCell(path[i]);
                if (cell != null)
                {
                    totalCost += GetMovementCost(cell, options);
                }
                else
                {
                    totalCost += 1f;
                }
            }

            return totalCost;
        }

        #endregion

        #region A* Implementation

        private List<HexCoordinates> CalculateAStarPath(HexCoordinates start, HexCoordinates end, PathOptions options)
        {
            PriorityQueue<HexCoordinates> openSet = new PriorityQueue<HexCoordinates>();
            Dictionary<HexCoordinates, HexCoordinates> cameFrom = new Dictionary<HexCoordinates, HexCoordinates>();
            Dictionary<HexCoordinates, float> gScore = new Dictionary<HexCoordinates, float>();
            Dictionary<HexCoordinates, float> fScore = new Dictionary<HexCoordinates, float>();

            openSet.Enqueue(start, 0);
            gScore[start] = 0;
            fScore[start] = Heuristic(start, end);

            int iterations = 0;

            while (openSet.Count > 0 && iterations < maxIterations)
            {
                iterations++;

                HexCoordinates current = openSet.Dequeue();
                lastSearched.Add(current);

                if (current == end)
                {
                    return ReconstructPath(cameFrom, current);
                }

                foreach (HexCoordinates neighbor in current.GetAllNeighbors())
                {
                    if (!IsValidCoordinate(neighbor)) continue;

                    HexCell neighborCell = WorldMapManager.Instance?.GetCell(neighbor);
                    if (neighborCell == null) continue;

                    // Gecis kontrolu
                    if (!options.IgnorePassability)
                    {
                        if (!TerrainProperties.IsPassable(neighborCell.TerrainType))
                        {
                            continue;
                        }

                        // Dolu hucre kontrolu (hedef haric)
                        if (neighbor != end && neighborCell.GetOccupantType() != OccupantType.Empty)
                        {
                            if (!options.IgnoreOccupants)
                            {
                                continue;
                            }
                        }
                    }

                    float moveCost = GetMovementCost(neighborCell, options);
                    float tentativeGScore = gScore[current] + moveCost;

                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = tentativeGScore + Heuristic(neighbor, end);

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Enqueue(neighbor, fScore[neighbor]);
                        }
                    }
                }
            }

            // Yol bulunamadi
            return null;
        }

        private float Heuristic(HexCoordinates a, HexCoordinates b)
        {
            // Hex mesafesi (Manhattan benzeri)
            return a.DistanceTo(b);
        }

        private List<HexCoordinates> ReconstructPath(Dictionary<HexCoordinates, HexCoordinates> cameFrom, HexCoordinates current)
        {
            List<HexCoordinates> path = new List<HexCoordinates> { current };

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Insert(0, current);
            }

            return path;
        }

        #endregion

        #region Helper Methods

        private bool IsValidCoordinate(HexCoordinates coords)
        {
            int halfWidth = HexMetrics.MapWidth / 2;
            int halfHeight = HexMetrics.MapHeight / 2;

            return coords.Q >= -halfWidth && coords.Q < halfWidth &&
                   coords.R >= -halfHeight && coords.R < halfHeight;
        }

        private float GetMovementCost(HexCell cell, PathOptions options)
        {
            if (cell == null) return 1f;

            float baseCost = 1f;
            float terrainModifier = TerrainProperties.GetMovementModifier(cell.TerrainType);

            if (terrainModifier <= 0)
            {
                return float.MaxValue; // Gecilmez
            }

            // Arazi maliyeti (daha dusuk modifier = daha yuksek maliyet)
            baseCost = 1f / terrainModifier;

            // Bolge bonusu
            if (options.PreferSafeZones)
            {
                int zone = cell.Zone;
                baseCost *= zone switch
                {
                    4 => 0.8f, // Dis bolge tercih edilir (daha guvenli)
                    3 => 0.9f,
                    2 => 1.0f,
                    1 => 1.2f, // Merkez bolgeden kacin
                    _ => 1f
                };
            }

            // Dusuman bolgesinden kacin
            if (options.AvoidEnemyTerritory)
            {
                // TODO: Ittifak bolge kontrolu
            }

            return baseCost;
        }

        #endregion

        #region Debug

        private void OnDrawGizmos()
        {
            if (!showPathGizmos) return;

            // Aranan hucreleri goster
            Gizmos.color = new Color(searchedColor.r, searchedColor.g, searchedColor.b, 0.3f);
            foreach (var coords in lastSearched)
            {
                Vector3 pos = coords.ToWorldPosition();
                Gizmos.DrawSphere(pos, 0.5f);
            }

            // Yolu goster
            if (lastPath.Count > 0)
            {
                Gizmos.color = pathColor;
                for (int i = 0; i < lastPath.Count - 1; i++)
                {
                    Vector3 from = lastPath[i].ToWorldPosition();
                    Vector3 to = lastPath[i + 1].ToWorldPosition();
                    Gizmos.DrawLine(from + Vector3.up * 0.5f, to + Vector3.up * 0.5f);
                    Gizmos.DrawSphere(from + Vector3.up * 0.5f, 0.3f);
                }

                if (lastPath.Count > 0)
                {
                    Vector3 end = lastPath[lastPath.Count - 1].ToWorldPosition();
                    Gizmos.DrawSphere(end + Vector3.up * 0.5f, 0.5f);
                }
            }
        }

        #endregion
    }

    #region Supporting Types

    /// <summary>
    /// Yol bulma secenekleri
    /// </summary>
    [System.Serializable]
    public class PathOptions
    {
        public bool IgnorePassability = false;
        public bool IgnoreOccupants = false;
        public bool PreferSafeZones = false;
        public bool AvoidEnemyTerritory = false;
        public bool PreferRoads = true;
        public long AllianceId = 0;
    }

    /// <summary>
    /// Yol bulma sonucu
    /// </summary>
    public struct PathResult
    {
        public PathStatus Status;
        public List<HexCoordinates> Path;
        public float TotalCost;

        public bool IsSuccess => Status == PathStatus.Success;
        public int Length => Path?.Count ?? 0;

        public PathResult(PathStatus status, List<HexCoordinates> path = null, float cost = 0f)
        {
            Status = status;
            Path = path ?? new List<HexCoordinates>();
            TotalCost = cost;
        }
    }

    /// <summary>
    /// Yol bulma durumlari
    /// </summary>
    public enum PathStatus
    {
        Success,
        NoPathFound,
        InvalidCoordinates,
        DestinationBlocked,
        DestinationTooFar,
        AlreadyAtDestination
    }

    /// <summary>
    /// Basit oncelik kuyrugu implementasyonu
    /// </summary>
    public class PriorityQueue<T>
    {
        private List<(T item, float priority)> elements = new List<(T, float)>();
        private HashSet<T> itemSet = new HashSet<T>();

        public int Count => elements.Count;

        public void Enqueue(T item, float priority)
        {
            elements.Add((item, priority));
            itemSet.Add(item);
            BubbleUp(elements.Count - 1);
        }

        public T Dequeue()
        {
            if (elements.Count == 0)
            {
                throw new System.InvalidOperationException("Queue is empty");
            }

            T item = elements[0].item;
            int lastIndex = elements.Count - 1;

            elements[0] = elements[lastIndex];
            elements.RemoveAt(lastIndex);
            itemSet.Remove(item);

            if (elements.Count > 0)
            {
                BubbleDown(0);
            }

            return item;
        }

        public bool Contains(T item)
        {
            return itemSet.Contains(item);
        }

        private void BubbleUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (elements[index].priority >= elements[parentIndex].priority)
                {
                    break;
                }

                var temp = elements[index];
                elements[index] = elements[parentIndex];
                elements[parentIndex] = temp;

                index = parentIndex;
            }
        }

        private void BubbleDown(int index)
        {
            while (true)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int smallest = index;

                if (leftChild < elements.Count && elements[leftChild].priority < elements[smallest].priority)
                {
                    smallest = leftChild;
                }

                if (rightChild < elements.Count && elements[rightChild].priority < elements[smallest].priority)
                {
                    smallest = rightChild;
                }

                if (smallest == index)
                {
                    break;
                }

                var temp = elements[index];
                elements[index] = elements[smallest];
                elements[smallest] = temp;

                index = smallest;
            }
        }
    }

    #endregion
}
