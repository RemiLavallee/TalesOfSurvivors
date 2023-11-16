using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStats : PoolObject
{
    public EnemyScriptableObject enemyData;
    private float currentMoveSpeed;
    [HideInInspector] public float currentHealth;
    private float currentDamage;
    [HideInInspector] public  float currentMaxHealth;
    public float despawnDistance = 20f;
    private Transform player;
    [SerializeField] private GameObject healthBar;
    public bool IsExploded { get; set; }
    private DropRateManager drop;
    private DamageFlash damageFlash;
    private Animator animator;

    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.CurrentHealth;
        currentDamage = enemyData.Damage;
        currentMaxHealth = enemyData.MaxHealth;
    }

    private void Start()
    {
        drop = GetComponent<DropRateManager>();
        player = FindObjectOfType<PlayerStats>().transform;
        damageFlash = GetComponent<DamageFlash>();
        animator = GetComponent<Animator>();
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
            return;
        }

        animator.SetTrigger("isHit");
        damageFlash.CallHitFlash();
    }

    private void Kill()
    {
        var enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();

        if (IsExploded && currentHealth <= 0)
        {
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            drop.OnDestroy();
           gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    private void ReturnEnemy()
    {
        var es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
    
    private IEnumerator DestroyAfterAnimation()
    {
        var enemyAnimator = GetComponent<Animator>();
        if (enemyAnimator != null)
        {
            healthBar.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
        drop.OnDestroy();
        gameObject.SetActive(false);
    }

    public override void Reset()
    {
        currentHealth = currentMaxHealth;
    }
    
}