using UnityEngine;
using System.Collections.Generic;
using EmpireWars.Core;
using EmpireWars.Data;

namespace EmpireWars.WorldMap
{
    /// <summary>
    /// 2D Hex Tilemap - Tek mesh ile tüm haritayı çizer
    /// Mafia City / Rise of Kingdoms tarzı performans
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexTilemap2D : MonoBehaviour
    {
        public static HexTilemap2D Instance { get; private set; }

        [Header("Settings")]
        [SerializeField] private Material tileMaterial;
        [SerializeField] private int visibleRadius = 16; // Görünür tile yarıçapı
        [SerializeField] private float tileSize = 1f;

        [Header("Colors")]
        [SerializeField] private Color grassColor = new Color(0.4f, 0.7f, 0.3f);
        [SerializeField] private Color waterColor = new Color(0.2f, 0.5f, 0.8f);
        [SerializeField] private Color sandColor = new Color(0.9f, 0.85f, 0.6f);
        [SerializeField] private Color forestColor = new Color(0.2f, 0.5f, 0.2f);
        [SerializeField] private Color mountainColor = new Color(0.5f, 0.5f, 0.5f);
        [SerializeField] private Color snowColor = new Color(0.95f, 0.95f, 1f);
        [SerializeField] private Color swampColor = new Color(0.3f, 0.4f, 0.3f);
        [SerializeField] private Color roadColor = new Color(0.6f, 0.5f, 0.4f);

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private Mesh hexMesh;

        private List<Vector3> vertices = new List<Vector3>();
        private List<int> triangles = new List<int>();
        private List<Color> colors = new List<Color>();
        private List<Vector2> uvs = new List<Vector2>();

        private Vector2Int lastCenterTile;
        private Camera mainCamera;
        private bool needsRebuild = true;

        // Hex geometry constants
        private const float HEX_WIDTH_MULTIPLIER = 1.732f; // sqrt(3)
        private const float HEX_HEIGHT_MULTIPLIER = 1.5f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();

            hexMesh = new Mesh();
            hexMesh.name = "HexTilemap";
            meshFilter.mesh = hexMesh;

            if (tileMaterial != null)
            {
                meshRenderer.material = tileMaterial;
            }
            else
            {
                // Varsayılan unlit material
                meshRenderer.material = new Material(Shader.Find("Unlit/VertexColor"));
            }
        }

        private void Start()
        {
            mainCamera = Camera.main;
            KingdomMapGenerator.SetMapSize(GameConfig.MapWidth);

            // İlk build
            RebuildMesh();

            Debug.Log($"HexTilemap2D: {visibleRadius * 2 + 1}x{visibleRadius * 2 + 1} = {(visibleRadius * 2 + 1) * (visibleRadius * 2 + 1)} tile, TEK MESH!");
        }

        private void Update()
        {
            if (mainCamera == null) return;

            // Kamera pozisyonundan merkez tile'ı hesapla
            Vector3 camPos = mainCamera.transform.position;
            Vector2Int centerTile = WorldToTile(camPos);

            // Merkez değiştiyse mesh'i yeniden oluştur
            if (centerTile != lastCenterTile || needsRebuild)
            {
                lastCenterTile = centerTile;
                RebuildMesh();
                needsRebuild = false;
            }
        }

        private Vector2Int WorldToTile(Vector3 worldPos)
        {
            float hexWidth = tileSize * HEX_WIDTH_MULTIPLIER;
            float hexHeight = tileSize * HEX_HEIGHT_MULTIPLIER;

            int q = Mathf.RoundToInt(worldPos.x / hexWidth);
            int r = Mathf.RoundToInt(worldPos.z / hexHeight);

            return new Vector2Int(q, r);
        }

