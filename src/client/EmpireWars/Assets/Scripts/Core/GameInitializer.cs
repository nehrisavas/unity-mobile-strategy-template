using UnityEngine;
using EmpireWars.WorldMap.Tiles;

namespace EmpireWars.Core
{
    /// <summary>
    /// Oyun baslatici - tum sistemleri otomatik kurar
    /// Sahneye bu scripti ekle, gerisini o halleder
    /// </summary>
    public class GameInitializer : MonoBehaviour
    {
        [Header("Databases (Inspector'dan ata)")]
        [SerializeField] private HexTilePrefabDatabase tilePrefabDatabase;
        [SerializeField] private TerrainDecorationDatabase decorationDatabase;

        [Header("Map Settings")]
        [SerializeField] private int mapWidth = 10;
        [SerializeField] private int mapHeight = 10;

        [Header("HD Settings")]
        [SerializeField] private bool enableHDSetup = true;

        // Olusturulan objeler
        private GameObject hexGridObj;
        private GameObject factoryObj;
        private HexTileFactory tileFactory;
        private HDSceneSetup hdSetup;

        private void Awake()
        {
            Debug.Log("=== EmpireWars Game Initializer ===");

            // HD Setup
            if (enableHDSetup)
            {
                SetupHD();
            }
        }

        private void Start()
        {
            // Harita olustur
            CreateMap();

            Debug.Log("=== Game Initialization Complete ===");
        }

        private void SetupHD()
        {
            hdSetup = GetComponent<HDSceneSetup>();
            if (hdSetup == null)
            {
                hdSetup = gameObject.AddComponent<HDSceneSetup>();
            }
            hdSetup.SetupHDScene();
        }

        private void CreateMap()
        {
            // HexGrid parent
            hexGridObj = new GameObject("HexGrid");
            hexGridObj.transform.SetParent(transform);

            // HexTileFactory
            factoryObj = new GameObject("HexTileFactory");
            factoryObj.transform.SetParent(transform);
            tileFactory = factoryObj.AddComponent<HexTileFactory>();

            // Database'leri ata (reflection ile)
            AssignDatabases();

            // Haritayi olustur
            if (tilePrefabDatabase != null)
            {
                tileFactory.GenerateTestGrid(mapWidth, mapHeight);
                Debug.Log($"Harita olusturuldu: {mapWidth}x{mapHeight}");
            }
            else
            {
                Debug.LogError("GameInitializer: tilePrefabDatabase atanmamis! Inspector'dan ata.");
            }
        }

        private void AssignDatabases()
        {
            var factoryType = typeof(HexTileFactory);
            var flags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;

            // Tile database
            var prefabDbField = factoryType.GetField("prefabDatabase", flags);
            if (prefabDbField != null && tilePrefabDatabase != null)
            {
                prefabDbField.SetValue(tileFactory, tilePrefabDatabase);
            }

            // Decoration database
            var decorDbField = factoryType.GetField("decorationDatabase", flags);
            if (decorDbField != null && decorationDatabase != null)
            {
                decorDbField.SetValue(tileFactory, decorationDatabase);
            }

            // Tiles parent
            var tilesParentField = factoryType.GetField("tilesParent", flags);
            if (tilesParentField != null)
            {
                tilesParentField.SetValue(tileFactory, hexGridObj.transform);
            }

            // Add decorations
            var addDecorField = factoryType.GetField("addDecorations", flags);
            if (addDecorField != null)
            {
                addDecorField.SetValue(tileFactory, decorationDatabase != null);
            }
        }

        /// <summary>
        /// Haritayi yeniden olusturur
        /// </summary>
        [ContextMenu("Regenerate Map")]
        public void RegenerateMap()
        {
            if (tileFactory != null)
            {
                tileFactory.ClearAllTiles();
                tileFactory.GenerateTestGrid(mapWidth, mapHeight);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Editor'da database degistiginde uyari goster
            if (tilePrefabDatabase == null)
            {
                Debug.LogWarning("GameInitializer: tilePrefabDatabase atanmamis!");
            }
        }
#endif
    }
}
