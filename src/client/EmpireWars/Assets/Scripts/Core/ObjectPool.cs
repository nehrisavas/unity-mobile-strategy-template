using UnityEngine;
using System;
using System.Collections.Generic;

namespace EmpireWars.Core
{
    /// <summary>
    /// Generic Object Pool sistemi
    /// GC pressure azaltmak için GameObject'leri yeniden kullanır
    /// </summary>
    /// <typeparam name="T">Pool edilecek component tipi</typeparam>
    public class ObjectPool<T> where T : Component
    {
        private readonly Queue<T> pool;
        private readonly Func<T> createFunc;
        private readonly Action<T> onGet;
        private readonly Action<T> onRelease;
        private readonly Action<T> onDestroy;
        private readonly Transform parent;
        private readonly int maxSize;

        private int activeCount;
        private int totalCreated;

        /// <summary>
        /// Pool'daki mevcut obje sayısı
        /// </summary>
        public int CountInPool => pool.Count;

        /// <summary>
        /// Aktif (kullanımda) obje sayısı
        /// </summary>
        public int CountActive => activeCount;

        /// <summary>
        /// Toplam oluşturulan obje sayısı
        /// </summary>
        public int CountTotal => totalCreated;

        /// <summary>
        /// Yeni ObjectPool oluştur
        /// </summary>
        /// <param name="createFunc">Yeni obje oluşturma fonksiyonu</param>
        /// <param name="onGet">Obje alındığında çağrılır (opsiyonel)</param>
        /// <param name="onRelease">Obje geri verildiğinde çağrılır (opsiyonel)</param>
        /// <param name="onDestroy">Obje yok edildiğinde çağrılır (opsiyonel)</param>
        /// <param name="parent">Objelerin parent'ı (opsiyonel)</param>
        /// <param name="initialSize">Başlangıç pool boyutu</param>
        /// <param name="maxSize">Maksimum pool boyutu (0 = sınırsız)</param>
        public ObjectPool(
            Func<T> createFunc,
            Action<T> onGet = null,
            Action<T> onRelease = null,
            Action<T> onDestroy = null,
            Transform parent = null,
            int initialSize = 0,
            int maxSize = 0)
        {
            this.pool = new Queue<T>();
            this.createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            this.onGet = onGet;
            this.onRelease = onRelease;
            this.onDestroy = onDestroy;
            this.parent = parent;
            this.maxSize = maxSize;

            // Pre-warm pool
            Prewarm(initialSize);
        }

        /// <summary>
        /// Pool'u belirtilen sayıda obje ile doldur (warm-up)
        /// </summary>
        public void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T obj = CreateNew();
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        /// <summary>
        /// Pool'dan obje al (yoksa yeni oluştur)
        /// </summary>
        public T Get()
        {
            T obj;

            if (pool.Count > 0)
            {
                obj = pool.Dequeue();

                // Null check (sahne değişimi vs)
                if (obj == null)
                {
                    obj = CreateNew();
                }
            }
            else
            {
                obj = CreateNew();
            }

            obj.gameObject.SetActive(true);
            activeCount++;
            onGet?.Invoke(obj);

            return obj;
        }

        /// <summary>
        /// Objeyi pool'a geri ver
        /// </summary>
        public void Release(T obj)
        {
            if (obj == null) return;

            onRelease?.Invoke(obj);
            obj.gameObject.SetActive(false);
            activeCount--;

            // Max size kontrolü
            if (maxSize > 0 && pool.Count >= maxSize)
            {
                // Pool dolu, objeyi yok et
                onDestroy?.Invoke(obj);
                UnityEngine.Object.Destroy(obj.gameObject);
                return;
            }

            pool.Enqueue(obj);
        }

        /// <summary>
        /// Pool'daki tüm objeleri yok et
        /// </summary>
        public void Clear()
        {
            while (pool.Count > 0)
            {
                T obj = pool.Dequeue();
                if (obj != null)
                {
                    onDestroy?.Invoke(obj);
                    UnityEngine.Object.Destroy(obj.gameObject);
                }
            }

            activeCount = 0;
        }

        private T CreateNew()
        {
            T obj = createFunc();

            if (parent != null && obj != null)
            {
                obj.transform.SetParent(parent);
            }

            totalCreated++;
            return obj;
        }
    }

    /// <summary>
    /// GameObject için basit pool (component gerektirmez)
    /// </summary>
    public class GameObjectPool
    {
        private readonly Queue<GameObject> pool;
        private readonly GameObject prefab;
        private readonly Transform parent;
        private readonly int maxSize;

        private int activeCount;
        private int totalCreated;

