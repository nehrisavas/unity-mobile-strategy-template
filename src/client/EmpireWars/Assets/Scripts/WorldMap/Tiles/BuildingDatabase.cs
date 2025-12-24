using UnityEngine;
using System.Collections.Generic;

namespace EmpireWars.WorldMap.Tiles
{
    /// <summary>
    /// Bina prefab'larını tutan database
    /// KayKit Medieval Hexagon Pack binalarını destekler
    /// Tüm renk varyantları: Green, Blue, Red, Yellow
    /// </summary>
    [CreateAssetMenu(fileName = "BuildingDatabase", menuName = "EmpireWars/Building Database")]
    public class BuildingDatabase : ScriptableObject
    {
        [Header("=== CASTLE/KINGDOM ===")]
        [Tooltip("Ana kale - 4 renk varyantı")]
        public GameObject castleGreen;
        public GameObject castleBlue;
        public GameObject castleRed;
        public GameObject castleYellow;

        [Header("=== TOWERS - Cannon ===")]
        [Tooltip("Topçu kulesi - tower_cannon")]
        public GameObject towerCannonGreen;
        public GameObject towerCannonBlue;
        public GameObject towerCannonRed;
        public GameObject towerCannonYellow;

        [Header("=== TOWERS - Catapult ===")]
        [Tooltip("Mancınık kulesi - tower_catapult")]
        public GameObject towerCatapultGreen;
        public GameObject towerCatapultBlue;
        public GameObject towerCatapultRed;
        public GameObject towerCatapultYellow;

        [Header("=== TOWERS - Watchtower ===")]
        [Tooltip("Gözetleme kulesi - watchtower")]
        public GameObject watchtowerGreen;
        public GameObject watchtowerBlue;
        public GameObject watchtowerRed;
        public GameObject watchtowerYellow;

        [Header("=== TOWERS - Normal ===")]
        [Tooltip("Normal kule - tower_A, tower_B")]
        public GameObject towerAGreen;
        public GameObject towerABlue;
        public GameObject towerARed;
        public GameObject towerAYellow;
        public GameObject towerBGreen;
        public GameObject towerBBlue;
        public GameObject towerBRed;
        public GameObject towerBYellow;

        [Header("=== TOWERS - Base ===")]
        [Tooltip("Kule temeli - tower_base")]
        public GameObject towerBaseGreen;
        public GameObject towerBaseBlue;
        public GameObject towerBaseRed;
        public GameObject towerBaseYellow;

        [Header("=== MILITARY ===")]
        [Tooltip("Kışla - barracks")]
        public GameObject barracksGreen;
        public GameObject barracksBlue;
        public GameObject barracksRed;
        public GameObject barracksYellow;

        [Tooltip("Okçu menzili - archeryrange")]
        public GameObject archeryRangeGreen;
        public GameObject archeryRangeBlue;
        public GameObject archeryRangeRed;
        public GameObject archeryRangeYellow;

        [Tooltip("Ahır - stables")]
        public GameObject stablesGreen;
        public GameObject stablesBlue;
        public GameObject stablesRed;
        public GameObject stablesYellow;

        [Header("=== ECONOMY ===")]
        [Tooltip("Belediye binası - townhall")]
        public GameObject townhallGreen;
        public GameObject townhallBlue;
        public GameObject townhallRed;
        public GameObject townhallYellow;

        [Tooltip("Pazar - market")]
        public GameObject marketGreen;
        public GameObject marketBlue;
        public GameObject marketRed;
        public GameObject marketYellow;

        [Tooltip("Demirci - blacksmith")]
        public GameObject blacksmithGreen;
        public GameObject blacksmithBlue;
        public GameObject blacksmithRed;
        public GameObject blacksmithYellow;

        [Tooltip("Atölye - workshop")]
        public GameObject workshopGreen;
        public GameObject workshopBlue;
        public GameObject workshopRed;
        public GameObject workshopYellow;

        [Header("=== PRODUCTION ===")]
        [Tooltip("Maden - mine")]
        public GameObject mineGreen;
        public GameObject mineBlue;
        public GameObject mineRed;
        public GameObject mineYellow;

