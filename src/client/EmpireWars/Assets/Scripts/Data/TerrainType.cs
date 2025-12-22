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
                _ => 1.0f
            };
        }

        public static bool IsPassable(TerrainType terrain)
        {
            return terrain != TerrainType.Mountain && terrain != TerrainType.Water;
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
                TerrainType.Mountain => "Dag",
                TerrainType.Water => "Su",
                TerrainType.Desert => "Col",
                TerrainType.Snow => "Kar",
                TerrainType.Swamp => "Bataklik",
                TerrainType.Road => "Yol",
                TerrainType.Bridge => "Kopru",
                TerrainType.Coast => "Kiyi",
                TerrainType.Farm => "Ciftlik",
                TerrainType.Mine => "Maden",
                TerrainType.Quarry => "Tas Ocagi",
                TerrainType.GoldMine => "Altin Madeni",
                TerrainType.GemMine => "Mucevher Madeni",
                _ => "Bilinmeyen"
            };
        }
    }
}
