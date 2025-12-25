using UnityEngine;
using System;
using System.Collections.Generic;
using EmpireWars.Core;
using EmpireWars.WorldMap;
using EmpireWars.WorldMap.Tiles;
using EmpireWars.Alliance;

namespace EmpireWars.Network
{
    /// <summary>
    /// Network Sync Manager
    /// Oyun durumunu sunucu ile senkronize eder
    /// Event-based mimari - tüm değişiklikleri dinler
    /// </summary>
    public class NetworkSyncManager : MonoBehaviour
    {
        public static NetworkSyncManager Instance { get; private set; }

        [Header("Sync Ayarları")]
        [Tooltip("Sunucuya gönderme aralığı (saniye)")]
        [SerializeField] private float syncInterval = 0.05f; // 20 Hz

        [Tooltip("Maksimum queue boyutu")]
        [SerializeField] private int maxQueueSize = 100;

        [Tooltip("Bağlantı timeout (saniye)")]
        [SerializeField] private float connectionTimeout = 30f;

        [Header("Debug")]
        [SerializeField] private bool logSync = false;
        [SerializeField] private bool simulateOffline = true; // Test için offline mod

        // Message queue
        private Queue<ClientMessage> outgoingQueue;
        private Queue<ServerMessage> incomingQueue;

        // State
        private bool isConnected;
        private float lastSyncTime;
        private float lastHeartbeat;
        private string currentPlayerId;

        // Transport (interface - farklı implementasyonlar olabilir)
        private INetworkTransport transport;

        // Events
        public static event Action OnConnected;
        public static event Action OnDisconnected;
        public static event Action<string> OnError;
        public static event Action<ServerMessage> OnMessageReceived;

        // Properties
        public bool IsConnected => isConnected;
        public int PendingMessageCount => outgoingQueue?.Count ?? 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Initialize()
        {
            outgoingQueue = new Queue<ClientMessage>();
            incomingQueue = new Queue<ServerMessage>();

            // Offline mod için dummy transport
            if (simulateOffline)
            {
                transport = new OfflineTransport();
            }
        }

        private void OnEnable()
        {
            SubscribeToGameEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromGameEvents();
        }

        private void Update()
        {
            if (!isConnected && !simulateOffline) return;

            // Gelen mesajları işle
            ProcessIncomingMessages();

            // Belirli aralıklarla gönder
            if (Time.time - lastSyncTime >= syncInterval)
            {
                lastSyncTime = Time.time;
                SendBatchedMessages();
            }

            // Heartbeat
            if (Time.time - lastHeartbeat >= 5f)
            {
                lastHeartbeat = Time.time;
                SendHeartbeat();
            }
        }

        #region Event Subscriptions

        private void SubscribeToGameEvents()
        {
            // Tile seçim olayları
            HexTile.OnTileClicked += OnTileClicked;

            // Alliance olayları
            if (AllianceManager.Instance != null)
            {
                AllianceManager.Instance.OnAllianceColorChanged += OnAllianceColorChanged;
            }

            // Map olayları
            GameConfig.OnMapSizeChanged += OnMapConfigChanged;

            Debug.Log("NetworkSyncManager: Subscribed to game events");
        }

        private void UnsubscribeFromGameEvents()
        {
            HexTile.OnTileClicked -= OnTileClicked;

            if (AllianceManager.Instance != null)
            {
                AllianceManager.Instance.OnAllianceColorChanged -= OnAllianceColorChanged;
            }

            GameConfig.OnMapSizeChanged -= OnMapConfigChanged;
        }

        #endregion

        #region Event Handlers

        private void OnTileClicked(HexTile tile)
        {
            if (tile == null) return;

            var coords = tile.Coordinates;
            var action = new PlayerAction("select_tile", currentPlayerId, coords.Q, coords.R);
            EnqueueAction(action);

            if (logSync) Debug.Log($"NetworkSyncManager: Tile selected ({coords.Q}, {coords.R})");
        }

