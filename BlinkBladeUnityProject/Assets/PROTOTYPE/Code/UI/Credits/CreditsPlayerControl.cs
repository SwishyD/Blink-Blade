using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPlayerControl : MonoBehaviour
{
    private float moveInput;
    private float lastMoveInput;
    public float currentMoveSpeed;
    public float moveSpeedMax;
    public float timeZeroToMax;
    public float timeMaxToZero;
    float decelRatePerSec;
    float accelRatePerSec;

    Rigidbody2D rb;
    SpriteRenderer spriteRend;
    public bool canMove;

    private void Start()
    {
        accelRatePerSec = moveSpeedMax / timeZeroToMax;
        decelRatePerSec = -moveSpeedMax / timeMaxToZero;
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canMove)
        {
            HandleMove();
        }
        accelRatePerSec = moveSpeedMax / timeZeroToMax;
        decelRatePerSec = -moveSpeedMax / timeMaxToZero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (!spriteRend.flipX)
            {
                spriteRend.flipX = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (spriteRend.flipX)
            {
                spriteRend.flipX = false;
            }
        }
        if (transform.position.x < -9)
        {
            transform.position = new Vector3(8.9f, transform.position.y, transform.position.z);
        }
        if(transform.position.x > 9)
        {
            transform.position = new Vector3(-8.9f, transform.position.y, transform.position.z);
        }
    }

    public void HandleMove()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            currentMoveSpeed += accelRatePerSec * Time.deltaTime;
            currentMoveSpeed = Mathf.Min(currentMoveSpeed, moveSpeedMax);
            rb.velocity = new Vector2(moveInput * currentMoveSpeed, rb.velocity.y);
            lastMoveInput = moveInput;
        }
        else if (moveInput == 0)
        {
            currentMoveSpeed += decelRatePerSec * Time.deltaTime;
            currentMoveSpeed = Mathf.Max(currentMoveSpeed, 0);
            rb.velocity = new Vector2(lastMoveInput * currentMoveSpeed, rb.velocity.y);
        }
    }
}
