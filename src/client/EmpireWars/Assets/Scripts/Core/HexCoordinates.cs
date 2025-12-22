using UnityEngine;
using System;

namespace EmpireWars.Core
{
    /// <summary>
    /// Hexagonal koordinat sistemi (Axial koordinatlar)
    /// Dokuman referansi: docs/01-game-design/map/MAP-SYSTEM.md
    /// </summary>
    [Serializable]
    public struct HexCoordinates : IEquatable<HexCoordinates>
    {
        [SerializeField] private int q;
        [SerializeField] private int r;

        public int Q => q;
        public int R => r;

        // Cube koordinatlari (q + r + s = 0)
        public int S => -q - r;

        public HexCoordinates(int q, int r)
        {
            this.q = q;
            this.r = r;
        }

        // World pozisyonundan hex koordinat olustur
        public static HexCoordinates FromPosition(Vector3 position)
        {
            float q = (position.x * (2f / 3f)) / HexMetrics.OuterRadius;
            float r = ((-position.x / 3f) + (Mathf.Sqrt(3f) / 3f) * position.z) / HexMetrics.OuterRadius;

            int qInt = Mathf.RoundToInt(q);
            int rInt = Mathf.RoundToInt(r);
            int sInt = Mathf.RoundToInt(-q - r);

            // Yuvarlama hatalarini duzelt
            float qDiff = Mathf.Abs(q - qInt);
            float rDiff = Mathf.Abs(r - rInt);
            float sDiff = Mathf.Abs(-q - r - sInt);

            if (qDiff > rDiff && qDiff > sDiff)
            {
                qInt = -rInt - sInt;
            }
            else if (rDiff > sDiff)
            {
                rInt = -qInt - sInt;
            }

            return new HexCoordinates(qInt, rInt);
        }

        // Hex koordinattan world pozisyonuna
        public Vector3 ToWorldPosition()
        {
            float x = (q + r * 0.5f - r / 2) * (HexMetrics.InnerRadius * 2f);
            float z = r * (HexMetrics.OuterRadius * 1.5f);
            // Z-fighting onlemek icin kucuk Y farki (koordinata bagli)
            float y = ((q * 7 + r * 13) % 100) * 0.0001f;
            return new Vector3(x, y, z);
        }

        // Iki hex arasi mesafe
        public int DistanceTo(HexCoordinates other)
        {
            return (Mathf.Abs(q - other.q)
                  + Mathf.Abs(q + r - other.q - other.r)
                  + Mathf.Abs(r - other.r)) / 2;
        }

        // Merkeze (0,0) mesafe
        public int DistanceToCenter()
        {
            return DistanceTo(new HexCoordinates(0, 0));
        }

        // 6 komsu hex koordinatlari
        public static readonly HexCoordinates[] Directions = new HexCoordinates[]
        {
            new HexCoordinates(1, -1),  // Kuzey-Dogu
            new HexCoordinates(1, 0),   // Dogu
            new HexCoordinates(0, 1),   // Guney-Dogu
            new HexCoordinates(-1, 1),  // Guney-Bati
            new HexCoordinates(-1, 0),  // Bati
            new HexCoordinates(0, -1)   // Kuzey-Bati
        };

        public HexCoordinates GetNeighbor(int direction)
        {
            direction = ((direction % 6) + 6) % 6; // 0-5 arasi normalize et
            return this + Directions[direction];
        }

        public HexCoordinates[] GetAllNeighbors()
        {
            HexCoordinates[] neighbors = new HexCoordinates[6];
            for (int i = 0; i < 6; i++)
            {
                neighbors[i] = GetNeighbor(i);
            }
            return neighbors;
        }

        // Bolge belirleme (1-4)
        public int GetZone()
        {
            int distance = DistanceToCenter();

            if (distance <= 200) return 1;  // Merkez
            if (distance <= 500) return 2;  // Ileri
            if (distance <= 800) return 3;  // Orta
            return 4;                        // Dis
        }

        // Operator overloads
        public static HexCoordinates operator +(HexCoordinates a, HexCoordinates b)
        {
            return new HexCoordinates(a.q + b.q, a.r + b.r);
        }

        public static HexCoordinates operator -(HexCoordinates a, HexCoordinates b)
        {
            return new HexCoordinates(a.q - b.q, a.r - b.r);
        }

        public static HexCoordinates operator *(HexCoordinates a, int scale)
        {
            return new HexCoordinates(a.q * scale, a.r * scale);
        }

        public static bool operator ==(HexCoordinates a, HexCoordinates b)
        {
            return a.q == b.q && a.r == b.r;
        }

        public static bool operator !=(HexCoordinates a, HexCoordinates b)
        {
            return !(a == b);
        }

        // IEquatable implementation
        public bool Equals(HexCoordinates other)
        {
            return q == other.q && r == other.r;
        }

        public override bool Equals(object obj)
        {
            return obj is HexCoordinates other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(q, r);
        }

        public override string ToString()
        {
            return $"({q}, {r})";
        }

        // Unique ID for dictionary keys
        public long ToUniqueId()
        {
            // Q ve R'yi tek bir long'a encode et
            // Q: 32 bit, R: 32 bit
            return ((long)(q + 1000000) << 32) | (uint)(r + 1000000);
        }

        public static HexCoordinates FromUniqueId(long id)
        {
            int q = (int)(id >> 32) - 1000000;
            int r = (int)(id & 0xFFFFFFFF) - 1000000;
            return new HexCoordinates(q, r);
        }
    }
}
