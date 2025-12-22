#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using EmpireWars.Map;
using EmpireWars.CameraSystem;
using EmpireWars.UI;

namespace EmpireWars.Editor
{
    /// <summary>
    /// Unity Editor - Dunya haritasi otomatik kurulum
    /// Menu: EmpireWars -> Setup World Map
    /// </summary>
    public class WorldMapSetup : EditorWindow
    {
        private bool createCamera = true;
        private bool createUI = true;
        private bool createLighting = true;
        private int testMapSize = 100; // Test icin kucuk harita

        [MenuItem("EmpireWars/Setup World Map Scene")]
        public static void ShowWindow()
        {
            GetWindow<WorldMapSetup>("World Map Setup");
        }

        private void OnGUI()
        {
            GUILayout.Label("Empire Wars - Dunya Haritasi Kurulumu", EditorStyles.boldLabel);
            GUILayout.Space(10);

            EditorGUILayout.HelpBox(
                "Bu arac dunya haritasi icin gerekli tum GameObject'leri olusturur.\n" +
                "Lutfen bos bir sahnede calistirin.",
                MessageType.Info
            );

            GUILayout.Space(10);

            createCamera = EditorGUILayout.Toggle("Kamera Olustur", createCamera);
            createUI = EditorGUILayout.Toggle("UI Olustur", createUI);
            createLighting = EditorGUILayout.Toggle("Isiklandirma Olustur", createLighting);

            GUILayout.Space(10);

            testMapSize = EditorGUILayout.IntSlider("Test Harita Boyutu", testMapSize, 20, 200);

            GUILayout.Space(20);

            if (GUILayout.Button("Sahneyi Kur", GUILayout.Height(40)))
            {
                SetupScene();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Sadece WorldMapManager Ekle", GUILayout.Height(30)))
            {
                CreateWorldMapManager();
            }
        }

        private void SetupScene()
        {
            // Mevcut objeleri temizleme uyarisi
            if (!EditorUtility.DisplayDialog(
                "Sahne Kurulumu",
                "Mevcut sahnede Empire Wars objelerini olusturacak. Devam edilsin mi?",
                "Evet",
                "Iptal"))
            {
                return;
            }

            // Ana container
            GameObject empireWarsRoot = new GameObject("===== EMPIRE WARS =====");

            // World Map Manager
            GameObject worldMapObj = CreateWorldMapManager();
            worldMapObj.transform.SetParent(empireWarsRoot.transform);

            // Kamera
            if (createCamera)
            {
                GameObject cameraObj = CreateMapCamera();
                cameraObj.transform.SetParent(empireWarsRoot.transform);
            }

            // Isiklandirma
            if (createLighting)
            {
                GameObject lightingObj = CreateLighting();
                lightingObj.transform.SetParent(empireWarsRoot.transform);
            }

            // UI
            if (createUI)
            {
                CreateUI();
            }

            // Ek sistemler
            CreateSupportingSystems(empireWarsRoot.transform);

            EditorUtility.DisplayDialog(
                "Kurulum Tamamlandi",
                "Dunya haritasi sahnesi basariyla kuruldu!\n\n" +
                "Play butonuna basarak haritayi gorebilirsiniz.\n\n" +
                "Not: KayKit asset'lerini prefab olarak atamak icin\n" +
                "WorldMapManager inspector'unda 'Arazi Modelleri'\n" +
                "bolumunu doldurun.",
                "Tamam"
            );

            // Sahneyi kaydet uyarisi
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene()
            );
        }

        private GameObject CreateWorldMapManager()
        {
            GameObject obj = new GameObject("WorldMapManager");
            WorldMapManager manager = obj.AddComponent<WorldMapManager>();

            // Varsayilan prefab olustur (bos hex)
            GameObject hexPrefab = CreateDefaultHexPrefab();
            SerializedObject serializedManager = new SerializedObject(manager);
            serializedManager.FindProperty("hexCellPrefab").objectReferenceValue = hexPrefab;
            serializedManager.FindProperty("generateOnStart").boolValue = true;
            serializedManager.ApplyModifiedProperties();

            Debug.Log("WorldMapManager olusturuldu");
            return obj;
        }

