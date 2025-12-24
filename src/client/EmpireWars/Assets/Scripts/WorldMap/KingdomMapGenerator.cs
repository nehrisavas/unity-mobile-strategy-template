using UnityEngine;
using System.Collections.Generic;
using EmpireWars.Data;

namespace EmpireWars.WorldMap
{
    /// <summary>
    /// Krallık merkezi etrafında profesyonel oyun haritası oluşturur
    /// - Merkez: Kale/Krallık binası
    /// - 3 birim bataklık çevresi
    /// - Köşelerde topçu kuleleri
    /// - Mesafeye göre maden dağılımı
    /// </summary>
    public static class KingdomMapGenerator
    {
        // Varsayilan harita boyutu (degistirilebilir)
        private static int _mapSize = 60;
        private static int _centerX = 30;
        private static int _centerY = 30;

        public static int MAP_SIZE => _mapSize;
        public static int CENTER_X => _centerX;
        public static int CENTER_Y => _centerY;

        // Bataklık alanı boyutu (merkeze göre) - ölçeklenir
        public static int SWAMP_INNER_RADIUS => Mathf.Max(2, _mapSize / 30);
        public static int SWAMP_OUTER_RADIUS => Mathf.Max(5, _mapSize / 12);

        // Maden mesafe sınırları - ölçeklenir
        public static int MINE_ZONE_1 => _mapSize * 8 / 60;
        public static int MINE_ZONE_2 => _mapSize * 14 / 60;
        public static int MINE_ZONE_3 => _mapSize * 20 / 60;
        public static int MINE_ZONE_4 => _mapSize * 27 / 60;

        // Mevsim bölgeleri - ölçeklenir
        public static int SNOW_ZONE_START => _mapSize * 5 / 60;
        public static int SNOW_ZONE_END => _mapSize * 18 / 60;
        public static int DESERT_ZONE_START => _mapSize * 42 / 60;

        /// <summary>
        /// Harita boyutunu ayarla (cagrilmadan once)
        /// </summary>
        public static void SetMapSize(int size)
        {
            _mapSize = Mathf.Max(20, size);
            _centerX = _mapSize / 2;
            _centerY = _mapSize / 2;
            Debug.Log($"KingdomMapGenerator: Harita boyutu {_mapSize}x{_mapSize} olarak ayarlandi");
        }

        /// <summary>
        /// Tile verisi - koordinat, terrain tipi ve maden seviyesi
        /// </summary>
        public struct TileData
        {
            public int Q;
            public int R;
            public TerrainType Terrain;
            public int MineLevel; // 0 = maden değil, 1-7 = maden seviyesi
            public bool HasBuilding;
            public string BuildingType;

            public TileData(int q, int r, TerrainType terrain, int mineLevel = 0, bool hasBuilding = false, string buildingType = "")
            {
                Q = q;
                R = r;
                Terrain = terrain;
                MineLevel = mineLevel;
                HasBuilding = hasBuilding;
                BuildingType = buildingType;
            }
        }

        /// <summary>
        /// Tüm harita tile'larını üretir
        /// </summary>
        public static List<TileData> GenerateMap()
        {
            var tiles = new List<TileData>();
            var random = new System.Random(12345); // Sabit seed - her seferinde aynı harita

            for (int r = 0; r < MAP_SIZE; r++)
            {
                for (int q = 0; q < MAP_SIZE; q++)
                {
                    var tile = GenerateTile(q, r, random);
                    tiles.Add(tile);
                }
            }

            return tiles;
        }