        [Tooltip("Kereste fabrikası - lumbermill")]
        public GameObject lumbermillGreen;
        public GameObject lumbermillBlue;
        public GameObject lumbermillRed;
        public GameObject lumbermillYellow;

        [Tooltip("Yel değirmeni - windmill")]
        public GameObject windmillGreen;
        public GameObject windmillBlue;
        public GameObject windmillRed;
        public GameObject windmillYellow;

        [Tooltip("Su değirmeni - watermill")]
        public GameObject watermillGreen;
        public GameObject watermillBlue;
        public GameObject watermillRed;
        public GameObject watermillYellow;

        [Header("=== RESIDENTIAL ===")]
        [Tooltip("Ev A - home_A")]
        public GameObject homeAGreen;
        public GameObject homeABlue;
        public GameObject homeARed;
        public GameObject homeAYellow;

        [Tooltip("Ev B - home_B")]
        public GameObject homeBGreen;
        public GameObject homeBBlue;
        public GameObject homeBRed;
        public GameObject homeBYellow;

        [Tooltip("Taverna - tavern")]
        public GameObject tavernGreen;
        public GameObject tavernBlue;
        public GameObject tavernRed;
        public GameObject tavernYellow;

        [Tooltip("Çadır - tent")]
        public GameObject tentGreen;
        public GameObject tentBlue;
        public GameObject tentRed;
        public GameObject tentYellow;

        [Header("=== RELIGIOUS ===")]
        [Tooltip("Kilise - church")]
        public GameObject churchGreen;
        public GameObject churchBlue;
        public GameObject churchRed;
        public GameObject churchYellow;

        [Tooltip("Tapınak - shrine")]
        public GameObject shrineGreen;
        public GameObject shrineBlue;
        public GameObject shrineRed;
        public GameObject shrineYellow;

        [Header("=== NAVAL ===")]
        [Tooltip("Tersane - shipyard")]
        public GameObject shipyardGreen;
        public GameObject shipyardBlue;
        public GameObject shipyardRed;
        public GameObject shipyardYellow;

        [Tooltip("Rıhtım - docks")]
        public GameObject docksGreen;
        public GameObject docksBlue;
        public GameObject docksRed;
        public GameObject docksYellow;

        [Header("=== UTILITY ===")]
        [Tooltip("Kuyu - well")]
        public GameObject wellGreen;
        public GameObject wellBlue;
        public GameObject wellRed;
        public GameObject wellYellow;

        [Header("=== NEUTRAL/DECORATION ===")]
        [Tooltip("Köprü - bridge")]
        public GameObject bridgeA;
        public GameObject bridgeB;

        [Tooltip("Yıkık bina - destroyed")]
        public GameObject buildingDestroyed;

        [Tooltip("İskele - scaffolding")]
        public GameObject scaffolding;

        [Tooltip("Sahne/Platform - stage")]
        public GameObject stageA;
        public GameObject stageB;
        public GameObject stageC;

        [Tooltip("Tahıl deposu - grain")]
        public GameObject buildingGrain;

        [Tooltip("Toprak/Temel - dirt")]
        public GameObject buildingDirt;

        [Header("=== WALLS ===")]
        [Tooltip("Düz duvar")]
        public GameObject wallStraight;
        public GameObject wallStraightGate;

        [Tooltip("Köşe duvar A")]
        public GameObject wallCornerAInside;
        public GameObject wallCornerAOutside;
        public GameObject wallCornerAGate;

        [Tooltip("Köşe duvar B")]
        public GameObject wallCornerBInside;
        public GameObject wallCornerBOutside;

        [Header("=== FENCES ===")]
        [Tooltip("Taş çit")]
        public GameObject fenceStoneStraight;
        public GameObject fenceStoneStraightGate;

        [Tooltip("Ahşap çit")]
        public GameObject fenceWoodStraight;
        public GameObject fenceWoodStraightGate;

        [Header("=== PROJECTILES ===")]
        [Tooltip("Mancınık mermisi")]
        public GameObject projectileCatapult;

