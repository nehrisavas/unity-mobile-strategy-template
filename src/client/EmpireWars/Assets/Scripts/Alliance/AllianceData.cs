using UnityEngine;
using System;
using System.Collections.Generic;

namespace EmpireWars.Alliance
{
    /// <summary>
    /// Tek bir ittifakın verileri
    /// </summary>
    [Serializable]
    public class AllianceInfo
    {
        [Tooltip("İttifak ID (benzersiz)")]
        public int allianceId;

        [Tooltip("İttifak adı")]
        public string allianceName;

        [Tooltip("İttifak rengi")]
        public Color allianceColor = Color.blue;

        [Tooltip("İttifak amblemi/bayrağı (opsiyonel)")]
        public Sprite allianceEmblem;

        [Tooltip("İttifak kurulma zamanı")]
        public DateTime createdAt;

        [Tooltip("Lider oyuncu ID")]
        public string leaderId;

        [Tooltip("Üye sayısı")]
        public int memberCount;

        public AllianceInfo()
        {
            allianceId = -1;
            allianceName = "Yeni İttifak";
            allianceColor = Color.blue;
            createdAt = DateTime.Now;
            memberCount = 1;
        }

        public AllianceInfo(int id, string name, Color color)
        {
            allianceId = id;
            allianceName = name;
            allianceColor = color;
            createdAt = DateTime.Now;
            memberCount = 1;
        }
    }

    /// <summary>
    /// Oyuncu ittifak bilgisi
    /// </summary>
    [Serializable]
    public class PlayerAllianceInfo
    {
        public string playerId;
        public int allianceId;
        public AllianceRole role;
        public DateTime joinedAt;

        public enum AllianceRole
        {
            Member = 0,
            Officer = 1,
            CoLeader = 2,
            Leader = 3
        }
    }

    /// <summary>
    /// İttifak yönetim sistemi
    /// Sınırsız ittifak ve renk desteği
    /// </summary>
    public class AllianceManager : MonoBehaviour
    {
        public static AllianceManager Instance { get; private set; }

        [Header("İttifak Verileri")]
        [SerializeField]
        private List<AllianceInfo> alliances = new List<AllianceInfo>();

        // Hızlı erişim için dictionary
        private Dictionary<int, AllianceInfo> allianceDict = new Dictionary<int, AllianceInfo>();

        // Oyuncu -> İttifak eşleşmesi
        private Dictionary<string, int> playerAllianceMap = new Dictionary<string, int>();

        // ID sayacı
        private int nextAllianceId = 1;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeDict();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeDict()
        {
            allianceDict.Clear();
            foreach (var alliance in alliances)
            {
                allianceDict[alliance.allianceId] = alliance;
                if (alliance.allianceId >= nextAllianceId)
                    nextAllianceId = alliance.allianceId + 1;
            }
        }

        /// <summary>
        /// Yeni ittifak oluştur
        /// </summary>
        public AllianceInfo CreateAlliance(string name, Color color, string leaderId)
        {
            AllianceInfo newAlliance = new AllianceInfo(nextAllianceId++, name, color);
            newAlliance.leaderId = leaderId;

            alliances.Add(newAlliance);
            allianceDict[newAlliance.allianceId] = newAlliance;

            // Lideri ittifaka ekle
            playerAllianceMap[leaderId] = newAlliance.allianceId;

            Debug.Log($"Yeni ittifak oluşturuldu: {name} (ID: {newAlliance.allianceId})");
            return newAlliance;
        }

        /// <summary>
        /// İttifak ID'sine göre ittifak bilgisi al
        /// </summary>
        public AllianceInfo GetAlliance(int allianceId)
        {
            if (allianceDict.TryGetValue(allianceId, out AllianceInfo alliance))
                return alliance;
            return null;
        }

        /// <summary>
        /// İttifak rengini al
        /// </summary>
        public Color GetAllianceColor(int allianceId)
        {
            var alliance = GetAlliance(allianceId);
            return alliance != null ? alliance.allianceColor : Color.gray;
        }

        /// <summary>
        /// Oyuncunun ittifak ID'sini al
        /// </summary>
        public int GetPlayerAllianceId(string playerId)
        {
            if (playerAllianceMap.TryGetValue(playerId, out int allianceId))
                return allianceId;
            return -1; // İttifaksız
        }

        /// <summary>
        /// Oyuncunun ittifak rengini al
        /// </summary>
        public Color GetPlayerAllianceColor(string playerId)
        {
            int allianceId = GetPlayerAllianceId(playerId);
            if (allianceId >= 0)
                return GetAllianceColor(allianceId);
            return Color.gray; // İttifaksız oyuncular gri
        }

        /// <summary>
        /// Oyuncuyu ittifaka ekle
        /// </summary>
        public bool JoinAlliance(string playerId, int allianceId)
        {
            if (!allianceDict.ContainsKey(allianceId))
                return false;

            // Önceki ittifaktan çık
            LeaveAlliance(playerId);

            playerAllianceMap[playerId] = allianceId;
            allianceDict[allianceId].memberCount++;

            return true;
        }

        /// <summary>
        /// Oyuncuyu ittifaktan çıkar
        /// </summary>
        public void LeaveAlliance(string playerId)
        {
            if (playerAllianceMap.TryGetValue(playerId, out int oldAllianceId))
            {
                playerAllianceMap.Remove(playerId);
                if (allianceDict.ContainsKey(oldAllianceId))
                    allianceDict[oldAllianceId].memberCount--;
            }
        }

        /// <summary>
        /// İttifak rengini değiştir
        /// </summary>
        public void SetAllianceColor(int allianceId, Color newColor)
        {
            if (allianceDict.TryGetValue(allianceId, out AllianceInfo alliance))
            {
                alliance.allianceColor = newColor;
                OnAllianceColorChanged?.Invoke(allianceId, newColor);
            }
        }

        /// <summary>
        /// Tüm ittifakları al
        /// </summary>
        public List<AllianceInfo> GetAllAlliances()
        {
            return new List<AllianceInfo>(alliances);
        }

        /// <summary>
        /// İttifak rengi değiştiğinde tetiklenir
        /// </summary>
        public event Action<int, Color> OnAllianceColorChanged;

        /// <summary>
        /// Demo için örnek ittifaklar oluştur
        /// </summary>
        [ContextMenu("Create Demo Alliances")]
        public void CreateDemoAlliances()
        {
            CreateAlliance("Mavi Ordu", new Color(0.2f, 0.4f, 0.9f), "player_1");
            CreateAlliance("Kızıl İmparatorluk", new Color(0.9f, 0.2f, 0.2f), "player_2");
            CreateAlliance("Yeşil Federasyon", new Color(0.2f, 0.8f, 0.3f), "player_3");
            CreateAlliance("Altın Hanlık", new Color(0.9f, 0.75f, 0.2f), "player_4");
            CreateAlliance("Mor Konfederasyon", new Color(0.6f, 0.2f, 0.8f), "player_5");
            CreateAlliance("Turuncu Birlik", new Color(0.95f, 0.5f, 0.1f), "player_6");
        }
    }
}
