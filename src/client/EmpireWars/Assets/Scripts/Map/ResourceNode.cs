using UnityEngine;
using EmpireWars.Core;
using EmpireWars.Data;

namespace EmpireWars.Map
{
    /// <summary>
    /// Kaynak noktasi sistemi
    /// Haritada toplanabilir kaynaklar (ciftlik, maden, ocak vb.)
    /// Dokuman referansi: docs/01-game-design/map/MAP-SYSTEM.md
    /// </summary>
    public class ResourceNode : MonoBehaviour
    {
        [Header("Kaynak Bilgileri")]
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private int level = 1;

        [Header("Kapasite")]
        [SerializeField] private long maxAmount = 50000;
        [SerializeField] private long currentAmount = 50000;
        [SerializeField] private float regenerationRate = 100f; // Dakikada

        [Header("Toplama")]
        [SerializeField] private float gatherRate = 1000f; // Saatte
        [SerializeField] private int maxGatherers = 1;
        [SerializeField] private int currentGatherers = 0;

        [Header("Sahiplik")]
        [SerializeField] private long ownerPlayerId = 0;
        [SerializeField] private long ownerAllianceId = 0;

        [Header("Koruma")]
        [SerializeField] private bool isProtected = false;
        [SerializeField] private float protectionEndTime = 0f;

        [Header("Referanslar")]
        [SerializeField] private HexCell parentCell;
        [SerializeField] private GameObject levelIndicator;
        [SerializeField] private GameObject gatheringEffect;

        // Events
        public System.Action<ResourceNode> OnResourceDepleted;
        public System.Action<ResourceNode> OnResourceReplenished;
        public System.Action<ResourceNode, long> OnGatheringStarted;
        public System.Action<ResourceNode, long, long> OnResourceGathered;

        #region Properties

        public ResourceType Type => resourceType;
        public int Level => level;
        public long MaxAmount => maxAmount;
        public long CurrentAmount => currentAmount;
        public float FillPercentage => maxAmount > 0 ? (float)currentAmount / maxAmount : 0f;
        public bool IsEmpty => currentAmount <= 0;
        public bool IsFull => currentAmount >= maxAmount;
        public bool CanGather => currentAmount > 0 && currentGatherers < maxGatherers;
        public bool IsOccupied => currentGatherers > 0;
        public long OwnerPlayerId => ownerPlayerId;
        public long OwnerAllianceId => ownerAllianceId;
        public bool IsProtected => isProtected && Time.time < protectionEndTime;
        public HexCell Cell => parentCell;

        #endregion

        #region Initialization

        public void Initialize(HexCell cell, ResourceType type, int nodeLevel = 1)
        {
            parentCell = cell;
            resourceType = type;
            level = Mathf.Clamp(nodeLevel, 1, 10);

            // Seviyeye gore kapasite ve oran ayarla
            SetupByLevel();

            // Kaynak dolu baslat
            currentAmount = maxAmount;

            gameObject.name = $"Resource_{type}_{cell.Coordinates}";
        }

        private void SetupByLevel()
        {
            // Her seviye kaynak miktarini ve toplama hizini arttirir
            float levelMultiplier = 1f + (level - 1) * 0.25f;

            // Kaynak tipine gore temel degerler
            var baseStats = GetBaseStats(resourceType);

            maxAmount = (long)(baseStats.baseAmount * levelMultiplier);
            gatherRate = baseStats.baseGatherRate * levelMultiplier;
            regenerationRate = baseStats.baseRegenRate * levelMultiplier;
            maxGatherers = baseStats.baseMaxGatherers + (level / 3);

            // Bolge bonusu (merkez bolge daha verimli)
            if (parentCell != null)
            {
                int zone = parentCell.Zone;
                float zoneMultiplier = zone switch
                {
                    1 => 2.0f,  // Merkez - 2x
                    2 => 1.5f,  // Ileri - 1.5x
                    3 => 1.2f,  // Orta - 1.2x
                    _ => 1.0f   // Dis - 1x
                };

                maxAmount = (long)(maxAmount * zoneMultiplier);
                gatherRate *= zoneMultiplier;
            }
        }

        private (long baseAmount, float baseGatherRate, float baseRegenRate, int baseMaxGatherers) GetBaseStats(ResourceType type)
        {
            return type switch
            {
                ResourceType.Food => (50000, 5000f, 500f, 3),
                ResourceType.Wood => (40000, 4000f, 400f, 3),
                ResourceType.Stone => (30000, 3000f, 300f, 2),
                ResourceType.Iron => (20000, 2000f, 200f, 2),
                ResourceType.Gold => (10000, 1000f, 100f, 1),
                ResourceType.Gem => (5000, 500f, 50f, 1),
                _ => (25000, 2500f, 250f, 2)
            };
        }

        #endregion

        #region Unity Lifecycle

        private void Update()
        {
            // Kaynak yenilenmesi
            if (currentAmount < maxAmount && currentGatherers == 0)
            {
                RegenerateResource();
            }

            // Koruma suresi kontrolu
            if (isProtected && Time.time >= protectionEndTime)
            {
                isProtected = false;
            }
        }

        #endregion

        #region Regeneration

        private void RegenerateResource()
        {
            float regenAmount = regenerationRate * (Time.deltaTime / 60f);
            currentAmount = (long)Mathf.Min(currentAmount + regenAmount, maxAmount);

            if (IsFull)
            {
                OnResourceReplenished?.Invoke(this);
            }
        }

        public void ReplenishFully()
        {
            currentAmount = maxAmount;
            OnResourceReplenished?.Invoke(this);
        }

        #endregion

        #region Gathering

