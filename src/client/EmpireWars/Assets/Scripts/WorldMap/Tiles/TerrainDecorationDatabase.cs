using UnityEngine;
using System.Collections.Generic;
using EmpireWars.Data;

namespace EmpireWars.WorldMap.Tiles
{
    /// <summary>
    /// Terrain tipine gore dekorasyon prefab'larini tutan database
    /// Dag, agac, kaya gibi ustune eklenen objeler
    /// KayKit Medieval Hexagon Pack dekorasyonlarini destekler
    /// </summary>
    [CreateAssetMenu(fileName = "TerrainDecorationDatabase", menuName = "EmpireWars/Terrain Decoration Database")]
    public class TerrainDecorationDatabase : ScriptableObject
    {
        [Header("=== NATURE DECORATIONS ===")]

        [Header("Forest Decorations")]
        [Tooltip("Orman agaclari - trees_A_large, trees_B_large vb.")]
        public GameObject[] forestTrees;

        [Header("Single Trees")]
        [Tooltip("Tek agaclar - tree_single_A, tree_single_B")]
        public GameObject[] singleTrees;

        [Header("Mountain Decorations")]
        [Tooltip("Dag modelleri - mountain_A, mountain_B, mountain_C (grass/trees varyantlari)")]
        public GameObject[] mountainModels;

        [Header("Hill Decorations")]
        [Tooltip("Tepe modelleri - hills_A, hills_B, hills_C, hill_single_A/B/C")]
        public GameObject[] hillRocks;

        [Header("Rock Decorations")]
        [Tooltip("Tek kaya modelleri - rock_single_A/B/C/D/E")]
        public GameObject[] singleRocks;

        [Header("Desert Decorations")]
        [Tooltip("Col dekorasyonlari - kuru kayalar, kumul")]
        public GameObject[] desertDecorations;

        [Header("Snow Decorations")]
        [Tooltip("Karli agac veya buz kayalari")]
        public GameObject[] snowDecorations;

        [Header("Swamp Decorations")]
        [Tooltip("Bataklik dekorasyonlari - kesik agac (tree_single_A_cut, tree_single_B_cut)")]
        public GameObject[] swampDecorations;

        [Header("Water Decorations")]
        [Tooltip("Su bitkileri - waterlily_A/B, waterplant_A/B/C")]
        public GameObject[] waterDecorations;

        [Header("Cloud Decorations")]
        [Tooltip("Bulut modelleri - cloud_big, cloud_small")]
        public GameObject[] cloudModels;

        [Header("=== PROP DECORATIONS ===")]

        [Header("Farm Props")]
        [Tooltip("Ciftlik proplari - haybale, sack, wheelbarrow, trough")]
        public GameObject[] farmProps;

        [Header("Military Props")]
        [Tooltip("Askeri proplar - barrel, crate, flag, tent, weaponrack")]
        public GameObject[] militaryProps;

        [Header("Resource Props")]
        [Tooltip("Kaynak proplari - resource_lumber, resource_stone, bucket")]
        public GameObject[] resourceProps;

        [Header("Coastal Props")]
        [Tooltip("Kiyi proplari - anchor, boat, boatrack")]
        public GameObject[] coastalProps;

        /// <summary>
        /// Terrain tipine gore rastgele bir dekorasyon prefab'i dondurur
        /// </summary>
        public GameObject GetRandomDecoration(TerrainType terrainType, int seed = 0)
        {
            GameObject[] pool = GetDecorationPool(terrainType);
            if (pool == null || pool.Length == 0) return null;

            // Seed'e gore tutarli rastgele secim (ayni tile her zaman ayni dekorasyon)
            int index = Mathf.Abs(seed) % pool.Length;
            return pool[index];
        }

        /// <summary>
        /// Terrain tipine gore dekorasyon havuzunu dondurur
        /// </summary>
        public GameObject[] GetDecorationPool(TerrainType terrainType)
        {
            return terrainType switch
            {
                TerrainType.Forest => forestTrees,
                TerrainType.Mountain => mountainModels,
                TerrainType.Hill => hillRocks,
                TerrainType.Desert => desertDecorations,
                TerrainType.Snow => snowDecorations,
                TerrainType.Swamp => swampDecorations,
                TerrainType.Water => waterDecorations,
                TerrainType.Coast => coastalProps,
                TerrainType.Farm => farmProps,
                TerrainType.Mine => resourceProps,
                TerrainType.Quarry => resourceProps,
                TerrainType.GoldMine => resourceProps,
                TerrainType.GemMine => resourceProps,
                _ => null
            };
        }

