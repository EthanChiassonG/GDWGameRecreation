using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float damage = 0.5f;
    public float lifeTime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