        /// <summary>
        /// Tek bir tile üretir
        /// </summary>
        private static TileData GenerateTile(int q, int r, System.Random random)
        {
            float distance = GetDistanceFromCenter(q, r);

            // 1. MERKEZ KALE
            if (q == CENTER_X && r == CENTER_Y)
            {
                return new TileData(q, r, TerrainType.Grass, 0, true, "castle_green");
            }

            // 2. BATAKLIK ÇEVRESİ (kare şeklinde)
            if (IsInSwampArea(q, r))
            {
                // Köşe kontrol - topçu kuleleri
                if (IsSwampCorner(q, r))
                {
                    return new TileData(q, r, TerrainType.Swamp, 0, true, "tower_cannon_green");
                }
                return new TileData(q, r, TerrainType.Swamp);
            }

            // 3. MERKEZ ÇEVRESİ (bataklık içi) - çimen ve yollar
            if (IsInsideSwamp(q, r))
            {
                // Merkeze giden yollar
                if (IsRoadToCenter(q, r))
                {
                    return new TileData(q, r, TerrainType.Road);
                }
                return new TileData(q, r, TerrainType.Grass);
            }

            // 4. KENAR SUYU (harita sınırları)
            if (IsMapEdgeWater(q, r))
            {
                return new TileData(q, r, TerrainType.Water);
            }

            // 5. KIYI ALANI
            if (IsCoastArea(q, r))
            {
                return new TileData(q, r, TerrainType.Coast);
            }

            // 6. KAR BÖLGESİ (kuzey kısım)
            if (IsInSnowZone(q, r))
            {
                return GenerateSnowTerrain(q, r, random);
            }

            // 7. ÇÖL BÖLGESİ (güney kısım)
            if (IsInDesertZone(q, r))
            {
                return GenerateDesertTerrain(q, r, random);
            }

            // 8. MADEN ALANLARI (mesafeye göre)
            var mineData = TryGenerateMine(q, r, distance, random);
            if (mineData.HasValue)
            {
                return mineData.Value;
            }

            // 9. DOĞAL ARAZİ (Perlin noise ile)
            return GenerateNaturalTerrain(q, r, random);
        }

