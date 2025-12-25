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
        // Varsayilan harita boyutu (PROD: 2000x2000)
        private static int _mapSize = 2000;
        private static int _centerX = 1000;
        private static int _centerY = 1000;

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
            public int BuildingLevel;   // 0-30 bina seviyesi
            public float BuildingRotation; // Bina rotasyonu (Y ekseni derece)

            public TileData(int q, int r, TerrainType terrain, int mineLevel = 0, MineType mineType = MineType.None, bool hasBuilding = false, string buildingType = "", int buildingLevel = 0, float buildingRotation = 0f)
            {
                Q = q;
                R = r;
                Terrain = terrain;
                MineLevel = mineLevel;
                MineType = mineType;
                HasBuilding = hasBuilding;
                BuildingType = buildingType;
                BuildingLevel = buildingLevel;
                BuildingRotation = buildingRotation;
            }
        }

        // Oyuncu şehri için sabit (0,0)
        // Oyuncu şehri devre dışı - sadece NPC Kingdom var
        public const int PLAYER_CITY_X = -9999; // Harita dışı
        public const int PLAYER_CITY_Y = -9999;
        public const int PLAYER_CITY_RADIUS = 0; // Devre dışı
        public const int MAX_BUILDING_LEVEL = 30;

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

            // 0. OYUNCU ŞEHRİ (0,0) - EN ÖNCELİKLİ
            if (IsInPlayerCity(q, r))
            {
                return GeneratePlayerCityTile(q, r, random);
            }

            // 1. MERKEZ KALE (NPC Krallık)
            if (q == CENTER_X && r == CENTER_Y)
            {
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "castle_green", MAX_BUILDING_LEVEL);
            }

            // 2. BATAKLIK ÇEVRESİ (kare şeklinde)
            if (IsInSwampArea(q, r))
            {
                // Köşe kontrol - topçu kuleleri
                if (IsSwampCorner(q, r))
                {
                    return new TileData(q, r, TerrainType.Swamp, 0, MineType.None, true, "tower_cannon_green", 15);
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
        /// Krallık merkezi - Tam teşekküllü NPC şehri
        /// Binalar, surlar, kuleler ve savunma yapıları
        /// </summary>
        private static TileData GenerateKingdomCenterTile(int q, int r, System.Random random)
        {
            int dx = q - CENTER_X;
            int dy = r - CENTER_Y;
            int absDx = Mathf.Abs(dx);
            int absDy = Mathf.Abs(dy);
            int ring = Mathf.Max(absDx, absDy);
            int hash = GetTileHash(q, r);

            // ════════════════════════════════════════════════════════════════
            // MERKEZ KALE (Ring 0) - Level 30
            // ════════════════════════════════════════════════════════════════
            if (q == CENTER_X && r == CENTER_Y)
            {
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "castle_blue", MAX_BUILDING_LEVEL);
            }

            // ════════════════════════════════════════════════════════════════
            // HALKA 1: Kale etrafı - Belediye, Market (Level 28-30)
            // ════════════════════════════════════════════════════════════════
            if (ring == 1)
            {
                // 4 ana yönde önemli binalar
                if (dx == 0 && dy == -1) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "townhall_blue", 30);
                if (dx == 0 && dy == 1) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "market_blue", 29);
                if (dx == -1 && dy == 0) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "blacksmith_blue", 28);
                if (dx == 1 && dy == 0) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "workshop_blue", 28);
                // Köşeler - yol
                return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
            }

            // ════════════════════════════════════════════════════════════════
            // HALKA 2: İç yol halkası + köşe kuleleri
            // ════════════════════════════════════════════════════════════════
            if (ring == 2)
            {
                // Köşelerde gözetleme kuleleri
                if (absDx == 2 && absDy == 2)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "watchtower_blue", 25);
                }
                return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
            }

            // ════════════════════════════════════════════════════════════════
            // HALKA 3: Askeri bölge (Level 25-28)
            // ════════════════════════════════════════════════════════════════
            if (ring == 3)
            {
                // Ana yollar - rotasyon ile
                // Dikey yol (dx == 0): 90°, Yatay yol (dy == 0): 0°
                if (dx == 0 || dy == 0)
                {
                    float roadRotation = (dx == 0) ? 90f : 0f;
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "", 0, roadRotation);
                }

                // Askeri binalar - 4 bölge
                if (dx > 0 && dy > 0) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "barracks_blue", 27);
                if (dx < 0 && dy > 0) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "archeryrange_blue", 26);
                if (dx > 0 && dy < 0) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "stables_blue", 26);
                if (dx < 0 && dy < 0) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "barracks_blue", 25);

                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
            }

            // ════════════════════════════════════════════════════════════════
            // HALKA 4: Su hendeği (savunma)
            // ════════════════════════════════════════════════════════════════
            if (ring == 4)
            {
                // 4 ana yönde köprüler - rotasyon ile
                if ((absDx == 4 && dy == 0) || (dx == 0 && absDy == 4))
                {
                    // Yatay köprü (dy == 0): 0°, Dikey köprü (dx == 0): 90°
                    float bridgeRotation = (dy == 0) ? 0f : 90f;
                    return new TileData(q, r, TerrainType.Bridge, 0, MineType.None, false, "", 0, bridgeRotation);
                }
                return new TileData(q, r, TerrainType.Water, 0, MineType.None, false, "");
            }

            // ════════════════════════════════════════════════════════════════
            // HALKA 5: İç sur + kuleler
            // ════════════════════════════════════════════════════════════════
            if (ring == 5)
            {
                // Köşelerde topçu kuleleri
                if (absDx == 5 && absDy == 5)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_cannon_blue", 28);
                }
                // Kapı kuleleri (4 ana yön)
                if ((absDx == 5 && dy == 0) || (dx == 0 && absDy == 5))
                {
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, true, "tower_a_blue", 25);
                }
                // Sur duvarları - dikdörtgen şekli için rotasyon
                // Sol/sağ kenarlar (dx == ±5): 90° (dikey duvar)
                // Üst/alt kenarlar (dy == ±5): 0° (yatay duvar)
                if (absDx == 5 || absDy == 5)
                {
                    float wallRotation = (absDx == 5) ? 90f : 0f;
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "wall_straight", 20, wallRotation);
                }
                return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
            }

            // ════════════════════════════════════════════════════════════════
            // HALKA 6-8: Üretim ve konut bölgesi
            // ════════════════════════════════════════════════════════════════
            if (ring >= 6 && ring <= 8)
            {
                // Ana yollar - rotasyon ile
                // Dikey yol (dx == 0): 90°, Yatay yol (dy == 0): 0°
                if (dx == 0 || dy == 0)
                {
                    float roadRotation = (dx == 0) ? 90f : 0f;
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "", 0, roadRotation);
                }

                // Bina dağılımı
                int buildingHash = hash % 20;
                int level = 18 + (hash % 8); // 18-25

                if (ring == 6)
                {
                    // Üretim binaları
                    if (buildingHash < 4) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "mine_blue", level);
                    if (buildingHash < 8) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "lumbermill_blue", level);
                    if (buildingHash < 12) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "windmill_blue", level);
                    if (buildingHash < 16) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "watermill_blue", level);
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                }

                if (ring == 7)
                {
                    // Konut binaları
                    if (buildingHash < 5) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "home_a_blue", level);
                    if (buildingHash < 10) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "home_b_blue", level);
                    if (buildingHash < 13) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tavern_blue", level);
                    if (buildingHash < 15) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "well_blue", level);
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                }

                if (ring == 8)
                {
                    // Dini ve özel binalar
                    if (buildingHash < 3) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "church_blue", level);
                    if (buildingHash < 6) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "shrine_blue", level);
                    if (buildingHash < 10) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "home_a_blue", level - 2);
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                }
            }

            // ════════════════════════════════════════════════════════════════
            // HALKA 9: Orta sur + kuleler
            // ════════════════════════════════════════════════════════════════
            if (ring == 9)
            {
                // Köşelerde mancınık kuleleri
                if (absDx == 9 && absDy == 9)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_catapult_blue", 24);
                }
                // Kapı kuleleri
                if ((absDx == 9 && dy == 0) || (dx == 0 && absDy == 9))
                {
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, true, "tower_b_blue", 22);
                }
                // Sur boyunca gözetleme kuleleri (her 3 tile'da)
                if ((absDx == 9 || absDy == 9) && (absDx + absDy) % 3 == 0)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "watchtower_blue", 20);
                }
                // Sur duvarları - dikdörtgen şekli için rotasyon
                if (absDx == 9 || absDy == 9)
                {
                    float wallRotation = (absDx == 9) ? 90f : 0f;
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "wall_straight", 18, wallRotation);
                }
                return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
            }

            // ════════════════════════════════════════════════════════════════
            // HALKA 10-14: Dış şehir - gölet, liman ve çiftlik
            // ════════════════════════════════════════════════════════════════
            if (ring >= 10 && ring <= 14)
            {
                // Ana yollar - rotasyon ile
                // Dikey yol (dx == 0): 90°, Yatay yol (dy == 0): 0°
                if (dx == 0 || dy == 0)
                {
                    float roadRotation = (dx == 0) ? 90f : 0f;
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "", 0, roadRotation);
                }

                int buildingHash = hash % 25;
                int level = 12 + (hash % 10); // 12-21

                // ════════════════════════════════════════════════════════════
                // DENİZ LİMANI BÖLGESİ (sağ taraf dx > 8, ring 11-14)
                // Tersane ve rıhtımlar kıyıda, etrafında su
                // ════════════════════════════════════════════════════════════
                if (dx > 8 && ring >= 11 && ring <= 14)
                {
                    // Deniz sağda, rıhtımlar sağa (denize) bakmalı = 90°
                    float seaDockRotation = 90f;

                    // En dış halka (14) - Açık deniz + büyük gemiler
                    if (ring == 14 && dx > 10)
                    {
                        // Açık denizde gemi (%25 şans)
                        if (buildingHash < 7)
                        {
                            // Gemi yönü - rastgele
                            float shipRotation = (buildingHash % 4) * 90f;
                            return new TileData(q, r, TerrainType.Water, 0, MineType.None, true, "ship_blue", level, shipRotation);
                        }
                        return new TileData(q, r, TerrainType.Water, 0, MineType.None, false, "");
                    }

                    // Ring 13 - Kıyı suyu + küçük tekneler
                    if (ring == 13 && dx > 9)
                    {
                        if (buildingHash < 5)
                        {
                            float boatRotation = (buildingHash % 4) * 90f;
                            return new TileData(q, r, TerrainType.Water, 0, MineType.None, true, "boat", level, boatRotation);
                        }
                        return new TileData(q, r, TerrainType.Water, 0, MineType.None, false, "");
                    }

                    // Ring 12 - Rıhtım ve tersane
                    if (ring == 12)
                    {
                        if (dx >= 11)
                        {
                            return new TileData(q, r, TerrainType.Coast, 0, MineType.None, false, "");
                        }
                        if (buildingHash < 4)
                        {
                            return new TileData(q, r, TerrainType.Coast, 0, MineType.None, true, "shipyard_blue", level, seaDockRotation);
                        }
                        if (buildingHash < 8)
                        {
                            return new TileData(q, r, TerrainType.Coast, 0, MineType.None, true, "docks_blue", level, seaDockRotation);
                        }
                    }

                    // Ring 11 - Liman arkası binalar
                    if (ring == 11)
                    {
                        if (buildingHash < 3)
                        {
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "docks_blue", level, seaDockRotation);
                        }
                        if (buildingHash < 6)
                        {
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "home_a_blue", level - 2);
                        }
                    }

                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                }

                // Çiftlik bölgesi
                if (buildingHash < 5)
                {
                    return new TileData(q, r, TerrainType.Farm, 0, MineType.None, false, "");
                }
                // Ormanlık alan
                if (buildingHash < 7)
                {
                    return new TileData(q, r, TerrainType.Forest, 0, MineType.None, false, "");
                }
                // Dağınık evler
                if (buildingHash < 11)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tent_blue", level - 5);
                }

                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
            }

            // ════════════════════════════════════════════════════════════════
            // HALKA 15: DIŞ SUR - Ana savunma hattı
            // ════════════════════════════════════════════════════════════════
            if (ring == 15)
            {
                // Köşelerde büyük topçu kuleleri
                if (absDx == 15 && absDy == 15)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_cannon_blue", 26);
                }
                // Ana kapı kuleleri (4 ana yön)
                if ((absDx == 15 && dy == 0) || (dx == 0 && absDy == 15))
                {
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, true, "tower_cannon_blue", 24);
                }
                // Sur boyunca kuleler (her 4 tile'da)
                if ((absDx == 15 || absDy == 15) && (absDx + absDy) % 4 == 0)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_a_blue", 20);
                }
                // Sur duvarları - dikdörtgen şekli için rotasyon
                if (absDx == 15 || absDy == 15)
                {
                    float wallRotation = (absDx == 15) ? 90f : 0f;
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "wall_straight", 15, wallRotation);
                }
                return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
            }

            // ════════════════════════════════════════════════════════════════
            // HALKA 16+: Sur dışı - Açık alan ve ileri karakollar
            // ════════════════════════════════════════════════════════════════
            if (ring > 15)
            {
                // Ana yollar
                // Dikey yol (dx == 0): 90°, Yatay yol (dy == 0): 0°
                if (dx == 0 || dy == 0)
                {
                    float roadRotation = (dx == 0) ? 90f : 0f;
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "", 0, roadRotation);
                }

                int buildingHash = hash % 30;
                float noise = Mathf.PerlinNoise(q * 0.1f + 100f, r * 0.1f + 100f);
                int level = 10 + (hash % 12); // 10-21

                // ════════════════════════════════════════════════════════════
                // ASKERİYE - Koordinat (43, -36) çevresinde
                // Ordu nizamında askeri binalar
                // ════════════════════════════════════════════════════════════
                int campX = 43;
                int campY = -36;
                int campDx = dx - campX;
                int campDy = dy - campY;
                int distFromCamp = Mathf.Max(Mathf.Abs(campDx), Mathf.Abs(campDy));

                if (distFromCamp <= 7)
                {
                    // ═══ KIŞLA İÇİ - Sadece binalar ═══

                    // Merkez - Komuta Kulesi
                    if (distFromCamp == 0)
                    {
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_cannon_blue", 30);
                    }

                    // Halka 1 - Yol
                    if (distFromCamp == 1)
                    {
                        return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
                    }

                    // Halka 2 - Kışlalar (4 yönde)
                    if (distFromCamp == 2)
                    {
                        if (campDx == 2 && campDy == 0) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "barracks_blue", 28);
                        if (campDx == -2 && campDy == 0) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "barracks_blue", 28);
                        if (campDx == 0 && campDy == 2) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "barracks_blue", 28);
                        if (campDx == 0 && campDy == -2) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "barracks_blue", 28);
                        // Köşelerde gözcü kuleleri
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "watchtower_blue", 25);
                    }

                    // Halka 3 - Yol
                    if (distFromCamp == 3)
                    {
                        return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
                    }

                    // Halka 4 - Okçu menzili, Ahırlar, Top kuleleri
                    if (distFromCamp == 4)
                    {
                        if (Mathf.Abs(campDy) == 4 && Mathf.Abs(campDx) <= 1)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "archeryrange_blue", 26);
                        if (Mathf.Abs(campDx) == 4 && Mathf.Abs(campDy) <= 1)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "stables_blue", 26);
                        if (Mathf.Abs(campDx) >= 3 && Mathf.Abs(campDy) >= 3)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_cannon_blue", 24);
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }

                    // Halka 5 - Yol
                    if (distFromCamp == 5)
                    {
                        return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
                    }

                    // Halka 6 - Mancınık kuleleri ve ek kışlalar
                    if (distFromCamp == 6)
                    {
                        if (Mathf.Abs(campDx) == 6 && Mathf.Abs(campDy) == 6)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_catapult_blue", 24);
                        if ((Mathf.Abs(campDx) == 6 && Mathf.Abs(campDy) <= 2) ||
                            (Mathf.Abs(campDy) == 6 && Mathf.Abs(campDx) <= 2))
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "barracks_blue", 22);
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }

                    // Halka 7 - Sur duvarları
                    if (distFromCamp == 7)
                    {
                        if ((campDx == 0 && Mathf.Abs(campDy) == 7) || (campDy == 0 && Mathf.Abs(campDx) == 7))
                            return new TileData(q, r, TerrainType.Road, 0, MineType.None, true, "tower_a_blue", 22);
                        float wallRot = (Mathf.Abs(campDx) == 7) ? 90f : 0f;
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "wall_straight", 20, wallRot);
                    }

                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                }

                // ═══════════════════════════════════════════════════════════════
                // ORDU NİZAMI - Kışlanın güneyinde (Y negatif yönde)
                // Komutan en önde, arkasında farklı birim tipleri
                // ═══════════════════════════════════════════════════════════════
                int armyY = campY - 12; // Kışlanın 12 birim güneyinde
                int armyDx = dx - campX;
                int armyDy = dy - armyY;

                // Ordu alanı: X=-7 ile +7, Y=0 ile -9 (10 sıra derinlik)
                if (armyDx >= -7 && armyDx <= 7 && armyDy >= -9 && armyDy <= 0)
                {
                    // Sıra 0 (en ön): Komutan ortada, kılıçlı muhafızlar yanlarda
                    if (armyDy == 0)
                    {
                        if (armyDx == 0) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "banner_blue", 1); // Komutan bayrağı
                        if (Mathf.Abs(armyDx) <= 2) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "sword_blue", 1); // Kılıçlı muhafız
                        if (Mathf.Abs(armyDx) <= 4) return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "shield_blue", 1); // Kalkanlı koruma
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }

                    // Sıra 1: Kılıçlı piyade
                    if (armyDy == -1)
                    {
                        if (Mathf.Abs(armyDx) <= 6)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "sword_blue", 1);
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }

                    // Sıra 2: Kalkanlı piyade
                    if (armyDy == -2)
                    {
                        if (Mathf.Abs(armyDx) <= 6)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "shield_blue", 1);
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }

                    // Sıra 3: Mızraklı piyade
                    if (armyDy == -3)
                    {
                        if (Mathf.Abs(armyDx) <= 6)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "spear_blue", 1);
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }

                    // Sıra 4: Okçular (yay)
                    if (armyDy == -4)
                    {
                        if (Mathf.Abs(armyDx) <= 6)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "bow_blue", 1);
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }

                    // Sıra 5: Mancınıklar (yanlarda) + Okçular (ortada)
                    if (armyDy == -5)
                    {
                        if (Mathf.Abs(armyDx) >= 5 && Mathf.Abs(armyDx) <= 7)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "catapult_blue", 1);
                        if (Mathf.Abs(armyDx) <= 4)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "bow_blue", 1);
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }

                    // Sıra 6: Topçular (yanlarda) + Askerler (ortada)
                    if (armyDy == -6)
                    {
                        if (Mathf.Abs(armyDx) >= 4 && Mathf.Abs(armyDx) <= 7)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "cannon_blue", 1);
                        if (Mathf.Abs(armyDx) <= 3)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "unit_blue", 1);
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }

                    // Sıra 7: Süvariler (atlılar)
                    if (armyDy == -7)
                    {
                        if (Mathf.Abs(armyDx) <= 7)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "horse_blue", 1);
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }

                    // Sıra 8: İkmal arabaları
                    if (armyDy == -8)
                    {
                        if (Mathf.Abs(armyDx) <= 5)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "cart_blue", 1);
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }

                    // Sıra 9: Çadırlar (arka)
                    if (armyDy == -9)
                    {
                        if (Mathf.Abs(armyDx) <= 6)
                            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tent_blue", 15);
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                    }
                }

                // ════════════════════════════════════════════════════════════
                // GÖLET ALANI - Koordinat (37, 21) çevresinde
                // UI: (37, 21), Tile: (1037, 1021)
                // ════════════════════════════════════════════════════════════
                int lakeX = 37;
                int lakeY = 21;
                int lakeDx = dx - lakeX;  // Gölete göre pozisyon
                int lakeDy = dy - lakeY;
                int distFromLake = Mathf.Max(Mathf.Abs(lakeDx), Mathf.Abs(lakeDy));

                if (distFromLake <= 6)
                {
                    // Gölet merkezi - derin su (yarıçap 2) + gemiler
                    if (distFromLake <= 2)
                    {
                        // Su üzerinde tekne/gemi (%30 şans)
                        if (buildingHash < 9)
                        {
                            // Tekne rotasyonu - rastgele yön
                            float boatRotation = (buildingHash % 4) * 90f;
                            return new TileData(q, r, TerrainType.Water, 0, MineType.None, true, "boat", level, boatRotation);
                        }
                        return new TileData(q, r, TerrainType.Water, 0, MineType.None, false, "");
                    }
                    // Sığ su (yarıçap 3) + küçük tekneler
                    if (distFromLake <= 3)
                    {
                        if (buildingHash < 6)
                        {
                            float boatRotation = (buildingHash % 4) * 90f;
                            return new TileData(q, r, TerrainType.Water, 0, MineType.None, true, "boat", level, boatRotation);
                        }
                        return new TileData(q, r, TerrainType.Water, 0, MineType.None, false, "");
                    }
                    // Kıyı (yarıçap 5) - Rıhtımlar suya dönük
                    if (distFromLake <= 4)
                    {
                        // Rıhtım rotasyonu - gölet merkezine baksın
                        float dockRotation = CalculateDockRotation(lakeDx, lakeDy);

                        if (buildingHash < 4)
                        {
                            return new TileData(q, r, TerrainType.Coast, 0, MineType.None, true, "docks_blue", level, dockRotation);
                        }
                        if (buildingHash < 6)
                        {
                            return new TileData(q, r, TerrainType.Coast, 0, MineType.None, true, "shipyard_blue", level, dockRotation);
                        }
                        return new TileData(q, r, TerrainType.Coast, 0, MineType.None, false, "");
                    }
                    // Gölet çevresi - park ve bahçe
                    if (buildingHash < 5)
                    {
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "well_blue", level - 5);
                    }
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                }

                // İleri gözetleme kuleleri (ring 18 ve 20'de)
                if ((ring == 18 || ring == 20) && (absDx == ring && absDy == ring))
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "watchtower_blue", 15);
                }

                // Ring 16-17: Sur çevresi - hendek ve savunma alanı
                if (ring <= 17)
                {
                    // Hendek suyu (sur etrafı)
                    if (ring == 16 && buildingHash < 8)
                    {
                        return new TileData(q, r, TerrainType.Water, 0, MineType.None, false, "");
                    }
                    // Kıyı
                    if (ring == 16 && buildingHash < 12)
                    {
                        return new TileData(q, r, TerrainType.Coast, 0, MineType.None, false, "");
                    }
                    // Boş alan (savunma görüş alanı)
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                }

                // Ring 18+: Doğal peyzaj - çeşitlilik artırıldı
                // Küçük göller
                if (noise > 0.85f && buildingHash < 5)
                {
                    return new TileData(q, r, TerrainType.Water, 0, MineType.None, false, "");
                }
                // Tepeler
                if (noise > 0.75f && buildingHash < 8)
                {
                    return new TileData(q, r, TerrainType.Hill, 0, MineType.None, false, "");
                }
                // Ormanlar - dağınık
                if (buildingHash < 4 && noise > 0.4f)
                {
                    return new TileData(q, r, TerrainType.Forest, 0, MineType.None, false, "");
                }
                // Çiftlikler
                if (buildingHash >= 4 && buildingHash < 10 && noise < 0.5f)
                {
                    return new TileData(q, r, TerrainType.Farm, 0, MineType.None, false, "");
                }
                // Köyler (dağınık evler)
                if (buildingHash >= 10 && buildingHash < 13 && ring < 22)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tent_blue", 5 + (hash % 5));
                }

                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
            }

            return new TileData(q, r, TerrainType.Grass);
        }

        /// <summary>
        /// Oyuncu şehri alanında mı? (0,0 etrafında)
        /// </summary>
        private static bool IsInPlayerCity(int q, int r)
        {
            int dx = Mathf.Abs(q - PLAYER_CITY_X);
            int dy = Mathf.Abs(r - PLAYER_CITY_Y);
            return dx <= PLAYER_CITY_RADIUS && dy <= PLAYER_CITY_RADIUS;
        }

        /// <summary>
        /// Oyuncu şehri tile'ı oluştur - Gelişmiş şehir yapısı
        /// Max seviye 30, tüm binalar dahil
        /// </summary>
        private static TileData GeneratePlayerCityTile(int q, int r, System.Random random)
        {
            int dx = q - PLAYER_CITY_X;
            int dy = r - PLAYER_CITY_Y;
            int ring = Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy));

            // Merkez - Ana Kale (Level 30)
            if (q == PLAYER_CITY_X && r == PLAYER_CITY_Y)
            {
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "castle_green", MAX_BUILDING_LEVEL);
            }

            // Halka 1: Belediye binası, market, blacksmith, workshop (Level 28-30)
            if (ring == 1)
            {
                string[] coreBuildings = { "townhall_green", "market_green", "blacksmith_green", "workshop_green" };
                int buildingIndex = ((dx + 1) + (dy + 1) * 3) % coreBuildings.Length;
                // Köşelerde çim bırak
                if (Mathf.Abs(dx) == 1 && Mathf.Abs(dy) == 1)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
                }
                int level = 28 + random.Next(3); // 28-30
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, coreBuildings[buildingIndex], level);
            }

            // Halka 2: Yol çevresi
            if (ring == 2)
            {
                // Köşelerde savunma kuleleri (Level 25)
                if (Mathf.Abs(dx) == 2 && Mathf.Abs(dy) == 2)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_cannon_green", 25);
                }
                return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
            }

            // Halka 3: Askeri binalar (Level 22-26)
            if (ring == 3)
            {
                string[] militaryBuildings = { "barracks_green", "archeryrange_green", "stables_green", "watchtower_green" };
                int hash = GetTileHash(q, r);

                // Yollar (merkeze giden)
                if (dx == 0 || dy == 0)
                {
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
                }

                int buildingIndex = (Mathf.Abs(hash)) % militaryBuildings.Length;
                int level = 22 + random.Next(5); // 22-26
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, militaryBuildings[buildingIndex], level);
            }

            // Halka 4: Üretim binaları (Level 18-24)
            if (ring == 4)
            {
                string[] productionBuildings = { "mine_green", "lumbermill_green", "windmill_green", "watermill_green" };
                int hash = GetTileHash(q, r);

                // Yollar
                if (dx == 0 || dy == 0)
                {
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
                }

                // Köşelerde gözetleme kuleleri
                if (Mathf.Abs(dx) == 4 && Mathf.Abs(dy) == 4)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_a_green", 20);
                }

                int buildingIndex = (Mathf.Abs(hash)) % productionBuildings.Length;
                int level = 18 + random.Next(7); // 18-24
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, productionBuildings[buildingIndex], level);
            }

            // Halka 5: Konut ve dini binalar (Level 15-20)
            if (ring == 5)
            {
                string[] residentialBuildings = { "home_a_green", "home_b_green", "tavern_green", "church_green", "shrine_green" };
                int hash = GetTileHash(q, r);

                // Yollar
                if (dx == 0 || dy == 0)
                {
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
                }

                int buildingIndex = (Mathf.Abs(hash)) % residentialBuildings.Length;
                int level = 15 + random.Next(6); // 15-20
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, residentialBuildings[buildingIndex], level);
            }

            // Halka 6: Denizcilik ve ek binalar (Level 12-18)
            if (ring == 6)
            {
                string[] mixedBuildings = { "shipyard_green", "docks_green", "tent_green", "home_a_green", "home_b_green" };
                int hash = GetTileHash(q, r);

                // Dış yol halkası
                if (dx == 0 || dy == 0)
                {
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
                }

                // Su alanları (liman için)
                if ((dx > 0 && Mathf.Abs(dy) <= 1) || (dx < 0 && Mathf.Abs(dy) <= 1))
                {
                    // Rıhtım veya tersane
                    if (hash % 3 == 0)
                    {
                        return new TileData(q, r, TerrainType.Coast, 0, MineType.None, true, dx > 0 ? "shipyard_green" : "docks_green", 15);
                    }
                    return new TileData(q, r, TerrainType.Coast, 0, MineType.None, false, "");
                }

                int buildingIndex = (Mathf.Abs(hash)) % mixedBuildings.Length;
                int level = 12 + random.Next(7); // 12-18
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, mixedBuildings[buildingIndex], level);
            }

            // Halka 7: Sur ve savunma hattı (Level 20-25)
            if (ring == 7)
            {
                // Köşelerde mancınık kuleleri
                if (Mathf.Abs(dx) == 7 && Mathf.Abs(dy) == 7)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_catapult_green", 22);
                }

                // Kenarlar boyunca duvar/yol
                if (Mathf.Abs(dx) == 7 || Mathf.Abs(dy) == 7)
                {
                    // Her 3 tile'da bir gözetleme kulesi
                    if ((dx + dy) % 3 == 0)
                    {
                        return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "watchtower_green", 18);
                    }
                    return new TileData(q, r, TerrainType.Road, 0, MineType.None, false, "");
                }

                // İç kısım - çiftlik ve bahçe
                int hash = GetTileHash(q, r);
                if (hash % 4 == 0)
                {
                    return new TileData(q, r, TerrainType.Farm, 0, MineType.None, false, "");
                }
                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
            }

            // Halka 8: Dış çiftlik ve kaynak alanı
            if (ring == 8)
            {
                int hash = GetTileHash(q, r);
                float normalized = (hash % 100) / 100f;

                // Dış köşelerde son savunma kuleleri
                if (Mathf.Abs(dx) == 8 && Mathf.Abs(dy) == 8)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tower_b_green", 15);
                }

                // %40 çiftlik
                if (normalized < 0.4f)
                {
                    return new TileData(q, r, TerrainType.Farm, 0, MineType.None, false, "");
                }
                // %20 orman
                else if (normalized < 0.6f)
                {
                    return new TileData(q, r, TerrainType.Forest, 0, MineType.None, false, "");
                }
                // %10 tent (geçici yapı)
                else if (normalized < 0.7f)
                {
                    return new TileData(q, r, TerrainType.Grass, 0, MineType.None, true, "tent_green", 5);
                }

                return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
            }

            // Varsayılan - çim
            return new TileData(q, r, TerrainType.Grass, 0, MineType.None, false, "");
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
        /// Rıhtım/iskele rotasyonu hesapla - su merkezine baksın
        /// </summary>
        private static float CalculateDockRotation(int dxFromCenter, int dyFromCenter)
        {
            // Gölet/su merkezine göre pozisyon -> suya dönük rotasyon
            // dxFromCenter > 0: Sağda (suya bakması için sola dön = 270°)
            // dxFromCenter < 0: Solda (suya bakması için sağa dön = 90°)
            // dyFromCenter > 0: Aşağıda (suya bakması için yukarı = 0°)
            // dyFromCenter < 0: Yukarıda (suya bakması için aşağı = 180°)

            int absDx = Mathf.Abs(dxFromCenter);
            int absDy = Mathf.Abs(dyFromCenter);

            // Hangi kenar daha baskın?
            if (absDx > absDy)
            {
                // Yatay kenar baskın
                return dxFromCenter > 0 ? 270f : 90f;
            }
            else if (absDy > absDx)
            {
                // Dikey kenar baskın
                return dyFromCenter > 0 ? 0f : 180f;
            }
            else
            {
                // Köşe - diagonal, dx'e göre karar ver
                return dxFromCenter > 0 ? 270f : 90f;
            }
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
