using UnityEngine;
using UnityEngine.Rendering;

namespace EmpireWars.Core
{
    /// <summary>
    /// Mobil performans optimizasyonları
    /// - GPU Instancing aktifleştirir
    /// - Material ayarlarını optimize eder
    /// - Draw call'ları azaltır
    /// </summary>
    public class MobilePerformanceManager : MonoBehaviour
    {
        private static MobilePerformanceManager _instance;
        public static MobilePerformanceManager Instance => _instance;

        [Header("Settings")]
        [SerializeField] private bool enableOnStart = true;
        [SerializeField] private bool logOptimizations = true;

        private int materialsOptimized = 0;
        private int renderersOptimized = 0;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (enableOnStart)
            {
                ApplyAllOptimizations();
            }
        }

        public void ApplyAllOptimizations()
        {
            bool isMobile = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;

            if (!isMobile && !Application.isEditor)
            {
                Debug.Log("MobilePerformanceManager: PC platform, optimizasyonlar atlanıyor");
                return;
            }

            // Frame rate ayarla
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;

            // Render ayarları
            ApplyRenderSettings();

            // Tüm material'larda GPU Instancing aktifleştir
            EnableGPUInstancingOnAllMaterials();

            // Static batching uygula
            ApplyStaticBatching();

            if (logOptimizations)
            {
                Debug.Log($"MobilePerformanceManager: {materialsOptimized} material optimize edildi, {renderersOptimized} renderer static batching'e alındı");
            }
        }

        private void ApplyRenderSettings()
        {
            // Gölgeleri kapat
            QualitySettings.shadows = ShadowQuality.Disable;

            // LOD ayarları
            QualitySettings.lodBias = 0.3f;
            QualitySettings.maximumLODLevel = 2;

            // Diğer ayarlar
            QualitySettings.antiAliasing = 0;
            QualitySettings.softParticles = false;
            QualitySettings.realtimeReflectionProbes = false;
            QualitySettings.billboardsFaceCameraPosition = false;
            QualitySettings.skinWeights = SkinWeights.TwoBones;
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;

            // Pixel light sayısını azalt
            QualitySettings.pixelLightCount = 0;

            if (logOptimizations)
            {
                Debug.Log("MobilePerformanceManager: Render ayarları optimize edildi");
            }
        }

        private void EnableGPUInstancingOnAllMaterials()
        {
            // Sahnedeki tüm Renderer'ları bul
            Renderer[] renderers = FindObjectsByType<Renderer>(FindObjectsSortMode.None);

            foreach (Renderer renderer in renderers)
            {
                if (renderer == null) continue;

                // Material'larda GPU Instancing aktifleştir
                foreach (Material mat in renderer.sharedMaterials)
                {
                    if (mat != null && mat.enableInstancing == false)
                    {
                        mat.enableInstancing = true;
                        materialsOptimized++;
                    }
                }
            }
        }

        private void ApplyStaticBatching()
        {
            // Statik olmayan tile'ları static yap
            GameObject tilesParent = GameObject.Find("TilesParent");
            if (tilesParent == null)
            {
                tilesParent = GameObject.Find("Tiles");
            }

            if (tilesParent != null)
            {
                // Tüm child'ları static yap
                foreach (Transform child in tilesParent.transform)
                {
                    if (!child.gameObject.isStatic)
                    {
                        child.gameObject.isStatic = true;
                        renderersOptimized++;
                    }
                }

                // Static batching uygula
                StaticBatchingUtility.Combine(tilesParent);

                if (logOptimizations)
                {
                    Debug.Log($"MobilePerformanceManager: Static batching uygulandı - {tilesParent.name}");
                }
            }
        }

        /// <summary>
        /// Yeni tile'lar eklendiğinde çağrılır
        /// </summary>
        public void OptimizeNewTiles(GameObject parent)
        {
            if (parent == null) return;

            Renderer[] renderers = parent.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                foreach (Material mat in renderer.sharedMaterials)
                {
                    if (mat != null)
                    {
                        mat.enableInstancing = true;
                    }
                }
            }
        }
    }
}
