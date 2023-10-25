using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private float currentMoveSpeed;
    [HideInInspector] public float currentHealth;
    private float currentDamage;
    [HideInInspector] public  float currentMaxHealth;
    public float despawnDistance = 20f;
    private Transform player;

    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.CurrentHealth;
        currentDamage = enemyData.Damage;
        currentMaxHealth = enemyData.MaxHealth;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        var enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
/*
    private void OnDestroy()
    {
        var enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
    }
*/
    void ReturnEnemy()
    {
        var es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position +
                             es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}