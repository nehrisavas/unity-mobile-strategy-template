using UnityEngine;
using EmpireWars.Core;
using System.Collections.Generic;

namespace EmpireWars.Map
{
    /// <summary>
    /// Yol gorsellestirme sistemi
    /// Secilen hedef icin olasi yolu gosterir
    /// </summary>
    public class PathVisualizer : MonoBehaviour
    {
        public static PathVisualizer Instance { get; private set; }

        [Header("Cizgi Ayarlari")]
        [SerializeField] private LineRenderer pathLine;
        [SerializeField] private Color pathColor = new Color(0.2f, 0.8f, 0.2f, 0.8f);
        [SerializeField] private Color blockedColor = new Color(0.8f, 0.2f, 0.2f, 0.8f);
        [SerializeField] private float lineWidth = 0.3f;
        [SerializeField] private float lineHeight = 0.5f;

        [Header("Isaret Ayarlari")]
        [SerializeField] private GameObject waypointPrefab;
        [SerializeField] private GameObject destinationPrefab;
        [SerializeField] private Transform waypointsContainer;

        [Header("Animasyon")]
        [SerializeField] private float pulseSpeed = 2f;
        [SerializeField] private float pulseMinAlpha = 0.4f;
        [SerializeField] private float pulseMaxAlpha = 1f;

        [Header("Arazi Vurgulama")]
        [SerializeField] private GameObject reachableHighlightPrefab;
        [SerializeField] private Color reachableColor = new Color(0.3f, 0.9f, 0.3f, 0.3f);
        [SerializeField] private Color attackableColor = new Color(0.9f, 0.3f, 0.3f, 0.3f);

        // Internal
        private List<HexCoordinates> currentPath;
        private List<GameObject> waypointObjects = new List<GameObject>();
        private List<GameObject> highlightObjects = new List<GameObject>();
        private GameObject destinationObject;
        private Material lineMaterial;
        private bool isAnimating = false;

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
            InitializeLineRenderer();
        }

        private void Update()
        {
            if (isAnimating)
            {
                AnimatePath();
            }
        }

        #endregion

        #region Initialization

        private void InitializeLineRenderer()
        {
            if (pathLine == null)
            {
                GameObject lineObj = new GameObject("PathLine");
                lineObj.transform.SetParent(transform);
                pathLine = lineObj.AddComponent<LineRenderer>();
            }

            // Material ayarla
            lineMaterial = new Material(Shader.Find("Sprites/Default"));
            pathLine.material = lineMaterial;
            pathLine.startWidth = lineWidth;
            pathLine.endWidth = lineWidth;
            pathLine.startColor = pathColor;
            pathLine.endColor = pathColor;
            pathLine.positionCount = 0;
            pathLine.useWorldSpace = true;

            // Container olustur
            if (waypointsContainer == null)
            {
                waypointsContainer = new GameObject("Waypoints").transform;
                waypointsContainer.SetParent(transform);
            }

            Hide();
        }

        #endregion

        #region Show Path

        public void ShowPath(List<HexCoordinates> path, bool isValid = true)
        {
            if (path == null || path.Count == 0)
            {
                Hide();
                return;
            }

            currentPath = path;
            ClearWaypoints();

            // Cizgi ayarla
            pathLine.positionCount = path.Count;
            Color lineColor = isValid ? pathColor : blockedColor;
            pathLine.startColor = lineColor;
            pathLine.endColor = lineColor;

            // Pozisyonlari ayarla
            for (int i = 0; i < path.Count; i++)
            {
                Vector3 worldPos = path[i].ToWorldPosition();
                worldPos.y = lineHeight;
                pathLine.SetPosition(i, worldPos);

                // Waypoint ekle (baslangic ve bitis haric)
                if (i > 0 && i < path.Count - 1 && waypointPrefab != null)
                {
                    GameObject waypoint = Instantiate(waypointPrefab, waypointsContainer);
                    waypoint.transform.position = worldPos;
                    waypointObjects.Add(waypoint);
                }
            }

            // Hedef isareti
            if (destinationPrefab != null && path.Count > 0)
            {
                Vector3 destPos = path[path.Count - 1].ToWorldPosition();
                destPos.y = lineHeight;

                if (destinationObject == null)
                {
                    destinationObject = Instantiate(destinationPrefab, waypointsContainer);
                }
                destinationObject.transform.position = destPos;
                destinationObject.SetActive(true);
            }

            pathLine.enabled = true;
            isAnimating = true;
        }

