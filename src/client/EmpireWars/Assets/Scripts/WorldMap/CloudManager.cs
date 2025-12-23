using UnityEngine;
using System.Collections.Generic;

namespace EmpireWars.WorldMap
{
    /// <summary>
    /// Harita uzerinde yavasce hareket eden bulutlar olusturur
    /// KayKit cloud_big ve cloud_small prefab'larini kullanir
    /// </summary>
    public class CloudManager : MonoBehaviour
    {
        public static CloudManager Instance { get; private set; }

        [Header("Cloud Prefabs")]
        [SerializeField] private GameObject cloudBigPrefab;
        [SerializeField] private GameObject cloudSmallPrefab;

        [Header("Settings")]
        [SerializeField] private int cloudCount = 15;
        [SerializeField] private float minHeight = 15f;
        [SerializeField] private float maxHeight = 25f;
        [SerializeField] private float minSpeed = 0.5f;
        [SerializeField] private float maxSpeed = 2f;
        [SerializeField] private float minScale = 1.5f;
        [SerializeField] private float maxScale = 4f;

        [Header("Spawn Area")]
        [SerializeField] private float areaWidth = 120f;
        [SerializeField] private float areaDepth = 120f;
        [SerializeField] private Vector3 areaCenter = new Vector3(60f, 0f, 60f);

        [Header("Wind Direction")]
        [SerializeField] private Vector3 windDirection = new Vector3(1f, 0f, 0.3f);

        private List<CloudInstance> clouds = new List<CloudInstance>();
        private Transform cloudsParent;

        private class CloudInstance
        {
            public GameObject gameObject;
            public float speed;
            public Vector3 direction;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            cloudsParent = new GameObject("Clouds").transform;
            cloudsParent.SetParent(transform);

            SpawnInitialClouds();
        }

        private void Update()
        {
            MoveClouds();
        }

        /// <summary>
        /// Baslangicta bulutlari olusturur
        /// </summary>
        private void SpawnInitialClouds()
        {
            if (cloudBigPrefab == null && cloudSmallPrefab == null)
            {
                Debug.LogWarning("CloudManager: Cloud prefab'lari atanmamis!");
                return;
            }

            for (int i = 0; i < cloudCount; i++)
            {
                SpawnCloud(true);
            }

            Debug.Log($"CloudManager: {cloudCount} bulut olusturuldu");
        }

        /// <summary>
        /// Yeni bulut olusturur
        /// </summary>
        private void SpawnCloud(bool randomPosition)
        {
            // Rastgele buyuk veya kucuk bulut
            GameObject prefab = Random.value > 0.5f ? cloudBigPrefab : cloudSmallPrefab;
            if (prefab == null)
            {
                prefab = cloudBigPrefab ?? cloudSmallPrefab;
            }
            if (prefab == null) return;

            // Pozisyon
            Vector3 position;
            if (randomPosition)
            {
                position = GetRandomPositionInArea();
            }
            else
            {
                // Kenardan spawn (ruzgar yonunun tersinden)
                position = GetSpawnPositionAtEdge();
            }

            // Yukseklik
            position.y = Random.Range(minHeight, maxHeight);

            // Bulut olustur
            GameObject cloud = Instantiate(prefab, position, Quaternion.identity);
            cloud.transform.SetParent(cloudsParent);
            cloud.name = $"Cloud_{clouds.Count}";

            // Rastgele olcek
            float scale = Random.Range(minScale, maxScale);
            cloud.transform.localScale = Vector3.one * scale;

            // Rastgele rotasyon (Y ekseni)
            cloud.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            // Instance olustur
            var instance = new CloudInstance
            {
                gameObject = cloud,
                speed = Random.Range(minSpeed, maxSpeed),
                direction = windDirection.normalized
            };

            clouds.Add(instance);
        }

