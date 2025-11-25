using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioState : MonoBehaviour
{
    // 1 = small, 2 = big (we're just using HP as lives/states)
    public int hp = 2;

    [Header("Stomp logic")]
    public float stompHeightOffset = 0.25f;   // how much higher Mario must be
    public float stompBounceVelocity = 8f;    // bounce after stomp

    [Header("Damage cooldown")]
    public float damageCooldown = 1f;         // seconds of invulnerability after hit
    private float lastHitTime = -999f;        // time of last damage

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
