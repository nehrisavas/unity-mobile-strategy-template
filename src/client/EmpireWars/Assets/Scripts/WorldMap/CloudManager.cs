using UnityEngine;
using System.Collections.Generic;
using EmpireWars.Core;

namespace EmpireWars.WorldMap
{
    /// <summary>
    /// Hafif bulut sistemi - sadece dekoratif amacli
    /// Object pooling ve minimal update ile optimize edilmis
    /// GameConfig'den ayarlari otomatik alir
    /// </summary>
    public class CloudManager : MonoBehaviour
    {
        public static CloudManager Instance { get; private set; }

        [Header("Cloud Prefabs")]
        [SerializeField] private GameObject cloudBigPrefab;
        [SerializeField] private GameObject cloudSmallPrefab;

        [Header("Settings")]
        [SerializeField] private bool useGameConfig = true;
        [SerializeField] private float minHeight = 25f;
        [SerializeField] private float maxHeight = 40f;
        [SerializeField] private float cloudSpeed = 1.5f;

        // GameConfig'den alinan degerler
        private int cloudCount;
        private float areaWidth;
        private float areaDepth;
        private Vector3 areaCenter;
        private float updateInterval;

        // Object pool
        private Transform[] cloudPool;
        private float[] cloudSpeeds;
        private bool initialized = false;
        private float updateTimer = 0f;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else { Destroy(gameObject); return; }
        }

        private void Start()
        {
            // GameConfig'den ayarlari al
            if (useGameConfig)
            {
                GameConfig.Initialize();
                cloudCount = GameConfig.CloudCount;
                areaWidth = GameConfig.WorldWidth;
                areaDepth = GameConfig.WorldHeight;
                areaCenter = GameConfig.WorldCenter;
                updateInterval = GameConfig.CloudUpdateInterval;
            }
            else
            {
                cloudCount = 12;
                areaWidth = 80f;
                areaDepth = 80f;
                areaCenter = new Vector3(40f, 0f, 40f);
                updateInterval = 0.1f;
            }

            InitializeClouds();
        }

        private void Update()
        {
            if (!initialized) return;

            updateTimer += Time.deltaTime;
            if (updateTimer >= updateInterval)
            {
                updateTimer = 0f;
                MoveClouds(updateInterval);
            }
        }

        private void InitializeClouds()
        {
            if (cloudBigPrefab == null && cloudSmallPrefab == null)
            {
                Debug.LogWarning("CloudManager: Prefab atanmamis");
                return;
            }

            cloudPool = new Transform[cloudCount];
            cloudSpeeds = new float[cloudCount];

            Transform parent = new GameObject("Clouds").transform;
            parent.SetParent(transform);

            for (int i = 0; i < cloudCount; i++)
            {
                GameObject prefab = (i % 2 == 0) ? cloudBigPrefab : cloudSmallPrefab;
                if (prefab == null) prefab = cloudBigPrefab ?? cloudSmallPrefab;
                if (prefab == null) continue;

                Vector3 pos = GetRandomPosition();
                GameObject cloud = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0, 360), 0));
                cloud.transform.SetParent(parent);
                cloud.transform.localScale = Vector3.one * Random.Range(2f, 4f);

                // Clouds layer'a ata (minimap'te gorunmemesi icin)
                int cloudsLayer = LayerMask.NameToLayer("Clouds");
                if (cloudsLayer >= 0)
                {
                    SetLayerRecursively(cloud, cloudsLayer);
                }

                cloudPool[i] = cloud.transform;
                cloudSpeeds[i] = Random.Range(0.5f, 1.5f) * cloudSpeed;
            }

            initialized = true;
            Debug.Log($"CloudManager: {cloudCount} bulut olusturuldu");
        }

        private void MoveClouds(float deltaTime)
        {
            for (int i = 0; i < cloudPool.Length; i++)
            {
                if (cloudPool[i] == null) continue;

                // Basit X yonunde hareket
                Vector3 pos = cloudPool[i].position;
                pos.x += cloudSpeeds[i] * deltaTime;

                // Alan disina ciktiysa diger tarafa tasi
                float halfWidth = areaWidth / 2f;
                if (pos.x > areaCenter.x + halfWidth + 10f)
                {
                    pos.x = areaCenter.x - halfWidth - 10f;
                    pos.z = areaCenter.z + Random.Range(-areaDepth / 2f, areaDepth / 2f);
                    pos.y = Random.Range(minHeight, maxHeight);
                }

                cloudPool[i].position = pos;
            }
        }

        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                areaCenter.x + Random.Range(-areaWidth / 2f, areaWidth / 2f),
                Random.Range(minHeight, maxHeight),
                areaCenter.z + Random.Range(-areaDepth / 2f, areaDepth / 2f)
            );
        }

        public void SetCloudPrefabs(GameObject big, GameObject small)
        {
            cloudBigPrefab = big;
            cloudSmallPrefab = small;
        }

        public void SetArea(Vector3 center, float width, float depth)
        {
            areaCenter = center;
            areaWidth = width;
            areaDepth = depth;
        }

        /// <summary>
        /// Obje ve tum child'larina layer ata
        /// </summary>
        private void SetLayerRecursively(GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }
}
