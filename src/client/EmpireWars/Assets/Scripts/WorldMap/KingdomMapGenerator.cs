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

        // Maden mesafe sınırları - 2000x2000 için optimize edildi
        // Merkez = (1000, 1000), her zone 200-300 tile genişliğinde
        public static int MINE_ZONE_1 => _mapSize * 10 / 100;   // 200 tile - Level 7 (Gem/Gold)
        public static int MINE_ZONE_2 => _mapSize * 20 / 100;   // 400 tile - Level 5-6 (Gold/Iron)
        public static int MINE_ZONE_3 => _mapSize * 32 / 100;   // 640 tile - Level 3-4 (Iron/Stone)
        public static int MINE_ZONE_4 => _mapSize * 45 / 100;   // 900 tile - Level 1-2 (Stone/Wood)

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
        /// Maden türleri - TerrainType ile eşleşir
        /// </summary>
        public enum MineType
        {
            None,
            Gold,       // Altın madeni → TerrainType.GoldMine
            Gem,        // Mücevher madeni → TerrainType.GemMine
            Iron,       // Demir madeni → TerrainType.Mine
            Stone,      // Taş ocağı → TerrainType.Quarry
            Wood        // Kereste → TerrainType.Forest
        }

        /// <summary>
        /// MineType'a karşılık gelen TerrainType'ı döndür
        /// </summary>
        public static TerrainType GetTerrainForMineType(MineType mineType)
        {
            return mineType switch
            {
                MineType.Gold => TerrainType.GoldMine,
                MineType.Gem => TerrainType.GemMine,
                MineType.Iron => TerrainType.Mine,
                MineType.Stone => TerrainType.Quarry,
                MineType.Wood => TerrainType.Forest,
                _ => TerrainType.Grass
            };
        }

        /// <summary>
        /// Tile verisi - koordinat, terrain tipi ve maden seviyesi
        /// </summary>
        public struct TileData
        {
            public int Q;
            public int R;
            public TerrainType Terrain;
            public int MineLevel;       // 0 = maden değil, 1-7 = maden seviyesi
            public MineType MineType;   // Maden türü
            public bool HasBuilding;
            public string BuildingType;

            public TileData(int q, int r, TerrainType terrain, int mineLevel = 0, MineType mineType = MineType.None, bool hasBuilding = false, string buildingType = "")
            {
                Q = q;
                R = r;
                Terrain = terrain;
                MineLevel = mineLevel;
                MineType = mineType;
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
        /// Belirli bir koordinattaki tile verisini döndürür (minimap için)
        /// Her koordinat için deterministik sonuç üretir
        /// </summary>
        public static TileData GetTileAt(int q, int r)
        {
            // Sınır kontrolü
            if (q < 0 || q >= MAP_SIZE || r < 0 || r >= MAP_SIZE)
            {
                return new TileData(q, r, TerrainType.Water);
            }

            // Her tile için benzersiz ama deterministik seed
            int tileSeed = 12345 + q * 10007 + r * 100003;
            var random = new System.Random(tileSeed);

            return GenerateTile(q, r, random);
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
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "castle_green");
            }

            // 2. BATAKLIK ÇEVRESİ (kare şeklinde)
            if (IsInSwampArea(q, r))
            {
                // Köşe kontrol - topçu kuleleri
                if (IsSwampCorner(q, r))
                {
                    return new TileData(q, r, TerrainType.Swamp, 0, MineType.None, true, "tower_cannon_green");
                }
                return new TileData(q, r, TerrainType.Swamp);
            }

            // 3. KRALLIK MERKEZİ - Görsel Şölen Alanı
            // Bataklık içindeki alan = dekoratif manzara (oynanabilir değil, sadece görsel)
            if (IsInsideSwamp(q, r))
            {
                return GenerateKingdomCenterTile(q, r, random);
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
        /// Krallık merkezi görsel şölen alanı
        /// Fütüristik manzara - kale, kuleler, su içinde gemiler, bahçeler
        /// </summary>
        private static TileData GenerateKingdomCenterTile(int q, int r, System.Random random)
        {
            int dx = q - CENTER_X;
            int dy = r - CENTER_Y;
            float dist = Mathf.Sqrt(dx * dx + dy * dy);

            // Merkez kale
            if (q == CENTER_X && r == CENTER_Y)
            {
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "castle_green");
            }

            // İç içe halkalar halinde dekoratif yapı
            int ring = Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy));

            // Halka 1: Merkez kaleyi çevreleyen yol
            if (ring == 1)
            {
                return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
            }

            // Halka 2: Su kanalı (hendek)
            if (ring == 2)
            {
                // Köşelerde köprü
                if ((Mathf.Abs(dx) == 2 && dy == 0) || (dx == 0 && Mathf.Abs(dy) == 2))
                {
                    return new TileData(q, r, TerrainType.Bridge, 0, MineType.None, false, "");
                }
                return new TileData(q, r, TerrainType.Water, 0, MineType.None, false, "");
            }

            // Halka 3: İç bahçe - çimen ve ağaçlar
            if (ring == 3)
            {
                // Her 2. tile'da ağaç
                if ((dx + dy) % 2 == 0)
                {
                    return new TileData(q, r, TerrainType.Forest, 0, MineType.None, false, "");
                }
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
            }

            // Halka 4: Dış yol halkası
            if (ring == 4)
            {
                // Köşelerde savunma kuleleri
                if (Mathf.Abs(dx) == 4 && Mathf.Abs(dy) == 4)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_cannon_green");
                }
                return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
            }

            // Halka 5+: Dış bahçe ve dekoratif alanlar
            if (ring >= 5)
            {
                int hash = GetTileHash(q, r);
                float normalized = (hash % 100) / 100f;

                // %15 su havuzu / gölet
                if (normalized < 0.15f)
                {
                    return new TileData(q, r, TerrainType.Water, 0, MineType.None, false, "");
                }
                // %20 orman/bahçe
                else if (normalized < 0.35f)
                {
                    return new TileData(q, r, TerrainType.Forest, 0, MineType.None, false, "");
                }
                // %15 çiftlik
                else if (normalized < 0.50f)
                {
                    return new TileData(q, r, TerrainType.Farm, 0, MineType.None, false, "");
                }
                // Ana yollar (merkeze giden)
                else if (dx == 0 || dy == 0)
                {
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
                }
                // Geri kalan çimen
                else
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                }
            }

            return new TileData(q, r, TerrainType.Grass);
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
        /// 2000x2000 harita için optimize edildi
        /// </summary>
        private static TileData? TryGenerateMine(int q, int r, float distance, System.Random random)
        {
            // Maden olasılığı - zone'a göre değişir
            float mineChance;
            if (distance <= MINE_ZONE_1) mineChance = 0.08f;      // %8 - Merkez yakını daha fazla
            else if (distance <= MINE_ZONE_2) mineChance = 0.06f; // %6
            else if (distance <= MINE_ZONE_3) mineChance = 0.04f; // %4
            else if (distance <= MINE_ZONE_4) mineChance = 0.03f; // %3
            else return null; // Çok uzak - maden yok

            if (random.NextDouble() > mineChance) return null;

            // Mesafeye göre maden tipi ve seviye
            MineType mineType;
            int mineLevel;

            if (distance <= MINE_ZONE_1)
            {
                // ZONE 1: En değerli - Level 6-7 (Gem/Gold)
                mineLevel = random.Next(6, 8);
                mineType = random.NextDouble() > 0.4 ? MineType.Gem : MineType.Gold;
            }
            else if (distance <= MINE_ZONE_2)
            {
                // ZONE 2: Yüksek değer - Level 4-6 (Gold/Iron)
                mineLevel = random.Next(4, 7);
                mineType = random.NextDouble() > 0.6 ? MineType.Gold : MineType.Iron;
            }
            else if (distance <= MINE_ZONE_3)
            {
                // ZONE 3: Orta değer - Level 2-4 (Iron/Stone)
                mineLevel = random.Next(2, 5);
                mineType = random.NextDouble() > 0.5 ? MineType.Iron : MineType.Stone;
            }
            else
            {
                // ZONE 4: Düşük değer - Level 1-2 (Stone/Wood)
                mineLevel = random.Next(1, 3);
                mineType = random.NextDouble() > 0.4 ? MineType.Stone : MineType.Wood;
            }

            // MineType'a karşılık gelen TerrainType'ı al
            TerrainType terrainType = GetTerrainForMineType(mineType);

            return new TileData(q, r, terrainType, mineLevel, mineType);
        }

        /// <summary>
        /// Doğal arazi üret - Dağınık dağılım (toplulaşma önleme)
        /// Perlin noise yerine deterministik hash + scatter algoritması
        /// </summary>
        private static TileData GenerateNaturalTerrain(int q, int r, System.Random random)
        {
            // Hash-based deterministik değer (her tile için sabit)
            int hash = GetTileHash(q, r);
            float normalized = (hash % 10000) / 10000f;

            // Perlin noise - büyük ölçekli bölgeler için (yumuşak geçişler)
            float regionNoise = Mathf.PerlinNoise(q * 0.02f + 0.5f, r * 0.02f + 0.5f);

            // Scatter noise - küçük ölçekli varyasyon için
            float scatterNoise = Mathf.PerlinNoise(q * 0.15f + 50f, r * 0.15f + 50f);

            // Her terrain tipi için ayrı hash kontrolü - DAĞINIK
            TerrainType terrain = TerrainType.Grass;

            // Dağlar - %3 şans, scatter ile dağınık
            if (normalized < 0.03f && scatterNoise > 0.5f)
            {
                terrain = TerrainType.Mountain;
            }
            // Tepeler - %5 şans
            else if (normalized < 0.08f && scatterNoise > 0.4f)
            {
                terrain = TerrainType.Hill;
            }
            // Ormanlar - %12 şans, dağınık
            else if (normalized > 0.15f && normalized < 0.27f && scatterNoise > 0.35f)
            {
                terrain = TerrainType.Forest;
            }
            // Su gölleri - %2 şans, çok seyrek
            else if (normalized > 0.95f && scatterNoise < 0.3f)
            {
                terrain = TerrainType.Water;
            }
            // Bataklık - %3 şans
            else if (normalized > 0.88f && normalized < 0.91f)
            {
                terrain = TerrainType.Swamp;
            }
            // Çiftlik - %4 şans
            else if (normalized > 0.50f && normalized < 0.54f && scatterNoise > 0.5f)
            {
                terrain = TerrainType.Farm;
            }
            // Yollar - harita üzerinde ana arterler
            else if (IsMainRoad(q, r))
            {
                terrain = TerrainType.Road;
            }

            return new TileData(q, r, terrain);
        }

        /// <summary>
        /// Tile için deterministik hash değeri
        /// </summary>
        private static int GetTileHash(int q, int r)
        {
            // Sabit seed ile tutarlı hash
            int hash = q * 73856093 ^ r * 19349663;
            return Mathf.Abs(hash);
        }

        /// <summary>
        /// Ana yol ağı kontrolü - harita üzerinde ana arterler
        /// </summary>
        private static bool IsMainRoad(int q, int r)
        {
            // Her 50 tile'da bir yatay ve dikey yollar
            int roadSpacing = _mapSize / 40;
            if (roadSpacing < 10) roadSpacing = 10;

            // Merkeze giden ana yollar
            bool horizontalToCenter = (r == CENTER_Y) && (q > SWAMP_OUTER_RADIUS || q < _mapSize - SWAMP_OUTER_RADIUS);
            bool verticalToCenter = (q == CENTER_X) && (r > SWAMP_OUTER_RADIUS || r < _mapSize - SWAMP_OUTER_RADIUS);

            // Grid yolları
            bool gridRoad = (q % roadSpacing == 0 || r % roadSpacing == 0) &&
                           GetTileHash(q, r) % 100 < 15; // %15 şansla yol

            return horizontalToCenter || verticalToCenter;
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

        /// <summary>
        /// Maden türü rengini döndür
        /// </summary>
        public static Color GetMineTypeColor(MineType type)
        {
            return type switch
            {
                MineType.Gold => new Color(1f, 0.84f, 0f),      // Altın sarısı
                MineType.Gem => new Color(0.9f, 0.2f, 0.9f),    // Mor (mücevher)
                MineType.Iron => new Color(0.6f, 0.6f, 0.7f),   // Gümüş grisi
                MineType.Stone => new Color(0.5f, 0.45f, 0.4f), // Taş kahve
                MineType.Wood => new Color(0.6f, 0.4f, 0.2f),   // Ahşap kahve
                _ => Color.white
            };
        }

        /// <summary>
        /// Maden türü kısa adını döndür (tile üzerinde görünecek)
        /// </summary>
        public static string GetMineTypeName(MineType type)
        {
            return type switch
            {
                MineType.Gold => "Altın",
                MineType.Gem => "Gem",
                MineType.Iron => "Demir",
                MineType.Stone => "Taş",
                MineType.Wood => "Odun",
                _ => ""
            };
        }

        /// <summary>
        /// Maden türü tam adını döndür
        /// </summary>
        public static string GetMineTypeFullName(MineType type)
        {
            return type switch
            {
                MineType.Gold => "Altın Madeni",
                MineType.Gem => "Mücevher Madeni",
                MineType.Iron => "Demir Madeni",
                MineType.Stone => "Taş Ocağı",
                MineType.Wood => "Kereste Alanı",
                _ => "Bilinmiyor"
            };
        }

        /// <summary>
        /// Maden türü ikonu/sembolü döndür
        /// </summary>
        public static string GetMineTypeIcon(MineType type)
        {
            return type switch
            {
                MineType.Gold => "⚱",    // Altın
                MineType.Gem => "💎",    // Mücevher
                MineType.Iron => "⚒",    // Demir
                MineType.Stone => "🪨",   // Taş
                MineType.Wood => "🪵",    // Odun
                _ => ""
            };
        }
    }
}
