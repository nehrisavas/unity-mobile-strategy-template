using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using EmpireWars.Core;
using EmpireWars.WorldMap.Tiles;
using EmpireWars.CameraSystem;

namespace EmpireWars.Editor
{
    /// <summary>
    /// WorldMap sahnesini otomatik kurar
    /// Menu: Tools > EmpireWars > Setup WorldMap Scene
    /// </summary>
    public class WorldMapSceneSetup : EditorWindow
    {
        private const string DATABASE_PATH = "Assets/ScriptableObjects";

        [MenuItem("Tools/EmpireWars/Setup WorldMap Scene")]
        public static void SetupScene()
        {
            SetupSceneInternal(true);
        }

        [MenuItem("Tools/EmpireWars/Assign Databases Only")]
        public static void AssignDatabasesOnly()
        {
            SetupSceneInternal(false);
        }

        private static void SetupSceneInternal(bool createObjects)
        {
            // Database'leri bul
            var tilePrefabDb = AssetDatabase.LoadAssetAtPath<HexTilePrefabDatabase>($"{DATABASE_PATH}/HexTilePrefabDatabase.asset");
            var decorationDb = AssetDatabase.LoadAssetAtPath<TerrainDecorationDatabase>($"{DATABASE_PATH}/TerrainDecorationDatabase.asset");

            if (tilePrefabDb == null)
            {
                EditorUtility.DisplayDialog("Hata",
                    "HexTilePrefabDatabase bulunamadi!\n\n" +
                    "Once 'Tools > EmpireWars > Setup KayKit Databases' calistirin.",
                    "Tamam");
                return;
            }

            // WorldMapBootstrap objesi olustur veya bul
            var bootstrapObj = GameObject.Find("WorldMapBootstrap");
            if (bootstrapObj == null)
            {
                bootstrapObj = new GameObject("WorldMapBootstrap");
                Debug.Log("WorldMapSceneSetup: WorldMapBootstrap objesi olusturuldu");
            }

            // WorldMapBootstrap component ekle
            var bootstrap = bootstrapObj.GetComponent<WorldMapBootstrap>();
            if (bootstrap == null)
            {
                bootstrap = bootstrapObj.AddComponent<WorldMapBootstrap>();
            }

            // Database'leri ata (SerializedObject ile)
            SerializedObject serializedBootstrap = new SerializedObject(bootstrap);

            var tilePrefabDbProp = serializedBootstrap.FindProperty("tilePrefabDatabase");
            if (tilePrefabDbProp != null)
            {
                tilePrefabDbProp.objectReferenceValue = tilePrefabDb;
            }

            var decorationDbProp = serializedBootstrap.FindProperty("decorationDatabase");
            if (decorationDbProp != null)
            {
                decorationDbProp.objectReferenceValue = decorationDb;
            }

            // Harita boyutunu 60x60 yap (Kingdom Map)
            var mapWidthProp = serializedBootstrap.FindProperty("mapWidth");
            if (mapWidthProp != null) mapWidthProp.intValue = 60;

            var mapHeightProp = serializedBootstrap.FindProperty("mapHeight");
            if (mapHeightProp != null) mapHeightProp.intValue = 60;

            serializedBootstrap.ApplyModifiedProperties();

            // Kamera kontrol objesi olustur veya bul
            var cameraControllerObj = GameObject.Find("MapCameraController");
            if (cameraControllerObj == null)
            {
                cameraControllerObj = new GameObject("MapCameraController");
                Debug.Log("WorldMapSceneSetup: MapCameraController objesi olusturuldu");
            }

            // MapCameraController component ekle
            var cameraController = cameraControllerObj.GetComponent<MapCameraController>();
            if (cameraController == null)
            {
                cameraController = cameraControllerObj.AddComponent<MapCameraController>();
            }

            // Kamera pozisyonunu ayarla (Kingdom merkezi 30,30)
            cameraControllerObj.transform.position = new Vector3(45f, 40f, 45f);

            // Main Camera'yi bul ve ayarla
            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                var cameraObj = new GameObject("Main Camera");
                cameraObj.tag = "MainCamera";
                mainCamera = cameraObj.AddComponent<Camera>();
                cameraObj.AddComponent<AudioListener>();
                Debug.Log("WorldMapSceneSetup: Main Camera olusturuldu");
            }

            // Kamerayi isometric aciya ayarla
            mainCamera.transform.SetParent(cameraControllerObj.transform);
            mainCamera.transform.localPosition = Vector3.zero;
            mainCamera.transform.localRotation = Quaternion.Euler(60f, 0f, 0f);
            mainCamera.orthographic = true;
            mainCamera.orthographicSize = 15f;
            mainCamera.nearClipPlane = 0.1f;
            mainCamera.farClipPlane = 100f;

            // MapCameraController'a kamera referansini ata
            SerializedObject serializedCamera = new SerializedObject(cameraController);
            var targetCameraProp = serializedCamera.FindProperty("targetCamera");
            if (targetCameraProp != null)
            {
                targetCameraProp.objectReferenceValue = mainCamera;
            }

            // Harita sinirlarini ayarla (60x60 harita icin)
            var mapSizeProp = serializedCamera.FindProperty("mapSize");
            if (mapSizeProp != null)
            {
                mapSizeProp.vector2Value = new Vector2(120f, 120f);
            }

            serializedCamera.ApplyModifiedProperties();

            // Directional Light ekle (yoksa)
            var lights = GameObject.FindObjectsByType<Light>(FindObjectsSortMode.None);
            bool hasDirectionalLight = false;
            foreach (var light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    hasDirectionalLight = true;
                    break;
                }
            }

            if (!hasDirectionalLight)
            {
                var lightObj = new GameObject("Directional Light");
                var light = lightObj.AddComponent<Light>();
                light.type = LightType.Directional;
                light.color = new Color(1f, 0.95f, 0.85f);
                light.intensity = 1.2f;
                lightObj.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
                Debug.Log("WorldMapSceneSetup: Directional Light olusturuldu");
            }

            // Sahneyi dirty olarak isaretle
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            // Selection'i WorldMapBootstrap'e ayarla
            Selection.activeGameObject = bootstrapObj;

            EditorUtility.DisplayDialog("Basarili",
                "WorldMap sahnesi kuruldu!\n\n" +
                "- WorldMapBootstrap olusturuldu\n" +
                "- Database'ler atandi\n" +
                "- Kamera ayarlandi (Kingdom merkezine)\n" +
                "- 60x60 Kingdom harita boyutu ayarlandi\n\n" +
                "ONEMLI: HexTileFactory objesine BuildingDatabase atayin!\n\n" +
                "Simdi Play butonuna basarak test edebilirsiniz.\n" +
                "Sahneyi kaydetmeyi unutmayin (Ctrl+S)!",
                "Tamam");

            Debug.Log("WorldMapSceneSetup: Sahne kurulumu tamamlandi!");
        }

        [MenuItem("Tools/EmpireWars/Quick Play Test")]
        public static void QuickPlayTest()
        {
            // Once sahneyi kur
            SetupScene();

            // Sahneyi kaydet
            EditorSceneManager.SaveOpenScenes();

            // Play moduna gec
            EditorApplication.isPlaying = true;
        }
    }
}
