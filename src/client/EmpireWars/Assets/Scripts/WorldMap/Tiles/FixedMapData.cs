using EmpireWars.Core;
using EmpireWars.Data;

namespace EmpireWars.WorldMap.Tiles
{
    /// <summary>
    /// Sabit harita verisi - her seferinde ayni harita olusturulur
    /// Her terrain tipinden en az 2 tane bulunur
    /// </summary>
    public static class FixedMapData
    {
        // 10x10 sabit harita - koordinat (q, r) ve terrain tipi
        // Harita degismez, her oyun ayni haritayi kullanir
        private static readonly (int q, int r, TerrainType type)[] MapTiles = new[]
        {
            // Row 0: Grass ovasi ve yol
            (0, 0, TerrainType.Grass),
            (1, 0, TerrainType.Grass),
            (2, 0, TerrainType.Road),     // Yol 1
            (3, 0, TerrainType.Grass),
            (4, 0, TerrainType.Forest),   // Orman 1
            (5, 0, TerrainType.Forest),   // Orman 2
            (6, 0, TerrainType.Hill),     // Tepe 1
            (7, 0, TerrainType.Mountain), // Dag 1
            (8, 0, TerrainType.Mountain), // Dag 2
            (9, 0, TerrainType.Snow),     // Kar 1

            // Row 1: Su ve kiyi
            (0, 1, TerrainType.Water),    // Su 1
            (1, 1, TerrainType.Water),    // Su 2
            (2, 1, TerrainType.Coast),    // Kiyi 1
            (3, 1, TerrainType.Grass),
            (4, 1, TerrainType.Grass),
            (5, 1, TerrainType.Forest),   // Orman 3
            (6, 1, TerrainType.Hill),     // Tepe 2
            (7, 1, TerrainType.Grass),
            (8, 1, TerrainType.Mountain), // Dag 3
            (9, 1, TerrainType.Snow),     // Kar 2

            // Row 2: Nehir ve bataklik
            (0, 2, TerrainType.Water),    // Su 3
            (1, 2, TerrainType.Coast),    // Kiyi 2
            (2, 2, TerrainType.Swamp),    // Bataklik 1
            (3, 2, TerrainType.Swamp),    // Bataklik 2
            (4, 2, TerrainType.Grass),
            (5, 2, TerrainType.Road),     // Yol 2
            (6, 2, TerrainType.Grass),
            (7, 2, TerrainType.Hill),     // Tepe 3
            (8, 2, TerrainType.Grass),
            (9, 2, TerrainType.Snow),     // Kar 3

            // Row 3: Col bolgesi
            (0, 3, TerrainType.Coast),    // Kiyi 3
            (1, 3, TerrainType.Grass),
            (2, 3, TerrainType.Grass),
            (3, 3, TerrainType.Road),     // Yol 3
            (4, 3, TerrainType.Desert),   // Col 1
            (5, 3, TerrainType.Desert),   // Col 2
            (6, 3, TerrainType.Desert),   // Col 3
            (7, 3, TerrainType.Grass),
            (8, 3, TerrainType.Forest),   // Orman 4
            (9, 3, TerrainType.Grass),

            // Row 4: Karisik arazi
            (0, 4, TerrainType.Grass),
            (1, 4, TerrainType.Forest),   // Orman 5
            (2, 4, TerrainType.Grass),
            (3, 4, TerrainType.Hill),     // Tepe 4
            (4, 4, TerrainType.Grass),
            (5, 4, TerrainType.Road),     // Yol 4
            (6, 4, TerrainType.Grass),
            (7, 4, TerrainType.Swamp),    // Bataklik 3
            (8, 4, TerrainType.Grass),
            (9, 4, TerrainType.Hill),     // Tepe 5

            // Row 5: Merkez alan
            (0, 5, TerrainType.Grass),
            (1, 5, TerrainType.Grass),
            (2, 5, TerrainType.Forest),   // Orman 6
            (3, 5, TerrainType.Grass),
            (4, 5, TerrainType.Road),     // Yol 5
            (5, 5, TerrainType.Grass),    // MERKEZ
            (6, 5, TerrainType.Road),     // Yol 6
            (7, 5, TerrainType.Grass),
            (8, 5, TerrainType.Forest),   // Orman 7
            (9, 5, TerrainType.Grass),

            // Row 6: Guney bolge
            (0, 6, TerrainType.Water),    // Su 4
            (1, 6, TerrainType.Coast),    // Kiyi 4
            (2, 6, TerrainType.Grass),
            (3, 6, TerrainType.Hill),     // Tepe 6
            (4, 6, TerrainType.Grass),
            (5, 6, TerrainType.Road),     // Yol 7
            (6, 6, TerrainType.Grass),
            (7, 6, TerrainType.Desert),   // Col 4
            (8, 6, TerrainType.Grass),
            (9, 6, TerrainType.Mountain), // Dag 4

            // Row 7: Dag etekleri
            (0, 7, TerrainType.Water),    // Su 5
            (1, 7, TerrainType.Grass),
            (2, 7, TerrainType.Swamp),    // Bataklik 4
            (3, 7, TerrainType.Grass),
            (4, 7, TerrainType.Forest),   // Orman 8
            (5, 7, TerrainType.Road),     // Yol 8
            (6, 7, TerrainType.Forest),   // Orman 9
            (7, 7, TerrainType.Grass),
            (8, 7, TerrainType.Hill),     // Tepe 7
            (9, 7, TerrainType.Mountain), // Dag 5

            // Row 8: Guney siniri
            (0, 8, TerrainType.Coast),    // Kiyi 5
            (1, 8, TerrainType.Grass),
            (2, 8, TerrainType.Grass),
            (3, 8, TerrainType.Road),     // Yol 9
            (4, 8, TerrainType.Grass),
            (5, 8, TerrainType.Snow),     // Kar 4
            (6, 8, TerrainType.Snow),     // Kar 5
            (7, 8, TerrainType.Hill),     // Tepe 8
            (8, 8, TerrainType.Grass),
            (9, 8, TerrainType.Grass),

            // Row 9: En guney
            (0, 9, TerrainType.Water),    // Su 6
            (1, 9, TerrainType.Water),    // Su 7
            (2, 9, TerrainType.Coast),    // Kiyi 6
            (3, 9, TerrainType.Grass),
            (4, 9, TerrainType.Forest),   // Orman 10
            (5, 9, TerrainType.Road),     // Yol 10
            (6, 9, TerrainType.Grass),
            (7, 9, TerrainType.Desert),   // Col 5
            (8, 9, TerrainType.Desert),   // Col 6
            (9, 9, TerrainType.Mountain), // Dag 6
        };

        /// <summary>
        /// Sabit harita tile verilerini dondurur
        /// </summary>
        public static (int q, int r, TerrainType type)[] GetMapTiles()
        {
            return MapTiles;
        }

        /// <summary>
        /// Harita boyutlarini dondurur
        /// </summary>
        public static (int width, int height) GetMapSize()
        {
            return (10, 10);
        }

        /// <summary>
        /// Belirli koordinattaki terrain tipini dondurur
        /// </summary>
        public static TerrainType GetTerrainAt(int q, int r)
        {
            foreach (var tile in MapTiles)
            {
                if (tile.q == q && tile.r == r)
                    return tile.type;
            }
            return TerrainType.Grass; // Varsayilan
        }

        /// <summary>
        /// Terrain tipi sayilarini dondurur (debug icin)
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
