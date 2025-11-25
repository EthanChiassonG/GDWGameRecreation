using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HamBroController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public GameObject hammerprefab;
    public Transform throwPoint;

    [Header("Horizontal Movement")]
    public float moveSpeed = 2f;
    //public float maxMoveSpeed = 2f;
    public float moveSwitchInterval = 2f;

    [Header("Jumping")]
    public float jumpForce = 7f;
    public float jumpInterval = 3.5f;

    [Header("Throwing")]
    public float throwInterval = 2.5f;
    public float throwForwardForce = 5f;
    public float throwUpwardForce = 3f;

    private float moveTimer = 0f;
    private float jumpTimer = 0f;
    private float throwTimer = 0f;
    private float moveDirection = 1f; // 1 = right, -1 = left

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
    private void Reset()
    {
        rb.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        moveTimer += Time.deltaTime;
        jumpTimer += Time.deltaTime;
        throwTimer += Time.deltaTime;

        if (moveTimer >= moveSwitchInterval)
        {
            moveDirection *= -1f;
            moveTimer = 0f;
            FlipSprite();
        }

        if (jumpTimer >= jumpInterval)
        {
            Jump();
            jumpTimer = 0;
        }

        if (throwTimer >= throwInterval)
        {
            ThrowHammer();
            throwTimer = 0f;
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void ThrowHammer()
    {
        if (hammerprefab == null || throwPoint == null) return;

        GameObject hammer = Instantiate(hammerprefab, throwPoint.position, Quaternion.identity);

        Rigidbody2D hammerRb = hammer.GetComponent<Rigidbody2D>();
        if (hammerRb != null)
        {
            Vector2 force = new Vector2(moveDirection * throwForwardForce, throwUpwardForce);
            hammerRb.AddForce(force, ForceMode2D.Impulse);
        }
    }
    private void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * moveDirection;
        transform.localScale = scale;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
