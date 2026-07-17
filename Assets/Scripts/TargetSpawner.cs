using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [System.Serializable]
    public class TargetEntry
    {
        public GameObject prefab;
        public float minSpawnInterval = 1f;
        public float maxSpawnInterval = 4f;
        [HideInInspector] public float nextSpawnTime;
    }

    [Header("Spawn Settings")]
    public TargetEntry[] targets;
    public Collider spawnBounds;
    public float spawnHeight = 1.5f;

    void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameActive) return;

        for (int i = 0; i < targets.Length; i++)
        {
            if (Time.time >= targets[i].nextSpawnTime)
            {
                SpawnTarget(targets[i].prefab);
                targets[i].nextSpawnTime = Time.time + Random.Range(targets[i].minSpawnInterval, targets[i].maxSpawnInterval);
            }
        }
    }

    void SpawnTarget(GameObject prefab)
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
