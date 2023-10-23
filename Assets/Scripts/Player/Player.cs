using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Vector3 directionToMouse;
    private Animator animator;
    [HideInInspector] public Vector2 movement;
    public PlayerStats stats;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
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

        rb.velocity = new Vector3(movement.x, movement.y, 0) * (Time.fixedDeltaTime * stats.initialSpeed);

        if (movement.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}