        public bool StartGathering(long playerId, long allianceId = 0)
        {
            if (!CanGather)
            {
                Debug.LogWarning($"Kaynak toplanamaz: {resourceType} at {parentCell?.Coordinates}");
                return false;
            }

            currentGatherers++;
            ownerPlayerId = playerId;
            ownerAllianceId = allianceId;

            // Toplama efekti
            if (gatheringEffect != null)
            {
                gatheringEffect.SetActive(true);
            }

            OnGatheringStarted?.Invoke(this, playerId);
            Debug.Log($"Kaynak toplama basladi: {resourceType} by Player {playerId}");

            return true;
        }

        public long GatherResource(float hours)
        {
            if (currentAmount <= 0)
            {
                return 0;
            }

            long gathered = (long)(gatherRate * hours);
            gathered = System.Math.Min(gathered, currentAmount);

            currentAmount -= gathered;

            if (currentAmount <= 0)
            {
                currentAmount = 0;
                OnResourceDepleted?.Invoke(this);
            }

            OnResourceGathered?.Invoke(this, ownerPlayerId, gathered);

            return gathered;
        }

        public void StopGathering()
        {
            currentGatherers = Mathf.Max(0, currentGatherers - 1);

            if (currentGatherers == 0)
            {
                ownerPlayerId = 0;
                ownerAllianceId = 0;

                if (gatheringEffect != null)
                {
                    gatheringEffect.SetActive(false);
                }
            }
        }

        public void ForceStopAllGathering()
        {
            currentGatherers = 0;
            ownerPlayerId = 0;
            ownerAllianceId = 0;

            if (gatheringEffect != null)
            {
                gatheringEffect.SetActive(false);
            }
        }

        #endregion

        #region Protection

        public void SetProtection(float durationSeconds)
        {
            isProtected = true;
            protectionEndTime = Time.time + durationSeconds;
        }

        public void RemoveProtection()
        {
            isProtected = false;
            protectionEndTime = 0f;
        }

        #endregion

        #region Utility

        public float GetEstimatedGatherTime()
        {
            if (gatherRate <= 0) return float.MaxValue;
            return currentAmount / gatherRate;
        }

        public ResourceNodeData ToData()
        {
            return new ResourceNodeData
            {
                ResourceType = resourceType,
                Level = level,
                CurrentAmount = currentAmount,
                MaxAmount = maxAmount,
                GatherRate = gatherRate,
                OwnerPlayerId = ownerPlayerId,
                OwnerAllianceId = ownerAllianceId,
                IsProtected = isProtected,
                ProtectionEndTime = protectionEndTime,
                CoordinatesQ = parentCell?.Coordinates.Q ?? 0,
                CoordinatesR = parentCell?.Coordinates.R ?? 0
            };
        }

        public void FromData(ResourceNodeData data)
        {
            resourceType = data.ResourceType;
            level = data.Level;
            currentAmount = data.CurrentAmount;
            maxAmount = data.MaxAmount;
            gatherRate = data.GatherRate;
            ownerPlayerId = data.OwnerPlayerId;
            ownerAllianceId = data.OwnerAllianceId;
            isProtected = data.IsProtected;
            protectionEndTime = data.ProtectionEndTime;
        }

        public override string ToString()
        {
            return $"ResourceNode[{resourceType} Lv{level}] {currentAmount:N0}/{maxAmount:N0} at {parentCell?.Coordinates}";
        }

        #endregion

        #region Visual Updates

        public void UpdateVisual()
        {
            // Seviye gostergesi
            if (levelIndicator != null)
            {
                var text = levelIndicator.GetComponent<TMPro.TextMeshPro>();
                if (text != null)
                {
                    text.text = $"Lv.{level}";
                }
            }

            // Renk degisimi (kaynak miktarina gore)
            UpdateResourceColor();
        }

        private void UpdateResourceColor()
        {
            var renderer = GetComponentInChildren<Renderer>();
            if (renderer == null) return;

            Color baseColor = GetResourceColor(resourceType);
            float fillRatio = FillPercentage;

            // Dusuk kaynak = soluk renk
            Color finalColor = Color.Lerp(Color.gray, baseColor, fillRatio);
            renderer.material.color = finalColor;
        }

        private Color GetResourceColor(ResourceType type)
        {
            return type switch
            {
                ResourceType.Food => new Color(0.2f, 0.8f, 0.2f),   // Yesil
                ResourceType.Wood => new Color(0.6f, 0.4f, 0.2f),   // Kahve
                ResourceType.Stone => new Color(0.5f, 0.5f, 0.5f),  // Gri
                ResourceType.Iron => new Color(0.3f, 0.3f, 0.4f),   // Koyu gri
                ResourceType.Gold => new Color(1f, 0.84f, 0f),      // Altin
                ResourceType.Gem => new Color(0.6f, 0.2f, 0.8f),    // Mor
                _ => Color.white
            };
        }

        #endregion
    }

    /// <summary>
    /// Kaynak tipleri
    /// </summary>
    public enum ResourceType : byte
    {
        Food = 0,
        Wood = 1,
        Stone = 2,
        Iron = 3,
        Gold = 4,
        Gem = 5
    }

    /// <summary>
    /// Kaynak noktasi veri yapisi (serialization icin)
    /// </summary>
    [System.Serializable]
    public struct ResourceNodeData
    {
        public ResourceType ResourceType;
        public int Level;
        public long CurrentAmount;
        public long MaxAmount;
        public float GatherRate;
        public long OwnerPlayerId;
        public long OwnerAllianceId;
        public bool IsProtected;
        public float ProtectionEndTime;
        public int CoordinatesQ;
        public int CoordinatesR;
    }
}