        private void OnAllianceColorChanged(int allianceId, Color newColor)
        {
            var data = new AllianceNetworkData
            {
                allianceId = allianceId,
                colorR = newColor.r,
                colorG = newColor.g,
                colorB = newColor.b
            };

            EnqueueMessage("alliance_update", data.ToJson());

            if (logSync) Debug.Log($"NetworkSyncManager: Alliance {allianceId} color changed");
        }

        private void OnMapConfigChanged(int width, int height)
        {
            // Map config değişikliği - genelde sadece sunucu gönderir
            if (logSync) Debug.Log($"NetworkSyncManager: Map config changed to {width}x{height}");
        }

        #endregion

        #region Message Queue

        /// <summary>
        /// Aksiyon kuyruğa ekle
        /// </summary>
        public void EnqueueAction(PlayerAction action)
        {
            var message = new ClientMessage("action", currentPlayerId, UnityEngine.JsonUtility.ToJson(action));
            EnqueueMessage(message);
        }

        /// <summary>
        /// Mesaj kuyruğa ekle
        /// </summary>
        public void EnqueueMessage(string type, string payload)
        {
            var message = new ClientMessage(type, currentPlayerId, payload);
            EnqueueMessage(message);
        }

        private void EnqueueMessage(ClientMessage message)
        {
            if (outgoingQueue.Count >= maxQueueSize)
            {
                // Eski mesajları at (overflow protection)
                outgoingQueue.Dequeue();
                Debug.LogWarning("NetworkSyncManager: Queue overflow, dropping old message");
            }

            outgoingQueue.Enqueue(message);
        }

        /// <summary>
        /// Kuyruktaki mesajları toplu gönder
        /// </summary>
        private void SendBatchedMessages()
        {
            if (outgoingQueue.Count == 0) return;

            // Batch oluştur
            List<ClientMessage> batch = new List<ClientMessage>();
            int batchSize = Mathf.Min(outgoingQueue.Count, 10); // Max 10 mesaj/batch

            for (int i = 0; i < batchSize; i++)
            {
                batch.Add(outgoingQueue.Dequeue());
            }

            // Transport üzerinden gönder
            if (transport != null)
            {
                transport.Send(batch);

                if (logSync) Debug.Log($"NetworkSyncManager: Sent {batch.Count} messages");
            }
        }

        /// <summary>
        /// Gelen mesajları işle
        /// </summary>
        private void ProcessIncomingMessages()
        {
            // Transport'tan gelen mesajları al
            if (transport != null)
            {
                var messages = transport.Receive();
                foreach (var msg in messages)
                {
                    incomingQueue.Enqueue(msg);
                }
            }

            // Kuyruktaki mesajları işle
            while (incomingQueue.Count > 0)
            {
                var message = incomingQueue.Dequeue();
                HandleServerMessage(message);
            }
        }

        private void HandleServerMessage(ServerMessage message)
        {
            OnMessageReceived?.Invoke(message);

            switch (message.messageType)
            {
                case "state_update":
                    HandleStateUpdate(message);
                    break;

                case "action_result":
                    HandleActionResult(message);
                    break;

                case "error":
                    HandleError(message);
                    break;

                case "pong":
                    // Heartbeat response
                    break;

                default:
                    if (logSync) Debug.Log($"NetworkSyncManager: Unknown message type: {message.messageType}");
                    break;
            }
        }

        private void HandleStateUpdate(ServerMessage message)
        {
            // Sunucudan gelen state güncellemesi
            // TODO: Parse payload and apply to game state
            if (logSync) Debug.Log("NetworkSyncManager: State update received");
        }

        private void HandleActionResult(ServerMessage message)
        {
            if (!message.success)
            {
                Debug.LogWarning($"NetworkSyncManager: Action failed - {message.errorCode}");
            }
        }

        private void HandleError(ServerMessage message)
        {
            Debug.LogError($"NetworkSyncManager: Server error - {message.errorCode}");
            OnError?.Invoke(message.errorCode);
        }

