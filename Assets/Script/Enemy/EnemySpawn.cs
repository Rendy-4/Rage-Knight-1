using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefab;
    public int startEnemiesPerWave = 5;  // jumlah musuh di wave pertama
    public int increasePerWave = 2;      // tambahan musuh tiap wave baru

    [Header("Spawn Settings")]
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 1f;
    public float RandomRangeX = 4f;
    public float RandomRangeY = 4f;

    [Header("Wave Settings")]
    public float timeBetweenWaves = 2f;
    private int currentWave = 0;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private bool spawningWave = false;

    void Start()
    {
        StartCoroutine(SpawnWaveLoop());
    }

    IEnumerator SpawnWaveLoop()
    {
        while (true)
        {
            // Bersihkan musuh null
            spawnedEnemies.RemoveAll(enemy => enemy == null);

            // Jika semua musuh dari wave sebelumnya sudah mati
            if (spawnedEnemies.Count == 0 && !spawningWave)
            {
                currentWave++;
                Debug.Log($"🌊 Wave {currentWave} Started! 🌊");

                spawningWave = true;
                yield return StartCoroutine(SpawnWave(currentWave));
                spawningWave = false;

                Debug.Log($"✅ Wave {currentWave} Completed!");
                yield return new WaitForSeconds(timeBetweenWaves);
            }

            yield return null;
        }
    }

    IEnumerator SpawnWave(int waveNumber)
    {
        // Jumlah musuh untuk wave ini meningkat setiap wave
        int enemiesThisWave = startEnemiesPerWave + (increasePerWave * (waveNumber - 1));

        for (int i = 0; i < enemiesThisWave; i++)
        {
            float randomX = Random.Range(-RandomRangeX, RandomRangeX);
            float randomY = Random.Range(-RandomRangeY, RandomRangeY);

            Vector3 randPos = new Vector3(
                transform.position.x + randomX,
                transform.position.y + randomY,
                0f);

            GameObject randomEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)]; 
            GameObject newEnemy = Instantiate(randomEnemy, randPos, Quaternion.identity);
            spawnedEnemies.Add(newEnemy);

            // jeda antar spawn biar gak langsung muncul semua
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(RandomRangeX * 2, RandomRangeY * 2, 0f));
    }
}
