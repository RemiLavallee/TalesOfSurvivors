using UnityEngine;
using UnityEngine.SceneManagement;

public class SharkAttackBehavior : MonoBehaviour
{
    [SerializeField] private SkillScriptableObject weaponData;
    [SerializeField] private float destroyDelay;
    [SerializeField] private float initialAngleInDegrees = 45f;
    private Vector2 direction;
    private float currentDamage;
    private float screenHalfWidth, screenHalfHeight;
    private WeaponController wc;
    [SerializeField] private float offsetY;
    
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
        CheckAndBounceX();
        CheckAndBounceY();
    }
    
    private void CheckAndBounceX()
    {
        var cameraX = Camera.main.transform.position.x;
        if ((transform.position.x > cameraX + screenHalfWidth && direction.x > 0) ||
            (transform.position.x < cameraX - screenHalfWidth && direction.x < 0))
        {
            direction.x = -direction.x;
        }
    }

    private void CheckAndBounceY()
    {
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            if ((transform.position.y > offsetY && direction.y > 0) ||
                (transform.position.y < -offsetY && direction.y < 0))
            {
                direction.y = -direction.y;
            }
        }
        else
        {
            var cameraY = Camera.main.transform.position.y;
            if ((transform.position.y > cameraY + screenHalfHeight && direction.y > 0) ||
                (transform.position.y < cameraY - screenHalfHeight && direction.y < 0))
            {
                direction.y = -direction.y;
            }
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
