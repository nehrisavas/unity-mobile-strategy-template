using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EmpireWars.WorldMap.Tiles;

namespace EmpireWars.Editor
{
    /// <summary>
    /// KayKit Medieval Hexagon Pack prefablarini otomatik olarak
    /// HexTilePrefabDatabase ve TerrainDecorationDatabase'e atar
    /// Menu: Tools > EmpireWars > Setup KayKit Databases
    /// </summary>
    public class KayKitAutoSetup : EditorWindow
    {
        private const string KAYKIT_PATH = "Assets/KayKit_Medieval_Hexagon";
        private const string DATABASE_PATH = "Assets/ScriptableObjects";

        private HexTilePrefabDatabase tilePrefabDatabase;
        private TerrainDecorationDatabase decorationDatabase;
        private BuildingDatabase buildingDatabase;

        [MenuItem("Tools/EmpireWars/Setup KayKit Databases")]
        public static void ShowWindow()
        {
            GetWindow<KayKitAutoSetup>("KayKit Auto Setup");
        }

        private void OnGUI()
        {
            GUILayout.Label("KayKit Medieval Hexagon Auto Setup", EditorStyles.boldLabel);
            GUILayout.Space(10);

            EditorGUILayout.HelpBox(
                "Bu tool KayKit Medieval Hexagon Pack prefablarini otomatik olarak database'lere atar.\n" +
                "Calistirmadan once database'leri olusturun veya mevcut olanlari secin.",
                MessageType.Info);

            GUILayout.Space(10);

            tilePrefabDatabase = (HexTilePrefabDatabase)EditorGUILayout.ObjectField(
                "Tile Prefab Database", tilePrefabDatabase, typeof(HexTilePrefabDatabase), false);

            decorationDatabase = (TerrainDecorationDatabase)EditorGUILayout.ObjectField(
                "Decoration Database", decorationDatabase, typeof(TerrainDecorationDatabase), false);

            buildingDatabase = (BuildingDatabase)EditorGUILayout.ObjectField(
                "Building Database", buildingDatabase, typeof(BuildingDatabase), false);

            GUILayout.Space(20);

            if (GUILayout.Button("Create New Databases", GUILayout.Height(30)))
            {
                CreateNewDatabases();
            }

            GUILayout.Space(10);

            EditorGUI.BeginDisabledGroup(tilePrefabDatabase == null && decorationDatabase == null && buildingDatabase == null);
            if (GUILayout.Button("Setup Tile Prefabs", GUILayout.Height(30)))
            {
                SetupTilePrefabs();
            }

            if (GUILayout.Button("Setup Decorations", GUILayout.Height(30)))
            {
                SetupDecorations();
            }

            if (GUILayout.Button("Setup Buildings", GUILayout.Height(30)))
            {
                SetupBuildings();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Setup ALL (Tiles + Decorations + Buildings)", GUILayout.Height(40)))
            {
                SetupTilePrefabs();
                SetupDecorations();
                SetupBuildings();
            }
            EditorGUI.EndDisabledGroup();
        }

        private void CreateNewDatabases()
        {
            // Ensure directory exists
            if (!AssetDatabase.IsValidFolder(DATABASE_PATH))
            {
                AssetDatabase.CreateFolder("Assets", "ScriptableObjects");
            }

            // Create Tile Prefab Database
            if (tilePrefabDatabase == null)
            {
                tilePrefabDatabase = ScriptableObject.CreateInstance<HexTilePrefabDatabase>();
                AssetDatabase.CreateAsset(tilePrefabDatabase, $"{DATABASE_PATH}/HexTilePrefabDatabase.asset");
            }

            // Create Decoration Database
            if (decorationDatabase == null)
            {
                decorationDatabase = ScriptableObject.CreateInstance<TerrainDecorationDatabase>();
                AssetDatabase.CreateAsset(decorationDatabase, $"{DATABASE_PATH}/TerrainDecorationDatabase.asset");
            }

            // Create Building Database
            if (buildingDatabase == null)
            {
                buildingDatabase = ScriptableObject.CreateInstance<BuildingDatabase>();
                AssetDatabase.CreateAsset(buildingDatabase, $"{DATABASE_PATH}/BuildingDatabase.asset");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("KayKitAutoSetup: All databases created successfully!");
        }

        private void SetupTilePrefabs()
        {
            if (tilePrefabDatabase == null)
            {
                Debug.LogError("KayKitAutoSetup: Tile Prefab Database is null!");
                return;
            }

            string tilesPath = $"{KAYKIT_PATH}/tiles";

            // Base tiles
            tilePrefabDatabase.grassTile = LoadPrefab($"{tilesPath}/base/hex_grass.fbx");
            tilePrefabDatabase.waterTile = LoadPrefab($"{tilesPath}/base/hex_water.fbx");
            tilePrefabDatabase.grassSlopedHighTile = LoadPrefab($"{tilesPath}/base/hex_grass_sloped_high.fbx");
            tilePrefabDatabase.grassSlopedLowTile = LoadPrefab($"{tilesPath}/base/hex_grass_sloped_low.fbx");

            // Forest, Mountain, Desert, Snow, Swamp - use grass as base (decorations add visuals)
            tilePrefabDatabase.forestTile = tilePrefabDatabase.grassTile;
            tilePrefabDatabase.mountainTile = tilePrefabDatabase.grassTile;
            tilePrefabDatabase.desertTile = tilePrefabDatabase.grassTile;
            tilePrefabDatabase.snowTile = tilePrefabDatabase.grassTile;
            tilePrefabDatabase.swampTile = tilePrefabDatabase.grassTile;
            tilePrefabDatabase.hillTile = tilePrefabDatabase.grassSlopedLowTile;
            tilePrefabDatabase.plainsTile = tilePrefabDatabase.grassTile;

            // Resource tiles (use grass as base)
            tilePrefabDatabase.farmTile = tilePrefabDatabase.grassTile;
            tilePrefabDatabase.mineTile = tilePrefabDatabase.grassTile;
            tilePrefabDatabase.quarryTile = tilePrefabDatabase.grassTile;
            tilePrefabDatabase.goldMineTile = tilePrefabDatabase.grassTile;
            tilePrefabDatabase.gemMineTile = tilePrefabDatabase.grassTile;

            // Default tile
            tilePrefabDatabase.defaultTile = tilePrefabDatabase.grassTile;

            // Road tiles (A-M)
            var roadTiles = new List<GameObject>();
            string[] roadVariants = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
            foreach (var variant in roadVariants)
            {
                var roadTile = LoadPrefab($"{tilesPath}/roads/hex_road_{variant}.fbx");
                if (roadTile != null) roadTiles.Add(roadTile);
            }
            // Also add sloped road variants
            var roadSlopedHigh = LoadPrefab($"{tilesPath}/roads/hex_road_A_sloped_high.fbx");
            var roadSlopedLow = LoadPrefab($"{tilesPath}/roads/hex_road_A_sloped_low.fbx");
            if (roadSlopedHigh != null) roadTiles.Add(roadSlopedHigh);
            if (roadSlopedLow != null) roadTiles.Add(roadSlopedLow);
            tilePrefabDatabase.roadTiles = roadTiles.ToArray();

            // River tiles (A-L)
            var riverTiles = new List<GameObject>();
            string[] riverVariants = { "A", "A_curvy", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
            foreach (var variant in riverVariants)
            {
                var riverTile = LoadPrefab($"{tilesPath}/rivers/hex_river_{variant}.fbx");
                if (riverTile != null) riverTiles.Add(riverTile);
            }
            tilePrefabDatabase.riverTiles = riverTiles.ToArray();

            // Coast tiles (A-E)
            var coastTiles = new List<GameObject>();
            string[] coastVariants = { "A", "B", "C", "D", "E" };
            foreach (var variant in coastVariants)
            {
                var coastTile = LoadPrefab($"{tilesPath}/coast/hex_coast_{variant}.fbx");
                if (coastTile != null) coastTiles.Add(coastTile);
            }
            tilePrefabDatabase.coastTiles = coastTiles.ToArray();

            // Bridge tiles (river crossings)
            var bridgeTiles = new List<GameObject>();
            bridgeTiles.Add(LoadPrefab($"{tilesPath}/rivers/hex_river_crossing_A.fbx"));
            bridgeTiles.Add(LoadPrefab($"{tilesPath}/rivers/hex_river_crossing_B.fbx"));
            tilePrefabDatabase.bridgeTiles = bridgeTiles.Where(x => x != null).ToArray();

            EditorUtility.SetDirty(tilePrefabDatabase);
            AssetDatabase.SaveAssets();

            Debug.Log($"KayKitAutoSetup: Tile Prefabs setup completed!\n" +
                      $"  Roads: {tilePrefabDatabase.roadTiles?.Length ?? 0}\n" +
                      $"  Rivers: {tilePrefabDatabase.riverTiles?.Length ?? 0}\n" +
                      $"  Coasts: {tilePrefabDatabase.coastTiles?.Length ?? 0}\n" +
                      $"  Bridges: {tilePrefabDatabase.bridgeTiles?.Length ?? 0}");
        }

        private void SetupDecorations()
        {
            if (decorationDatabase == null)
            {
                Debug.LogError("KayKitAutoSetup: Decoration Database is null!");
                return;
            }

            string naturePath = $"{KAYKIT_PATH}/decoration/nature";
            string propsPath = $"{KAYKIT_PATH}/decoration/props";

            // Forest Trees
            var forestTrees = new List<GameObject>();
            forestTrees.Add(LoadPrefab($"{naturePath}/trees_A_large.fbx"));
            forestTrees.Add(LoadPrefab($"{naturePath}/trees_A_medium.fbx"));
            forestTrees.Add(LoadPrefab($"{naturePath}/trees_A_small.fbx"));
            forestTrees.Add(LoadPrefab($"{naturePath}/trees_B_large.fbx"));
            forestTrees.Add(LoadPrefab($"{naturePath}/trees_B_medium.fbx"));
            forestTrees.Add(LoadPrefab($"{naturePath}/trees_B_small.fbx"));
            decorationDatabase.forestTrees = forestTrees.Where(x => x != null).ToArray();

            // Single Trees
            var singleTrees = new List<GameObject>();
            singleTrees.Add(LoadPrefab($"{naturePath}/tree_single_A.fbx"));
            singleTrees.Add(LoadPrefab($"{naturePath}/tree_single_B.fbx"));
            decorationDatabase.singleTrees = singleTrees.Where(x => x != null).ToArray();

            // Mountain Models
            var mountains = new List<GameObject>();
            mountains.Add(LoadPrefab($"{naturePath}/mountain_A.fbx"));
            mountains.Add(LoadPrefab($"{naturePath}/mountain_A_grass.fbx"));
            mountains.Add(LoadPrefab($"{naturePath}/mountain_A_grass_trees.fbx"));
            mountains.Add(LoadPrefab($"{naturePath}/mountain_B.fbx"));
            mountains.Add(LoadPrefab($"{naturePath}/mountain_B_grass.fbx"));
            mountains.Add(LoadPrefab($"{naturePath}/mountain_B_grass_trees.fbx"));
            mountains.Add(LoadPrefab($"{naturePath}/mountain_C.fbx"));
            mountains.Add(LoadPrefab($"{naturePath}/mountain_C_grass.fbx"));
            mountains.Add(LoadPrefab($"{naturePath}/mountain_C_grass_trees.fbx"));
            decorationDatabase.mountainModels = mountains.Where(x => x != null).ToArray();

            // Hill Rocks
            var hills = new List<GameObject>();
            hills.Add(LoadPrefab($"{naturePath}/hills_A.fbx"));
            hills.Add(LoadPrefab($"{naturePath}/hills_A_trees.fbx"));
            hills.Add(LoadPrefab($"{naturePath}/hills_B.fbx"));
            hills.Add(LoadPrefab($"{naturePath}/hills_B_trees.fbx"));
            hills.Add(LoadPrefab($"{naturePath}/hills_C.fbx"));
            hills.Add(LoadPrefab($"{naturePath}/hills_C_trees.fbx"));
            hills.Add(LoadPrefab($"{naturePath}/hill_single_A.fbx"));
            hills.Add(LoadPrefab($"{naturePath}/hill_single_B.fbx"));
            hills.Add(LoadPrefab($"{naturePath}/hill_single_C.fbx"));
            decorationDatabase.hillRocks = hills.Where(x => x != null).ToArray();

            // Single Rocks
            var rocks = new List<GameObject>();
            rocks.Add(LoadPrefab($"{naturePath}/rock_single_A.fbx"));
            rocks.Add(LoadPrefab($"{naturePath}/rock_single_B.fbx"));
            rocks.Add(LoadPrefab($"{naturePath}/rock_single_C.fbx"));
            rocks.Add(LoadPrefab($"{naturePath}/rock_single_D.fbx"));
            rocks.Add(LoadPrefab($"{naturePath}/rock_single_E.fbx"));
            decorationDatabase.singleRocks = rocks.Where(x => x != null).ToArray();

            // Desert decorations (use rocks)
            decorationDatabase.desertDecorations = decorationDatabase.singleRocks;

            // Snow decorations (use mountains with grass)
            decorationDatabase.snowDecorations = mountains.Where(x => x != null && x.name.Contains("grass")).ToArray();

            // Swamp decorations (cut trees)
            var swamp = new List<GameObject>();
            swamp.Add(LoadPrefab($"{naturePath}/tree_single_A_cut.fbx"));
            swamp.Add(LoadPrefab($"{naturePath}/tree_single_B_cut.fbx"));
            swamp.Add(LoadPrefab($"{naturePath}/trees_A_cut.fbx"));
            swamp.Add(LoadPrefab($"{naturePath}/trees_B_cut.fbx"));
            decorationDatabase.swampDecorations = swamp.Where(x => x != null).ToArray();

            // Water decorations
            var water = new List<GameObject>();
            water.Add(LoadPrefab($"{naturePath}/waterlily_A.fbx"));
            water.Add(LoadPrefab($"{naturePath}/waterlily_B.fbx"));
            water.Add(LoadPrefab($"{naturePath}/waterplant_A.fbx"));
            water.Add(LoadPrefab($"{naturePath}/waterplant_B.fbx"));
            water.Add(LoadPrefab($"{naturePath}/waterplant_C.fbx"));
            decorationDatabase.waterDecorations = water.Where(x => x != null).ToArray();

            // Cloud decorations
            var clouds = new List<GameObject>();
            clouds.Add(LoadPrefab($"{naturePath}/cloud_big.fbx"));
            clouds.Add(LoadPrefab($"{naturePath}/cloud_small.fbx"));
            decorationDatabase.cloudModels = clouds.Where(x => x != null).ToArray();

            // Farm Props
            var farmProps = new List<GameObject>();
            farmProps.Add(LoadPrefab($"{propsPath}/haybale.fbx"));
            farmProps.Add(LoadPrefab($"{propsPath}/sack.fbx"));
            farmProps.Add(LoadPrefab($"{propsPath}/wheelbarrow.fbx"));
            farmProps.Add(LoadPrefab($"{propsPath}/trough.fbx"));
            farmProps.Add(LoadPrefab($"{propsPath}/trough_long.fbx"));
            decorationDatabase.farmProps = farmProps.Where(x => x != null).ToArray();

            // Military Props
            var militaryProps = new List<GameObject>();
            militaryProps.Add(LoadPrefab($"{propsPath}/barrel.fbx"));
            militaryProps.Add(LoadPrefab($"{propsPath}/crate_A_big.fbx"));
            militaryProps.Add(LoadPrefab($"{propsPath}/crate_B_big.fbx"));
            militaryProps.Add(LoadPrefab($"{propsPath}/flag_blue.fbx"));
            militaryProps.Add(LoadPrefab($"{propsPath}/flag_green.fbx"));
            militaryProps.Add(LoadPrefab($"{propsPath}/flag_red.fbx"));
            militaryProps.Add(LoadPrefab($"{propsPath}/tent.fbx"));
            militaryProps.Add(LoadPrefab($"{propsPath}/weaponrack.fbx"));
            militaryProps.Add(LoadPrefab($"{propsPath}/target.fbx"));
            decorationDatabase.militaryProps = militaryProps.Where(x => x != null).ToArray();

            // Resource Props
            var resourceProps = new List<GameObject>();
            resourceProps.Add(LoadPrefab($"{propsPath}/resource_lumber.fbx"));
            resourceProps.Add(LoadPrefab($"{propsPath}/resource_stone.fbx"));
            resourceProps.Add(LoadPrefab($"{propsPath}/bucket_empty.fbx"));
            resourceProps.Add(LoadPrefab($"{propsPath}/bucket_water.fbx"));
            resourceProps.Add(LoadPrefab($"{propsPath}/pallet.fbx"));
            resourceProps.Add(LoadPrefab($"{propsPath}/crate_long_A.fbx"));
            resourceProps.Add(LoadPrefab($"{propsPath}/crate_long_B.fbx"));
            decorationDatabase.resourceProps = resourceProps.Where(x => x != null).ToArray();

            // Coastal Props
            var coastalProps = new List<GameObject>();
            coastalProps.Add(LoadPrefab($"{propsPath}/anchor.fbx"));
            coastalProps.Add(LoadPrefab($"{propsPath}/boat.fbx"));
            coastalProps.Add(LoadPrefab($"{propsPath}/boatrack.fbx"));
            decorationDatabase.coastalProps = coastalProps.Where(x => x != null).ToArray();

            EditorUtility.SetDirty(decorationDatabase);
            AssetDatabase.SaveAssets();

            Debug.Log($"KayKitAutoSetup: Decorations setup completed!\n" +
                      $"  Forest Trees: {decorationDatabase.forestTrees?.Length ?? 0}\n" +
                      $"  Single Trees: {decorationDatabase.singleTrees?.Length ?? 0}\n" +
                      $"  Mountains: {decorationDatabase.mountainModels?.Length ?? 0}\n" +
                      $"  Hills: {decorationDatabase.hillRocks?.Length ?? 0}\n" +
                      $"  Rocks: {decorationDatabase.singleRocks?.Length ?? 0}\n" +
                      $"  Swamp: {decorationDatabase.swampDecorations?.Length ?? 0}\n" +
                      $"  Water: {decorationDatabase.waterDecorations?.Length ?? 0}\n" +
                      $"  Clouds: {decorationDatabase.cloudModels?.Length ?? 0}\n" +
                      $"  Farm Props: {decorationDatabase.farmProps?.Length ?? 0}\n" +
                      $"  Military Props: {decorationDatabase.militaryProps?.Length ?? 0}\n" +
                      $"  Resource Props: {decorationDatabase.resourceProps?.Length ?? 0}\n" +
                      $"  Coastal Props: {decorationDatabase.coastalProps?.Length ?? 0}");
        }

        private void SetupBuildings()
        {
            if (buildingDatabase == null)
            {
                Debug.LogError("KayKitAutoSetup: Building Database is null!");
                return;
            }

            string greenPath = $"{KAYKIT_PATH}/buildings/green";
            string bluePath = $"{KAYKIT_PATH}/buildings/blue";
            string redPath = $"{KAYKIT_PATH}/buildings/red";
            string neutralPath = $"{KAYKIT_PATH}/buildings/neutral";

            // Castles
            buildingDatabase.castleGreen = LoadPrefab($"{greenPath}/building_castle_green.fbx");
            buildingDatabase.castleBlue = LoadPrefab($"{bluePath}/building_castle_blue.fbx");
            buildingDatabase.castleRed = LoadPrefab($"{redPath}/building_castle_red.fbx");

            // Tower Cannon
            buildingDatabase.towerCannonGreen = LoadPrefab($"{greenPath}/building_tower_cannon_green.fbx");
            buildingDatabase.towerCannonBlue = LoadPrefab($"{bluePath}/building_tower_cannon_blue.fbx");
            buildingDatabase.towerCannonRed = LoadPrefab($"{redPath}/building_tower_cannon_red.fbx");

            // Tower Catapult
            buildingDatabase.towerCatapultGreen = LoadPrefab($"{greenPath}/building_tower_catapult_green.fbx");
            buildingDatabase.towerCatapultBlue = LoadPrefab($"{bluePath}/building_tower_catapult_blue.fbx");
            buildingDatabase.towerCatapultRed = LoadPrefab($"{redPath}/building_tower_catapult_red.fbx");

            // Watchtower
            buildingDatabase.watchtowerGreen = LoadPrefab($"{greenPath}/building_watchtower_green.fbx");
            buildingDatabase.watchtowerBlue = LoadPrefab($"{bluePath}/building_watchtower_blue.fbx");
            buildingDatabase.watchtowerRed = LoadPrefab($"{redPath}/building_watchtower_red.fbx");

            // Normal Towers
            buildingDatabase.towerAGreen = LoadPrefab($"{greenPath}/building_tower_A_green.fbx");
            buildingDatabase.towerBGreen = LoadPrefab($"{greenPath}/building_tower_B_green.fbx");

            // Barracks
            buildingDatabase.barracksGreen = LoadPrefab($"{greenPath}/building_barracks_green.fbx");
            buildingDatabase.barracksBlue = LoadPrefab($"{bluePath}/building_barracks_blue.fbx");
            buildingDatabase.barracksRed = LoadPrefab($"{redPath}/building_barracks_red.fbx");

            // Archery Range
            buildingDatabase.archeryrangeGreen = LoadPrefab($"{greenPath}/building_archeryrange_green.fbx");

            // Stables
            buildingDatabase.stablesGreen = LoadPrefab($"{greenPath}/building_stables_green.fbx");

            // Townhall
            buildingDatabase.townhallGreen = LoadPrefab($"{greenPath}/building_townhall_green.fbx");

            // Market
            buildingDatabase.marketGreen = LoadPrefab($"{greenPath}/building_market_green.fbx");

            // Blacksmith
            buildingDatabase.blacksmithGreen = LoadPrefab($"{greenPath}/building_blacksmith_green.fbx");

            // Workshop
            buildingDatabase.workshopGreen = LoadPrefab($"{greenPath}/building_workshop_green.fbx");

            // Mine
            buildingDatabase.mineGreen = LoadPrefab($"{greenPath}/building_mine_green.fbx");

            // Lumbermill
            buildingDatabase.lumbermillGreen = LoadPrefab($"{greenPath}/building_lumbermill_green.fbx");

            // Mills
            buildingDatabase.windmillGreen = LoadPrefab($"{greenPath}/building_windmill_green.fbx");
            buildingDatabase.watermillGreen = LoadPrefab($"{greenPath}/building_watermill_green.fbx");

            // Homes
            buildingDatabase.homeAGreen = LoadPrefab($"{greenPath}/building_home_A_green.fbx");
            buildingDatabase.homeBGreen = LoadPrefab($"{greenPath}/building_home_B_green.fbx");

            // Tavern
            buildingDatabase.tavernGreen = LoadPrefab($"{greenPath}/building_tavern_green.fbx");

            // Tent
            buildingDatabase.tentGreen = LoadPrefab($"{greenPath}/building_tent_green.fbx");

            // Church
            buildingDatabase.churchGreen = LoadPrefab($"{greenPath}/building_church_green.fbx");

            // Shrine
            buildingDatabase.shrineGreen = LoadPrefab($"{greenPath}/building_shrine_green.fbx");

            // Shipyard
            buildingDatabase.shipyardGreen = LoadPrefab($"{greenPath}/building_shipyard_green.fbx");

            // Docks
            buildingDatabase.docksGreen = LoadPrefab($"{greenPath}/building_docks_green.fbx");

            // Neutral buildings
            buildingDatabase.bridgeA = LoadPrefab($"{neutralPath}/building_bridge_A.fbx");
            buildingDatabase.bridgeB = LoadPrefab($"{neutralPath}/building_bridge_B.fbx");
            buildingDatabase.buildingDestroyed = LoadPrefab($"{neutralPath}/building_destroyed.fbx");
            buildingDatabase.scaffolding = LoadPrefab($"{neutralPath}/building_scaffolding.fbx");

            EditorUtility.SetDirty(buildingDatabase);
            AssetDatabase.SaveAssets();

            Debug.Log("KayKitAutoSetup: Buildings setup completed!\n" +
                      $"  Castles: 3 (green/blue/red)\n" +
                      $"  Towers: 9+ variants\n" +
                      $"  Military: barracks, archery, stables\n" +
                      $"  Economy: townhall, market, blacksmith, workshop\n" +
                      $"  Production: mine, lumbermill, windmill, watermill\n" +
                      $"  Residential: homes, tavern, tent\n" +
                      $"  Religious: church, shrine\n" +
                      $"  Naval: shipyard, docks\n" +
                      $"  Neutral: bridges, destroyed, scaffolding");
        }

        private GameObject LoadPrefab(string path)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
            {
                Debug.LogWarning($"KayKitAutoSetup: Could not load prefab at {path}");
            }
            return prefab;
        }
    }
}
