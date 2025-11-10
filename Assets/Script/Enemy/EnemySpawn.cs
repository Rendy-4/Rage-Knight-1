using UnityEngine;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;
    public int maxEnemies = 5;
    public float RandomRangeX = 3f;
    public float RandomRangeY = 3f;

    private float timeUntilSpawn;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        // Hapus musuh yang sudah mati (destroyed)
        spawnedEnemies.RemoveAll(enemy => enemy == null);

        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0f && spawnedEnemies.Count < maxEnemies)
        {
            float randomX = Random.Range(-RandomRangeX, RandomRangeX);
            float randomY = Random.Range(-RandomRangeY, RandomRangeY);

            Vector3 randPos = new Vector3(
                transform.position.x + randomX,
                transform.position.y + randomY,
                0f);

            GameObject newEnemy = Instantiate(enemyPrefab, randPos, Quaternion.identity);
            spawnedEnemies.Add(newEnemy);

            SetTimeUntilSpawn();
        }
    }

    void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(RandomRangeX * 2, RandomRangeY * 2, 0f));
    }
}
