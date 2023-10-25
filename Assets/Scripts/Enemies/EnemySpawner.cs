using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public string waveName;
        public int waveQuota;
        public float spawnInterval;
        public int spawnCount;
        public List<EnemyGroup> enemyGroups;
    }

    [Serializable]
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
    
    [Header("Spawn Attributes")]
    private float spawnTimer;
    public float waveInterval;
    public int enemiesAlive;
    public bool maxEnemiesReached;
    public int maxEnemiesAllowed;
    private bool waitingForNextWave = false;
    

    [Header("Spawn position")] 
    public List<Transform> relativeSpawnPoints;

     private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveMaxCount();
    }

     private void Update()
     {
         if (waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota && !waitingForNextWave)
         {
             StartCoroutine(BeginNextWave());
         }
         spawnTimer += Time.deltaTime;

         if (spawnTimer >= waves[currentWaveCount].spawnInterval)
         {
             spawnTimer = 0f;
             SpawnEnemies();
         }
     }

     public IEnumerator BeginNextWave()
     {
         waitingForNextWave = true;
         yield return new WaitForSeconds(waveInterval);

         if (currentWaveCount < waves.Count - 1)
         {
             currentWaveCount++;
             CalculateWaveMaxCount();
         }
         waitingForNextWave = false;
     }

     private void CalculateWaveMaxCount()
    {
        var currentWaveMaxCount = 0;

        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveMaxCount += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveMaxCount;
    }

     void SpawnEnemies()
     {
         foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
         {
             if (enemyGroup.spawnCount < enemyGroup.enemyCount && enemiesAlive < maxEnemiesAllowed)
             {
                 Instantiate(enemyGroup.enemyPrefab,
                     player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position,
                     Quaternion.identity);

                 enemyGroup.spawnCount++;
                 waves[currentWaveCount].spawnCount++;
                 enemiesAlive++;
             }
         }

         maxEnemiesReached = (enemiesAlive >= maxEnemiesAllowed);
     }

     public void OnEnemyKilled()
     {
         enemiesAlive--;
         enemiesAlive = Mathf.Max(0, enemiesAlive);
     }
}