        private GameObject CreateDefaultHexPrefab()
        {
            // Assets/Prefabs klasoru olustur
            string prefabFolder = "Assets/Prefabs";
            if (!AssetDatabase.IsValidFolder(prefabFolder))
            {
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            }

            string hexFolder = prefabFolder + "/Hex";
            if (!AssetDatabase.IsValidFolder(hexFolder))
            {
                AssetDatabase.CreateFolder(prefabFolder, "Hex");
            }

            // Varsayilan hex prefab'i olustur
            GameObject hexObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            hexObj.name = "HexCell";
            hexObj.transform.localScale = new Vector3(1.7f, 0.1f, 1.7f);

            // HexCell component ekle
            hexObj.AddComponent<HexCell>();

            // Collider ayarla
            CapsuleCollider collider = hexObj.GetComponent<CapsuleCollider>();
            if (collider != null)
            {
                DestroyImmediate(collider);
            }
            BoxCollider boxCollider = hexObj.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(1.7f, 0.2f, 1.7f);

            // Prefab olarak kaydet
            string prefabPath = hexFolder + "/HexCell.prefab";
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(hexObj, prefabPath);

            // Sahnedeki objeyi sil
            DestroyImmediate(hexObj);

            Debug.Log($"Hex prefab olusturuldu: {prefabPath}");
            return prefab;
        }

        private GameObject CreateMapCamera()
        {
            // Mevcut Main Camera'yi bul veya olustur
            UnityEngine.Camera mainCamera = UnityEngine.Camera.main;
            GameObject cameraObj;

            if (mainCamera != null)
            {
                cameraObj = mainCamera.gameObject;
            }
            else
            {
                cameraObj = new GameObject("Main Camera");
                cameraObj.tag = "MainCamera";
                mainCamera = cameraObj.AddComponent<UnityEngine.Camera>();
            }

            // Kamera ayarlari
            mainCamera.orthographic = true;
            mainCamera.orthographicSize = 50f;
            mainCamera.nearClipPlane = 0.1f;
            mainCamera.farClipPlane = 1000f;
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
            mainCamera.backgroundColor = new Color(0.1f, 0.15f, 0.2f);

            // Pozisyon ve rotasyon (yukari bakan isometric)
            cameraObj.transform.position = new Vector3(0, 100, -50);
            cameraObj.transform.rotation = Quaternion.Euler(60, 0, 0);

            // Kamera kontrolcusu ekle
            if (cameraObj.GetComponent<MapCameraController>() == null)
            {
                cameraObj.AddComponent<MapCameraController>();
            }

            // Audio Listener ekle
            if (cameraObj.GetComponent<AudioListener>() == null)
            {
                cameraObj.AddComponent<AudioListener>();
            }

            Debug.Log("Harita kamerasi olusturuldu");
            return cameraObj;
        }

        private GameObject CreateLighting()
        {
            GameObject lightingRoot = new GameObject("Lighting");

            // Directional Light (Gunes)
            GameObject sunObj = new GameObject("Sun");
            sunObj.transform.SetParent(lightingRoot.transform);
            Light sunLight = sunObj.AddComponent<Light>();
            sunLight.type = LightType.Directional;
            sunLight.color = new Color(1f, 0.95f, 0.85f);
            sunLight.intensity = 1.2f;
            sunLight.shadows = LightShadows.Soft;
            sunObj.transform.rotation = Quaternion.Euler(50, -30, 0);

            // Ambient ayarlari
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.4f, 0.5f, 0.6f);
            RenderSettings.ambientEquatorColor = new Color(0.3f, 0.35f, 0.4f);
            RenderSettings.ambientGroundColor = new Color(0.2f, 0.2f, 0.25f);

            Debug.Log("Isiklandirma olusturuldu");
            return lightingRoot;
        }

