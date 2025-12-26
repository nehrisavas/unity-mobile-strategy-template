using UnityEngine;
using EmpireWars.Core;
using EmpireWars.CameraSystem;

namespace EmpireWars.WorldMap
{
    /// <summary>
    /// 2D Hex Tilemap için basit bootstrap
    /// 3D sistemin yerine geçer - çok daha performanslı
    /// </summary>
    public class WorldMap2DBootstrap : MonoBehaviour
    {
        [Header("2D Settings")]
        [SerializeField] private int visibleRadius = 20;
        [SerializeField] private Material tileMaterial;

        [Header("Options")]
        [SerializeField] private bool createMinimap = true;
        [SerializeField] private bool createUI = true;

        private void Start()
        {
            Debug.Log("=== WorldMap 2D Bootstrap ===");

            // GameConfig başlat
            GameConfig.Initialize();

            // 2D Tilemap oluştur
            Create2DTilemap();

            // Kamera ayarla
            SetupCamera();

            // UI oluştur
            if (createUI)
            {
                CreateUI();
            }

            Debug.Log($"WorldMap2D: {GameConfig.MapWidth}x{GameConfig.MapHeight} harita, TEK DRAW CALL!");
        }

        private void Create2DTilemap()
        {
            // Tilemap objesi oluştur
            GameObject tilemapObj = new GameObject("HexTilemap2D");
            var tilemap = tilemapObj.AddComponent<HexTilemap2D>();

            // Material ata (opsiyonel)
            if (tileMaterial != null)
            {
                var renderer = tilemapObj.GetComponent<MeshRenderer>();
                renderer.material = tileMaterial;
            }

            // Visible radius ayarla
            tilemap.SetVisibleRadius(visibleRadius);

            Debug.Log($"2D Tilemap oluşturuldu - Görünür yarıçap: {visibleRadius}");
        }

        private void SetupCamera()
        {
            Camera cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError("Main Camera bulunamadı!");
                return;
            }

            // Orthographic kamera (2D için daha iyi)
            cam.orthographic = true;
            cam.orthographicSize = 30f;

            // Kamerayı harita merkezine taşı
            float centerX = GameConfig.MapWidth * 1.732f * 0.5f; // hex width
            float centerZ = GameConfig.MapHeight * 1.5f * 0.5f;  // hex height
            cam.transform.position = new Vector3(centerX, 50f, centerZ);
            cam.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // Yukarıdan bak

            // MapCameraController varsa kapat (2D için farklı kontrol gerekebilir)
            var camController = cam.GetComponent<MapCameraController>();
            if (camController != null)
            {
                // 2D modu için ayarla
                Debug.Log("MapCameraController 2D moda ayarlandı");
            }
        }

        private void CreateUI()
        {
            // Basit UI - FPS göstergesi
            GameObject canvasObj = new GameObject("Canvas2D");
            var canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();

            // FPS Text
            GameObject fpsObj = new GameObject("FPS");
            fpsObj.transform.SetParent(canvasObj.transform);
            var fpsText = fpsObj.AddComponent<UnityEngine.UI.Text>();
            fpsText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            fpsText.fontSize = 24;
            fpsText.color = Color.white;
            fpsText.alignment = TextAnchor.UpperLeft;

            var fpsRect = fpsText.GetComponent<RectTransform>();
            fpsRect.anchorMin = new Vector2(0, 1);
            fpsRect.anchorMax = new Vector2(0, 1);
            fpsRect.pivot = new Vector2(0, 1);
            fpsRect.anchoredPosition = new Vector2(10, -10);
            fpsRect.sizeDelta = new Vector2(200, 50);

            // FPS counter component
            fpsObj.AddComponent<FPSCounter>();
        }
    }

    /// <summary>
    /// Basit FPS sayacı
    /// </summary>
    public class FPSCounter : MonoBehaviour
    {
        private UnityEngine.UI.Text fpsText;
        private float deltaTime = 0f;
        private float updateInterval = 0.5f;
        private float timer = 0f;

        private void Start()
        {
            fpsText = GetComponent<UnityEngine.UI.Text>();
        }

        private void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            timer += Time.unscaledDeltaTime;

            if (timer >= updateInterval)
            {
                float fps = 1.0f / deltaTime;
                fpsText.text = $"FPS: {fps:F1}";
                timer = 0f;
            }
        }
    }
}
