using System;
using UnityEngine;

public class SpinAttackBehavior : MonoBehaviour
{
    public SkillScriptableObject weaponData;
    private WeaponController wc;
    [SerializeField] private float radius = 2.0f;
    [SerializeField] private float rotationSpeed = 50.0f;
    [SerializeField] private float spriteRotationSpeed = 360.0f;
    private float angle;
    [SerializeField] private float destroyDelay = 3f;
    private GameObject player;
    private float currentDamage;

    public void Start()
    {
        wc = FindObjectOfType<WeaponController>();
        Destroy(gameObject, destroyDelay);
        player = GameObject.FindGameObjectWithTag("Player");
        currentDamage = weaponData.Damage;
    }

    private void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        var x = player.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        var y = player.transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        transform.position = new Vector2(x, y);
        
        transform.Rotate(0, 0, spriteRotationSpeed * Time.deltaTime);
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