        /// <summary>
        /// Bina adına göre prefab döndürür
        /// Tüm renk varyantlarını destekler
        /// </summary>
        public GameObject GetBuildingPrefab(string buildingType)
        {
            if (string.IsNullOrEmpty(buildingType)) return null;

            return buildingType.ToLower() switch
            {
                // === CASTLES ===
                "castle_green" => castleGreen,
                "castle_blue" => castleBlue,
                "castle_red" => castleRed,
                "castle_yellow" => castleYellow,

                // === TOWERS - Cannon ===
                "tower_cannon_green" => towerCannonGreen,
                "tower_cannon_blue" => towerCannonBlue,
                "tower_cannon_red" => towerCannonRed,
                "tower_cannon_yellow" => towerCannonYellow,

                // === TOWERS - Catapult ===
                "tower_catapult_green" => towerCatapultGreen,
                "tower_catapult_blue" => towerCatapultBlue,
                "tower_catapult_red" => towerCatapultRed,
                "tower_catapult_yellow" => towerCatapultYellow,

                // === TOWERS - Watchtower ===
                "watchtower_green" => watchtowerGreen,
                "watchtower_blue" => watchtowerBlue,
                "watchtower_red" => watchtowerRed,
                "watchtower_yellow" => watchtowerYellow,

                // === TOWERS - Normal A ===
                "tower_a_green" => towerAGreen,
                "tower_a_blue" => towerABlue,
                "tower_a_red" => towerARed,
                "tower_a_yellow" => towerAYellow,

                // === TOWERS - Normal B ===
                "tower_b_green" => towerBGreen,
                "tower_b_blue" => towerBBlue,
                "tower_b_red" => towerBRed,
                "tower_b_yellow" => towerBYellow,

                // === TOWERS - Base ===
                "tower_base_green" => towerBaseGreen,
                "tower_base_blue" => towerBaseBlue,
                "tower_base_red" => towerBaseRed,
                "tower_base_yellow" => towerBaseYellow,

                // === MILITARY - Barracks ===
                "barracks_green" => barracksGreen,
                "barracks_blue" => barracksBlue,
                "barracks_red" => barracksRed,
                "barracks_yellow" => barracksYellow,

                // === MILITARY - Archery Range ===
                "archeryrange_green" => archeryRangeGreen,
                "archeryrange_blue" => archeryRangeBlue,
                "archeryrange_red" => archeryRangeRed,
                "archeryrange_yellow" => archeryRangeYellow,

                // === MILITARY - Stables ===
                "stables_green" => stablesGreen,
                "stables_blue" => stablesBlue,
                "stables_red" => stablesRed,
                "stables_yellow" => stablesYellow,

                // === ECONOMY - Townhall ===
                "townhall_green" => townhallGreen,
                "townhall_blue" => townhallBlue,
                "townhall_red" => townhallRed,
                "townhall_yellow" => townhallYellow,

                // === ECONOMY - Market ===
                "market_green" => marketGreen,
                "market_blue" => marketBlue,
                "market_red" => marketRed,
                "market_yellow" => marketYellow,

                // === ECONOMY - Blacksmith ===
                "blacksmith_green" => blacksmithGreen,
                "blacksmith_blue" => blacksmithBlue,
                "blacksmith_red" => blacksmithRed,
                "blacksmith_yellow" => blacksmithYellow,

                // === ECONOMY - Workshop ===
                "workshop_green" => workshopGreen,
                "workshop_blue" => workshopBlue,
                "workshop_red" => workshopRed,
                "workshop_yellow" => workshopYellow,

                // === PRODUCTION - Mine ===
                "mine_green" => mineGreen,
                "mine_blue" => mineBlue,
                "mine_red" => mineRed,
                "mine_yellow" => mineYellow,

                // === PRODUCTION - Lumbermill ===
                "lumbermill_green" => lumbermillGreen,
                "lumbermill_blue" => lumbermillBlue,
                "lumbermill_red" => lumbermillRed,
                "lumbermill_yellow" => lumbermillYellow,

                // === PRODUCTION - Windmill ===
                "windmill_green" => windmillGreen,
                "windmill_blue" => windmillBlue,
                "windmill_red" => windmillRed,
                "windmill_yellow" => windmillYellow,

                // === PRODUCTION - Watermill ===
                "watermill_green" => watermillGreen,
                "watermill_blue" => watermillBlue,
                "watermill_red" => watermillRed,
                "watermill_yellow" => watermillYellow,

                // === RESIDENTIAL - Home A ===
                "home_a_green" => homeAGreen,
                "home_a_blue" => homeABlue,
                "home_a_red" => homeARed,
                "home_a_yellow" => homeAYellow,

                // === RESIDENTIAL - Home B ===
                "home_b_green" => homeBGreen,
                "home_b_blue" => homeBBlue,
                "home_b_red" => homeBRed,
                "home_b_yellow" => homeBYellow,

                // === RESIDENTIAL - Tavern ===
                "tavern_green" => tavernGreen,
                "tavern_blue" => tavernBlue,
                "tavern_red" => tavernRed,
                "tavern_yellow" => tavernYellow,

                // === RESIDENTIAL - Tent ===
                "tent_green" => tentGreen,
                "tent_blue" => tentBlue,
                "tent_red" => tentRed,
                "tent_yellow" => tentYellow,

                // === RELIGIOUS - Church ===
                "church_green" => churchGreen,
                "church_blue" => churchBlue,
                "church_red" => churchRed,
                "church_yellow" => churchYellow,

                // === RELIGIOUS - Shrine ===
                "shrine_green" => shrineGreen,
                "shrine_blue" => shrineBlue,
                "shrine_red" => shrineRed,
                "shrine_yellow" => shrineYellow,

                // === NAVAL - Shipyard ===
                "shipyard_green" => shipyardGreen,
                "shipyard_blue" => shipyardBlue,
                "shipyard_red" => shipyardRed,
                "shipyard_yellow" => shipyardYellow,

                // === NAVAL - Docks ===
                "docks_green" => docksGreen,
                "docks_blue" => docksBlue,
                "docks_red" => docksRed,
                "docks_yellow" => docksYellow,

                // === UTILITY - Well ===
                "well_green" => wellGreen,
                "well_blue" => wellBlue,
                "well_red" => wellRed,
                "well_yellow" => wellYellow,

                // === NEUTRAL/DECORATION ===
                "bridge_a" => bridgeA,
                "bridge_b" => bridgeB,
                "destroyed" => buildingDestroyed,
                "scaffolding" => scaffolding,
                "stage_a" => stageA,
                "stage_b" => stageB,
                "stage_c" => stageC,
                "grain" => buildingGrain,
                "dirt" => buildingDirt,

                // === WALLS ===
                "wall_straight" => wallStraight,
                "wall_straight_gate" => wallStraightGate,
                "wall_corner_a_inside" => wallCornerAInside,
                "wall_corner_a_outside" => wallCornerAOutside,
                "wall_corner_a_gate" => wallCornerAGate,
                "wall_corner_b_inside" => wallCornerBInside,
                "wall_corner_b_outside" => wallCornerBOutside,

                // === FENCES ===
                "fence_stone_straight" => fenceStoneStraight,
                "fence_stone_straight_gate" => fenceStoneStraightGate,
                "fence_wood_straight" => fenceWoodStraight,
                "fence_wood_straight_gate" => fenceWoodStraightGate,

                // === PROJECTILES ===
                "projectile_catapult" => projectileCatapult,

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
                3 => "_yellow",
                _ => "_green"
            };

            return GetBuildingPrefab(buildingType + teamSuffix);
        }

        /// <summary>
        /// Tüm bina tiplerinin listesini döndür
        /// </summary>
        public static string[] GetAllBuildingTypes()
        {
            return new string[]
            {
                "castle", "tower_cannon", "tower_catapult", "watchtower",
                "tower_a", "tower_b", "tower_base",
                "barracks", "archeryrange", "stables",
                "townhall", "market", "blacksmith", "workshop",
                "mine", "lumbermill", "windmill", "watermill",
                "home_a", "home_b", "tavern", "tent",
                "church", "shrine",
                "shipyard", "docks",
                "well"
            };
        }

        /// <summary>
        /// Renk varyantlarının listesi
        /// </summary>
        public static string[] GetColorVariants()
        {
            return new string[] { "green", "blue", "red", "yellow" };
        }
    }
}