        public void ShowPathFromResult(PathResult result)
        {
            ShowPath(result.Path, result.IsSuccess);
        }

        #endregion

        #region Show Reachable

        public void ShowReachableCells(List<HexCoordinates> cells, bool isAttackRange = false)
        {
            ClearHighlights();

            if (cells == null || cells.Count == 0 || reachableHighlightPrefab == null)
            {
                return;
            }

            Color highlightColor = isAttackRange ? attackableColor : reachableColor;

            foreach (var coords in cells)
            {
                Vector3 worldPos = coords.ToWorldPosition();
                worldPos.y = 0.1f;

                GameObject highlight = Instantiate(reachableHighlightPrefab, waypointsContainer);
                highlight.transform.position = worldPos;

                // Renk ayarla
                Renderer renderer = highlight.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = highlightColor;
                }

                highlightObjects.Add(highlight);
            }
        }

        public void ShowMovementRange(HexCoordinates center, int range)
        {
            if (HexPathfinder.Instance == null) return;

            var reachableCells = HexPathfinder.Instance.GetReachableCells(center, range);
            ShowReachableCells(reachableCells, false);
        }

        public void ShowAttackRange(HexCoordinates center, int range)
        {
            List<HexCoordinates> attackCells = new List<HexCoordinates>();

            for (int q = -range; q <= range; q++)
            {
                for (int r = Mathf.Max(-range, -q - range); r <= Mathf.Min(range, -q + range); r++)
                {
                    if (q == 0 && r == 0) continue;
                    attackCells.Add(center + new HexCoordinates(q, r));
                }
            }

            ShowReachableCells(attackCells, true);
        }

        #endregion

        #region Hide

        public void Hide()
        {
            if (pathLine != null)
            {
                pathLine.enabled = false;
                pathLine.positionCount = 0;
            }

            ClearWaypoints();
            ClearHighlights();
            currentPath = null;
            isAnimating = false;
        }

        private void ClearWaypoints()
        {
            foreach (var waypoint in waypointObjects)
            {
                if (waypoint != null)
                {
                    Destroy(waypoint);
                }
            }
            waypointObjects.Clear();

            if (destinationObject != null)
            {
                destinationObject.SetActive(false);
            }
        }

        private void ClearHighlights()
        {
            foreach (var highlight in highlightObjects)
            {
                if (highlight != null)
                {
                    Destroy(highlight);
                }
            }
            highlightObjects.Clear();
        }

        #endregion

        #region Animation

        private void AnimatePath()
        {
            if (pathLine == null) return;

            // Pulse efekti
            float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
            float alpha = Mathf.Lerp(pulseMinAlpha, pulseMaxAlpha, t);

            Color currentColor = pathLine.startColor;
            currentColor.a = alpha;
            pathLine.startColor = currentColor;
            pathLine.endColor = currentColor;

            // Waypoint'leri de anımasyonla
            foreach (var waypoint in waypointObjects)
            {
                if (waypoint == null) continue;

                Renderer renderer = waypoint.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Color color = renderer.material.color;
                    color.a = alpha;
                    renderer.material.color = color;
                }
            }
        }

        #endregion

        #region Utility

        public bool IsShowing()
        {
            return pathLine != null && pathLine.enabled;
        }

        public int GetPathLength()
        {
            return currentPath?.Count ?? 0;
        }

        public List<HexCoordinates> GetCurrentPath()
        {
            return currentPath;
        }

        public float GetPathCost()
        {
            if (currentPath == null || HexPathfinder.Instance == null)
            {
                return 0f;
            }

            return HexPathfinder.Instance.CalculatePathCost(currentPath);
        }

        #endregion
    }
}
