using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private Rigidbody2D rb;
    private GameObject player;
    public float scaleX = 1f;
    public float scaleY = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        var direction = player.transform.position - transform.position;

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-scaleX, scaleY, 1f);
        }
        else
        {
            transform.localScale = new Vector3(scaleX, scaleY, 1f);
        }

        rb.velocity = enemyData.MoveSpeed * Time.fixedDeltaTime * direction.normalized;

    }
}