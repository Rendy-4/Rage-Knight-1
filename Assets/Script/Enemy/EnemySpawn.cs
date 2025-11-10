using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;
    public int maxSpawn = 5;
    public float timeUntilSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0f && CountEnemies() < maxSpawn)
        {
            Vector3 randPos = transform.position + (Vector3)Random.insideUnitCircle * 3f;
            Instantiate(enemyPrefab, randPos, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    public void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }

    int CountEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