        /// <summary>
        /// Terrain tipinin dekorasyon gerektirip gerektirmedigini dondurur
        /// </summary>
        public bool RequiresDecoration(TerrainType terrainType)
        {
            return terrainType switch
            {
                TerrainType.Forest => true,
                TerrainType.Mountain => true,
                TerrainType.Hill => true,
                TerrainType.Desert => true,
                TerrainType.Snow => true,
                TerrainType.Swamp => true,
                TerrainType.Water => true,
                TerrainType.Coast => true,
                TerrainType.Farm => true,
                TerrainType.Mine => true,
                TerrainType.Quarry => true,
                TerrainType.GoldMine => true,
                TerrainType.GemMine => true,
                _ => false
            };
        }

        /// <summary>
        /// Terrain tipine gore dekorasyon olcegi dondurur
        /// </summary>
        public float GetDecorationScale(TerrainType terrainType)
        {
            return terrainType switch
            {
                TerrainType.Mountain => 1.0f,   // Dag buyuk
                TerrainType.Forest => 0.8f,     // Agaclar normal
                TerrainType.Hill => 0.6f,       // Tepeler orta
                TerrainType.Desert => 0.4f,     // Col kuculmus
                TerrainType.Snow => 0.7f,       // Kar normal
                TerrainType.Swamp => 0.5f,      // Bataklik orta
                TerrainType.Water => 0.4f,      // Su bitkileri kucuk
                TerrainType.Coast => 0.6f,      // Kiyi proplari orta
                TerrainType.Farm => 0.5f,       // Ciftlik proplari orta
                TerrainType.Mine => 0.7f,       // Maden proplari normal
                TerrainType.Quarry => 0.7f,     // Tas ocagi proplari normal
                TerrainType.GoldMine => 0.7f,   // Altin madeni proplari
                TerrainType.GemMine => 0.7f,    // Mucevher madeni proplari
                _ => 1.0f
            };
        }

        /// <summary>
        /// Terrain tipine gore dekorasyon Y offset dondurur
        /// </summary>
        public float GetDecorationYOffset(TerrainType terrainType)
        {
            return terrainType switch
            {
                TerrainType.Mountain => 0.0f,   // Dag yerde
                TerrainType.Forest => 0.05f,    // Agaclar hafif yukari
                TerrainType.Hill => 0.1f,       // Tepeler yukari
                TerrainType.Water => 0.02f,     // Su bitkileri su seviyesinde
                TerrainType.Coast => 0.05f,     // Kiyi proplari hafif yukari
                TerrainType.Farm => 0.05f,      // Ciftlik proplari hafif yukari
                TerrainType.Mine => 0.05f,      // Maden proplari hafif yukari
                TerrainType.Quarry => 0.05f,    // Tas ocagi
                TerrainType.GoldMine => 0.05f,  // Altin madeni
                TerrainType.GemMine => 0.05f,   // Mucevher madeni
                _ => 0.0f
            };
        }

        /// <summary>
        /// Rastgele bulut dekorasyonu dondurur (haritada havaya yerlestirilir)
        /// </summary>
        public GameObject GetRandomCloud(int seed)
        {
            if (cloudModels == null || cloudModels.Length == 0) return null;
            int index = Mathf.Abs(seed) % cloudModels.Length;
            return cloudModels[index];
        }

        /// <summary>
        /// Rastgele tek agac dondurur (seyrek agaclik alanlar icin)
        /// </summary>
        public GameObject GetRandomSingleTree(int seed)
        {
            if (singleTrees == null || singleTrees.Length == 0) return null;
            int index = Mathf.Abs(seed) % singleTrees.Length;
            return singleTrees[index];
        }

        /// <summary>
        /// Rastgele tek kaya dondurur
        /// </summary>
        public GameObject GetRandomRock(int seed)
        {
            if (singleRocks == null || singleRocks.Length == 0) return null;
            int index = Mathf.Abs(seed) % singleRocks.Length;
            return singleRocks[index];
        }
    }
}
