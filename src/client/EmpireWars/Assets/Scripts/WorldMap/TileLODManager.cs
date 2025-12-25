using UnityEngine;
using System.Collections.Generic;
using EmpireWars.Core;
using EmpireWars.CameraSystem;
using EmpireWars.WorldMap.Tiles;

namespace EmpireWars.WorldMap
{
    /// <summary>
    /// Tile LOD (Level of Detail) Manager
    /// Kamera zoom seviyesine göre tile detaylarını yönetir
    /// Uzakta basit görünüm, yakında tam detay
    /// </summary>
    public class TileLODManager : MonoBehaviour
    {
        public static TileLODManager Instance { get; private set; }

        [Header("LOD Seviyeleri")]
        [Tooltip("Tam detay için maksimum zoom")]
        [SerializeField] private float fullDetailZoom = 30f;

        [Tooltip("Orta detay için maksimum zoom")]
        [SerializeField] private float mediumDetailZoom = 50f;

        [Tooltip("Minimum detay için maksimum zoom (bunun üstü texture mode)")]
        [SerializeField] private float lowDetailZoom = 70f;

        [Header("LOD Özellikleri")]
        [Tooltip("Uzakta dekorasyonları gizle")]
        [SerializeField] private bool hideDecorationsOnZoomOut = true;

        [Tooltip("Uzakta badge'leri gizle")]
        [SerializeField] private bool hideBadgesOnZoomOut = true;

        [Tooltip("Uzakta binaları basitleştir")]
        [SerializeField] private bool simplifyBuildingsOnZoomOut = true;

        [Header("Performans")]
        [Tooltip("LOD güncelleme aralığı (saniye)")]
        [SerializeField] private float updateInterval = 0.2f;

        // Current state
        private LODLevel currentLOD = LODLevel.Full;
        private float lastUpdateTime;
        private float lastZoom;

        // Cached references
        private List<LODObject> lodObjects = new List<LODObject>();
        private bool isDirty = true;

        public enum LODLevel
        {
            Full = 0,      // Tam detay - yakın zoom
            Medium = 1,    // Orta detay - biraz uzak
            Low = 2,       // Düşük detay - uzak
            Minimal = 3    // Minimum - çok uzak (sadece terrain rengi)
        }

        // Events
        public static event System.Action<LODLevel> OnLODChanged;

        public LODLevel CurrentLOD => currentLOD;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // Başlangıç değerlerini GameConfig'den al (static class)
            fullDetailZoom = GameConfig.MinZoom + 20f;
            mediumDetailZoom = (GameConfig.MinZoom + GameConfig.MaxZoom) / 2f;
            lowDetailZoom = GameConfig.MaxZoom - 10f;

            UpdateLOD(true);
        }

        private void Update()
        {
            if (Time.time - lastUpdateTime < updateInterval) return;
            lastUpdateTime = Time.time;

            UpdateLOD(false);
        }

        /// <summary>
        /// LOD seviyesini güncelle
        /// </summary>
        private void UpdateLOD(bool force)
        {
            float currentZoom = GetCurrentZoom();

            // Zoom değişmediyse çık
            if (!force && Mathf.Abs(currentZoom - lastZoom) < 1f) return;
            lastZoom = currentZoom;

            // Yeni LOD seviyesini hesapla
            LODLevel newLOD = CalculateLODLevel(currentZoom);

            // LOD değiştiyse uygula
            if (force || newLOD != currentLOD)
            {
                currentLOD = newLOD;
                ApplyLOD();
                OnLODChanged?.Invoke(currentLOD);
            }
        }

        private float GetCurrentZoom()
        {
            if (MapCameraController.Instance != null)
            {
                return MapCameraController.Instance.GetCurrentZoom();
            }
            if (Camera.main != null && Camera.main.orthographic)
            {
                return Camera.main.orthographicSize;
            }
            return GameConfig.DefaultZoom;
        }

        private LODLevel CalculateLODLevel(float zoom)
        {
            if (zoom <= fullDetailZoom)
                return LODLevel.Full;
            if (zoom <= mediumDetailZoom)
                return LODLevel.Medium;
            if (zoom <= lowDetailZoom)
                return LODLevel.Low;
            return LODLevel.Minimal;
        }

        /// <summary>
        /// LOD ayarlarını tüm objelere uygula
        /// </summary>
        private void ApplyLOD()
        {
            // Badge görünürlüğü
            if (hideBadgesOnZoomOut)
            {
                bool showBadges = currentLOD == LODLevel.Full || currentLOD == LODLevel.Medium;
                GameConfig.SetShowBadges(showBadges);
            }

            // Dekorasyon görünürlüğü
            if (hideDecorationsOnZoomOut)
            {
                ApplyDecorationVisibility();
            }

            // Bina basitleştirme
            if (simplifyBuildingsOnZoomOut)
            {
                ApplyBuildingSimplification();
            }

            Debug.Log($"TileLODManager: LOD changed to {currentLOD} (Zoom: {lastZoom:F1})");
        }

