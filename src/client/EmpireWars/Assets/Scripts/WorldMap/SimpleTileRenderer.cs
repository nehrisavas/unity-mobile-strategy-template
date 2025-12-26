using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using EmpireWars.Core;
using EmpireWars.Data;

namespace EmpireWars.WorldMap
{
    /// <summary>
    /// Basit tile renderer - TÜM tile'ları TEK MESH'te birleştirir
    /// Bu sayede 1 draw call ile tüm harita çizilir
    /// Mafia City, Rise of Kingdoms gibi oyunlar bu yaklaşımı kullanır
    /// </summary>
    public class SimpleTileRenderer : MonoBehaviour
    {
        public static SimpleTileRenderer Instance { get; private set; }

        [Header("Settings")]
        [SerializeField] private Material tileMaterial;
        [SerializeField] private int viewRadius = 16; // Görünür tile yarıçapı (33x33 = 1089 tile)

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private Mesh combinedMesh;

        private List<Vector3> vertices = new List<Vector3>();
        private List<int> triangles = new List<int>();
        private List<Color> colors = new List<Color>();
        private List<Vector2> uvs = new List<Vector2>();

        private Vector2Int lastCenterTile;
        private bool needsRebuild = true;

        // Hex boyutları
        private const float HEX_SIZE = 1f;
        private static readonly float HEX_WIDTH = HEX_SIZE * 2f;
        private static readonly float HEX_HEIGHT = HEX_SIZE * Mathf.Sqrt(3f);

        // Terrain renkleri
        private static readonly Dictionary<TerrainType, Color> TerrainColors = new Dictionary<TerrainType, Color>
        {
            { TerrainType.Grass, new Color(0.4f, 0.7f, 0.3f) },
            { TerrainType.Water, new Color(0.2f, 0.5f, 0.8f) },
            { TerrainType.Forest, new Color(0.2f, 0.5f, 0.2f) },
            { TerrainType.Mountain, new Color(0.5f, 0.5f, 0.5f) },
            { TerrainType.Desert, new Color(0.9f, 0.8f, 0.5f) },
            { TerrainType.Snow, new Color(0.95f, 0.95f, 1f) },
            { TerrainType.Swamp, new Color(0.3f, 0.4f, 0.3f) },
            { TerrainType.Hill, new Color(0.6f, 0.5f, 0.4f) },
            { TerrainType.Coast, new Color(0.7f, 0.7f, 0.5f) },
            { TerrainType.Road, new Color(0.6f, 0.5f, 0.4f) },
            { TerrainType.Farm, new Color(0.8f, 0.7f, 0.3f) },
            { TerrainType.Mine, new Color(0.4f, 0.35f, 0.3f) },
            { TerrainType.GoldMine, new Color(1f, 0.85f, 0f) },
            { TerrainType.GemMine, new Color(0.8f, 0.2f, 0.8f) },
            { TerrainType.Quarry, new Color(0.5f, 0.5f, 0.5f) },
            { TerrainType.Bridge, new Color(0.5f, 0.4f, 0.3f) },
            { TerrainType.River, new Color(0.3f, 0.6f, 0.9f) },
        };

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            // Mesh components
            meshFilter = gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.AddComponent<MeshRenderer>();

            // Material oluştur (yoksa)
            if (tileMaterial == null)
            {
                // Önce custom shader'ı dene
                Shader shader = Shader.Find("EmpireWars/VertexColorUnlit");
                if (shader == null)
                {
                    shader = Shader.Find("Universal Render Pipeline/Unlit");
                }
                if (shader == null)
                {
                    shader = Shader.Find("Unlit/Color");
                }

                tileMaterial = new Material(shader);
                tileMaterial.enableInstancing = true;
                Debug.Log($"SimpleTileRenderer: Material oluşturuldu - Shader: {shader?.name ?? "NULL"}");
            }
            meshRenderer.material = tileMaterial;
            meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            meshRenderer.receiveShadows = false;

            combinedMesh = new Mesh();
            combinedMesh.name = "CombinedTileMesh";
            meshFilter.mesh = combinedMesh;
        }

