using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using EmpireWars.UI;

namespace EmpireWars.Core
{
    /// <summary>
    /// Mobile Strategy Game kalitesinde sahne kurulumu
    /// Mafia City / Rise of Kingdoms tarzinda goruntuler
    /// </summary>
    [ExecuteAlways]
    public class HDSceneSetup : MonoBehaviour
    {
        [Header("Auto Setup")]
        [SerializeField] private bool setupOnStart = true;
        [SerializeField] private bool setupLighting = true;
        [SerializeField] private bool setupPostProcessing = true;
        [SerializeField] private bool setupCamera = true;
        [SerializeField] private bool setupMinimap = true;

        [Header("Lighting - Mobile Strategy Style")]
        [SerializeField] private Color sunColor = new Color(1f, 0.92f, 0.8f);    // Sicak gunes
        [SerializeField] private float sunIntensity = 1.8f;                       // Parlak
        [SerializeField] private Color ambientColor = new Color(0.5f, 0.55f, 0.65f); // Mavi tonlu ambient
        [SerializeField] private float ambientIntensity = 1.2f;

        [Header("Post Processing - Vibrant Mobile Style")]
        [SerializeField] private float bloomIntensity = 0.6f;
        [SerializeField] private float bloomThreshold = 0.85f;
        [SerializeField] private float vignetteIntensity = 0.2f;
        [SerializeField] private float saturation = 15f;         // Daha canli renkler
        [SerializeField] private float contrast = 12f;           // Daha net
        [SerializeField] private float exposure = 0.3f;          // Daha parlak

        private Light mainLight;
        private Volume globalVolume;
        private Camera mainCamera;
        private MiniMapController minimapController;

        private void Start()
        {
            if (setupOnStart && Application.isPlaying)
            {
                SetupHDScene();
            }
        }

        [ContextMenu("Setup HD Scene")]
        public void SetupHDScene()
        {
            Debug.Log("=== Mobile Strategy Game Style Setup ===");

            if (setupLighting) SetupHDLighting();
            if (setupPostProcessing) SetupHDPostProcessing();
            if (setupCamera) SetupHDCamera();
            if (setupMinimap && Application.isPlaying) SetupMinimap();

            Debug.Log("=== Setup Complete ===");
        }

