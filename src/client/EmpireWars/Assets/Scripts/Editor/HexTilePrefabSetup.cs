using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using EmpireWars.WorldMap.Tiles;

namespace EmpireWars.Editor
{
    /// <summary>
    /// KayKit Medieval Hexagon Pack FBX dosyalarini prefab'lara donusturur
    /// ve HexTilePrefabDatabase'i otomatik doldurur
    /// </summary>
    public class HexTilePrefabSetup : EditorWindow
    {
        private const string KayKitTilesPath = "Assets/KayKit_Medieval_Hexagon/tiles";
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

            EditorGUILayout.HelpBox(
                "1. 'Create Prefab Database' - Database yoksa olustur\n" +
                "2. 'Auto Setup All' - KayKit FBX'lerden prefab olustur ve database'e ata\n" +
                "3. 'Create Test Grid' - Sahneye test hexleri ekle",
                MessageType.Info
            );

            GUILayout.Space(10);

            if (GUILayout.Button("Create Prefab Database", GUILayout.Height(30)))
            {
                CreatePrefabDatabase();
            }

            GUILayout.Space(5);

            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("AUTO SETUP ALL (Recommended)", GUILayout.Height(40)))
            {
                AutoSetupAll();
            }
            GUI.backgroundColor = Color.white;

            GUILayout.Space(20);
            GUILayout.Label("Manual Steps", EditorStyles.boldLabel);

            if (GUILayout.Button("1. Create Prefabs from KayKit FBX"))
            {
                SetupPrefabsFromKayKit();
            }

            if (targetDatabase != null && GUILayout.Button("2. Auto-Assign Prefabs to Database"))
            {
                AutoAssignPrefabsToDatabase();
            }

            GUILayout.Space(20);
            GUILayout.Label("Scene Actions", EditorStyles.boldLabel);

            if (GUILayout.Button("Create Test Hex Grid (10x10)", GUILayout.Height(25)))
            {
                CreateTestHexGrid();
            }

