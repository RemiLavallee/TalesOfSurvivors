using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAttackBehavior : MonoBehaviour
{
    public SkillScriptableObject weaponData;
    [SerializeField] private float destroyDelay;
    [SerializeField] private float initialAngleInDegrees = 45f;
    private Vector2 direction;
    private float currentDamage;
    private float screenHalfWidth, screenHalfHeight;
    private WeaponController wc;
    
    private void Awake()
    {
        currentDamage = weaponData.Damage;
        float initialAngleInRadians = initialAngleInDegrees * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(initialAngleInRadians), Mathf.Sin(initialAngleInRadians));
        wc = FindObjectOfType<WeaponController>();
    }
    
    private void Update()
    {
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        screenHalfHeight = Camera.main.orthographicSize;
        
        transform.position += (Vector3) direction * (weaponData.Speed * Time.deltaTime);

        BounceOffScreen();

        AdjustSpriteDirection();

        Destroy(gameObject, destroyDelay);
    }
    
    private void BounceOffScreen()
    {
        var cameraX = Camera.main.transform.position.x;
        var cameraY = Camera.main.transform.position.y;
        
        if (transform.position.x > cameraX + screenHalfWidth && direction.x > 0)
        {
            direction.x = -direction.x;
        }

        if (transform.position.x < cameraX - screenHalfWidth && direction.x < 0)
        {
            direction.x = -direction.x;
        }

        if (transform.position.y > cameraY + screenHalfHeight && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        if (transform.position.y < cameraY - screenHalfHeight && direction.y < 0)
        {
            direction.y = -direction.y;
        }
    }

    private void AdjustSpriteDirection()
    {
        var localScale = transform.localScale;

        if (direction.x < 0)
        {
            localScale.x = Mathf.Abs(localScale.x);
        }

        else if (direction.x > 0)
        {
            localScale.x = -Mathf.Abs(localScale.x);
        }

        transform.localScale = localScale;
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
