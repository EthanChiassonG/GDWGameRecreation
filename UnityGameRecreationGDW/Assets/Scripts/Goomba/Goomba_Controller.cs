using UnityEngine;

public class Goomba_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform yr;
    public float speed;
    public int direction = 1; // to flip direction of goomba when colliding with wall

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        yr = GetComponent<Transform>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1; // Flip direction
            yr.Rotate(0f, 180f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