        /// <summary>
        /// Bulutlari hareket ettirir
        /// </summary>
        private void MoveClouds()
        {
            for (int i = clouds.Count - 1; i >= 0; i--)
            {
                var cloud = clouds[i];
                if (cloud.gameObject == null)
                {
                    clouds.RemoveAt(i);
                    continue;
                }

                // Hareket
                cloud.gameObject.transform.position += cloud.direction * cloud.speed * Time.deltaTime;

                // Alan disina ciktiysa yeniden spawn
                if (IsOutOfArea(cloud.gameObject.transform.position))
                {
                    Destroy(cloud.gameObject);
                    clouds.RemoveAt(i);
                    SpawnCloud(false);
                }
            }
        }

        /// <summary>
        /// Alan icinde rastgele pozisyon
        /// </summary>
        private Vector3 GetRandomPositionInArea()
        {
            float x = areaCenter.x + Random.Range(-areaWidth / 2f, areaWidth / 2f);
            float z = areaCenter.z + Random.Range(-areaDepth / 2f, areaDepth / 2f);
            return new Vector3(x, 0, z);
        }

        /// <summary>
        /// Kenarda spawn pozisyonu (ruzgar yonunun tersinden)
        /// </summary>
        private Vector3 GetSpawnPositionAtEdge()
        {
            // Ruzgarin geldigi kenardan spawn
            float x, z;

            if (Mathf.Abs(windDirection.x) > Mathf.Abs(windDirection.z))
            {
                // X yonunde hareket - sol veya sag kenardan
                x = windDirection.x > 0 ? areaCenter.x - areaWidth / 2f - 10f : areaCenter.x + areaWidth / 2f + 10f;
                z = areaCenter.z + Random.Range(-areaDepth / 2f, areaDepth / 2f);
            }
            else
            {
                // Z yonunde hareket - on veya arka kenardan
                x = areaCenter.x + Random.Range(-areaWidth / 2f, areaWidth / 2f);
                z = windDirection.z > 0 ? areaCenter.z - areaDepth / 2f - 10f : areaCenter.z + areaDepth / 2f + 10f;
            }

            return new Vector3(x, 0, z);
        }

        /// <summary>
        /// Pozisyon alan disinda mi?
        /// </summary>
        private bool IsOutOfArea(Vector3 pos)
        {
            float margin = 20f;
            return pos.x < areaCenter.x - areaWidth / 2f - margin ||
                   pos.x > areaCenter.x + areaWidth / 2f + margin ||
                   pos.z < areaCenter.z - areaDepth / 2f - margin ||
                   pos.z > areaCenter.z + areaDepth / 2f + margin;
        }

        /// <summary>
        /// Ruzgar yonunu degistir
        /// </summary>
        public void SetWindDirection(Vector3 direction)
        {
            windDirection = direction.normalized;
            foreach (var cloud in clouds)
            {
                cloud.direction = windDirection;
            }
        }

        /// <summary>
        /// Bulut sayisini ayarla
        /// </summary>
        public void SetCloudCount(int count)
        {
            cloudCount = count;

            // Fazla bulutlari sil
            while (clouds.Count > cloudCount)
            {
                int index = clouds.Count - 1;
                if (clouds[index].gameObject != null)
                {
                    Destroy(clouds[index].gameObject);
                }
                clouds.RemoveAt(index);
            }

            // Eksik bulutlari ekle
            while (clouds.Count < cloudCount)
            {
                SpawnCloud(true);
            }
        }

        /// <summary>
        /// Tum bulutlari temizle
        /// </summary>
        public void ClearAllClouds()
        {
            foreach (var cloud in clouds)
            {
                if (cloud.gameObject != null)
                {
                    Destroy(cloud.gameObject);
                }
            }
            clouds.Clear();
        }

        /// <summary>
        /// Prefab'lari runtime'da ata
        /// </summary>
        public void SetCloudPrefabs(GameObject bigCloud, GameObject smallCloud)
        {
            cloudBigPrefab = bigCloud;
            cloudSmallPrefab = smallCloud;
        }
    }
}