        /// <summary>
        /// Merkeze olan mesafeyi hesapla
        /// </summary>
        private static float GetDistanceFromCenter(int q, int r)
        {
            int dx = q - CENTER_X;
            int dy = r - CENTER_Y;
            return Mathf.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Bataklık alanında mı? (kare şeklinde)
        /// </summary>
        private static bool IsInSwampArea(int q, int r)
        {
            int dx = Mathf.Abs(q - CENTER_X);
            int dy = Mathf.Abs(r - CENTER_Y);

            // Kare bataklık - iç ve dış sınır arasında
            bool inOuter = dx <= SWAMP_OUTER_RADIUS && dy <= SWAMP_OUTER_RADIUS;
            bool inInner = dx <= SWAMP_INNER_RADIUS && dy <= SWAMP_INNER_RADIUS;

            return inOuter && !inInner;
        }

        /// <summary>
        /// Bataklık içinde mi? (merkez etrafı)
        /// </summary>
        private static bool IsInsideSwamp(int q, int r)
        {
            int dx = Mathf.Abs(q - CENTER_X);
            int dy = Mathf.Abs(r - CENTER_Y);
            return dx <= SWAMP_INNER_RADIUS && dy <= SWAMP_INNER_RADIUS;
        }

        /// <summary>
        /// Bataklık köşesi mi? (topçu kulesi yeri)
        /// </summary>
        private static bool IsSwampCorner(int q, int r)
        {
            int dx = q - CENTER_X;
            int dy = r - CENTER_Y;

            // 4 köşe
            return (dx == SWAMP_OUTER_RADIUS && dy == SWAMP_OUTER_RADIUS) ||
                   (dx == SWAMP_OUTER_RADIUS && dy == -SWAMP_OUTER_RADIUS) ||
                   (dx == -SWAMP_OUTER_RADIUS && dy == SWAMP_OUTER_RADIUS) ||
                   (dx == -SWAMP_OUTER_RADIUS && dy == -SWAMP_OUTER_RADIUS);
        }

        /// <summary>
        /// Merkeze giden yol mu?
        /// </summary>
        private static bool IsRoadToCenter(int q, int r)
        {
            // 4 yön - merkeze doğru yollar
            return (q == CENTER_X && r != CENTER_Y) || (r == CENTER_Y && q != CENTER_X);
        }

        /// <summary>
        /// Harita kenarı su mu?
        /// </summary>
        private static bool IsMapEdgeWater(int q, int r)
        {
            int edgeDistance = 3;
            return q < edgeDistance || q >= MAP_SIZE - edgeDistance ||
                   r < edgeDistance || r >= MAP_SIZE - edgeDistance;
        }

        /// <summary>
        /// Kıyı alanı mı?
        /// </summary>
        private static bool IsCoastArea(int q, int r)
        {
            int coastStart = 3;
            int coastEnd = 5;
            return (q >= coastStart && q < coastEnd) ||
                   (q >= MAP_SIZE - coastEnd && q < MAP_SIZE - coastStart) ||
                   (r >= coastStart && r < coastEnd) ||
                   (r >= MAP_SIZE - coastEnd && r < MAP_SIZE - coastStart);
        }

        /// <summary>
        /// Kar bölgesinde mi? (kuzey kısım)
        /// </summary>
        private static bool IsInSnowZone(int q, int r)
        {
            int snowQStart = _mapSize * 5 / 60;
            int snowQEnd = _mapSize * 25 / 60;
            return r >= SNOW_ZONE_START && r <= SNOW_ZONE_END && q >= snowQStart && q <= snowQEnd;
        }

        /// <summary>
        /// Çöl bölgesinde mi? (güney kısım)
        /// </summary>
        private static bool IsInDesertZone(int q, int r)
        {
            int desertQStart = _mapSize * 35 / 60;
            int desertQEnd = _mapSize * 55 / 60;
            int edgeMargin = _mapSize * 5 / 60;
            return r >= DESERT_ZONE_START && r < MAP_SIZE - edgeMargin && q >= desertQStart && q <= desertQEnd;
        }

        /// <summary>
        /// Kar bölgesi arazisi üret
        /// </summary>
        private static TileData GenerateSnowTerrain(int q, int r, System.Random random)
        {
            float noise = Mathf.PerlinNoise(q * 0.15f, r * 0.15f);

            TerrainType terrain;
            if (noise > 0.8f)
            {
                terrain = TerrainType.Mountain; // Karlı dağlar
            }
            else if (noise > 0.6f)
            {
                terrain = TerrainType.Hill; // Karlı tepeler
            }
            else if (noise < 0.2f)
            {
                terrain = TerrainType.Forest; // Karlı orman
            }
            else
            {
                terrain = TerrainType.Snow; // Düz kar
            }

            return new TileData(q, r, terrain);
        }

        /// <summary>
        /// Çöl bölgesi arazisi üret
        /// </summary>
        private static TileData GenerateDesertTerrain(int q, int r, System.Random random)
        {
            float noise = Mathf.PerlinNoise(q * 0.12f + 50f, r * 0.12f + 50f);

            TerrainType terrain;
            if (noise > 0.85f)
            {
                terrain = TerrainType.Mountain; // Çöl dağları
            }
            else if (noise > 0.7f)
            {
                terrain = TerrainType.Hill; // Çöl tepeleri
            }
            else
            {
                terrain = TerrainType.Desert; // Düz çöl
            }

            return new TileData(q, r, terrain);
        }

        /// <summary>
        /// Maden üretmeye çalış (mesafeye göre)
        /// </summary>
        private static TileData? TryGenerateMine(int q, int r, float distance, System.Random random)
        {
            // Maden olasılığı - daha seyrek dağılım
            float mineChance = 0.03f; // %3 şans

            if (random.NextDouble() > mineChance) return null;

            // Mesafeye göre maden tipi ve seviye
            TerrainType mineType;
            int mineLevel;

            if (distance <= MINE_ZONE_1)
            {
                // En değerli madenler - Level 7
                mineLevel = 7;
                mineType = random.NextDouble() > 0.5 ? TerrainType.GoldMine : TerrainType.GemMine;
            }
            else if (distance <= MINE_ZONE_2)
            {
                // Orta-yüksek değer - Level 5-6
                mineLevel = random.Next(5, 7);
                mineType = random.NextDouble() > 0.7 ? TerrainType.GoldMine : TerrainType.Mine;
            }
            else if (distance <= MINE_ZONE_3)
            {
                // Orta değer - Level 3-4
                mineLevel = random.Next(3, 5);
                mineType = random.NextDouble() > 0.8 ? TerrainType.Mine : TerrainType.Quarry;
            }
            else if (distance <= MINE_ZONE_4)
            {
                // Düşük değer - Level 1-2
                mineLevel = random.Next(1, 3);
                mineType = TerrainType.Quarry;
            }
            else
            {
                return null; // Çok uzak - maden yok
            }

            return new TileData(q, r, mineType, mineLevel);
        }

        /// <summary>
        /// Doğal arazi üret (Perlin noise ile)
        /// </summary>
        private static TileData GenerateNaturalTerrain(int q, int r, System.Random random)
        {
            // Perlin noise için koordinatları ölçekle
            float scale1 = 0.08f;
            float scale2 = 0.12f;

            float noise1 = Mathf.PerlinNoise(q * scale1 + 0.5f, r * scale1 + 0.5f);
            float noise2 = Mathf.PerlinNoise(q * scale2 + 100f, r * scale2 + 100f);

            // Terrain tipi belirle
            TerrainType terrain;

            if (noise1 > 0.75f)
            {
                // Dağlık alan
                terrain = noise1 > 0.85f ? TerrainType.Mountain : TerrainType.Hill;
            }
            else if (noise1 < 0.25f)
            {
                // Su/bataklık alanı
                if (noise1 < 0.15f)
                {
                    terrain = TerrainType.Water;
                }
                else
                {
                    terrain = TerrainType.Swamp;
                }
            }
            else if (noise2 > 0.65f)
            {
                // Orman
                terrain = TerrainType.Forest;
            }
            else if (noise2 < 0.2f)
            {
                // Çöl/kar
                terrain = noise1 > 0.5f ? TerrainType.Snow : TerrainType.Desert;
            }
            else
            {
                // Düz çimen
                terrain = TerrainType.Grass;

                // Rastgele çiftlik (%2 şans)
                if (random.NextDouble() < 0.02f)
                {
                    terrain = TerrainType.Farm;
                }
            }

            return new TileData(q, r, terrain);
        }

        /// <summary>
        /// Nehir yolu üret (2 nokta arası)
        /// </summary>
        public static List<(int q, int r)> GenerateRiverPath(int startQ, int startR, int endQ, int endR, System.Random random)
        {
            var path = new List<(int q, int r)>();

            int currentQ = startQ;
            int currentR = startR;

            while (currentQ != endQ || currentR != endR)
            {
                path.Add((currentQ, currentR));

                // Hedefe doğru ilerle (rastgele sapma ile)
                int dq = endQ - currentQ;
                int dr = endR - currentR;

                if (Mathf.Abs(dq) > Mathf.Abs(dr))
                {
                    currentQ += dq > 0 ? 1 : -1;
                    if (random.NextDouble() < 0.3f && dr != 0)
                    {
                        currentR += dr > 0 ? 1 : -1;
                    }
                }
                else
                {
                    currentR += dr > 0 ? 1 : -1;
                    if (random.NextDouble() < 0.3f && dq != 0)
                    {
                        currentQ += dq > 0 ? 1 : -1;
                    }
                }
            }

            path.Add((endQ, endR));
            return path;
        }

        /// <summary>
        /// Harita boyutunu döndür
        /// </summary>
        public static (int width, int height) GetMapSize()
        {
            return (MAP_SIZE, MAP_SIZE);
        }

        /// <summary>
        /// Maden seviyesi rengini döndür (UI için)
        /// </summary>
        public static Color GetMineLevelColor(int level)
        {
            return level switch
            {
                7 => new Color(1f, 0.84f, 0f),      // Altın
                6 => new Color(0.9f, 0.4f, 0.9f),   // Mor
                5 => new Color(0.2f, 0.6f, 1f),     // Mavi
                4 => new Color(0.2f, 0.8f, 0.2f),   // Yeşil
                3 => new Color(0.8f, 0.8f, 0.2f),   // Sarı
                2 => new Color(0.8f, 0.5f, 0.2f),   // Turuncu
                1 => new Color(0.6f, 0.6f, 0.6f),   // Gri
                _ => Color.white
            };
        }
    }
}
