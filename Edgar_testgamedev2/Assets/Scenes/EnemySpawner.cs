using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public float initialSpawnInterval = 3f;
    public float minSpawnInterval = 0.2f;
    public float difficultyIncreaseRate = 0.1f;

    private float currentSpawnInterval;
    private float nextSpawnTime;
    private float nextDifficultyIncreaseTime;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        nextSpawnTime = Time.time + currentSpawnInterval;
        nextDifficultyIncreaseTime = Time.time + 10f;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + currentSpawnInterval;
        }

        if (Time.time >= nextDifficultyIncreaseTime)
        {
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - difficultyIncreaseRate);
            nextDifficultyIncreaseTime = Time.time + 10f;
        }
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[index].position, Quaternion.identity);
    }
}
