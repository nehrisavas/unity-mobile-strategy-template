using UnityEngine;
using System.Collections.Generic;
using EmpireWars.Data;

namespace EmpireWars.WorldMap.Tiles
{
    /// <summary>
    /// Terrain tipine gore dekorasyon prefab'larini tutan database
    /// Dag, agac, kaya gibi ustune eklenen objeler
    /// </summary>
    [CreateAssetMenu(fileName = "TerrainDecorationDatabase", menuName = "EmpireWars/Terrain Decoration Database")]
    public class TerrainDecorationDatabase : ScriptableObject
    {
        [Header("Forest Decorations")]
        [Tooltip("Orman agaclari - hex ustune eklenir")]
        public GameObject[] forestTrees;

        [Header("Mountain Decorations")]
        [Tooltip("Dag modelleri - gecilmez alan")]
        public GameObject[] mountainModels;

        [Header("Hill Decorations")]
        [Tooltip("Tepe modelleri veya kayalar")]
        public GameObject[] hillRocks;

        [Header("Desert Decorations")]
        [Tooltip("Col dekorasyonlari - kaktus, kuru agac vb.")]
        public GameObject[] desertDecorations;

        [Header("Snow Decorations")]
        [Tooltip("Karli agac veya buz kayalari")]
        public GameObject[] snowDecorations;

        [Header("Swamp Decorations")]
        [Tooltip("Bataklik dekorasyonlari - kesik agac, sivri ot")]
        public GameObject[] swampDecorations;

        [Header("Rock Decorations")]
        [Tooltip("Tek kaya modelleri - genel kullanim")]
        public GameObject[] singleRocks;

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
                _ => 0.0f
            };
        }
    }
}
