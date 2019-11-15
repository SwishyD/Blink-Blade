using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cane : MonoBehaviour
{
    Rigidbody2D rb;

    public Vector2 force = new Vector2(-100,50);
    public float angVel = -20;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);
        rb.angularVelocity = angVel;
        Invoke("DestroySelf", 50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
