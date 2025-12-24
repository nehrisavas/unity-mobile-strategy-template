namespace EmpireWars.Data
{
    /// <summary>
    /// Arazi tipleri
    /// Dokuman referansi: docs/01-game-design/map/MAP-SYSTEM.md - Bolum 1.3.6
    /// </summary>
    public enum TerrainType : byte
    {
        // Temel araziler
        Grass = 0,          // Duz ova - 1x hareket
        Forest = 1,         // Orman - 0.7x hareket, +20% gizlenme
        Hill = 2,           // Tepe - 0.5x hareket, +10% savunma
        Mountain = 3,       // Dag - GECILMEZ
        Water = 4,          // Su/Deniz - GECILMEZ
        Desert = 5,         // Col - 0.8x hareket, +20% yiyecek tuketimi
        Snow = 6,           // Kar - 0.6x hareket, +30% yiyecek tuketimi
        Swamp = 7,          // Bataklik - 0.4x hareket, hastalik riski

        // Ozel araziler
        Road = 10,          // Yol - 1.5x hareket
        Bridge = 11,        // Kopru - Su uzerinden gecis
        Coast = 12,         // Kiyi - Su kenari
        River = 13,         // Nehir - Gecilmez (kopru ile gecilebilir)
        GrassSlopedHigh = 14, // Yuksek egimli cimen
        GrassSlopedLow = 15,  // Alcak egimli cimen

        // Kaynak arazileri
        Farm = 20,          // Ciftlik alani
        Mine = 21,          // Maden alani
        Quarry = 22,        // Tas ocagi alani
        GoldMine = 23,      // Altin madeni alani
        GemMine = 24        // Mucevher madeni alani
    }

    /// <summary>
    /// Arazi ozellikleri
    /// </summary>
    public static class TerrainProperties
    {
        public static float GetMovementModifier(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Grass => 1.0f,
                TerrainType.Forest => 0.7f,
                TerrainType.Hill => 0.5f,
                TerrainType.Mountain => 0f,     // Gecilmez
                TerrainType.Water => 0f,        // Gecilmez
                TerrainType.Desert => 0.8f,
                TerrainType.Snow => 0.6f,
                TerrainType.Swamp => 0.4f,
                TerrainType.Road => 1.5f,
                TerrainType.Bridge => 1.0f,
                TerrainType.Coast => 0.9f,
                TerrainType.River => 0f,            // Gecilmez
                TerrainType.GrassSlopedHigh => 0.7f,
                TerrainType.GrassSlopedLow => 0.8f,
                _ => 1.0f
            };
        }

        public static bool IsPassable(TerrainType terrain)
        {
            return terrain != TerrainType.Mountain && terrain != TerrainType.Water && terrain != TerrainType.River;
        }

        public static float GetDefenseBonus(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Hill => 0.1f,       // +10%
                TerrainType.Forest => 0.05f,    // +5%
                TerrainType.Mountain => 0f,     // Ulasilamaz
                _ => 0f
            };
        }

        public static float GetStealthBonus(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Forest => 0.2f,     // +20%
                TerrainType.Swamp => 0.1f,      // +10%
                _ => 0f
            };
        }

        public static float GetFoodConsumptionModifier(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Desert => 1.2f,     // +20%
                TerrainType.Snow => 1.3f,       // +30%
                _ => 1.0f
            };
        }

        public static string GetDisplayName(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Grass => "Ova",
                TerrainType.Forest => "Orman",
                TerrainType.Hill => "Tepe",
                TerrainType.Mountain => "Dağ",
                TerrainType.Water => "Su",
                TerrainType.Desert => "Çöl",
                TerrainType.Snow => "Kar",
                TerrainType.Swamp => "Bataklık",
                TerrainType.Road => "Yol",
                TerrainType.Bridge => "Köprü",
                TerrainType.Coast => "Kıyı",
                TerrainType.River => "Nehir",
                TerrainType.GrassSlopedHigh => "Eğimli Arazi (Yüksek)",
                TerrainType.GrassSlopedLow => "Eğimli Arazi (Alçak)",
                TerrainType.Farm => "Çiftlik",
                TerrainType.Mine => "Maden",
                TerrainType.Quarry => "Taş Ocağı",
                TerrainType.GoldMine => "Altın Madeni",
                TerrainType.GemMine => "Mücevher Madeni",
                _ => "Bilinmeyen"
            };
        }

        /// <summary>
        /// Arazi avantajlarını açıklayan metin döndür
        /// </summary>
        public static string GetAdvantageDescription(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Grass => "Standart arazi. Hareket ve savaş için bonus yok.",
                TerrainType.Forest => "Gizlenme +20%, Savunma +5%, Hareket -30%. Pusu için ideal.",
                TerrainType.Hill => "Savunma +10%, Hareket -50%. Görüş menzili +2. Stratejik üstünlük.",
                TerrainType.Mountain => "Geçilemez. Doğal sınır ve savunma bariyeri.",
                TerrainType.Water => "Geçilemez. Gemi ile ulaşılabilir (ileride).",
                TerrainType.Desert => "Yiyecek tüketimi +20%, Hareket -20%. Gizlenme zor.",
                TerrainType.Snow => "Yiyecek tüketimi +30%, Hareket -40%. Soğuk hasarı riski.",
                TerrainType.Swamp => "Gizlenme +10%, Hareket -60%. Hastalık riski %5.",
                TerrainType.Road => "Hareket +50%. Ticaret ve lojistik için ideal.",
                TerrainType.Bridge => "Su üzerinden geçiş sağlar. Stratejik öneme sahip.",
                TerrainType.Coast => "Balıkçılık +20%, Hareket -10%. Deniz ticareti yapılabilir.",
                TerrainType.River => "Geçilemez. Köprü veya gemi gerektirir.",
                TerrainType.Farm => "Yiyecek üretimi +50%. Tarım için ideal.",
                TerrainType.Mine => "Demir üretimi. Silah/zırh yapımı için gerekli.",
                TerrainType.Quarry => "Taş üretimi. İnşaat için gerekli.",
                TerrainType.GoldMine => "Altın üretimi. Ekonomi için kritik.",
                TerrainType.GemMine => "Mücevher üretimi. Lüks ticaret için değerli.",
                _ => "Bilinmeyen arazi tipi."
            };
        }

        /// <summary>
        /// Arazi saldırı bonusu
        /// </summary>
        public static float GetAttackBonus(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Hill => 0.15f,      // +15% - Yükseklik avantajı
                TerrainType.Grass => 0.05f,     // +5% - Açık alan
                TerrainType.Forest => -0.05f,   // -5% - Görüş engeli
                TerrainType.Swamp => -0.15f,    // -15% - Zor zemin
                TerrainType.Snow => -0.10f,     // -10% - Soğuk
                TerrainType.Desert => -0.05f,   // -5% - Sıcak
                _ => 0f
            };
        }

        /// <summary>
        /// Görüş menzili bonusu (tile cinsinden)
        /// </summary>
        public static int GetVisionBonus(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Hill => 2,          // +2 tile görüş
                TerrainType.Mountain => 0,      // Geçilemez
                TerrainType.Forest => -1,       // -1 tile (ağaçlar engeller)
                TerrainType.Swamp => -1,        // -1 tile (sis)
                _ => 0
            };
        }

        /// <summary>
        /// Kaynak üretim bonusu
        /// </summary>
        public static float GetResourceBonus(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Farm => 0.5f,       // +50% yiyecek
                TerrainType.Forest => 0.3f,     // +30% kereste
                TerrainType.Mine => 1.0f,       // +100% demir
                TerrainType.Quarry => 0.8f,     // +80% taş
                TerrainType.GoldMine => 1.5f,   // +150% altın
                TerrainType.GemMine => 2.0f,    // +200% mücevher
                TerrainType.Coast => 0.2f,      // +20% balık
                _ => 0f
            };
        }

        /// <summary>
        /// Arazi rengi (minimap için)
        /// </summary>
        public static UnityEngine.Color GetTerrainColor(TerrainType terrain)
        {
            return terrain switch
            {
                TerrainType.Grass => new UnityEngine.Color(0.4f, 0.7f, 0.3f),      // Yeşil
                TerrainType.Forest => new UnityEngine.Color(0.2f, 0.5f, 0.2f),     // Koyu yeşil
                TerrainType.Hill => new UnityEngine.Color(0.6f, 0.5f, 0.4f),       // Kahverengi
                TerrainType.Mountain => new UnityEngine.Color(0.5f, 0.5f, 0.5f),   // Gri
                TerrainType.Water => new UnityEngine.Color(0.2f, 0.4f, 0.8f),      // Mavi
                TerrainType.Desert => new UnityEngine.Color(0.9f, 0.85f, 0.6f),    // Sarı
                TerrainType.Snow => new UnityEngine.Color(0.95f, 0.95f, 1f),       // Beyaz
                TerrainType.Swamp => new UnityEngine.Color(0.4f, 0.5f, 0.3f),      // Koyu yeşil-kahve
                TerrainType.Road => new UnityEngine.Color(0.7f, 0.6f, 0.5f),       // Açık kahve
                TerrainType.Coast => new UnityEngine.Color(0.8f, 0.85f, 0.6f),     // Bej
                TerrainType.Farm => new UnityEngine.Color(0.8f, 0.75f, 0.4f),      // Sarı-yeşil
                TerrainType.Mine => new UnityEngine.Color(0.5f, 0.5f, 0.6f),       // Gri-mavi
                TerrainType.GoldMine => new UnityEngine.Color(1f, 0.84f, 0f),      // Altın
                TerrainType.GemMine => new UnityEngine.Color(0.8f, 0.2f, 0.8f),    // Mor
                _ => UnityEngine.Color.white
            };
        }
    }
}
