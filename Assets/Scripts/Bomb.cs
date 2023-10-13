using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ICollectible
{
    [SerializeField] private AudioSource bombSound;
    public float explosionRadius = 10f;
    public LayerMask enemyLayer;
    
    public void Collect()
    {
        Explode();
        bombSound.Play();
        Destroy(gameObject);
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
            }
        }
    }
}
