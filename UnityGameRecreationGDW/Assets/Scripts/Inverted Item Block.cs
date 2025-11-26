using System;
using UnityEngine;

public class InvertedItemBlock : MonoBehaviour
{
    public GameObject Contains;
    public Transform Spawnspot;
    private bool Used = false;


    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.name.Contains("MarioBody"))
        {
            float boxY = transform.position.y;
            float marioY = other.transform.position.y;

            if (boxY < marioY && !Used)
            {
                Instantiate(Contains, (Spawnspot));
                Used = true;
            }
        }
    }
}