        private void SetupMinimap()
        {
            // Canvas bul veya olustur
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("UI Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }

            minimapController = FindFirstObjectByType<MiniMapController>();
            if (minimapController == null)
            {
                GameObject minimapObj = new GameObject("MiniMap Controller");
                minimapObj.transform.SetParent(canvas.transform);
                minimapController = minimapObj.AddComponent<MiniMapController>();
            }
            minimapController.Initialize();
            Debug.Log("HDSceneSetup: Dairesel minimap olusturuldu");
        }

        private void SetupHDLighting()
        {
            // Ana isik (Gunes)
            mainLight = FindFirstObjectByType<Light>();
            if (mainLight == null)
            {
                GameObject lightObj = new GameObject("Directional Light (Sun)");
                mainLight = lightObj.AddComponent<Light>();
                mainLight.type = LightType.Directional;
            }

            mainLight.color = sunColor;
            mainLight.intensity = sunIntensity;
            mainLight.shadows = LightShadows.Soft;
            mainLight.shadowStrength = 0.8f;
            mainLight.shadowBias = 0.02f;
            mainLight.shadowNormalBias = 0.3f;
            mainLight.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            // URP Light ayarlari (shadow resolution Pipeline Asset'ten kontrol edilir)

            // Ambient lighting
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = ambientColor;
            RenderSettings.ambientIntensity = ambientIntensity;

            // Skybox ve fog
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogColor = new Color(0.7f, 0.8f, 0.9f);
            RenderSettings.fogStartDistance = 50f;
            RenderSettings.fogEndDistance = 200f;

            Debug.Log("HDSceneSetup: Lighting ayarlandi");
        }

        private void SetupHDPostProcessing()
        {
            // Global Volume bul veya olustur
            globalVolume = FindFirstObjectByType<Volume>();
            if (globalVolume == null)
            {
                GameObject volumeObj = new GameObject("Global Volume");
                globalVolume = volumeObj.AddComponent<Volume>();
                globalVolume.isGlobal = true;
            }

            // Volume Profile olustur
            VolumeProfile profile = globalVolume.profile;
            if (profile == null)
            {
                profile = ScriptableObject.CreateInstance<VolumeProfile>();
                globalVolume.profile = profile;
            }

            // Bloom - Parlak ve canli
            if (!profile.TryGet<Bloom>(out var bloom))
            {
                bloom = profile.Add<Bloom>(true);
            }
            bloom.active = true;
            bloom.threshold.overrideState = true;
            bloom.threshold.value = bloomThreshold;
            bloom.intensity.overrideState = true;
            bloom.intensity.value = bloomIntensity;
            bloom.scatter.overrideState = true;
            bloom.scatter.value = 0.65f;
            bloom.tint.overrideState = true;
            bloom.tint.value = new Color(1f, 0.95f, 0.9f); // Hafif sicak bloom
            bloom.highQualityFiltering.overrideState = true;
            bloom.highQualityFiltering.value = true;

            // Color Adjustments - Canli mobil oyun renkleri
            if (!profile.TryGet<ColorAdjustments>(out var colorAdj))
            {
                colorAdj = profile.Add<ColorAdjustments>(true);
            }
            colorAdj.active = true;
            colorAdj.saturation.overrideState = true;
            colorAdj.saturation.value = saturation;
            colorAdj.contrast.overrideState = true;
            colorAdj.contrast.value = contrast;
            colorAdj.postExposure.overrideState = true;
            colorAdj.postExposure.value = exposure;
            colorAdj.colorFilter.overrideState = true;
            colorAdj.colorFilter.value = new Color(1f, 0.98f, 0.95f); // Hafif sicak filtre

            // Tonemapping (ACES sinematik)
            if (!profile.TryGet<Tonemapping>(out var tonemap))
            {
                tonemap = profile.Add<Tonemapping>(true);
            }
            tonemap.active = true;
            tonemap.mode.overrideState = true;
            tonemap.mode.value = TonemappingMode.ACES;

            // Vignette
            if (!profile.TryGet<Vignette>(out var vignette))
            {
                vignette = profile.Add<Vignette>(true);
            }
            vignette.active = true;
            vignette.intensity.overrideState = true;
            vignette.intensity.value = vignetteIntensity;
            vignette.smoothness.overrideState = true;
            vignette.smoothness.value = 0.4f;

            // White Balance (sicak tonlar)
            if (!profile.TryGet<WhiteBalance>(out var wb))
            {
                wb = profile.Add<WhiteBalance>(true);
            }
            wb.active = true;
            wb.temperature.overrideState = true;
            wb.temperature.value = 5f; // Hafif sicak

            // Lift Gamma Gain (sinematik look)
            if (!profile.TryGet<LiftGammaGain>(out var lgg))
            {
                lgg = profile.Add<LiftGammaGain>(true);
            }
            lgg.active = true;
            lgg.lift.overrideState = true;
            lgg.lift.value = new Vector4(1f, 1f, 1.02f, 0f); // Hafif mavi golge
            lgg.gamma.overrideState = true;
            lgg.gamma.value = new Vector4(1f, 1f, 1f, 0.05f); // Hafif parlaklik
            lgg.gain.overrideState = true;
            lgg.gain.value = new Vector4(1f, 0.98f, 0.95f, 0f); // Sicak highlight

            Debug.Log("HDSceneSetup: Post-processing ayarlandi");
        }

        private void SetupHDCamera()
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                mainCamera = FindFirstObjectByType<Camera>();
            }

            if (mainCamera == null)
            {
                Debug.LogWarning("HDSceneSetup: Kamera bulunamadi!");
                return;
            }

            // Kamera ayarlari
            mainCamera.allowHDR = true;
            mainCamera.allowMSAA = true;

            // URP Camera ayarlari
            var urpCamera = mainCamera.GetComponent<UniversalAdditionalCameraData>();
            if (urpCamera == null)
            {
                urpCamera = mainCamera.gameObject.AddComponent<UniversalAdditionalCameraData>();
            }

            urpCamera.renderPostProcessing = true;
            urpCamera.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
            urpCamera.antialiasingQuality = AntialiasingQuality.High;
            urpCamera.dithering = true;

            Debug.Log("HDSceneSetup: Kamera ayarlandi");
        }

#if UNITY_EDITOR
        [ContextMenu("Reset to Defaults")]
        public void ResetToDefaults()
        {
            sunColor = new Color(1f, 0.95f, 0.85f);
            sunIntensity = 1.5f;
            ambientColor = new Color(0.4f, 0.45f, 0.5f);
            ambientIntensity = 1f;
            bloomIntensity = 0.5f;
            vignetteIntensity = 0.25f;
            saturation = 10f;
            contrast = 10f;
        }
#endif
    }
}
