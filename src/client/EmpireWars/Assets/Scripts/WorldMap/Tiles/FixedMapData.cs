using EmpireWars.Core;
using EmpireWars.Data;

namespace EmpireWars.WorldMap.Tiles
{
    /// <summary>
    /// Sabit harita verisi - 20x20 optimize harita
    /// Tum KayKit tile tiplerini icerir
    /// Her terrain tipinden en az 2 ornek
    /// </summary>
    public static class FixedMapData
    {
        public const int MAP_WIDTH = 20;
        public const int MAP_HEIGHT = 20;

        /// <summary>
        /// Harita tile verileri (q, r, terrain type)
        /// Profesyonel strateji oyunu haritasi tasarimi
        /// </summary>
        private static readonly (int q, int r, TerrainType type)[] MapTiles = new[]
        {
            // ========== ROW 0 (Kuzey - Su ve Kiyi) ==========
            (0, 0, TerrainType.Water), (1, 0, TerrainType.Water), (2, 0, TerrainType.Coast),
            (3, 0, TerrainType.Coast), (4, 0, TerrainType.Grass), (5, 0, TerrainType.Grass),
            (6, 0, TerrainType.Forest), (7, 0, TerrainType.Forest), (8, 0, TerrainType.Mountain),
            (9, 0, TerrainType.Mountain), (10, 0, TerrainType.Mountain), (11, 0, TerrainType.Hill),
            (12, 0, TerrainType.Hill), (13, 0, TerrainType.Forest), (14, 0, TerrainType.Grass),
            (15, 0, TerrainType.Coast), (16, 0, TerrainType.Water), (17, 0, TerrainType.Water),
            (18, 0, TerrainType.Water), (19, 0, TerrainType.Water),

            // ========== ROW 1 ==========
            (0, 1, TerrainType.Water), (1, 1, TerrainType.Coast), (2, 1, TerrainType.Grass),
            (3, 1, TerrainType.Road), (4, 1, TerrainType.Road), (5, 1, TerrainType.Grass),
            (6, 1, TerrainType.Grass), (7, 1, TerrainType.Forest), (8, 1, TerrainType.Hill),
            (9, 1, TerrainType.Mountain), (10, 1, TerrainType.Hill), (11, 1, TerrainType.Grass),
            (12, 1, TerrainType.Grass), (13, 1, TerrainType.Forest), (14, 1, TerrainType.Grass),
            (15, 1, TerrainType.Grass), (16, 1, TerrainType.Coast), (17, 1, TerrainType.Water),
            (18, 1, TerrainType.Water), (19, 1, TerrainType.Water),

            // ========== ROW 2 ==========
            (0, 2, TerrainType.Coast), (1, 2, TerrainType.Grass), (2, 2, TerrainType.Grass),
            (3, 2, TerrainType.Grass), (4, 2, TerrainType.Road), (5, 2, TerrainType.Grass),
            (6, 2, TerrainType.River), (7, 2, TerrainType.River), (8, 2, TerrainType.Bridge),
            (9, 2, TerrainType.Grass), (10, 2, TerrainType.Grass), (11, 2, TerrainType.GrassSlopedLow),
            (12, 2, TerrainType.Grass), (13, 2, TerrainType.Grass), (14, 2, TerrainType.Forest),
            (15, 2, TerrainType.Forest), (16, 2, TerrainType.Grass), (17, 2, TerrainType.Coast),
            (18, 2, TerrainType.Water), (19, 2, TerrainType.Water),

            // ========== ROW 3 ==========
            (0, 3, TerrainType.Grass), (1, 3, TerrainType.Grass), (2, 3, TerrainType.Desert),
            (3, 3, TerrainType.Desert), (4, 3, TerrainType.Road), (5, 3, TerrainType.River),
            (6, 3, TerrainType.Grass), (7, 3, TerrainType.Grass), (8, 3, TerrainType.Grass),
            (9, 3, TerrainType.Grass), (10, 3, TerrainType.GrassSlopedHigh), (11, 3, TerrainType.Hill),
            (12, 3, TerrainType.Hill), (13, 3, TerrainType.Grass), (14, 3, TerrainType.Grass),
            (15, 3, TerrainType.Forest), (16, 3, TerrainType.Grass), (17, 3, TerrainType.Grass),
            (18, 3, TerrainType.Coast), (19, 3, TerrainType.Water),

            // ========== ROW 4 ==========
            (0, 4, TerrainType.Grass), (1, 4, TerrainType.Desert), (2, 4, TerrainType.Desert),
            (3, 4, TerrainType.Grass), (4, 4, TerrainType.Road), (5, 4, TerrainType.River),
            (6, 4, TerrainType.Grass), (7, 4, TerrainType.Farm), (8, 4, TerrainType.Farm),
            (9, 4, TerrainType.Grass), (10, 4, TerrainType.Grass), (11, 4, TerrainType.GrassSlopedLow),
            (12, 4, TerrainType.Grass), (13, 4, TerrainType.Grass), (14, 4, TerrainType.Grass),
            (15, 4, TerrainType.Grass), (16, 4, TerrainType.Grass), (17, 4, TerrainType.Grass),
            (18, 4, TerrainType.Grass), (19, 4, TerrainType.Coast),

            // ========== ROW 5 ==========
            (0, 5, TerrainType.Grass), (1, 5, TerrainType.Grass), (2, 5, TerrainType.Grass),
            (3, 5, TerrainType.Grass), (4, 5, TerrainType.Road), (5, 5, TerrainType.Bridge),
            (6, 5, TerrainType.River), (7, 5, TerrainType.Grass), (8, 5, TerrainType.Farm),
            (9, 5, TerrainType.Grass), (10, 5, TerrainType.Road), (11, 5, TerrainType.Road),
            (12, 5, TerrainType.Road), (13, 5, TerrainType.Grass), (14, 5, TerrainType.Swamp),
            (15, 5, TerrainType.Swamp), (16, 5, TerrainType.Grass), (17, 5, TerrainType.Forest),
            (18, 5, TerrainType.Forest), (19, 5, TerrainType.Grass),

            // ========== ROW 6 ==========
            (0, 6, TerrainType.Forest), (1, 6, TerrainType.Forest), (2, 6, TerrainType.Grass),
            (3, 6, TerrainType.Grass), (4, 6, TerrainType.Road), (5, 6, TerrainType.Grass),
            (6, 6, TerrainType.River), (7, 6, TerrainType.Grass), (8, 6, TerrainType.Grass),
            (9, 6, TerrainType.Road), (10, 6, TerrainType.Grass), (11, 6, TerrainType.Grass),
            (12, 6, TerrainType.Road), (13, 6, TerrainType.Swamp), (14, 6, TerrainType.Swamp),
            (15, 6, TerrainType.Grass), (16, 6, TerrainType.Grass), (17, 6, TerrainType.Grass),
            (18, 6, TerrainType.Forest), (19, 6, TerrainType.Forest),

            // ========== ROW 7 ==========
            (0, 7, TerrainType.Forest), (1, 7, TerrainType.Grass), (2, 7, TerrainType.Grass),
            (3, 7, TerrainType.Road), (4, 7, TerrainType.Road), (5, 7, TerrainType.Grass),
            (6, 7, TerrainType.River), (7, 7, TerrainType.River), (8, 7, TerrainType.Bridge),
            (9, 7, TerrainType.Road), (10, 7, TerrainType.Grass), (11, 7, TerrainType.Grass),
            (12, 7, TerrainType.Road), (13, 7, TerrainType.Grass), (14, 7, TerrainType.Grass),
            (15, 7, TerrainType.Grass), (16, 7, TerrainType.Hill), (17, 7, TerrainType.Hill),
            (18, 7, TerrainType.Grass), (19, 7, TerrainType.Forest),

            // ========== ROW 8 ==========
            (0, 8, TerrainType.Grass), (1, 8, TerrainType.Grass), (2, 8, TerrainType.Road),
            (3, 8, TerrainType.Road), (4, 8, TerrainType.Grass), (5, 8, TerrainType.Grass),
            (6, 8, TerrainType.Grass), (7, 8, TerrainType.River), (8, 8, TerrainType.Grass),
            (9, 8, TerrainType.Road), (10, 8, TerrainType.Mine), (11, 8, TerrainType.Mine),
            (12, 8, TerrainType.Road), (13, 8, TerrainType.Grass), (14, 8, TerrainType.Grass),
            (15, 8, TerrainType.GrassSlopedHigh), (16, 8, TerrainType.Mountain), (17, 8, TerrainType.Mountain),
            (18, 8, TerrainType.Hill), (19, 8, TerrainType.Grass),

            // ========== ROW 9 ==========
            (0, 9, TerrainType.Grass), (1, 9, TerrainType.Road), (2, 9, TerrainType.Road),
            (3, 9, TerrainType.Grass), (4, 9, TerrainType.Grass), (5, 9, TerrainType.Snow),
            (6, 9, TerrainType.Snow), (7, 9, TerrainType.River), (8, 9, TerrainType.Grass),
            (9, 9, TerrainType.Road), (10, 9, TerrainType.Road), (11, 9, TerrainType.Road),
            (12, 9, TerrainType.Road), (13, 9, TerrainType.Grass), (14, 9, TerrainType.GrassSlopedLow),
            (15, 9, TerrainType.Mountain), (16, 9, TerrainType.Mountain), (17, 9, TerrainType.GoldMine),
            (18, 9, TerrainType.Hill), (19, 9, TerrainType.Grass),

            // ========== ROW 10 ==========
            (0, 10, TerrainType.Road), (1, 10, TerrainType.Road), (2, 10, TerrainType.Grass),
            (3, 10, TerrainType.Grass), (4, 10, TerrainType.Snow), (5, 10, TerrainType.Snow),
            (6, 10, TerrainType.Grass), (7, 10, TerrainType.River), (8, 10, TerrainType.River),
            (9, 10, TerrainType.Bridge), (10, 10, TerrainType.Road), (11, 10, TerrainType.Grass),
            (12, 10, TerrainType.Grass), (13, 10, TerrainType.Grass), (14, 10, TerrainType.Hill),
            (15, 10, TerrainType.Mountain), (16, 10, TerrainType.GemMine), (17, 10, TerrainType.Mountain),
            (18, 10, TerrainType.Grass), (19, 10, TerrainType.Grass),

            // ========== ROW 11 ==========
            (0, 11, TerrainType.Road), (1, 11, TerrainType.Grass), (2, 11, TerrainType.Grass),
            (3, 11, TerrainType.Grass), (4, 11, TerrainType.Grass), (5, 11, TerrainType.Grass),
            (6, 11, TerrainType.Grass), (7, 11, TerrainType.Grass), (8, 11, TerrainType.River),
            (9, 11, TerrainType.Grass), (10, 11, TerrainType.Road), (11, 11, TerrainType.Quarry),
            (12, 11, TerrainType.Quarry), (13, 11, TerrainType.Grass), (14, 11, TerrainType.Grass),
            (15, 11, TerrainType.Hill), (16, 11, TerrainType.Hill), (17, 11, TerrainType.Grass),
            (18, 11, TerrainType.Forest), (19, 11, TerrainType.Forest),

            // ========== ROW 12 ==========
            (0, 12, TerrainType.Road), (1, 12, TerrainType.Road), (2, 12, TerrainType.Grass),
            (3, 12, TerrainType.Forest), (4, 12, TerrainType.Forest), (5, 12, TerrainType.Grass),
            (6, 12, TerrainType.Grass), (7, 12, TerrainType.Grass), (8, 12, TerrainType.River),
            (9, 12, TerrainType.Grass), (10, 12, TerrainType.Road), (11, 12, TerrainType.Grass),
            (12, 12, TerrainType.Grass), (13, 12, TerrainType.Grass), (14, 12, TerrainType.Grass),
            (15, 12, TerrainType.Grass), (16, 12, TerrainType.Grass), (17, 12, TerrainType.Grass),
            (18, 12, TerrainType.Forest), (19, 12, TerrainType.Forest),

            // ========== ROW 13 ==========
            (0, 13, TerrainType.Grass), (1, 13, TerrainType.Road), (2, 13, TerrainType.Road),
            (3, 13, TerrainType.Road), (4, 13, TerrainType.Forest), (5, 13, TerrainType.Forest),
            (6, 13, TerrainType.Grass), (7, 13, TerrainType.Grass), (8, 13, TerrainType.River),
            (9, 13, TerrainType.River), (10, 13, TerrainType.Bridge), (11, 13, TerrainType.Road),
            (12, 13, TerrainType.Road), (13, 13, TerrainType.Grass), (14, 13, TerrainType.Desert),
            (15, 13, TerrainType.Desert), (16, 13, TerrainType.Grass), (17, 13, TerrainType.Grass),
            (18, 13, TerrainType.Grass), (19, 13, TerrainType.Forest),

            // ========== ROW 14 ==========
            (0, 14, TerrainType.Grass), (1, 14, TerrainType.Grass), (2, 14, TerrainType.Grass),
            (3, 14, TerrainType.Road), (4, 14, TerrainType.Grass), (5, 14, TerrainType.Forest),
            (6, 14, TerrainType.Forest), (7, 14, TerrainType.Grass), (8, 14, TerrainType.Grass),
            (9, 14, TerrainType.River), (10, 14, TerrainType.Grass), (11, 14, TerrainType.Grass),
            (12, 14, TerrainType.Road), (13, 14, TerrainType.Desert), (14, 14, TerrainType.Desert),
            (15, 14, TerrainType.Desert), (16, 14, TerrainType.Grass), (17, 14, TerrainType.Grass),
            (18, 14, TerrainType.Grass), (19, 14, TerrainType.Grass),

            // ========== ROW 15 ==========
            (0, 15, TerrainType.Coast), (1, 15, TerrainType.Grass), (2, 15, TerrainType.Grass),
            (3, 15, TerrainType.Road), (4, 15, TerrainType.Road), (5, 15, TerrainType.Grass),
            (6, 15, TerrainType.Forest), (7, 15, TerrainType.Grass), (8, 15, TerrainType.Grass),
            (9, 15, TerrainType.River), (10, 15, TerrainType.Grass), (11, 15, TerrainType.Grass),
            (12, 15, TerrainType.Road), (13, 15, TerrainType.Grass), (14, 15, TerrainType.Grass),
            (15, 15, TerrainType.Grass), (16, 15, TerrainType.Grass), (17, 15, TerrainType.Hill),
            (18, 15, TerrainType.Hill), (19, 15, TerrainType.Grass),

            // ========== ROW 16 ==========
            (0, 16, TerrainType.Water), (1, 16, TerrainType.Coast), (2, 16, TerrainType.Grass),
            (3, 16, TerrainType.Grass), (4, 16, TerrainType.Road), (5, 16, TerrainType.Grass),
            (6, 16, TerrainType.Grass), (7, 16, TerrainType.Grass), (8, 16, TerrainType.River),
            (9, 16, TerrainType.River), (10, 16, TerrainType.Grass), (11, 16, TerrainType.Grass),
            (12, 16, TerrainType.Road), (13, 16, TerrainType.Road), (14, 16, TerrainType.Grass),
            (15, 16, TerrainType.Grass), (16, 16, TerrainType.GrassSlopedHigh), (17, 16, TerrainType.Mountain),
            (18, 16, TerrainType.Mountain), (19, 16, TerrainType.Hill),

            // ========== ROW 17 ==========
            (0, 17, TerrainType.Water), (1, 17, TerrainType.Water), (2, 17, TerrainType.Coast),
            (3, 17, TerrainType.Coast), (4, 17, TerrainType.Road), (5, 17, TerrainType.Road),
            (6, 17, TerrainType.Grass), (7, 17, TerrainType.River), (8, 17, TerrainType.River),
            (9, 17, TerrainType.Grass), (10, 17, TerrainType.Grass), (11, 17, TerrainType.Grass),
            (12, 17, TerrainType.Grass), (13, 17, TerrainType.Road), (14, 17, TerrainType.Road),
            (15, 17, TerrainType.Grass), (16, 17, TerrainType.GrassSlopedLow), (17, 17, TerrainType.Hill),
            (18, 17, TerrainType.Mountain), (19, 17, TerrainType.Mountain),

            // ========== ROW 18 ==========
            (0, 18, TerrainType.Water), (1, 18, TerrainType.Water), (2, 18, TerrainType.Water),
            (3, 18, TerrainType.Coast), (4, 18, TerrainType.Coast), (5, 18, TerrainType.Road),
            (6, 18, TerrainType.River), (7, 18, TerrainType.River), (8, 18, TerrainType.Grass),
            (9, 18, TerrainType.Grass), (10, 18, TerrainType.Forest), (11, 18, TerrainType.Forest),
            (12, 18, TerrainType.Grass), (13, 18, TerrainType.Grass), (14, 18, TerrainType.Road),
            (15, 18, TerrainType.Road), (16, 18, TerrainType.Grass), (17, 18, TerrainType.Grass),
            (18, 18, TerrainType.Hill), (19, 18, TerrainType.Mountain),

            // ========== ROW 19 (Guney) ==========
            (0, 19, TerrainType.Water), (1, 19, TerrainType.Water), (2, 19, TerrainType.Water),
            (3, 19, TerrainType.Water), (4, 19, TerrainType.Coast), (5, 19, TerrainType.Coast),
            (6, 19, TerrainType.Coast), (7, 19, TerrainType.Grass), (8, 19, TerrainType.Grass),
            (9, 19, TerrainType.Forest), (10, 19, TerrainType.Forest), (11, 19, TerrainType.Forest),
            (12, 19, TerrainType.Grass), (13, 19, TerrainType.Grass), (14, 19, TerrainType.Grass),
            (15, 19, TerrainType.Road), (16, 19, TerrainType.Road), (17, 19, TerrainType.Grass),
            (18, 19, TerrainType.Grass), (19, 19, TerrainType.Hill),
        };

        /// <summary>
        /// Harita tile'larini dondur
        /// </summary>
        public static (int q, int r, TerrainType type)[] GetMapTiles()
        {
            return MapTiles;
        }

        /// <summary>
        /// Harita boyutlarini dondur
        /// </summary>
        public static (int width, int height) GetMapSize()
        {
            return (MAP_WIDTH, MAP_HEIGHT);
        }

        /// <summary>
        /// Belirli koordinattaki terrain tipini dondur
        /// </summary>
        public static TerrainType GetTerrainAt(int q, int r)
        {
            foreach (var tile in MapTiles)
            {
                if (tile.q == q && tile.r == r)
                {
                    return tile.type;
                }
            }
            return TerrainType.Grass; // Default
        }

        /// <summary>
        /// Terrain tipi sayilarini dondur (debug icin)
        /// </summary>
        public static System.Collections.Generic.Dictionary<TerrainType, int> GetTerrainCounts()
        {
            var counts = new System.Collections.Generic.Dictionary<TerrainType, int>();
            foreach (var tile in MapTiles)
            {
                if (!counts.ContainsKey(tile.type))
                    counts[tile.type] = 0;
                counts[tile.type]++;
            }
            return counts;
        }
    }
}
