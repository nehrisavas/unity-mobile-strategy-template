using UnityEngine;
using EmpireWars.Alliance;
using EmpireWars.WorldMap.Tiles;

namespace EmpireWars.Units
{
    /// <summary>
    /// Birim spawn sistemi - İttifak renkleriyle birim oluşturur
    /// </summary>
    public class UnitSpawner : MonoBehaviour
    {
        public static UnitSpawner Instance { get; private set; }

        [Header("Referanslar")]
        [SerializeField] private BuildingDatabase buildingDatabase;

        [Header("Spawn Ayarları")]
        [SerializeField] private float defaultTintStrength = 0.6f;
        [SerializeField] private bool applyColorOnSpawn = true;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Birim spawn et (ittifak rengi ile)
        /// </summary>
        /// <param name="unitType">Birim tipi (örn: "soldier_knight_male")</param>
        /// <param name="position">Spawn pozisyonu</param>
        /// <param name="allianceId">İttifak ID</param>
        /// <returns>Oluşturulan birim</returns>
        public GameObject SpawnUnit(string unitType, Vector3 position, int allianceId)
        {
            return SpawnUnit(unitType, position, allianceId, Quaternion.identity);
        }

        /// <summary>
        /// Birim spawn et (ittifak rengi ve rotasyon ile)
        /// </summary>
        public GameObject SpawnUnit(string unitType, Vector3 position, int allianceId, Quaternion rotation)
        {
            if (buildingDatabase == null)
            {
                Debug.LogError("UnitSpawner: BuildingDatabase atanmamış!");
                return null;
            }

            // Prefab'ı al
            GameObject prefab = buildingDatabase.GetBuildingPrefab(unitType);
            if (prefab == null)
            {
                Debug.LogWarning($"UnitSpawner: '{unitType}' prefab'ı bulunamadı!");
                return null;
            }

            // Spawn et
            GameObject unit = Instantiate(prefab, position, rotation);
            unit.name = $"{unitType}_{allianceId}";

            // İttifak rengini uygula
            if (applyColorOnSpawn && AllianceManager.Instance != null)
            {
                Color allianceColor = AllianceManager.Instance.GetAllianceColor(allianceId);
                UnitColorSystem.ApplyAllianceColor(unit, allianceColor, defaultTintStrength);

                // Renk komponentini ekle (sonradan değişiklik için)
                var colorComp = unit.AddComponent<UnitAllianceColor>();
                colorComp.allianceColor = allianceColor;
                colorComp.tintStrength = defaultTintStrength;
                colorComp.applyOnStart = false; // Zaten uyguladık
            }

            return unit;
        }

        /// <summary>
        /// Birim spawn et (doğrudan renk ile)
        /// </summary>
        public GameObject SpawnUnitWithColor(string unitType, Vector3 position, Color color, Quaternion rotation = default)
        {
            if (rotation == default) rotation = Quaternion.identity;

            if (buildingDatabase == null)
            {
                Debug.LogError("UnitSpawner: BuildingDatabase atanmamış!");
                return null;
            }

            GameObject prefab = buildingDatabase.GetBuildingPrefab(unitType);
            if (prefab == null)
            {
                Debug.LogWarning($"UnitSpawner: '{unitType}' prefab'ı bulunamadı!");
                return null;
            }

            GameObject unit = Instantiate(prefab, position, rotation);
            UnitColorSystem.ApplyAllianceColor(unit, color, defaultTintStrength);

            var colorComp = unit.AddComponent<UnitAllianceColor>();
            colorComp.allianceColor = color;
            colorComp.tintStrength = defaultTintStrength;
            colorComp.applyOnStart = false;

            return unit;
        }

        /// <summary>
        /// Oyuncu ID'sine göre birim spawn et
        /// </summary>
        public GameObject SpawnUnitForPlayer(string unitType, Vector3 position, string playerId)
        {
            int allianceId = -1;
            if (AllianceManager.Instance != null)
            {
                allianceId = AllianceManager.Instance.GetPlayerAllianceId(playerId);
            }

            return SpawnUnit(unitType, position, allianceId);
        }

        /// <summary>
        /// Ordu formasyonu spawn et
        /// </summary>
        public void SpawnArmyFormation(Vector3 centerPosition, int allianceId, int rows = 5, int columns = 10)
        {
            string[] unitTypes = new string[]
            {
                "soldier_knight_male",
                "soldier_modular_male",
                "soldier_modular_female",
                "soldier_light_male",
                "soldier_light_female"
            };

            float spacing = 1.5f;
            int unitTypeIndex = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Vector3 offset = new Vector3(
                        (col - columns / 2f) * spacing,
                        0,
                        (row - rows / 2f) * spacing
                    );

                    string unitType = unitTypes[unitTypeIndex % unitTypes.Length];
                    SpawnUnit(unitType, centerPosition + offset, allianceId);

                    unitTypeIndex++;
                }
            }
        }

        /// <summary>
        /// BuildingDatabase'i ayarla
        /// </summary>
        public void SetBuildingDatabase(BuildingDatabase db)
        {
            buildingDatabase = db;
        }
    }
}
