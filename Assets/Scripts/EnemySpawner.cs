using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public int waveMaxCount;
        public float spawnInterval;
        public int spawnCount;
        public List<EnemyGroup> enemyGroups;
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
    public int currentWaveCount;
    private Transform player;
    public string lastWaveName;
    public bool isLastWaveCompleted = false;
    
    [Header("Spawn Attributes")]
    private float spawnTimer;
    public float waveInterval;
    public int enemiesAlive;
    public bool maxEnemiesReached;
    public int maxEnemiesAllowed;

    [Header("Spawn position")] 
    public List<Transform> relativeSpawnPoints;

     private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveMaxCount();
    }

     private void Update()
     {
         if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
         {
             StartCoroutine(BeginNextWave());
         }
         spawnTimer += Time.fixedDeltaTime;

         if (spawnTimer >= waves[currentWaveCount].spawnInterval)
         {
             spawnTimer = 0f;
             SpawnEnemies();
         }
         
         if (enemiesAlive == -1 && waves[currentWaveCount].waveName == lastWaveName)
         {
             isLastWaveCompleted = true;
         }
     }

     IEnumerator BeginNextWave()
     {
         yield return new WaitForSeconds(waveInterval);

         if (currentWaveCount < waves.Count - 1)
         {
             currentWaveCount++;
             CalculateWaveMaxCount();
         }
     }

     private void CalculateWaveMaxCount()
    {
        var currentWaveMaxCount = 0;

        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveMaxCount += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveMaxCount = currentWaveMaxCount;
    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveMaxCount && !maxEnemiesReached)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                    
                    Instantiate(enemyGroup.enemyPrefab,
                        player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position,
                        Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
