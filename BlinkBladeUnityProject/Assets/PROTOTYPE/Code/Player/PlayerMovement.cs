using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance = null;

    public SwordSpawner spawner;

    public bool canMove;

    public float speed;
    public Rigidbody2D _rb;

    private float moveInput;
    public float jumpForce;
    public float doubleJumpForce;

    public bool doubleJumpReady = false;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public bool isHanging = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            HandleMove();
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if(isGrounded == true && Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        else if(isHanging)
        {
            this.transform.position = SwordSpawner.instance.CloneSword.transform.position;
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
                isHanging = false;
                Destroy(spawner.CloneSword);
                spawner.CloneSword = null;
                spawner.swordSpawned = false;
            }
        }
        else if(doubleJumpReady == true && Input.GetKeyDown(KeyCode.W))
        {
            DoubleJump();
        }

        //Quick Fall
        if (isGrounded == false && Input.GetKey(KeyCode.S))
        {
            _rb.gravityScale = 10;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            ResetGravity();
        }

        if(isHanging == true && Input.GetKey(KeyCode.S))
        {
            ResetGravity();
            isHanging = false;
            Destroy(spawner.CloneSword);
            spawner.CloneSword = null;
            spawner.swordSpawned = false;
        }
    }

    public void HandleMove()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(moveInput * speed, _rb.velocity.y);
    }

    public void Jump()
    {
        ResetGravity();
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.velocity = Vector2.up * jumpForce;
        doubleJumpReady = true;
    }

    public void DoubleJump()
    {
        ResetGravity();
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.velocity = Vector2.up * doubleJumpForce;
        doubleJumpReady = false;
    }

    public void ResetGravity()
    {
        canMove = true;
        _rb.gravityScale = 5;
    }

    public void FreezePos()
    {
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = 0;
        isHanging = true;
        canMove = false;
    }

    public void PlayerNormal()
    {
        canMove = true;
        isHanging = false;
    }
}