        private void SendHeartbeat()
        {
            if (transport != null && isConnected)
            {
                var ping = new ClientMessage("ping", currentPlayerId, "");
                transport.Send(new List<ClientMessage> { ping });
            }
        }

        #endregion

        #region Connection Management

        /// <summary>
        /// Sunucuya bağlan
        /// </summary>
        public void Connect(string serverUrl, string playerId)
        {
            currentPlayerId = playerId;

            if (simulateOffline)
            {
                isConnected = true;
                OnConnected?.Invoke();
                Debug.Log("NetworkSyncManager: Connected (offline mode)");
                return;
            }

            // TODO: Gerçek bağlantı implementasyonu
            // transport.Connect(serverUrl);
        }

        /// <summary>
        /// Bağlantıyı kes
        /// </summary>
        public void Disconnect()
        {
            isConnected = false;

            if (transport != null)
            {
                transport.Disconnect();
            }

            OnDisconnected?.Invoke();
            Debug.Log("NetworkSyncManager: Disconnected");
        }

        #endregion

        #region Public API

        /// <summary>
        /// Tile verisi iste
        /// </summary>
        public void RequestTileData(int q, int r)
        {
            var request = new { q, r };
            EnqueueMessage("tile_request", UnityEngine.JsonUtility.ToJson(request));
        }

        /// <summary>
        /// Chunk verisi iste
        /// </summary>
        public void RequestChunkData(int chunkX, int chunkY)
        {
            var request = new { chunkX, chunkY };
            EnqueueMessage("chunk_request", UnityEngine.JsonUtility.ToJson(request));
        }

        /// <summary>
        /// Kaynak toplama başlat
        /// </summary>
        public void StartGathering(int q, int r)
        {
            var action = new PlayerAction("gather", currentPlayerId, q, r);
            EnqueueAction(action);
        }

        /// <summary>
        /// Bina inşa et
        /// </summary>
        public void BuildStructure(int q, int r, string buildingType)
        {
            var action = new PlayerAction("build", currentPlayerId, q, r);
            action.parameters = $"{{\"buildingType\":\"{buildingType}\"}}";
            EnqueueAction(action);
        }

        /// <summary>
        /// Ordu hareket ettir
        /// </summary>
        public void MoveArmy(int fromQ, int fromR, int toQ, int toR)
        {
            var action = new PlayerAction("move_army", currentPlayerId, toQ, toR);
            action.parameters = $"{{\"fromQ\":{fromQ},\"fromR\":{fromR}}}";
            EnqueueAction(action);
        }

        #endregion
    }

    /// <summary>
    /// Network transport interface
    /// Farklı protokoller için soyutlama (WebSocket, TCP, etc.)
    /// </summary>
    public interface INetworkTransport
    {
        void Connect(string url);
        void Disconnect();
        void Send(List<ClientMessage> messages);
        List<ServerMessage> Receive();
        bool IsConnected { get; }
    }

    /// <summary>
    /// Offline test için dummy transport
    /// </summary>
    public class OfflineTransport : INetworkTransport
    {
        private Queue<ServerMessage> responses = new Queue<ServerMessage>();

        public bool IsConnected => true;

        public void Connect(string url) { }
        public void Disconnect() { }

        public void Send(List<ClientMessage> messages)
        {
            // Offline modda mesajları simüle et
            foreach (var msg in messages)
            {
                if (msg.messageType == "ping")
                {
                    responses.Enqueue(new ServerMessage
                    {
                        messageType = "pong",
                        success = true,
                        timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                    });
                }
                else if (msg.messageType == "action")
                {
                    responses.Enqueue(new ServerMessage
                    {
                        messageType = "action_result",
                        success = true,
                        timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                    });
                }
            }
        }

        public List<ServerMessage> Receive()
        {
            var result = new List<ServerMessage>();
            while (responses.Count > 0)
            {
                result.Add(responses.Dequeue());
            }
            return result;
        }
    }
}
