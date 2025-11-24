using UnityEngine;

public class Goomba_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform yr;
    public GameObject Sprite;
    private Transform spriteflipper;


    public float Spinspeed;
    private float MaxSpinspeed;
    public float StepCount;
    public float speed;
    public int direction = 1; // to flip direction of goomba when colliding with wall

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        yr = GetComponent<Transform>();
        spriteflipper = Sprite.GetComponent<Transform>();
        MaxSpinspeed = Spinspeed;
    }

    void Update()
    {
        Spinspeed -= Time.deltaTime;
        if (Spinspeed <= 0)
        {
            StepCount += 1;
            Spinspeed = MaxSpinspeed; 
            GoombaRotate();
        }

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
    private void GoombaRotate()
    {
        if (StepCount % 2 == 0)
        {
            spriteflipper.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            spriteflipper.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