        private void RebuildMesh()
        {
            vertices.Clear();
            triangles.Clear();
            colors.Clear();
            uvs.Clear();

            int centerQ = lastCenterTile.x;
            int centerR = lastCenterTile.y;

            // Görünür alan içindeki tüm tile'ları ekle
            for (int dq = -visibleRadius; dq <= visibleRadius; dq++)
            {
                for (int dr = -visibleRadius; dr <= visibleRadius; dr++)
                {
                    int q = centerQ + dq;
                    int r = centerR + dr;

                    // Harita sınırları
                    if (q < 0 || q >= GameConfig.MapWidth || r < 0 || r >= GameConfig.MapHeight)
                        continue;

                    // Tile verisini al
                    var tileData = KingdomMapGenerator.GetTileAt(q, r);
                    Color tileColor = GetTerrainColor(tileData.Terrain);

                    // Hex mesh ekle
                    AddHexToMesh(q, r, tileColor);
                }
            }

            // Mesh'i güncelle
            hexMesh.Clear();
            hexMesh.SetVertices(vertices);
            hexMesh.SetTriangles(triangles, 0);
            hexMesh.SetColors(colors);
            hexMesh.SetUVs(0, uvs);
            hexMesh.RecalculateNormals();
            hexMesh.RecalculateBounds();
        }

        private void AddHexToMesh(int q, int r, Color color)
        {
            // Hex merkez pozisyonu
            float hexWidth = tileSize * HEX_WIDTH_MULTIPLIER;
            float hexHeight = tileSize * HEX_HEIGHT_MULTIPLIER;

            float x = q * hexWidth;
            float z = r * hexHeight;

            // Offset rows (hex grid stagger)
            if (r % 2 != 0)
            {
                x += hexWidth * 0.5f;
            }

            Vector3 center = new Vector3(x, 0, z);
            int startVertex = vertices.Count;

            // Hex köşeleri (6 köşe + merkez)
            vertices.Add(center); // Merkez
            colors.Add(color);
            uvs.Add(new Vector2(0.5f, 0.5f));

            for (int i = 0; i < 6; i++)
            {
                float angle = (60f * i - 30f) * Mathf.Deg2Rad;
                Vector3 corner = center + new Vector3(
                    tileSize * Mathf.Cos(angle),
                    0,
                    tileSize * Mathf.Sin(angle)
                );
                vertices.Add(corner);
                colors.Add(color);
                uvs.Add(new Vector2(
                    0.5f + 0.5f * Mathf.Cos(angle),
                    0.5f + 0.5f * Mathf.Sin(angle)
                ));
            }

            // Üçgenler (6 üçgen, fan şeklinde)
            for (int i = 0; i < 6; i++)
            {
                triangles.Add(startVertex); // Merkez
                triangles.Add(startVertex + 1 + i);
                triangles.Add(startVertex + 1 + (i + 1) % 6);
            }
        }

        private Color GetTerrainColor(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Grass => grassColor,
                TerrainType.Water => waterColor,
                TerrainType.Coast => sandColor,
                TerrainType.Desert => sandColor,
                TerrainType.Forest => forestColor,
                TerrainType.Mountain => mountainColor,
                TerrainType.Hill => new Color(0.45f, 0.55f, 0.35f),
                TerrainType.Snow => snowColor,
                TerrainType.Swamp => swampColor,
                TerrainType.Road => roadColor,
                TerrainType.Bridge => roadColor,
                TerrainType.Farm => new Color(0.7f, 0.65f, 0.3f),
                TerrainType.Mine => mountainColor,
                TerrainType.Quarry => new Color(0.6f, 0.55f, 0.5f),
                TerrainType.GoldMine => new Color(0.9f, 0.8f, 0.2f),
                TerrainType.GemMine => new Color(0.8f, 0.3f, 0.8f),
                _ => grassColor
            };
        }

        public void ForceRebuild()
        {
            needsRebuild = true;
        }

        public void SetVisibleRadius(int radius)
        {
            visibleRadius = Mathf.Clamp(radius, 4, 32);
            needsRebuild = true;
        }
    }
}
