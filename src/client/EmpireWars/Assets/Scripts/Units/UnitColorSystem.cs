using UnityEngine;
using System.Collections.Generic;

namespace EmpireWars.Units
{
    /// <summary>
    /// Birim renk sistemi - İttifak renklerini dinamik olarak uygular
    /// Sınırsız renk desteği
    /// </summary>
    public static class UnitColorSystem
    {
        // Renk uygulanan materyalleri cache'le (performans için)
        private static Dictionary<string, Material> materialCache = new Dictionary<string, Material>();

        /// <summary>
        /// Bir GameObject'e ittifak rengi uygula
        /// </summary>
        /// <param name="unit">Renk uygulanacak birim</param>
        /// <param name="allianceColor">İttifak rengi</param>
        /// <param name="tintStrength">Renk yoğunluğu (0-1)</param>
        public static void ApplyAllianceColor(GameObject unit, Color allianceColor, float tintStrength = 0.7f)
        {
            if (unit == null) return;

            Renderer[] renderers = unit.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                ApplyColorToRenderer(renderer, allianceColor, tintStrength);
            }
        }

        /// <summary>
        /// Renderer'a renk uygula (MaterialPropertyBlock ile - performanslı)
        /// </summary>
        private static void ApplyColorToRenderer(Renderer renderer, Color allianceColor, float tintStrength)
        {
            if (renderer == null) return;

            MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propBlock);

            // Orijinal rengi al ve ittifak rengiyle karıştır
            Color originalColor = Color.white;
            if (renderer.sharedMaterial != null && renderer.sharedMaterial.HasProperty("_BaseColor"))
            {
                originalColor = renderer.sharedMaterial.GetColor("_BaseColor");
            }
            else if (renderer.sharedMaterial != null && renderer.sharedMaterial.HasProperty("_Color"))
            {
                originalColor = renderer.sharedMaterial.GetColor("_Color");
            }

            // Renkleri karıştır
            Color tintedColor = Color.Lerp(originalColor, allianceColor, tintStrength);
            tintedColor.a = originalColor.a;

            // URP ve Built-in için
            propBlock.SetColor("_BaseColor", tintedColor);
            propBlock.SetColor("_Color", tintedColor);

            renderer.SetPropertyBlock(propBlock);
        }

        /// <summary>
        /// Yeni materyal instance oluşturarak renk uygula
        /// (Daha fazla kontrol ama daha fazla bellek kullanır)
        /// </summary>
        public static void ApplyAllianceColorWithMaterialInstance(GameObject unit, Color allianceColor, float tintStrength = 0.7f)
        {
            if (unit == null) return;

            Renderer[] renderers = unit.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                if (renderer.sharedMaterial == null) continue;

                // Cache key oluştur
                string cacheKey = $"{renderer.sharedMaterial.name}_{ColorToHex(allianceColor)}";

                Material mat;
                if (!materialCache.TryGetValue(cacheKey, out mat))
                {
                    // Yeni materyal oluştur
                    mat = new Material(renderer.sharedMaterial);

                    Color originalColor = Color.white;
                    if (mat.HasProperty("_BaseColor"))
                        originalColor = mat.GetColor("_BaseColor");
                    else if (mat.HasProperty("_Color"))
                        originalColor = mat.GetColor("_Color");

                    Color tintedColor = Color.Lerp(originalColor, allianceColor, tintStrength);

                    if (mat.HasProperty("_BaseColor"))
                        mat.SetColor("_BaseColor", tintedColor);
                    if (mat.HasProperty("_Color"))
                        mat.SetColor("_Color", tintedColor);

                    materialCache[cacheKey] = mat;
                }

                renderer.material = mat;
            }
        }

        /// <summary>
        /// Rengi hex string'e çevir (cache key için)
        /// </summary>
        private static string ColorToHex(Color color)
        {
            return $"{(int)(color.r * 255):X2}{(int)(color.g * 255):X2}{(int)(color.b * 255):X2}";
        }

        /// <summary>
        /// Hex string'den renk oluştur
        /// </summary>
        public static Color HexToColor(string hex)
        {
            hex = hex.TrimStart('#');

            if (hex.Length == 6)
            {
                byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                return new Color(r / 255f, g / 255f, b / 255f);
            }

            return Color.white;
        }

        /// <summary>
        /// Cache'i temizle (sahne değişiminde çağır)
        /// </summary>
        public static void ClearCache()
        {
            foreach (var mat in materialCache.Values)
            {
                if (mat != null)
                    Object.Destroy(mat);
            }
            materialCache.Clear();
        }
    }

    /// <summary>
    /// Birime eklenebilir renk komponenti
    /// </summary>
    public class UnitAllianceColor : MonoBehaviour
    {
        [Header("İttifak Renk Ayarları")]
        [Tooltip("İttifak rengi")]
        public Color allianceColor = Color.blue;

        [Tooltip("Renk yoğunluğu")]
        [Range(0f, 1f)]
        public float tintStrength = 0.7f;

        [Tooltip("Başlangıçta renk uygula")]
        public bool applyOnStart = true;

        private void Start()
        {
            if (applyOnStart)
            {
                ApplyColor();
            }
        }

        /// <summary>
        /// Rengi uygula
        /// </summary>
        public void ApplyColor()
        {
            UnitColorSystem.ApplyAllianceColor(gameObject, allianceColor, tintStrength);
        }

        /// <summary>
        /// Yeni renk ayarla ve uygula
        /// </summary>
        public void SetAllianceColor(Color color)
        {
            allianceColor = color;
            ApplyColor();
        }

        /// <summary>
        /// Hex kodundan renk ayarla
        /// </summary>
        public void SetAllianceColorFromHex(string hexColor)
        {
            allianceColor = UnitColorSystem.HexToColor(hexColor);
            ApplyColor();
        }
    }
}
