using UnityEngine;
using UnityEditor;
using System.IO;

namespace EmpireWars.Editor
{
    /// <summary>
    /// Polytope Studio materyallerini orijinal shader'a geri döndürür
    /// Menu: Tools > EmpireWars > Fix Polytope Materials for URP
    /// </summary>
    public class PolytopeURPFixer : EditorWindow
    {
        [MenuItem("Tools/EmpireWars/Fix Polytope Materials for URP")]
        public static void FixPolytopeMaterials()
        {
            // Orijinal Polytope shader'ını bul
            Shader originalShader = Shader.Find("Polytope Studio/ PT_Medieval Armors Shader PBR");

            if (originalShader == null)
            {
                Debug.LogError("Polytope orijinal shader bulunamadı! Shader dosyasının varlığını kontrol edin.");
                Debug.Log("Alternatif: URP Lit shader kullanılacak...");
                UseURPFallback();
                return;
            }

            // Texture'ları bul
            string texturePath = "Assets/Polytope Studio/Lowpoly_Characters/Sources/Modular_Armors/Textures/";

            Texture2D tex0 = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath + "PT_Armors_Skin_Eye_Hair_Mask_01.png");
            Texture2D tex1 = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath + "PT_Armors_Skin_Eye_Hair_Mask_02.png");
            Texture2D tex2 = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath + "PT_Armors_Base_Texture.png");
            Texture2D tex3 = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath + "PT_Armors_Leather_Mask_01.png");
            Texture2D tex4 = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath + "PT_Armors_Feathers_Mask_02.png");
            Texture2D tex5 = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath + "PT_Armors_Cloth_Mask_01.png");
            Texture2D tex6 = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath + "PT_Armors_Metal_Mask_01.png");
            Texture2D tex7 = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath + "PT_Armors_Gems_Mask_01.png");

            // Armor materyalini düzelt
            string armorMatPath = "Assets/Polytope Studio/Lowpoly_Characters/Sources/Modular_Armors/Materials/PT_Armors_Material.mat";
            Material armorMat = AssetDatabase.LoadAssetAtPath<Material>(armorMatPath);

            if (armorMat != null)
            {
                armorMat.shader = originalShader;

                // Texture'ları ata
                if (tex0 != null) armorMat.SetTexture("_Texture0", tex0);
                if (tex1 != null) armorMat.SetTexture("_Texture1", tex1);
                if (tex2 != null) armorMat.SetTexture("_Texture2", tex2);
                if (tex3 != null) armorMat.SetTexture("_Texture3", tex3);
                if (tex4 != null) armorMat.SetTexture("_Texture4", tex4);
                if (tex5 != null) armorMat.SetTexture("_Texture5", tex5);
                if (tex6 != null) armorMat.SetTexture("_Texture6", tex6);
                if (tex7 != null) armorMat.SetTexture("_Texture7", tex7);

                EditorUtility.SetDirty(armorMat);
                Debug.Log("PT_Armors_Material orijinal shader'a döndürüldü!");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("PolytopeURPFixer: Orijinal Polytope shader geri yüklendi!");
            Debug.LogWarning("NOT: Bu shader Built-in RP için. URP'de pembe görünebilir. Çözüm için Polytope'un URP versiyonunu indirin.");
        }

        private static void UseURPFallback()
        {
            Shader urpLit = Shader.Find("Universal Render Pipeline/Lit");
            if (urpLit == null) return;

            string baseTexPath = "Assets/Polytope Studio/Lowpoly_Characters/Sources/Modular_Armors/Textures/PT_Armors_Base_Texture.png";
            Texture2D baseTex = AssetDatabase.LoadAssetAtPath<Texture2D>(baseTexPath);

            string armorMatPath = "Assets/Polytope Studio/Lowpoly_Characters/Sources/Modular_Armors/Materials/PT_Armors_Material.mat";
            Material armorMat = AssetDatabase.LoadAssetAtPath<Material>(armorMatPath);

            if (armorMat != null)
            {
                armorMat.shader = urpLit;
                if (baseTex != null)
                {
                    armorMat.SetTexture("_BaseMap", baseTex);
                }
                armorMat.SetColor("_BaseColor", new Color(0.9f, 0.75f, 0.65f, 1f));
                EditorUtility.SetDirty(armorMat);
            }

            AssetDatabase.SaveAssets();
            Debug.Log("URP Lit fallback kullanıldı.");
        }
    }
}
