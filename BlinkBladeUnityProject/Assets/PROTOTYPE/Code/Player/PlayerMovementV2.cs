using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{
    private PlayerAnimator playerAnim;

    private float moveInput;
    private float lastMoveInput;
    public float currentMoveSpeed;
    public float moveSpeedMax;
    public float timeZeroToMax;
    public float timeMaxToZero;
    float decelRatePerSec;
    float accelRatePerSec;


    

    public bool canMove;

    Rigidbody2D rb;

    public SwordSpawner spawner;

    public static PlayerMovementV2 instance = null;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        accelRatePerSec = moveSpeedMax / timeZeroToMax;
        decelRatePerSec = -moveSpeedMax / timeMaxToZero;
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        canMove = true;
        playerAnim = GetComponentInChildren<PlayerAnimator>();
    }

    private void Update()
    {
        if (canMove)
        {
            HandleMove();

        }
        accelRatePerSec = moveSpeedMax / timeZeroToMax;
        decelRatePerSec = -moveSpeedMax / timeMaxToZero;
    }    

    public void HandleMove()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if(moveInput != 0)
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
