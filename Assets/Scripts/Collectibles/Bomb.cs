using UnityEngine;

public class Bomb : MonoBehaviour, ICollectible
{
    public float explosionRadius = 10f;
    public CircleCollider2D col;
    public SpriteRenderer sprite;
    private AudioManager audioManager;
    private ExplodeEffect flashScreen;

    public void Awake()
    {
        flashScreen = FindObjectOfType<ExplodeEffect>();
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
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= explosionRadius)
            {
                var enemyStats = enemy.GetComponent<EnemyStats>();
                var maxDamage = 50f;
                var damage = maxDamage * (1 - distanceToEnemy / explosionRadius);
                
                if (enemyStats.currentHealth - damage <= 0)
                {
                    enemyStats.IsExploded = true;
                    var enemyAnimator = enemy.GetComponent<Animator>();
                    if (enemyAnimator != null)
                    {
                        enemyAnimator.SetBool("isExploded", true);
                    }
                }
                
                enemyStats.TakeDamage(damage);
                audioManager.PlayGameSound(audioManager.bomb);
            }
        }
        
        if(flashScreen != null) flashScreen.StartFlash();
    }
}