        private void CreateUI()
        {
            // Canvas olustur veya bul
            Canvas canvas = FindObjectOfType<Canvas>();
            GameObject canvasObj;

            if (canvas == null)
            {
                canvasObj = new GameObject("Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }
            else
            {
                canvasObj = canvas.gameObject;
            }

            // EventSystem olustur
            if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
            {
                GameObject eventSystemObj = new GameObject("EventSystem");
                eventSystemObj.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystemObj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }

            // Tile Info Panel
            CreateTileInfoPanel(canvasObj.transform);

            // Mini Map placeholder
            CreateMiniMapPlaceholder(canvasObj.transform);

            Debug.Log("UI olusturuldu");
        }

        private void CreateTileInfoPanel(Transform canvasTransform)
        {
            // Panel root
            GameObject panelObj = new GameObject("TileInfoPanel");
            panelObj.transform.SetParent(canvasTransform, false);

            RectTransform rect = panelObj.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(1, 0);
            rect.anchoredPosition = new Vector2(-20, 20);
            rect.sizeDelta = new Vector2(300, 400);

            // Background
            UnityEngine.UI.Image bg = panelObj.AddComponent<UnityEngine.UI.Image>();
            bg.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);

            // CanvasGroup
            panelObj.AddComponent<CanvasGroup>();

            // TileInfoPanel component
            panelObj.AddComponent<TileInfoPanel>();

            // Baslik text
            GameObject titleObj = new GameObject("Title");
            titleObj.transform.SetParent(panelObj.transform, false);
            RectTransform titleRect = titleObj.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0, 1);
            titleRect.anchorMax = new Vector2(1, 1);
            titleRect.pivot = new Vector2(0.5f, 1);
            titleRect.anchoredPosition = new Vector2(0, -10);
            titleRect.sizeDelta = new Vector2(-20, 40);

            TMPro.TextMeshProUGUI titleText = titleObj.AddComponent<TMPro.TextMeshProUGUI>();
            titleText.text = "Tile Info";
            titleText.fontSize = 24;
            titleText.alignment = TMPro.TextAlignmentOptions.Center;
        }

        private void CreateMiniMapPlaceholder(Transform canvasTransform)
        {
            GameObject miniMapObj = new GameObject("MiniMap");
            miniMapObj.transform.SetParent(canvasTransform, false);

            RectTransform rect = miniMapObj.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 1);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(1, 1);
            rect.anchoredPosition = new Vector2(-20, -20);
            rect.sizeDelta = new Vector2(200, 200);

            UnityEngine.UI.Image bg = miniMapObj.AddComponent<UnityEngine.UI.Image>();
            bg.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

            // Placeholder text
            GameObject textObj = new GameObject("PlaceholderText");
            textObj.transform.SetParent(miniMapObj.transform, false);
            RectTransform textRect = textObj.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.sizeDelta = Vector2.zero;

            TMPro.TextMeshProUGUI text = textObj.AddComponent<TMPro.TextMeshProUGUI>();
            text.text = "Mini Map\n(Coming Soon)";
            text.fontSize = 18;
            text.alignment = TMPro.TextAlignmentOptions.Center;
        }

        private void CreateSupportingSystems(Transform parent)
        {
            // Fog of War Manager
            GameObject fogObj = new GameObject("FogOfWarManager");
            fogObj.transform.SetParent(parent);
            fogObj.AddComponent<FogOfWarManager>();

            // Pathfinder
            GameObject pathfinderObj = new GameObject("HexPathfinder");
            pathfinderObj.transform.SetParent(parent);
            pathfinderObj.AddComponent<HexPathfinder>();

            // Path Visualizer
            GameObject pathVisObj = new GameObject("PathVisualizer");
            pathVisObj.transform.SetParent(parent);
            pathVisObj.AddComponent<PathVisualizer>();

            // Selection Manager
            GameObject selectionObj = new GameObject("HexSelectionManager");
            selectionObj.transform.SetParent(parent);
            selectionObj.AddComponent<HexSelectionManager>();

            Debug.Log("Destek sistemleri olusturuldu");
        }
    }
}
#endif
