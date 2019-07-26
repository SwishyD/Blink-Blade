using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    public float speed;
    public float JumpForce;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector2(-speed, 0f);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            _rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rb.velocity = new Vector2(speed, 0f);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            _rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(new Vector2(0, JumpForce));
        }
    }
}
