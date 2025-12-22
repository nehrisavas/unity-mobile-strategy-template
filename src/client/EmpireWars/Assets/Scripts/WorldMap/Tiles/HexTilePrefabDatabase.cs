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
        [Header("Base Tiles")]
        [Tooltip("Cimen hex tile prefab")]
        public GameObject grassTile;

        [Tooltip("Su hex tile prefab")]
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

        [Tooltip("Duz arazi hex tile prefab")]
        public GameObject plainsTile;

        [Header("Special Tiles")]
        [Tooltip("Yol tile'lari - farkli yonler icin")]
        public GameObject[] roadTiles;

        [Tooltip("Nehir tile'lari")]
        public GameObject[] riverTiles;

        [Tooltip("Kiyi tile'lari")]
        public GameObject[] coastTiles;

        [Header("Fallback")]
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
                TerrainType.Mountain => mountainTile ?? fallback,  // KayKit'te yok, grass kullan
                TerrainType.Desert => desertTile ?? fallback,      // KayKit'te yok, grass kullan
                TerrainType.Snow => snowTile ?? fallback,          // KayKit'te yok, grass kullan
                TerrainType.Swamp => swampTile ?? fallback,
                TerrainType.Hill => plainsTile ?? fallback,
                TerrainType.Road => fallback,
                TerrainType.Coast => coastTiles != null && coastTiles.Length > 0 ? coastTiles[0] : (waterTile ?? fallback),
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
    }
}
