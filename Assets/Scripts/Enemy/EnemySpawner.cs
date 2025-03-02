using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // The red cylinder prefab
    public Transform[] waypoints;        // Waypoints from start to end
    private float spawnInterval;     // Time between spawns
    private int maxSpawnCount;
    private int spawnCount;

    private float spawnTimer = 0f;

    void Start()
    {
        spawnInterval = 2f;
        maxSpawnCount = 5;
        spawnCount = 0;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && spawnCount < maxSpawnCount)
        {
            spawnTimer = 0f;
            SpawnEnemy();
            spawnCount++;
        }
    }

    void SpawnEnemy()
    {
        // Instantiate enemy at the first waypoint
        GameObject newEnemy = Instantiate(enemyPrefab, waypoints[0].position, Quaternion.identity);

        Enemy enemy_class = newEnemy.GetComponent<Enemy>();

        // Assign waypoints to the mover
        if (enemy_class != null)
        {
            enemy_class.waypoints = waypoints;
        }
    }

    public void FunnelEffect(int reducedSpawn, float slowerSpawnRate)
    {
        maxSpawnCount = maxSpawnCount - reducedSpawn;
        spawnInterval = spawnInterval * slowerSpawnRate;
    }
}