using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class BasicGravity : MonoBehaviour
{
    float Gravity;
    Rigidbody2D Rigid;
    GameObject thisone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Gravity = Rigid.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            Gravity = -Gravity;
            Rigid.gravityScale = Gravity;
        }
    }
}
