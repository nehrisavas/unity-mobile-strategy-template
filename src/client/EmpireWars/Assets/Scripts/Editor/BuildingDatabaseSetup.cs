using UnityEngine;
using UnityEditor;
using System.IO;
using EmpireWars.Core;
using EmpireWars.WorldMap.Tiles;

namespace EmpireWars.Editor
{
    /// <summary>
    /// BuildingDatabase'i otomatik doldurur
    /// Menu: Tools > EmpireWars > Setup Building Database
    /// </summary>
    public class BuildingDatabaseSetup : EditorWindow
    {
        private const string KAYKIT_PATH = "Assets/KayKit_Medieval_Hexagon/building";
        private const string DATABASE_PATH = "Assets/ScriptableObjects/BuildingDatabase.asset";

        [MenuItem("Tools/EmpireWars/Setup Building Database")]
        public static void SetupDatabase()
        {
            // BuildingDatabase bul veya olustur
            BuildingDatabase db = AssetDatabase.LoadAssetAtPath<BuildingDatabase>(DATABASE_PATH);

            if (db == null)
            {
                // ScriptableObjects klasoru olustur
                string dir = Path.GetDirectoryName(DATABASE_PATH);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                db = ScriptableObject.CreateInstance<BuildingDatabase>();
                AssetDatabase.CreateAsset(db, DATABASE_PATH);
                Debug.Log($"BuildingDatabaseSetup: Yeni BuildingDatabase olusturuldu: {DATABASE_PATH}");
            }

            SerializedObject serializedDb = new SerializedObject(db);
            int assignedCount = 0;

            // Tum bina tiplerini tara
            string[] buildingTypes = new string[]
            {
                // Castles
                "castle",
                // Towers
                "tower_cannon", "tower_catapult", "watchtower", "tower_A", "tower_B", "tower_base",
                // Military
                "barracks", "archeryrange", "stables",
                // Economy
                "townhall", "market", "blacksmith", "workshop",
                // Production
                "mine", "lumbermill", "windmill", "watermill",
                // Residential
                "home_A", "home_B", "tavern", "tent",
                // Religious
                "church", "shrine",
                // Naval
                "shipyard", "docks",
                // Utility
                "well"
            };

            string[] colors = new string[] { "green", "blue", "red", "yellow" };

            foreach (string buildingType in buildingTypes)
            {
                foreach (string color in colors)
                {
                    string fieldName = GetFieldName(buildingType, color);
                    string prefabName = GetPrefabName(buildingType, color);

                    var prop = serializedDb.FindProperty(fieldName);
                    if (prop != null)
                    {
                        GameObject prefab = FindPrefab(prefabName);
                        if (prefab != null)
                        {
                            prop.objectReferenceValue = prefab;
                            assignedCount++;
                        }
                    }
                }
            }

            // Neutral/Decoration buildings (color-less)
            AssignNeutralBuilding(serializedDb, "bridgeA", "bridge_A", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "bridgeB", "bridge_B", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "buildingDestroyed", "building_destroyed", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "scaffolding", "scaffolding", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "stageA", "stage_A", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "stageB", "stage_B", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "stageC", "stage_C", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "buildingGrain", "building_grain", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "buildingDirt", "building_dirt", ref assignedCount);

            // Walls
            AssignNeutralBuilding(serializedDb, "wallStraight", "wall_straight", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "wallStraightGate", "wall_straight_gate", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "wallCornerAInside", "wall_corner_A_inside", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "wallCornerAOutside", "wall_corner_A_outside", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "wallCornerAGate", "wall_corner_A_gate", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "wallCornerBInside", "wall_corner_B_inside", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "wallCornerBOutside", "wall_corner_B_outside", ref assignedCount);

            // Fences
            AssignNeutralBuilding(serializedDb, "fenceStoneStraight", "fence_stone_straight", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "fenceStoneStraightGate", "fence_stone_straight_gate", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "fenceWoodStraight", "fence_wood_straight", ref assignedCount);
            AssignNeutralBuilding(serializedDb, "fenceWoodStraightGate", "fence_wood_straight_gate", ref assignedCount);

            // Projectiles
            AssignNeutralBuilding(serializedDb, "projectileCatapult", "projectile_catapult", ref assignedCount);

            // Ships (units klasöründe)
            AssignShip(serializedDb, "shipGreen", "ship_green_full", ref assignedCount);
            AssignShip(serializedDb, "shipBlue", "ship_blue_full", ref assignedCount);
            AssignShip(serializedDb, "shipRed", "ship_red_full", ref assignedCount);
            AssignShip(serializedDb, "shipYellow", "ship_yellow_full", ref assignedCount);

            // Units - Soldiers
            AssignUnit(serializedDb, "unitGreen", "unit_green_full", ref assignedCount);
            AssignUnit(serializedDb, "unitBlue", "unit_blue_full", ref assignedCount);
            AssignUnit(serializedDb, "unitRed", "unit_red_full", ref assignedCount);
            AssignUnit(serializedDb, "unitYellow", "unit_yellow_full", ref assignedCount);

            // Units - Cavalry
            AssignUnit(serializedDb, "horseGreen", "horse_green_full", ref assignedCount);
            AssignUnit(serializedDb, "horseBlue", "horse_blue_full", ref assignedCount);
            AssignUnit(serializedDb, "horseRed", "horse_red_full", ref assignedCount);
            AssignUnit(serializedDb, "horseYellow", "horse_yellow_full", ref assignedCount);

            // Units - Artillery
            AssignUnit(serializedDb, "cannonGreen", "cannon_green_full", ref assignedCount);
            AssignUnit(serializedDb, "cannonBlue", "cannon_blue_full", ref assignedCount);
            AssignUnit(serializedDb, "cannonRed", "cannon_red_full", ref assignedCount);
            AssignUnit(serializedDb, "cannonYellow", "cannon_yellow_full", ref assignedCount);
            AssignUnit(serializedDb, "catapultGreen", "catapult_green_full", ref assignedCount);
            AssignUnit(serializedDb, "catapultBlue", "catapult_blue_full", ref assignedCount);
            AssignUnit(serializedDb, "catapultRed", "catapult_red_full", ref assignedCount);
            AssignUnit(serializedDb, "catapultYellow", "catapult_yellow_full", ref assignedCount);

            // Units - Support
            AssignUnit(serializedDb, "bannerGreen", "banner_green_full", ref assignedCount);
            AssignUnit(serializedDb, "bannerBlue", "banner_blue_full", ref assignedCount);
            AssignUnit(serializedDb, "bannerRed", "banner_red_full", ref assignedCount);
            AssignUnit(serializedDb, "bannerYellow", "banner_yellow_full", ref assignedCount);
            AssignUnit(serializedDb, "cartGreen", "cart_green_full", ref assignedCount);
            AssignUnit(serializedDb, "cartBlue", "cart_blue_full", ref assignedCount);
            AssignUnit(serializedDb, "cartRed", "cart_red_full", ref assignedCount);
            AssignUnit(serializedDb, "cartYellow", "cart_yellow_full", ref assignedCount);

            // Units - Weapons
            AssignUnit(serializedDb, "swordGreen", "sword_green_full", ref assignedCount);
            AssignUnit(serializedDb, "swordBlue", "sword_blue_full", ref assignedCount);
            AssignUnit(serializedDb, "swordRed", "sword_red_full", ref assignedCount);
            AssignUnit(serializedDb, "swordYellow", "sword_yellow_full", ref assignedCount);
            AssignUnit(serializedDb, "shieldGreen", "shield_green_full", ref assignedCount);
            AssignUnit(serializedDb, "shieldBlue", "shield_blue_full", ref assignedCount);
            AssignUnit(serializedDb, "shieldRed", "shield_red_full", ref assignedCount);
            AssignUnit(serializedDb, "shieldYellow", "shield_yellow_full", ref assignedCount);
            AssignUnit(serializedDb, "bowGreen", "bow_green_full", ref assignedCount);
            AssignUnit(serializedDb, "bowBlue", "bow_blue_full", ref assignedCount);
            AssignUnit(serializedDb, "bowRed", "bow_red_full", ref assignedCount);
            AssignUnit(serializedDb, "bowYellow", "bow_yellow_full", ref assignedCount);
            AssignUnit(serializedDb, "spearGreen", "spear_green_full", ref assignedCount);
            AssignUnit(serializedDb, "spearBlue", "spear_blue_full", ref assignedCount);
            AssignUnit(serializedDb, "spearRed", "spear_red_full", ref assignedCount);
            AssignUnit(serializedDb, "spearYellow", "spear_yellow_full", ref assignedCount);

            // Boats (decoration/props klasöründe)
            AssignDecoration(serializedDb, "boat", "boat", ref assignedCount);
            AssignDecoration(serializedDb, "boatrack", "boatrack", ref assignedCount);

            serializedDb.ApplyModifiedProperties();
            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();

            Debug.Log($"BuildingDatabaseSetup: {assignedCount} prefab atandi!");

            // WorldMapBootstrap'a da ata
            AssignToBootstrap(db);
        }

