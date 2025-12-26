using UnityEngine;
using TMPro;

namespace EmpireWars.UI
{
    /// <summary>
    /// Ekranda FPS gösterir
    /// </summary>
    public class FPSDisplay : MonoBehaviour
    {
        private TextMeshProUGUI fpsText;
        private float deltaTime = 0f;
        private float updateInterval = 0.5f;
        private float timer = 0f;

        private void Start()
        {
            fpsText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            timer += Time.unscaledDeltaTime;

            if (timer >= updateInterval)
            {
                float fps = 1.0f / deltaTime;
                float ms = deltaTime * 1000f;

                if (fpsText != null)
                {
                    fpsText.text = $"FPS: {fps:F1}\nMS: {ms:F1}";
                }

                timer = 0f;
            }
        }
    }
}
