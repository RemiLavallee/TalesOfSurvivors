using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Vector3 directionToMouse;
    private Animator animator;
    [HideInInspector] public Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private Sprite idleSprite;
    private bool isFacingRight = true;
    public PlayerStats stats;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        idleSprite = spriteRenderer.sprite;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }
        
        var camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        directionToMouse = camPos - transform.position;
        directionToMouse.z = 0;
        directionToMouse.Normalize();
    }

    private void FixedUpdate()
    {
        animator.SetBool("isWalking", Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.y) > 0);

        rb.velocity = new Vector3(movement.x, movement.y, 0) * (Time.fixedDeltaTime * stats.speed);

        if (movement.x < 0)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x > 0)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}