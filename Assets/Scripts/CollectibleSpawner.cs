using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [System.Serializable]
    public class CollectibleEntry
    {
        public GameObject prefab;
        public float minSpawnInterval = 10f;
        public float maxSpawnInterval = 20f;
        [HideInInspector] public float nextSpawnTime;
    }

    public CollectibleEntry[] collectibles;
    public Collider spawnBounds;
    public float spawnHeight = 0.5f;

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameActive) return;

        for (int i = 0; i < collectibles.Length; i++)
        {
            if (Time.time >= collectibles[i].nextSpawnTime)
            {
                SpawnCollectible(collectibles[i].prefab);
                collectibles[i].nextSpawnTime = Time.time + Random.Range(collectibles[i].minSpawnInterval, collectibles[i].maxSpawnInterval);
            }
        }
    }

    void SpawnCollectible(GameObject prefab)
    {
        Bounds b = spawnBounds.bounds;
        Vector3 pos = new Vector3(
            Random.Range(b.min.x, b.max.x),
            spawnHeight,
            Random.Range(b.min.z, b.max.z)
        );

        Instantiate(prefab, pos, Quaternion.identity);
    }
}
