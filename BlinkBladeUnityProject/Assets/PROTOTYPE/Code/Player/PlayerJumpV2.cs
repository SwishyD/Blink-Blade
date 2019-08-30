using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpV2 : MonoBehaviour
{
    private PlayerAnimator playerAnim;
    
    //Jump
    public float jumpVelocity;
    public float doubleJumpVelocity;
    public float groundedSkin = 0.05f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float defaultGrav = 3f;
    public float maxVelocityDown = -20f;
    public float quickFallMaxVelocityDown = -40f;
    public float boxOffset;
    static float t = 0.0f;
    public float currentVelocityDown;
        
    public LayerMask mask;
    public Transform feetPos;

    public bool doubleJumpReady = false;
    bool jumpRequest;
    bool doubleJumpRequest;
    bool isGrounded;
    bool hasJumped;
    public bool isHanging;
    public bool isQuickFalling;



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
        playerAnim = GetComponentInChildren<PlayerAnimator>();
        PlayerMovementV2.instance.canMove = true;
    }

    private void Update()
    {
        currentVelocityDown = rb.velocity.y;

        if (isGrounded)
        {
            hasJumped = false;
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            jumpRequest = true;
        }
        else if (isHanging)
        {
            if (!spawner.closeToGround && !spawner.stuckDown)
            {
                transform.position = spawner.cloneSword.transform.GetChild(0).transform.position;
            }
            else if (spawner.closeToGround && !spawner.stuckDown)
            {
                transform.position = spawner.cloneSword.transform.GetChild(0).transform.position + new Vector3(0, 1f, 0);
            }
            else if (spawner.closeToGround && spawner.stuckDown)
            {
                transform.position = spawner.cloneSword.transform.GetChild(0).transform.position + new Vector3(0, 0.8f, 0);
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                ResetGravity();
                Jump();                
                isHanging = false;
                Destroy(spawner.cloneSword);
                spawner.cloneSword = null;
                spawner.swordSpawned = false;
            }
        }
        else if (((doubleJumpReady || !hasJumped) && !isGrounded) && ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))))
        {
            doubleJumpRequest = true;
        }

        if (isHanging == true && Input.GetKeyDown(KeyCode.S))
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
            isQuickFalling = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(currentVelocityDown, quickFallMaxVelocityDown, t));
            t += 0.5f * Time.deltaTime;
            playerAnim.SetPlayerQuickFall(true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isQuickFalling = false;
            playerAnim.SetPlayerQuickFall(false);
        }

    }

    private void FixedUpdate()
    {

        if (jumpRequest == true)
        {
            Jump();
        }
        else if(doubleJumpRequest == true)
        {
            ResetGravity();
            DoubleJump();
        }

        if (isHanging)
        {
            maxVelocityDown = 0f;

        }
        else
        {
            maxVelocityDown = -20f;
        }

        isGrounded = Physics2D.OverlapBox(feetPos.position, boxSize, 0f, mask) != null;
        playerAnim.SetPlayerGrounded(isGrounded);



        if (rb.velocity.y < 0 && !isHanging)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space)) && !isHanging)
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = defaultGrav;
        }
       
        if (rb.velocity.y < maxVelocityDown && !isQuickFalling && !isHanging)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocityDown);
        }
        else if (isHanging)
        {
            rb.velocity = Vector2.zero;
        }

        playerAnim.SetPlayerYVelocity(rb.velocity.y);
    }


    void Jump()
    {
        rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        jumpRequest = false;
        isGrounded = false;
        doubleJumpReady = true;
        PlayerMovementV2.instance.canMove = true;
        hasJumped = true;
        if (isGrounded)
        {
            playerAnim.PlayerJumpTrigger();
        }
        AudioManager.instance.Play("Jump");
    }
    public void DoubleJump()
    {
        rb.AddForce(Vector2.up * doubleJumpVelocity, ForceMode2D.Impulse);
        doubleJumpRequest = false;
        doubleJumpReady = false;
        hasJumped = true;
        t = 0f;
        AudioManager.instance.Play("DJump");
    }

    public void ResetGravity()
    {
        PlayerMovementV2.instance.canMove = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = defaultGrav;
        t = 0f;
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
