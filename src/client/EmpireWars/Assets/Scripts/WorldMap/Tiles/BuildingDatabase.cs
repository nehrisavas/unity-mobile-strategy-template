using UnityEngine;
using System.Collections.Generic;

namespace EmpireWars.WorldMap.Tiles
{
    /// <summary>
    /// Bina prefab'larını tutan database
    /// KayKit Medieval Hexagon Pack binalarını destekler
    /// </summary>
    [CreateAssetMenu(fileName = "BuildingDatabase", menuName = "EmpireWars/Building Database")]
    public class BuildingDatabase : ScriptableObject
    {
        [Header("=== CASTLE/KINGDOM ===")]
        [Tooltip("Ana kale - castle_green, castle_blue, castle_red")]
        public GameObject castleGreen;
        public GameObject castleBlue;
        public GameObject castleRed;

        [Header("=== TOWERS ===")]
        [Tooltip("Topçu kulesi - tower_cannon")]
        public GameObject towerCannonGreen;
        public GameObject towerCannonBlue;
        public GameObject towerCannonRed;

        [Tooltip("Mancınık kulesi - tower_catapult")]
        public GameObject towerCatapultGreen;
        public GameObject towerCatapultBlue;
        public GameObject towerCatapultRed;

        [Tooltip("Gözetleme kulesi - watchtower")]
        public GameObject watchtowerGreen;
        public GameObject watchtowerBlue;
        public GameObject watchtowerRed;

        [Tooltip("Normal kule - tower_A, tower_B")]
        public GameObject towerAGreen;
        public GameObject towerBGreen;

        [Header("=== MILITARY ===")]
        [Tooltip("Kışla - barracks")]
        public GameObject barracksGreen;
        public GameObject barracksBlue;
        public GameObject barracksRed;

        [Tooltip("Okçu menzili - archeryrange")]
        public GameObject archeryRangeGreen;

        [Tooltip("Ahır - stables")]
        public GameObject stablesGreen;

        [Header("=== ECONOMY ===")]
        [Tooltip("Belediye binası - townhall")]
        public GameObject townhallGreen;

        [Tooltip("Pazar - market")]
        public GameObject marketGreen;

        [Tooltip("Demirci - blacksmith")]
        public GameObject blacksmithGreen;

        [Tooltip("Atölye - workshop")]
        public GameObject workshopGreen;

        [Header("=== PRODUCTION ===")]
        [Tooltip("Maden - mine")]
        public GameObject mineGreen;

        [Tooltip("Kereste fabrikası - lumbermill")]
        public GameObject lumbermillGreen;

        [Tooltip("Değirmen - windmill, watermill")]
        public GameObject windmillGreen;
        public GameObject watermillGreen;

        [Header("=== RESIDENTIAL ===")]
        [Tooltip("Ev - home_A, home_B")]
        public GameObject homeAGreen;
        public GameObject homeBGreen;

        [Tooltip("Taverna - tavern")]
        public GameObject tavernGreen;

        [Tooltip("Çadır - tent")]
        public GameObject tentGreen;

        [Header("=== RELIGIOUS ===")]
        [Tooltip("Kilise - church")]
        public GameObject churchGreen;

        [Tooltip("Tapınak - shrine")]
        public GameObject shrineGreen;

        [Header("=== NAVAL ===")]
        [Tooltip("Tersane - shipyard")]
        public GameObject shipyardGreen;

        [Tooltip("Rıhtım - docks")]
        public GameObject docksGreen;

        [Header("=== NEUTRAL ===")]
        [Tooltip("Köprü - bridge")]
        public GameObject bridgeA;
        public GameObject bridgeB;

        [Tooltip("Yıkık bina - destroyed")]
        public GameObject buildingDestroyed;

        [Tooltip("İskele - scaffolding")]
        public GameObject scaffolding;

        /// <summary>
        /// Bina adına göre prefab döndürür
        /// </summary>
        public GameObject GetBuildingPrefab(string buildingType)
        {
            return buildingType?.ToLower() switch
            {
                // Castles
                "castle_green" => castleGreen,
                "castle_blue" => castleBlue,
                "castle_red" => castleRed,

                // Towers - Cannon
                "tower_cannon_green" => towerCannonGreen,
                "tower_cannon_blue" => towerCannonBlue,
                "tower_cannon_red" => towerCannonRed,

                // Towers - Catapult
                "tower_catapult_green" => towerCatapultGreen,
                "tower_catapult_blue" => towerCatapultBlue,
                "tower_catapult_red" => towerCatapultRed,

                // Towers - Watchtower
                "watchtower_green" => watchtowerGreen,
                "watchtower_blue" => watchtowerBlue,
                "watchtower_red" => watchtowerRed,

                // Towers - Normal
                "tower_a_green" => towerAGreen,
                "tower_b_green" => towerBGreen,

                // Military
                "barracks_green" => barracksGreen,
                "barracks_blue" => barracksBlue,
                "barracks_red" => barracksRed,
                "archeryrange_green" => archeryRangeGreen,
                "stables_green" => stablesGreen,

                // Economy
                "townhall_green" => townhallGreen,
                "market_green" => marketGreen,
                "blacksmith_green" => blacksmithGreen,
                "workshop_green" => workshopGreen,

                // Production
                "mine_green" => mineGreen,
                "lumbermill_green" => lumbermillGreen,
                "windmill_green" => windmillGreen,
                "watermill_green" => watermillGreen,

                // Residential
                "home_a_green" => homeAGreen,
                "home_b_green" => homeBGreen,
                "tavern_green" => tavernGreen,
                "tent_green" => tentGreen,

                // Religious
                "church_green" => churchGreen,
                "shrine_green" => shrineGreen,

                // Naval
                "shipyard_green" => shipyardGreen,
                "docks_green" => docksGreen,

                // Neutral
                "bridge_a" => bridgeA,
                "bridge_b" => bridgeB,
                "destroyed" => buildingDestroyed,
                "scaffolding" => scaffolding,

                _ => null
            };
        }

        /// <summary>
        /// Takıma göre bina prefab'ı döndürür
        /// </summary>
        public GameObject GetBuildingForTeam(string buildingType, int teamId)
        {
            string teamSuffix = teamId switch
            {
                0 => "_green",
                1 => "_blue",
                2 => "_red",
                _ => "_green"
            };

            return GetBuildingPrefab(buildingType + teamSuffix);
        }
    }
}
