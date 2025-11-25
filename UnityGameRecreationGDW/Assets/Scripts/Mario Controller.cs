using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class MarioController : MonoBehaviour
{
    Rigidbody2D marioRigid;
    Transform Mariotrans;
    float Gravity;
    public float moveSpeed;
    public float RunMultiplier;
    public float Runinput;
    public float JumpForce;
    public float MaxSpeed;

    private bool m_IsGrounded = false;
    private bool JumpInput;
    private bool GravityFlip = false;

    void Start()
    {
        marioRigid = GetComponent<Rigidbody2D>();
        Mariotrans = GetComponent<Transform>();
        Gravity = marioRigid.gravityScale;
    }


    void Update()
    {
        Runinput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W))
        {
            JumpInput = true;
        }
        else
        {
            JumpInput = false;
        }

        if (Runinput == 0 && marioRigid.linearVelocityY == 0)
        {
            marioRigid.linearDamping = 8;
        }
        else
        {
            marioRigid.linearDamping = 1;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            GravityFlip = true;
        }
        else
        {
            GravityFlip = false;
        }
    }


    private void FixedUpdate()
    {
        if (Runinput != 0) 
        {
            Movement();
        }

        if (JumpInput && m_IsGrounded)
        {
            Jumping();
        }

        if (GravityFlip && m_IsGrounded)
        {
            GravityFlipper();
        }
    }
    void Movement()
    {
        if (Mathf.Abs(marioRigid.linearVelocityX) >= MaxSpeed)
        {
            return;
        }
        if (Runinput > 0 && marioRigid.gravityScale > 0)
        {
            marioRigid.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);
            Mariotrans.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Runinput > 0 && marioRigid.gravityScale < 0)
        {
            marioRigid.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);
            Mariotrans.rotation = Quaternion.Euler(0, 180, 180);
        }
        if (Runinput < 0)
        {
            marioRigid.AddForce(Vector2.left * moveSpeed, ForceMode2D.Force);
            Mariotrans.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Runinput < 0)
        {
            marioRigid.AddForce(Vector2.left * moveSpeed, ForceMode2D.Force);
            Mariotrans.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    void Jumping()
    {

        if(Gravity > 0)
        {
        marioRigid.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        m_IsGrounded = false;
        }
        
        if (Gravity < 0)
        {
            marioRigid.AddForce(Vector2.down * JumpForce, ForceMode2D.Impulse);
            m_IsGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_IsGrounded = true;
    }

    void GravityFlipper()
    {
        Gravity = -Gravity;
        marioRigid.gravityScale = Gravity;
        m_IsGrounded = false;
    }
}
