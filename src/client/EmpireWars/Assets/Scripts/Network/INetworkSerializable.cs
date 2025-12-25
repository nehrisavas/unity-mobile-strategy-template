using System;

namespace EmpireWars.Network
{
    /// <summary>
    /// Network üzerinden serileştirilebilir objeler için interface
    /// </summary>
    public interface INetworkSerializable
    {
        /// <summary>
        /// Objeyi network verisi olarak döndür
        /// </summary>
        NetworkData ToNetworkData();

        /// <summary>
        /// Network verisinden objeyi güncelle
        /// </summary>
        void FromNetworkData(NetworkData data);

        /// <summary>
        /// Objenin benzersiz kimliği
        /// </summary>
        string NetworkId { get; }

        /// <summary>
        /// Son güncelleme zaman damgası
        /// </summary>
        long LastUpdateTimestamp { get; }
    }

    /// <summary>
    /// Network veri tabanı - JSON serileştirme için
    /// </summary>
    [Serializable]
    public class NetworkData
    {
        public string type;           // Veri tipi (tile, player, resource, etc.)
        public string id;             // Benzersiz ID
        public long timestamp;        // Unix timestamp (ms)
        public string payload;        // JSON payload

        public NetworkData() { }

        public NetworkData(string type, string id, string payload)
        {
            this.type = type;
            this.id = id;
            this.timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            this.payload = payload;
        }
    }

    /// <summary>
    /// Tile network verisi
    /// </summary>
    [Serializable]
    public class TileNetworkData
    {
        public int q;
        public int r;
        public int terrainType;
        public int ownerPlayerId;
        public int ownerAllianceId;
        public bool isExplored;
        public bool hasBuilding;
        public string buildingType;
        public int buildingLevel;
        public int mineLevel;
        public int mineType;

        public string ToJson()
        {
            return UnityEngine.JsonUtility.ToJson(this);
        }

        public static TileNetworkData FromJson(string json)
        {
            return UnityEngine.JsonUtility.FromJson<TileNetworkData>(json);
        }
    }

    /// <summary>
    /// Player network verisi
    /// </summary>
    [Serializable]
    public class PlayerNetworkData
    {
        public string playerId;
        public string playerName;
        public int allianceId;
        public int level;
        public long power;
        public int cityQ;  // Şehir koordinatı
        public int cityR;

        public string ToJson()
        {
            return UnityEngine.JsonUtility.ToJson(this);
        }

        public static PlayerNetworkData FromJson(string json)
        {
            return UnityEngine.JsonUtility.FromJson<PlayerNetworkData>(json);
        }
    }

    /// <summary>
    /// Resource node network verisi
    /// </summary>
    [Serializable]
    public class ResourceNetworkData
    {
        public int q;
        public int r;
        public int resourceType;
        public int level;
        public long currentAmount;
        public long maxAmount;
        public string ownerPlayerId;
        public int ownerAllianceId;
        public bool isProtected;
        public long protectionEndTime;

        public string ToJson()
        {
            return UnityEngine.JsonUtility.ToJson(this);
        }

        public static ResourceNetworkData FromJson(string json)
        {
            return UnityEngine.JsonUtility.FromJson<ResourceNetworkData>(json);
        }
    }

    /// <summary>
    /// Alliance network verisi
    /// </summary>
    [Serializable]
    public class AllianceNetworkData
    {
        public int allianceId;
        public string name;
        public float colorR;
        public float colorG;
        public float colorB;
        public string leaderId;
        public int memberCount;
        public long power;

        public string ToJson()
        {
            return UnityEngine.JsonUtility.ToJson(this);
        }

        public static AllianceNetworkData FromJson(string json)
        {
            return UnityEngine.JsonUtility.FromJson<AllianceNetworkData>(json);
        }
    }

    /// <summary>
    /// Client -> Server mesajları
    /// </summary>
    [Serializable]
    public class ClientMessage
    {
        public string messageType;  // action, ping, sync_request, etc.
        public string playerId;
        public long timestamp;
        public string payload;

        public ClientMessage() { }

        public ClientMessage(string type, string playerId, string payload)
        {
            this.messageType = type;
            this.playerId = playerId;
            this.timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            this.payload = payload;
        }
    }

    /// <summary>
    /// Server -> Client mesajları
    /// </summary>
    [Serializable]
    public class ServerMessage
    {
        public string messageType;  // state_update, action_result, error, etc.
        public long timestamp;
        public bool success;
        public string errorCode;
        public string payload;
    }

    /// <summary>
    /// Oyuncu aksiyonları
    /// </summary>
    [Serializable]
    public class PlayerAction
    {
        public string actionType;   // move, gather, build, attack, etc.
        public string playerId;
        public int targetQ;
        public int targetR;
        public string targetId;
        public string parameters;   // Ek parametreler (JSON)
        public long timestamp;

        public PlayerAction() { }

        public PlayerAction(string type, string playerId, int q, int r)
        {
            this.actionType = type;
            this.playerId = playerId;
            this.targetQ = q;
            this.targetR = r;
            this.timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}
