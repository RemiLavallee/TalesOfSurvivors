using UnityEngine;

public class PlayerMainAttack : MonoBehaviour
{
    public SkillScriptableObject skillData;
    [SerializeField] private float destroyDelay;
    private float currentDamage;

    private void Awake()
    {
        currentDamage = skillData.Damage ;
    }

    private void Update()
    {
        transform.position += transform.right * (6f * Time.deltaTime);
        Destroy(gameObject, destroyDelay);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
    }
}