        private static string GetFieldName(string buildingType, string color)
        {
            // tower_cannon + green -> towerCannonGreen
            // Special case: archeryrange -> archeryRange (camelCase in field name)
            string[] parts = buildingType.Split('_');
            string result = parts[0].ToLower();

            // Handle special compound words that need internal camelCase
            if (result == "archeryrange")
            {
                result = "archeryRange";
            }

            for (int i = 1; i < parts.Length; i++)
            {
                if (parts[i].Length > 0)
                {
                    result += char.ToUpper(parts[i][0]) + parts[i].Substring(1).ToLower();
                }
            }

            result += char.ToUpper(color[0]) + color.Substring(1).ToLower();
            return result;
        }

        private static string GetPrefabName(string buildingType, string color)
        {
            // tower_cannon + green -> building_tower_cannon_green
            return $"building_{buildingType}_{color}";
        }

        private static GameObject FindPrefab(string prefabName)
        {
            // KayKit klasorunde ara - tum alt klasorlerde
            string[] searchPaths = new[] {
                "Assets/KayKit_Medieval_Hexagon/buildings",
                "Assets/KayKit_Medieval_Hexagon/buildings/blue",
                "Assets/KayKit_Medieval_Hexagon/buildings/green",
                "Assets/KayKit_Medieval_Hexagon/buildings/red",
                "Assets/KayKit_Medieval_Hexagon/buildings/yellow",
                "Assets/KayKit_Medieval_Hexagon/buildings/neutral"
            };

            foreach (string searchPath in searchPaths)
            {
                // Direkt FBX dosyasini dene
                string fbxPath = $"{searchPath}/{prefabName}.fbx";
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(fbxPath);
                if (prefab != null) return prefab;
            }

            // Alternatif: building_ prefix'siz dene (neutral icin)
            if (prefabName.StartsWith("building_"))
            {
                string withoutPrefix = prefabName.Substring(9); // "building_" = 9 karakter
                foreach (string searchPath in searchPaths)
                {
                    string fbxPath = $"{searchPath}/{withoutPrefix}.fbx";
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(fbxPath);
                    if (prefab != null) return prefab;
                }
            }

            return null;
        }

