using UnityEngine;

namespace EmpireWars.Core
{
    /// <summary>
    /// Hex boyutlari ve sabitleri
    /// Dokuman referansi: docs/01-game-design/map/MAP-SYSTEM.md
    /// </summary>
    public static class HexMetrics
    {
        // Hex boyutlari (KayKit asset'lerine gore ayarlanacak)
        public const float OuterRadius = 1f;
        public const float InnerRadius = OuterRadius * 0.866025404f; // sqrt(3)/2

        // Harita boyutlari (TEST: 50x50, PROD: 2000x2000)
        public const int MapWidth = 50;
        public const int MapHeight = 50;
        public const int TotalTiles = MapWidth * MapHeight; // 2.500 (test)

        // Chunk sistemi (performans icin)
        public const int ChunkSizeX = 20;
        public const int ChunkSizeZ = 20;
        public const int ChunksX = MapWidth / ChunkSizeX;   // 100 chunk
        public const int ChunksZ = MapHeight / ChunkSizeZ;  // 100 chunk

        // Bolge sinirlari (merkeze uzaklik) - TEST icin kucuk degerler
        public const int Zone1Radius = 10;    // Merkez (PROD: 200)
        public const int Zone2Radius = 20;    // Ileri (PROD: 500)
        public const int Zone3Radius = 30;    // Orta (PROD: 800)
        // Zone4 = 30+ (Dis)

        // Hex koselerinin pozisyonlari (flat-top hex)
        private static Vector3[] corners;

        public static Vector3[] Corners
        {
            get
            {
                if (corners == null)
                {
                    corners = new Vector3[7];
                    for (int i = 0; i < 7; i++)
                    {
                        float angle = 60f * i - 30f;
                        float rad = Mathf.Deg2Rad * angle;
                        corners[i] = new Vector3(
                            OuterRadius * Mathf.Cos(rad),
                            0f,
                            OuterRadius * Mathf.Sin(rad)
                        );
                    }
                }
                return corners;
            }
        }

        // Hex merkezinden koseye vektorler
        public static Vector3 GetCorner(int index)
        {
            return Corners[index % 6];
        }

        // Pointy-top hex icin alternatif (gerekirse)
        public static Vector3 GetPointyCorner(int index)
        {
            float angle = 60f * index;
            float rad = Mathf.Deg2Rad * angle;
            return new Vector3(
                OuterRadius * Mathf.Cos(rad),
                0f,
                OuterRadius * Mathf.Sin(rad)
            );
        }

        // Koordinatlar gecerli mi kontrol
        public static bool IsValidCoordinate(int q, int r)
        {
            // Harita sinirlari icinde mi?
            // Merkez (0,0) olmak uzere -1000 ile +1000 arasi
            int halfWidth = MapWidth / 2;
            int halfHeight = MapHeight / 2;

            return q >= -halfWidth && q < halfWidth &&
                   r >= -halfHeight && r < halfHeight;
        }

        public static bool IsValidCoordinate(HexCoordinates coords)
        {
            return IsValidCoordinate(coords.Q, coords.R);
        }

        // Chunk indeksi hesapla
        public static (int chunkX, int chunkZ) GetChunkIndex(HexCoordinates coords)
        {
            int halfWidth = MapWidth / 2;
            int halfHeight = MapHeight / 2;

            int normalizedQ = coords.Q + halfWidth;
            int normalizedR = coords.R + halfHeight;

            int chunkX = normalizedQ / ChunkSizeX;
            int chunkZ = normalizedR / ChunkSizeZ;

            return (chunkX, chunkZ);
        }

        // Chunk icindeki lokal indeks
        public static (int localX, int localZ) GetLocalIndex(HexCoordinates coords)
        {
            int halfWidth = MapWidth / 2;
            int halfHeight = MapHeight / 2;

            int normalizedQ = coords.Q + halfWidth;
            int normalizedR = coords.R + halfHeight;

            int localX = normalizedQ % ChunkSizeX;
            int localZ = normalizedR % ChunkSizeZ;

            return (localX, localZ);
        }
    }
}
