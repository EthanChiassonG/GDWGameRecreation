using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioState : MonoBehaviour
{
    // 1 = small, 2 = big (we're just using HP as lives/states)
    public int hp = 2;

    [Header("Stomp logic")]
    public float stompHeightOffset = 0.25f;   // how much higher Mario must be
    public float stompBounceVelocity = 8f;    // bounce after stomp

    public GameObject Fireball;
    public GameObject Mario;
    public Vector3 Offset = new Vector3(0, 1, 0);
    public Vector3 InvertedOffset = new Vector3(0, -2, 0);

    [Header("Damage cooldown")]
    public float damageCooldown = 1f;         // seconds of invulnerability after hit
    private float lastHitTime = -999f;        // time of last damage

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && hp >= 3)
        {
            ShootFireball();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        // Only handle Goombas for now
        if (other.name.Contains("Goomba"))
        {
            float marioY = transform.position.y;
            float goombaY = other.transform.position.y;

            // Mario clearly above Goomba -> stomp
            if (marioY > goombaY + stompHeightOffset)
            {
                Destroy(other);

                // Bounce Mario upward a bit
                if (rb != null)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, stompBounceVelocity);
                }
            }
            else
            {
                // Side / bottom hit -> damage Mario (with cooldown)
                TakeDamage();
            }

        }
        if (other.CompareTag("Mushroom"))
        {
            if (hp <= 2)
            {
                hp = 2;
            }
            Destroy(other);
        }
        if (other.CompareTag("FireFlower"))
        {
            if (hp <= 3)
            {
                hp = 3;
            }
            Destroy(other);
        }

        if (collision.gameObject.CompareTag("Kill"))
        {
            Die();
        }
    }

    private void TakeDamage()
    {
        // If still in invulnerability window, ignore the hit
        if (Time.time < lastHitTime + damageCooldown)
        {
            Debug.Log("Hit ignored (invulnerability).");
            return;
        }

        lastHitTime = Time.time;

        hp--;
        Debug.Log("Mario hit. HP now: " + hp);

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Mario died, reloading scene.");
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    private void ShootFireball()
    {
        Vector3 spawnPointPosition = transform.position;

        if(rb.gravityScale <= 0)
        {
            Vector3 finalSpawnPosition = spawnPointPosition + InvertedOffset;
            Instantiate(Fireball, finalSpawnPosition, transform.rotation);
        }
        else
        {
            Vector3 finalSpawnPosition = spawnPointPosition + Offset;
            Instantiate(Fireball, finalSpawnPosition, transform.rotation);
        }
        
    }

}
