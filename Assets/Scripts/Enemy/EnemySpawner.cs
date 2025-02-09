using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // The red cylinder prefab
    public Transform[] waypoints;        // Waypoints from start to end
    public float spawnInterval = 2f;     // Time between spawns
    public int maxSpawnCount = 5;
    public int spawnCount = 0;

    private float spawnTimer = 0f;

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

        // Assign waypoints to the mover
        EnemyMover mover = newEnemy.GetComponent<EnemyMover>();
        if (mover != null)
        {
            mover.waypoints = waypoints;
        }
    }
}