        private static void AssignNeutralBuilding(SerializedObject db, string fieldName, string prefabName, ref int count)
        {
            var prop = db.FindProperty(fieldName);
            if (prop != null)
            {
                GameObject prefab = FindPrefab(prefabName);
                if (prefab != null)
                {
                    prop.objectReferenceValue = prefab;
                    count++;
                }
            }
        }

        private static void AssignShip(SerializedObject db, string fieldName, string prefabName, ref int count)
        {
            var prop = db.FindProperty(fieldName);
            if (prop != null)
            {
                // Ships are in units folder
                string[] colors = new[] { "blue", "green", "red", "yellow" };
                foreach (string color in colors)
                {
                    string path = $"Assets/KayKit_Medieval_Hexagon/units/{color}/{prefabName}.fbx";
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    if (prefab != null)
                    {
                        prop.objectReferenceValue = prefab;
                        count++;
                        return;
                    }
                }
            }
        }

        private static void AssignUnit(SerializedObject db, string fieldName, string prefabName, ref int count)
        {
            var prop = db.FindProperty(fieldName);
            if (prop != null)
            {
                // Units are in units folder, organized by color
                string[] colors = new[] { "blue", "green", "red", "yellow" };
                foreach (string color in colors)
                {
                    string path = $"Assets/KayKit_Medieval_Hexagon/units/{color}/{prefabName}.fbx";
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    if (prefab != null)
                    {
                        prop.objectReferenceValue = prefab;
                        count++;
                        return;
                    }
                }
            }
        }

        private static void AssignDecoration(SerializedObject db, string fieldName, string prefabName, ref int count)
        {
            var prop = db.FindProperty(fieldName);
            if (prop != null)
            {
                string path = $"Assets/KayKit_Medieval_Hexagon/decoration/props/{prefabName}.fbx";
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab != null)
                {
                    prop.objectReferenceValue = prefab;
                    count++;
                }
            }
        }

        private static void AssignToBootstrap(BuildingDatabase db)
        {
            // Sahnedeki WorldMapBootstrap'i bul
            WorldMapBootstrap bootstrap = Object.FindFirstObjectByType<WorldMapBootstrap>();
            if (bootstrap != null)
            {
                SerializedObject serializedBootstrap = new SerializedObject(bootstrap);
                var buildingDbProp = serializedBootstrap.FindProperty("buildingDatabase");
                if (buildingDbProp != null)
                {
                    buildingDbProp.objectReferenceValue = db;
                    serializedBootstrap.ApplyModifiedProperties();
                    EditorUtility.SetDirty(bootstrap);
                    Debug.Log("BuildingDatabaseSetup: WorldMapBootstrap'a BuildingDatabase atandi!");
                }
            }
            else
            {
                Debug.LogWarning("BuildingDatabaseSetup: Sahnede WorldMapBootstrap bulunamadi. Manuel olarak atayin.");
            }
        }
    }
}
