using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySpawner;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //all enemies for this wave
        public int waveQuota;
        public float spawnInterval;
        public int SpawnCount;
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }
    public List<Wave> waves;
    public int currentWaveCount; //wave index

    [Header("Spawner Attributes")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReach = false;
    public float waveInterval;

    [Header("Spawn Points")]
    public List<Transform> relativeSpawnPoints;

    private bool waveStarted = false;
    int currentWaveQuota = 0;

    Transform player;

    public GameObject finalBossPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {
        if (!waveStarted && currentWaveCount < waves.Count && waves[currentWaveCount].waveQuota == currentWaveQuota)
        {
            StartCoroutine(BeginNextWave());
            waveStarted = true; // set waveStarted to true to prevent the coroutine from starting multiple times

        }

        spawnTimer += Time.deltaTime;
        if(spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
            waveStarted = false; // reset waveStarted to false so that the next wave can be started
        }
    }

    private void CalculateWaveQuota()
    {
        currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        //Debug.LogWarning(currentWaveQuota);

    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].SpawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReach)
        {
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive >= maxEnemiesAllowed) 
                    {
                        maxEnemiesReach = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].SpawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReach = false;
        }
    }

    public void SpawnSupremeSimian()
    {
        Debug.Log("Spawning Supreme Simian");
        //pass in monkey Supreme Simian prefab
        //how to end game once he dies
        //dilemma
        Instantiate(finalBossPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
