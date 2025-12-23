using UnityEngine;
using System;
using System.Collections.Generic;
using EmpireWars.Data;

namespace EmpireWars.WorldMap.Tiles
{
    /// <summary>
    /// Terrain tipi ile prefab eslemelerini tutan ScriptableObject database
    /// </summary>
    [CreateAssetMenu(fileName = "HexTilePrefabDatabase", menuName = "EmpireWars/Hex Tile Prefab Database")]
    public class HexTilePrefabDatabase : ScriptableObject
    {
        [Header("=== BASE TILES ===")]
        [Tooltip("Cimen hex tile prefab - hex_grass")]
        public GameObject grassTile;

        [Tooltip("Su hex tile prefab - hex_water")]
        public GameObject waterTile;

        [Tooltip("Orman hex tile prefab (cimen + agac dekorasyonu)")]
        public GameObject forestTile;

        [Tooltip("Dag hex tile prefab")]
        public GameObject mountainTile;

        [Tooltip("Col hex tile prefab")]
        public GameObject desertTile;

        [Tooltip("Kar hex tile prefab")]
        public GameObject snowTile;

        [Tooltip("Bataklik hex tile prefab")]
        public GameObject swampTile;

        [Tooltip("Tepe hex tile prefab")]
        public GameObject hillTile;

        [Tooltip("Duz arazi hex tile prefab")]
        public GameObject plainsTile;

        [Header("=== SLOPED TILES ===")]
        [Tooltip("Yuksek egimli cimen - hex_grass_sloped_high")]
        public GameObject grassSlopedHighTile;

        [Tooltip("Alcak egimli cimen - hex_grass_sloped_low")]
        public GameObject grassSlopedLowTile;

        [Header("=== SPECIAL TILES ===")]
        [Tooltip("Yol tile'lari - hex_road_A ~ hex_road_M")]
        public GameObject[] roadTiles;

        [Tooltip("Nehir tile'lari - hex_river_A ~ hex_river_L")]
        public GameObject[] riverTiles;

        [Tooltip("Kiyi tile'lari - hex_coast_A ~ hex_coast_E")]
        public GameObject[] coastTiles;

        [Tooltip("Kopru tile'lari - hex_river_crossing_A/B")]
        public GameObject[] bridgeTiles;

        [Header("=== RESOURCE TILES ===")]
        [Tooltip("Ciftlik tile'i")]
        public GameObject farmTile;

        [Tooltip("Maden tile'i")]
        public GameObject mineTile;

        [Tooltip("Tas ocagi tile'i")]
        public GameObject quarryTile;

        [Tooltip("Altin madeni tile'i")]
        public GameObject goldMineTile;

        [Tooltip("Mucevher madeni tile'i")]
        public GameObject gemMineTile;

        [Header("=== FALLBACK ===")]
        [Tooltip("Varsayilan tile (eger terrain tipi bulunamazsa)")]
        public GameObject defaultTile;

        /// <summary>
        /// Terrain tipine gore uygun prefab'i dondurur
        /// KayKit'te olmayan terrain tipleri icin grassTile fallback olarak kullanilir
        /// </summary>
        public GameObject GetTilePrefab(TerrainType terrainType)
        {
            GameObject fallback = grassTile ?? defaultTile;

            return terrainType switch
            {
                TerrainType.Grass => grassTile ?? defaultTile,
                TerrainType.Water => waterTile ?? fallback,
                TerrainType.Forest => forestTile ?? fallback,
                TerrainType.Mountain => mountainTile ?? fallback,
                TerrainType.Desert => desertTile ?? fallback,
                TerrainType.Snow => snowTile ?? fallback,
                TerrainType.Swamp => swampTile ?? fallback,
                TerrainType.Hill => hillTile ?? plainsTile ?? fallback,
                TerrainType.Road => roadTiles != null && roadTiles.Length > 0 ? roadTiles[0] : fallback,
                TerrainType.Coast => coastTiles != null && coastTiles.Length > 0 ? coastTiles[0] : fallback,
                TerrainType.River => riverTiles != null && riverTiles.Length > 0 ? riverTiles[0] : fallback,
                TerrainType.Bridge => bridgeTiles != null && bridgeTiles.Length > 0 ? bridgeTiles[0] : fallback,
                TerrainType.GrassSlopedHigh => grassSlopedHighTile ?? fallback,
                TerrainType.GrassSlopedLow => grassSlopedLowTile ?? fallback,
                TerrainType.Farm => farmTile ?? fallback,
                TerrainType.Mine => mineTile ?? fallback,
                TerrainType.Quarry => quarryTile ?? fallback,
                TerrainType.GoldMine => goldMineTile ?? fallback,
                TerrainType.GemMine => gemMineTile ?? fallback,
                _ => fallback
            };
        }

        /// <summary>
        /// Yol tile'i dondurur (baglanti yonune gore)
        /// </summary>
        public GameObject GetRoadTile(int connectionMask)
        {
            if (roadTiles == null || roadTiles.Length == 0)
                return null;

            int index = Mathf.Clamp(connectionMask % roadTiles.Length, 0, roadTiles.Length - 1);
            return roadTiles[index];
        }

        /// <summary>
        /// Nehir tile'i dondurur
        /// </summary>
        public GameObject GetRiverTile(int connectionMask)
        {
            if (riverTiles == null || riverTiles.Length == 0)
                return null;

            int index = Mathf.Clamp(connectionMask % riverTiles.Length, 0, riverTiles.Length - 1);
            return riverTiles[index];
        }

        /// <summary>
        /// Kiyi tile'i dondurur
        /// </summary>
        public GameObject GetCoastTile(int connectionMask)
        {
            if (coastTiles == null || coastTiles.Length == 0)
                return null;

            int index = Mathf.Clamp(connectionMask % coastTiles.Length, 0, coastTiles.Length - 1);
            return coastTiles[index];
        }

        /// <summary>
        /// Kopru tile'i dondurur
        /// </summary>
        public GameObject GetBridgeTile(int connectionMask)
        {
            if (bridgeTiles == null || bridgeTiles.Length == 0)
                return null;

            int index = Mathf.Clamp(connectionMask % bridgeTiles.Length, 0, bridgeTiles.Length - 1);
            return bridgeTiles[index];
        }
    }
}