        private void ApplyDecorationVisibility()
        {
            bool showDecorations = currentLOD == LODLevel.Full;

            // Tüm dekorasyonları bul ve görünürlüğünü ayarla
            var decorations = FindObjectsByType<Transform>(FindObjectsSortMode.None);
            foreach (var obj in decorations)
            {
                if (obj.name.StartsWith("Decor_"))
                {
                    obj.gameObject.SetActive(showDecorations);
                }
            }
        }

        private void ApplyBuildingSimplification()
        {
            // LOD seviyesine göre bina detaylarını ayarla
            // Full: Tüm detaylar
            // Medium: Animasyonlar kapalı
            // Low: Basit mesh
            // Minimal: Sadece placeholder

            // Bu implementasyon bina prefab'larına LOD group eklenmesini gerektirir
            // Şimdilik sadece shadow'ları kontrol edelim

            var renderers = FindObjectsByType<Renderer>(FindObjectsSortMode.None);
            foreach (var renderer in renderers)
            {
                if (renderer.transform.parent != null &&
                    renderer.transform.parent.name.StartsWith("Building_"))
                {
                    // Uzakta gölgeleri kapat
                    renderer.shadowCastingMode = currentLOD == LODLevel.Full
                        ? UnityEngine.Rendering.ShadowCastingMode.On
                        : UnityEngine.Rendering.ShadowCastingMode.Off;
                }
            }
        }

        /// <summary>
        /// LOD objesi kaydet (dinamik yüklenen objeler için)
        /// </summary>
        public void RegisterLODObject(LODObject obj)
        {
            if (!lodObjects.Contains(obj))
            {
                lodObjects.Add(obj);
                obj.ApplyLOD(currentLOD);
            }
        }

        /// <summary>
        /// LOD objesi kaldır
        /// </summary>
        public void UnregisterLODObject(LODObject obj)
        {
            lodObjects.Remove(obj);
        }

        /// <summary>
        /// Tüm LOD objelerini güncelle
        /// </summary>
        public void RefreshAllLODObjects()
        {
            foreach (var obj in lodObjects)
            {
                if (obj != null)
                {
                    obj.ApplyLOD(currentLOD);
                }
            }
        }

        /// <summary>
        /// Manuel LOD tetikle (test için)
        /// </summary>
        public void ForceLOD(LODLevel level)
        {
            currentLOD = level;
            ApplyLOD();
            OnLODChanged?.Invoke(currentLOD);
        }
    }

    /// <summary>
    /// LOD yönetilen obje componenti
    /// Tile'lara veya binalara eklenebilir
    /// </summary>
    public class LODObject : MonoBehaviour
    {
        [Header("LOD Ayarları")]
        [SerializeField] private GameObject fullDetailObject;
        [SerializeField] private GameObject mediumDetailObject;
        [SerializeField] private GameObject lowDetailObject;

        [Header("Bileşenler")]
        [SerializeField] private Renderer[] decorationRenderers;
        [SerializeField] private ParticleSystem[] effects;

        private TileLODManager.LODLevel currentLOD;

        private void Start()
        {
            if (TileLODManager.Instance != null)
            {
                TileLODManager.Instance.RegisterLODObject(this);
            }
        }

        private void OnDestroy()
        {
            if (TileLODManager.Instance != null)
            {
                TileLODManager.Instance.UnregisterLODObject(this);
            }
        }

        /// <summary>
        /// LOD seviyesini uygula
        /// </summary>
        public void ApplyLOD(TileLODManager.LODLevel level)
        {
            currentLOD = level;

            // Mesh değiştirme
            if (fullDetailObject != null)
                fullDetailObject.SetActive(level == TileLODManager.LODLevel.Full);

            if (mediumDetailObject != null)
                mediumDetailObject.SetActive(level == TileLODManager.LODLevel.Medium);

            if (lowDetailObject != null)
                lowDetailObject.SetActive(level == TileLODManager.LODLevel.Low ||
                                          level == TileLODManager.LODLevel.Minimal);

            // Dekorasyonlar
            bool showDecorations = level == TileLODManager.LODLevel.Full;
            foreach (var renderer in decorationRenderers)
            {
                if (renderer != null)
                    renderer.enabled = showDecorations;
            }

            // Efektler
            bool showEffects = level == TileLODManager.LODLevel.Full ||
                              level == TileLODManager.LODLevel.Medium;
            foreach (var effect in effects)
            {
                if (effect != null)
                {
                    if (showEffects && !effect.isPlaying)
                        effect.Play();
                    else if (!showEffects && effect.isPlaying)
                        effect.Stop();
                }
            }
        }
    }
}