        private void Start()
        {
            Debug.Log($"SimpleTileRenderer: START - viewRadius={viewRadius}, MapSize={GameConfig.MapWidth}x{GameConfig.MapHeight}");

            // İlk build
            RebuildMesh();

            Debug.Log($"SimpleTileRenderer: İlk mesh build tamamlandı - {vertices.Count / 7} tile");
        }

        private void Update()
        {
            // Kamera pozisyonuna göre merkez tile'ı bul
            Vector2Int centerTile = GetCenterTile();

            // Merkez değiştiyse mesh'i yeniden oluştur
            if (centerTile != lastCenterTile || needsRebuild)
            {
                lastCenterTile = centerTile;
                needsRebuild = false;
                RebuildMesh();
            }
        }

        private Vector2Int GetCenterTile()
        {
            Camera cam = Camera.main;
            if (cam == null) return Vector2Int.zero;

            Vector3 camPos = cam.transform.position;

            // World pozisyondan tile koordinatına çevir
            int q = Mathf.RoundToInt(camPos.x / (HEX_WIDTH * 0.75f));
            int r = Mathf.RoundToInt(camPos.z / HEX_HEIGHT);

            return new Vector2Int(q, r);
        }

        public void RebuildMesh()
        {
            vertices.Clear();
            triangles.Clear();
            colors.Clear();
            uvs.Clear();

            Vector2Int center = lastCenterTile;
            int radius = viewRadius;

            // Görünür alandaki tile'ları oluştur
            for (int dq = -radius; dq <= radius; dq++)
            {
                for (int dr = -radius; dr <= radius; dr++)
                {
                    int q = center.x + dq;
                    int r = center.y + dr;

                    // Sınır kontrolü
                    if (q < 0 || q >= GameConfig.MapWidth || r < 0 || r >= GameConfig.MapHeight)
                        continue;

                    // Tile verisini al
                    var tileData = KingdomMapGenerator.GetTileAt(q, r);

                    // Hex mesh ekle
                    AddHexToMesh(q, r, tileData.Terrain);
                }
            }

            // Mesh'i güncelle
            combinedMesh.Clear();
            combinedMesh.SetVertices(vertices);
            combinedMesh.SetTriangles(triangles, 0);
            combinedMesh.SetColors(colors);
            combinedMesh.SetUVs(0, uvs);
            combinedMesh.RecalculateNormals();
            combinedMesh.RecalculateBounds();

            // Sadece başlangıçta veya büyük değişikliklerde log
            if (vertices.Count > 0)
            {
                // Debug.Log($"SimpleTileRenderer: {vertices.Count / 7} tile, 1 draw call");
            }
        }

        private void AddHexToMesh(int q, int r, TerrainType terrain)
        {
            // Hex merkez pozisyonu (offset coordinates)
            float x = q * HEX_WIDTH * 0.75f;
            float z = r * HEX_HEIGHT + (q % 2 == 1 ? HEX_HEIGHT * 0.5f : 0);
            Vector3 center = new Vector3(x, 0, z);

            // Terrain rengi
            Color color = TerrainColors.ContainsKey(terrain) ? TerrainColors[terrain] : Color.magenta;

            int startVertex = vertices.Count;

            // Merkez vertex
            vertices.Add(center);
            colors.Add(color);
            uvs.Add(new Vector2(0.5f, 0.5f));

            // 6 köşe vertex
            for (int i = 0; i < 6; i++)
            {
                float angle = (60f * i - 30f) * Mathf.Deg2Rad;
                Vector3 corner = center + new Vector3(
                    HEX_SIZE * Mathf.Cos(angle),
                    0,
                    HEX_SIZE * Mathf.Sin(angle)
                );
                vertices.Add(corner);
                colors.Add(color);
                uvs.Add(new Vector2(0.5f + 0.5f * Mathf.Cos(angle), 0.5f + 0.5f * Mathf.Sin(angle)));
            }

            // 6 üçgen (fan)
            for (int i = 0; i < 6; i++)
            {
                triangles.Add(startVertex); // merkez
                triangles.Add(startVertex + 1 + i); // köşe i
                triangles.Add(startVertex + 1 + (i + 1) % 6); // köşe i+1
            }
        }

        public void ForceRebuild()
        {
            needsRebuild = true;
        }
    }
}
