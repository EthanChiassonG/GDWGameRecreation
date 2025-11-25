using UnityEngine;

public class FireballBehavior : MonoBehaviour
{
    private Rigidbody2D FireRB;
    public float travelspeed;
    public float lifetime;

    private void Start()
    {
        FireRB = GetComponent<Rigidbody2D>();
        FireRB.AddForce(Vector2.right * travelspeed, ForceMode2D.Force);
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        FireRB.AddForce(Vector2.right * travelspeed, ForceMode2D.Force);
        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
