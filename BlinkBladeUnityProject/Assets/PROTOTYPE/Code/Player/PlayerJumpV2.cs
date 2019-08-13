using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpV2 : MonoBehaviour
{

    
    public float jumpVelocity;
    public float doubleJumpVelocity;
    public float groundedSkin = 0.05f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float defaultGrav = 3;
    public float maxVelocityDown = -20;
    public float boxOffset;

    public LayerMask mask;
    public Transform feetPos;

    public bool doubleJumpReady = false;
    bool jumpRequest;
    bool doubleJumpRequest;
    bool isGrounded;
    bool hasJumped;
    public bool isHanging;



    Vector2 playerSize;
    public Vector2 boxSize;

    Rigidbody2D rb;

    public SwordSpawner spawner;


    public static PlayerJumpV2 instance = null;

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

       
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x -0.03f , groundedSkin);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerNormal();
        ResetGravity();
        PlayerMovementV2.instance.canMove = true;
    }

    private void Update()
    {
        if (isGrounded)
        {
            hasJumped = false;
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            jumpRequest = true;
        }
        else if (isHanging)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ResetGravity();
                Jump();                
                isHanging = false;
                Destroy(spawner.cloneSword);
                spawner.cloneSword = null;
                spawner.swordSpawned = false;
            }
        }
        else if (((doubleJumpReady || !hasJumped) && !isGrounded) && (Input.GetKeyDown(KeyCode.W) ))
        {
            doubleJumpRequest = true;
        }

        if (isHanging == true && Input.GetKey(KeyCode.S))
        {
            ResetGravity();
            isHanging = false;
            Destroy(spawner.cloneSword);
            spawner.cloneSword = null;
            spawner.swordSpawned = false;
            doubleJumpReady = true;
        }

        if (isGrounded == false && Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocityDown);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            ResetGravity();
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log(rb.velocity.y);

        if (jumpRequest == true)
        {
            Jump();
        }
        else if(doubleJumpRequest == true)
        {
            ResetGravity();
            DoubleJump();
        }
        
        
        isGrounded = (Physics2D.OverlapBox(feetPos.position, boxSize, 0f, mask) != null);         
        

        

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
       
        if (rb.velocity.y < maxVelocityDown)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocityDown);
        }

    }


    void Jump()
    {
        rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        jumpRequest = false;
        isGrounded = false;
        doubleJumpReady = true;
        PlayerMovementV2.instance.canMove = true;
        hasJumped = true;
    }
    public void DoubleJump()
    {
        rb.AddForce(Vector2.up * doubleJumpVelocity, ForceMode2D.Impulse);
        doubleJumpRequest = false;
        doubleJumpReady = false;
        hasJumped = true;
    }

    public void ResetGravity()
    {
        PlayerMovementV2.instance.canMove = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = defaultGrav;
    }

    public void FreezePos()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        isHanging = true;
        PlayerMovementV2.instance.canMove = false;
        hasJumped = false;
    }

    public void PlayerNormal()
    {
        PlayerMovementV2.instance.canMove = true;
        isHanging = false;
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawCube(feetPos.position, boxSize);
    }
}