        public int CountInPool => pool.Count;
        public int CountActive => activeCount;
        public int CountTotal => totalCreated;

        public GameObjectPool(GameObject prefab, Transform parent = null, int initialSize = 0, int maxSize = 0)
        {
            this.pool = new Queue<GameObject>();
            this.prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
            this.parent = parent;
            this.maxSize = maxSize;

            Prewarm(initialSize);
        }

        public void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject obj = CreateNew();
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public GameObject Get()
        {
            GameObject obj;

            if (pool.Count > 0)
            {
                obj = pool.Dequeue();
                if (obj == null)
                {
                    obj = CreateNew();
                }
            }
            else
            {
                obj = CreateNew();
            }

            obj.SetActive(true);
            activeCount++;
            return obj;
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            GameObject obj = Get();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }

        public void Release(GameObject obj)
        {
            if (obj == null) return;

            obj.SetActive(false);
            activeCount--;

            if (maxSize > 0 && pool.Count >= maxSize)
            {
                UnityEngine.Object.Destroy(obj);
                return;
            }

            pool.Enqueue(obj);
        }

        public void Clear()
        {
            while (pool.Count > 0)
            {
                GameObject obj = pool.Dequeue();
                if (obj != null)
                {
                    UnityEngine.Object.Destroy(obj);
                }
            }
            activeCount = 0;
        }

        private GameObject CreateNew()
        {
            GameObject obj = UnityEngine.Object.Instantiate(prefab, parent);
            totalCreated++;
            return obj;
        }
    }

    /// <summary>
    /// Tile Pool Manager - ChunkedTileLoader için özelleştirilmiş pool
    /// </summary>
    public class TilePoolManager : MonoBehaviour
    {
        public static TilePoolManager Instance { get; private set; }

        [Header("Pool Ayarları")]
        [SerializeField] private int initialPoolSize = 512;
        [SerializeField] private int maxPoolSize = 2048;

        private Dictionary<string, GameObjectPool> tilePools;
        private Transform poolParent;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Initialize()
        {
            tilePools = new Dictionary<string, GameObjectPool>();

            // Pool parent oluştur (hierarchy temiz kalsın)
            GameObject parentObj = new GameObject("TilePool");
            parentObj.transform.SetParent(transform);
            poolParent = parentObj.transform;
        }

        /// <summary>
        /// Prefab için pool oluştur veya mevcut olanı döndür
        /// </summary>
        public GameObjectPool GetOrCreatePool(GameObject prefab)
        {
            string key = prefab.name;

            if (!tilePools.TryGetValue(key, out GameObjectPool pool))
            {
                pool = new GameObjectPool(prefab, poolParent, 0, maxPoolSize);
                tilePools[key] = pool;
            }

            return pool;
        }

        /// <summary>
        /// Prefab'dan tile al (pool'dan veya yeni oluştur)
        /// </summary>
        public GameObject GetTile(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObjectPool pool = GetOrCreatePool(prefab);
            return pool.Get(position, rotation);
        }

        /// <summary>
        /// Tile'ı pool'a geri ver
        /// </summary>
        public void ReleaseTile(GameObject tile)
        {
            if (tile == null) return;

            // Hangi pool'a ait bul
            string baseName = tile.name.Replace("(Clone)", "").Trim();

            if (tilePools.TryGetValue(baseName, out GameObjectPool pool))
            {
                pool.Release(tile);
            }
            else
            {
                // Pool bulunamadı, destroy et
                Destroy(tile);
            }
        }

        /// <summary>
        /// Belirli prefab için pool'u önceden doldur
        /// </summary>
        public void PrewarmPool(GameObject prefab, int count)
        {
            GameObjectPool pool = GetOrCreatePool(prefab);
            pool.Prewarm(count);
        }

        /// <summary>
        /// Tüm pool'ları temizle
        /// </summary>
        public void ClearAllPools()
        {
            foreach (var pool in tilePools.Values)
            {
                pool.Clear();
            }
            tilePools.Clear();
        }

        /// <summary>
        /// Pool istatistiklerini döndür
        /// </summary>
        public string GetPoolStats()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("=== Tile Pool Stats ===");

            int totalInPool = 0;
            int totalActive = 0;

            foreach (var kvp in tilePools)
            {
                sb.AppendLine($"{kvp.Key}: Pool={kvp.Value.CountInPool}, Active={kvp.Value.CountActive}");
                totalInPool += kvp.Value.CountInPool;
                totalActive += kvp.Value.CountActive;
            }

            sb.AppendLine($"TOTAL: Pool={totalInPool}, Active={totalActive}");
            return sb.ToString();
        }

        private void OnDestroy()
        {
            ClearAllPools();
        }
    }
}
