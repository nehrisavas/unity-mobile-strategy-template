using UnityEngine;
using UnityEditor;
using System.IO;
using EmpireWars.WorldMap.Tiles;

namespace EmpireWars.Editor
{
    /// <summary>
    /// KayKit Medieval Hexagon Pack FBX dosyalarini prefab'lara donusturur
    /// ve HexTilePrefabDatabase'i otomatik doldurur
    /// </summary>
    public class HexTilePrefabSetup : EditorWindow
    {
        private const string KayKitPath = "Assets/KayKit_Medieval_Hexagon";
        private const string PrefabOutputPath = "Assets/Prefabs/HexTiles";
        private const string DatabasePath = "Assets/ScriptableObjects";

        private HexTilePrefabDatabase targetDatabase;

        [MenuItem("EmpireWars/Hex Tile Prefab Setup")]
        public static void ShowWindow()
        {
            GetWindow<HexTilePrefabSetup>("Hex Tile Prefab Setup");
        }

        private void OnGUI()
        {
            GUILayout.Label("KayKit Hex Tile Prefab Setup", EditorStyles.boldLabel);
            GUILayout.Space(10);

            targetDatabase = (HexTilePrefabDatabase)EditorGUILayout.ObjectField(
                "Target Database",
                targetDatabase,
                typeof(HexTilePrefabDatabase),
                false
            );

            GUILayout.Space(10);

            if (GUILayout.Button("Create Prefab Database"))
            {
                CreatePrefabDatabase();
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Setup Prefabs from KayKit"))
            {
                SetupPrefabsFromKayKit();
            }

            GUILayout.Space(5);

            if (targetDatabase != null && GUILayout.Button("Auto-Assign Prefabs to Database"))
            {
                AutoAssignPrefabsToDatabase();
            }

            GUILayout.Space(20);
            GUILayout.Label("Quick Actions", EditorStyles.boldLabel);

            if (GUILayout.Button("Create Test Hex Grid in Scene"))
            {
                CreateTestHexGrid();
            }

            if (GUILayout.Button("Clear All Hex Tiles in Scene"))
            {
                ClearHexTiles();
            }
        }

        private void CreatePrefabDatabase()
        {
            // Klasoru olustur
            if (!AssetDatabase.IsValidFolder(DatabasePath))
            {
                string[] folders = DatabasePath.Split('/');
                string currentPath = folders[0];
                for (int i = 1; i < folders.Length; i++)
                {
                    string nextPath = currentPath + "/" + folders[i];
                    if (!AssetDatabase.IsValidFolder(nextPath))
                    {
                        AssetDatabase.CreateFolder(currentPath, folders[i]);
                    }
                    currentPath = nextPath;
                }
            }

            // ScriptableObject olustur
            HexTilePrefabDatabase database = ScriptableObject.CreateInstance<HexTilePrefabDatabase>();
            string assetPath = $"{DatabasePath}/HexTilePrefabDatabase.asset";

            AssetDatabase.CreateAsset(database, assetPath);
            AssetDatabase.SaveAssets();

            targetDatabase = database;
            Selection.activeObject = database;

            Debug.Log($"HexTilePrefabDatabase olusturuldu: {assetPath}");
        }

        private void SetupPrefabsFromKayKit()
        {
            // Prefab klasorunu olustur
            if (!AssetDatabase.IsValidFolder(PrefabOutputPath))
            {
                string[] folders = PrefabOutputPath.Split('/');
                string currentPath = folders[0];
                for (int i = 1; i < folders.Length; i++)
                {
                    string nextPath = currentPath + "/" + folders[i];
                    if (!AssetDatabase.IsValidFolder(nextPath))
                    {
                        AssetDatabase.CreateFolder(currentPath, folders[i]);
                    }
                    currentPath = nextPath;
                }
            }

            // Alt klasorleri olustur
            CreateFolderIfNeeded($"{PrefabOutputPath}/Base");
            CreateFolderIfNeeded($"{PrefabOutputPath}/Roads");
            CreateFolderIfNeeded($"{PrefabOutputPath}/Rivers");
            CreateFolderIfNeeded($"{PrefabOutputPath}/Coast");

            int prefabCount = 0;

            // Base tiles
            prefabCount += CreatePrefabsFromFolder($"{KayKitPath}/tiles/base", $"{PrefabOutputPath}/Base");

            // Road tiles
            prefabCount += CreatePrefabsFromFolder($"{KayKitPath}/tiles/roads", $"{PrefabOutputPath}/Roads");

            // River tiles
            prefabCount += CreatePrefabsFromFolder($"{KayKitPath}/tiles/rivers", $"{PrefabOutputPath}/Rivers");

            // Coast tiles
            prefabCount += CreatePrefabsFromFolder($"{KayKitPath}/tiles/coast", $"{PrefabOutputPath}/Coast");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Toplam {prefabCount} prefab olusturuldu.");
        }

        private int CreatePrefabsFromFolder(string sourcePath, string targetPath)
        {
            if (!AssetDatabase.IsValidFolder(sourcePath))
            {
                Debug.LogWarning($"Klasor bulunamadi: {sourcePath}");
                return 0;
            }

            string[] guids = AssetDatabase.FindAssets("t:Model", new[] { sourcePath });
            int count = 0;

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

                if (model != null)
                {
                    string prefabName = Path.GetFileNameWithoutExtension(assetPath);
                    string prefabPath = $"{targetPath}/{prefabName}.prefab";

                    // Prefab zaten varsa atla
                    if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
                    {
                        continue;
                    }

                    // Model'i instantiate et
                    GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(model);

                    // Collider ekle (yoksa)
                    if (instance.GetComponent<Collider>() == null)
                    {
                        MeshCollider collider = instance.AddComponent<MeshCollider>();
                        collider.convex = true;
                    }

                    // Prefab olarak kaydet
                    GameObject prefab = PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
                    DestroyImmediate(instance);

                    count++;
                }
            }

            return count;
        }

