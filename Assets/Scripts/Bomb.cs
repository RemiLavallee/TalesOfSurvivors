using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ICollectible
{
    public float explosionRadius = 10f;
    public CircleCollider2D col;
    public SpriteRenderer sprite;
    private AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    public void Collect()
    { 
        Explode();
        col.enabled = false;
        sprite.enabled = false;
        Destroy(gameObject,2f);
    }

    private void Explode()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= explosionRadius)
            {
                var enemyStats = enemy.GetComponent<EnemyStats>();
                float maxDamage = 50f;
                float damage = maxDamage * (1 - distanceToEnemy / explosionRadius);
                enemyStats.TakeDamage(damage);
                audioManager.PlayGameSound(audioManager.bomb);
            }
        }
    }
}