            if (GUILayout.Button("Clear All Hex Tiles", GUILayout.Height(25)))
            {
                ClearHexTiles();
            }
        }

        private void AutoSetupAll()
        {
            // 1. Database yoksa olustur
            if (targetDatabase == null)
            {
                CreatePrefabDatabase();
            }

            // 2. Prefab'lari olustur
            SetupPrefabsFromKayKit();

            // 3. Database'e ata
            AutoAssignPrefabsToDatabase();

            // 4. Test grid olustur
            CreateTestHexGrid();

            Debug.Log("Tum islemler tamamlandi!");
        }

        private void CreatePrefabDatabase()
        {
            CreateFolderIfNeeded(DatabasePath);

            string assetPath = $"{DatabasePath}/HexTilePrefabDatabase.asset";

            // Varsa yukle
            targetDatabase = AssetDatabase.LoadAssetAtPath<HexTilePrefabDatabase>(assetPath);
            if (targetDatabase != null)
            {
                Debug.Log("Mevcut database yuklendi.");
                return;
            }

            // Yoksa olustur
            HexTilePrefabDatabase database = ScriptableObject.CreateInstance<HexTilePrefabDatabase>();
            AssetDatabase.CreateAsset(database, assetPath);
            AssetDatabase.SaveAssets();

            targetDatabase = database;
            Selection.activeObject = database;

            Debug.Log($"HexTilePrefabDatabase olusturuldu: {assetPath}");
        }

        private void SetupPrefabsFromKayKit()
        {
            // Ana prefab klasorunu olustur
            CreateFolderIfNeeded(PrefabOutputPath);
            CreateFolderIfNeeded($"{PrefabOutputPath}/Base");
            CreateFolderIfNeeded($"{PrefabOutputPath}/Roads");
            CreateFolderIfNeeded($"{PrefabOutputPath}/Rivers");
            CreateFolderIfNeeded($"{PrefabOutputPath}/Coast");

            int prefabCount = 0;

            // Base tiles
            prefabCount += CreatePrefabsFromFolder($"{KayKitTilesPath}/base", $"{PrefabOutputPath}/Base");

            // Road tiles
            prefabCount += CreatePrefabsFromFolder($"{KayKitTilesPath}/roads", $"{PrefabOutputPath}/Roads");

            // River tiles (waterless dahil degil)
            prefabCount += CreatePrefabsFromFolder($"{KayKitTilesPath}/rivers", $"{PrefabOutputPath}/Rivers", false);

            // Coast tiles (waterless dahil degil)
            prefabCount += CreatePrefabsFromFolder($"{KayKitTilesPath}/coast", $"{PrefabOutputPath}/Coast", false);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Toplam {prefabCount} prefab olusturuldu/guncellendi.");
        }

        private int CreatePrefabsFromFolder(string sourcePath, string targetPath, bool includeSubfolders = true)
        {
            if (!AssetDatabase.IsValidFolder(sourcePath))
            {
                Debug.LogWarning($"Klasor bulunamadi: {sourcePath}");
                return 0;
            }

            string[] searchFolders = includeSubfolders ? null : new[] { sourcePath };
            string[] guids = AssetDatabase.FindAssets("t:Model", searchFolders ?? new[] { sourcePath });
            int count = 0;

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                // Sadece dogrudan bu klasorden gelen dosyalari al (subfolder haric)
                if (!includeSubfolders)
                {
                    string directory = Path.GetDirectoryName(assetPath).Replace("\\", "/");
                    if (directory != sourcePath) continue;
                }

                // waterless dosyalarini atla
                if (assetPath.Contains("waterless")) continue;

                GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                if (model == null) continue;

                string prefabName = Path.GetFileNameWithoutExtension(assetPath);
                string prefabPath = $"{targetPath}/{prefabName}.prefab";

                // Prefab zaten varsa atla
                if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
                {
                    count++;
                    continue;
                }

                // Model'i instantiate et
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(model);

                // MeshCollider ekle
                MeshFilter meshFilter = instance.GetComponentInChildren<MeshFilter>();
                if (meshFilter != null && instance.GetComponent<Collider>() == null)
                {
                    MeshCollider collider = instance.AddComponent<MeshCollider>();
                    collider.sharedMesh = meshFilter.sharedMesh;
                    collider.convex = true;
                }

                // Prefab olarak kaydet
                PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
                DestroyImmediate(instance);

                count++;
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
            targetDatabase.hillTile = LoadPrefab($"{PrefabOutputPath}/Base/hex_grass_sloped_high.prefab");
            targetDatabase.defaultTile = targetDatabase.grassTile;

            // Diger terrain tipleri icin grass fallback (ustune dekorasyon eklenecek)
            targetDatabase.forestTile = targetDatabase.grassTile;
            targetDatabase.mountainTile = targetDatabase.grassTile;
            targetDatabase.desertTile = targetDatabase.grassTile;
            targetDatabase.snowTile = targetDatabase.grassTile;
            targetDatabase.swampTile = targetDatabase.grassTile;
            targetDatabase.plainsTile = targetDatabase.grassTile;

            // Road tiles (A-M)
            var roadTiles = new List<GameObject>();
            foreach (char c in "ABCDEFGHIJKLM")
            {
                GameObject road = LoadPrefab($"{PrefabOutputPath}/Roads/hex_road_{c}.prefab");
                if (road != null) roadTiles.Add(road);
            }
            // Sloped road tiles da ekle
            GameObject slopedHigh = LoadPrefab($"{PrefabOutputPath}/Roads/hex_road_A_sloped_high.prefab");
            GameObject slopedLow = LoadPrefab($"{PrefabOutputPath}/Roads/hex_road_A_sloped_low.prefab");
            if (slopedHigh != null) roadTiles.Add(slopedHigh);
            if (slopedLow != null) roadTiles.Add(slopedLow);
            targetDatabase.roadTiles = roadTiles.ToArray();

            // River tiles (A-L + curvy + crossing)
            var riverTiles = new List<GameObject>();
            foreach (char c in "ABCDEFGHIJKL")
            {
                GameObject river = LoadPrefab($"{PrefabOutputPath}/Rivers/hex_river_{c}.prefab");
                if (river != null) riverTiles.Add(river);
            }
            GameObject curvy = LoadPrefab($"{PrefabOutputPath}/Rivers/hex_river_A_curvy.prefab");
            GameObject crossingA = LoadPrefab($"{PrefabOutputPath}/Rivers/hex_river_crossing_A.prefab");
            GameObject crossingB = LoadPrefab($"{PrefabOutputPath}/Rivers/hex_river_crossing_B.prefab");
            if (curvy != null) riverTiles.Add(curvy);
            if (crossingA != null) riverTiles.Add(crossingA);
            if (crossingB != null) riverTiles.Add(crossingB);
            targetDatabase.riverTiles = riverTiles.ToArray();

            // Coast tiles (A-E)
            var coastTiles = new List<GameObject>();
            foreach (char c in "ABCDE")
            {
                GameObject coast = LoadPrefab($"{PrefabOutputPath}/Coast/hex_coast_{c}.prefab");
                if (coast != null) coastTiles.Add(coast);
            }
            targetDatabase.coastTiles = coastTiles.ToArray();

            EditorUtility.SetDirty(targetDatabase);
            AssetDatabase.SaveAssets();

            Debug.Log($"Database guncellendi:\n" +
                     $"- Grass: {(targetDatabase.grassTile != null ? "OK" : "EKSIK")}\n" +
                     $"- Water: {(targetDatabase.waterTile != null ? "OK" : "EKSIK")}\n" +
                     $"- Hill: {(targetDatabase.hillTile != null ? "OK" : "EKSIK")}\n" +
                     $"- Roads: {roadTiles.Count} adet\n" +
                     $"- Rivers: {riverTiles.Count} adet\n" +
                     $"- Coasts: {coastTiles.Count} adet");
        }

        private GameObject LoadPrefab(string path)
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(path);
        }

        private void CreateFolderIfNeeded(string path)
        {
            if (AssetDatabase.IsValidFolder(path)) return;

            string[] parts = path.Split('/');
            string current = parts[0];

            for (int i = 1; i < parts.Length; i++)
            {
                string next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                {
                    AssetDatabase.CreateFolder(current, parts[i]);
                }
                current = next;
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

            // Database'i factory'ye ata
            if (targetDatabase != null)
            {
                SerializedObject so = new SerializedObject(factory);
                so.FindProperty("prefabDatabase").objectReferenceValue = targetDatabase;
                so.FindProperty("tilesParent").objectReferenceValue = hexGrid.transform;
                so.ApplyModifiedProperties();
            }

            // Test grid olustur (10x10)
            factory.GenerateTestGrid(10, 10);

            Debug.Log("10x10 test grid olusturuldu - tum terrain tipleri dahil.");
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