        private void AutoAssignPrefabsToDatabase()
        {
            if (targetDatabase == null)
            {
                Debug.LogError("Hedef database secilmedi!");
                return;
            }

            // Base tiles
            targetDatabase.grassTile = LoadPrefab($"{PrefabOutputPath}/Base/hex_grass.prefab");
            targetDatabase.waterTile = LoadPrefab($"{PrefabOutputPath}/Base/hex_water.prefab");
            targetDatabase.defaultTile = targetDatabase.grassTile;

            // Forest icin grass kullan (uzerine agac eklenecek)
            targetDatabase.forestTile = targetDatabase.grassTile;
            targetDatabase.plainsTile = targetDatabase.grassTile;
            targetDatabase.swampTile = targetDatabase.grassTile;

            // Road tiles
            var roadTiles = new System.Collections.Generic.List<GameObject>();
            for (char c = 'A'; c <= 'M'; c++)
            {
                GameObject road = LoadPrefab($"{PrefabOutputPath}/Roads/hex_road_{c}.prefab");
                if (road != null) roadTiles.Add(road);
            }
            targetDatabase.roadTiles = roadTiles.ToArray();

            // River tiles
            var riverTiles = new System.Collections.Generic.List<GameObject>();
            for (char c = 'A'; c <= 'L'; c++)
            {
                GameObject river = LoadPrefab($"{PrefabOutputPath}/Rivers/hex_river_{c}.prefab");
                if (river != null) riverTiles.Add(river);
            }
            targetDatabase.riverTiles = riverTiles.ToArray();

            // Coast tiles
            var coastTiles = new System.Collections.Generic.List<GameObject>();
            for (char c = 'A'; c <= 'E'; c++)
            {
                GameObject coast = LoadPrefab($"{PrefabOutputPath}/Coast/hex_coast_{c}.prefab");
                if (coast != null) coastTiles.Add(coast);
            }
            targetDatabase.coastTiles = coastTiles.ToArray();

            EditorUtility.SetDirty(targetDatabase);
            AssetDatabase.SaveAssets();

            Debug.Log("Database prefab'larla dolduruldu!");
        }

        private GameObject LoadPrefab(string path)
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }

        private void CreateFolderIfNeeded(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                string parent = Path.GetDirectoryName(path).Replace("\\", "/");
                string folderName = Path.GetFileName(path);
                AssetDatabase.CreateFolder(parent, folderName);
            }
        }

        private void CreateTestHexGrid()
        {
            // HexGrid parent bul veya olustur
            GameObject hexGrid = GameObject.Find("HexGrid");
            if (hexGrid == null)
            {
                hexGrid = new GameObject("HexGrid");
            }

            // HexTileFactory bul veya olustur
            HexTileFactory factory = Object.FindFirstObjectByType<HexTileFactory>();
            if (factory == null)
            {
                GameObject factoryObj = new GameObject("HexTileFactory");
                factory = factoryObj.AddComponent<HexTileFactory>();
            }

            // Eger database varsa, factory'ye ata
            if (targetDatabase != null)
            {
                SerializedObject so = new SerializedObject(factory);
                so.FindProperty("prefabDatabase").objectReferenceValue = targetDatabase;
                so.FindProperty("tilesParent").objectReferenceValue = hexGrid.transform;
                so.ApplyModifiedProperties();
            }

            // Test grid olustur
            factory.GenerateTestGrid(10, 10);

            Debug.Log("10x10 test grid olusturuldu.");
        }

        private void ClearHexTiles()
        {
            HexTileFactory factory = Object.FindFirstObjectByType<HexTileFactory>();
            if (factory != null)
            {
                factory.ClearAllTiles();
            }

            // Manuel temizlik
            GameObject hexGrid = GameObject.Find("HexGrid");
            if (hexGrid != null)
            {
                for (int i = hexGrid.transform.childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(hexGrid.transform.GetChild(i).gameObject);
                }
            }

            Debug.Log("Tum hex tile'lar temizlendi.");
        }
    }
}
