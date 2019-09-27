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
    public static float t = 0.0f;
    public float currentVelocityDown;
     
    //For ground check
    public LayerMask mask;
    public Transform feetPos;

    //Player states
    public bool doubleJumpReady = false;
    bool jumpRequest;
    bool doubleJumpRequest;
    public bool isGrounded;
    bool hasJumped;
    public bool isHanging;
    public bool isQuickFalling;
    public bool isFlipped;



    Vector2 playerSize;
    public Vector2 boxSize;

    Rigidbody2D rb;

    public SwordSpawner spawner;
    [SerializeField] ParticleSystem dJumpPFX;
    [SerializeField] AudioSource quickFallSound;

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
        
        //Setting Ground box 
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x -0.03f , groundedSkin);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetGravity();
        playerAnim = GetComponentInChildren<PlayerAnimator>();
        PlayerMovementV2.instance.canMove = true;
        PlayerNormal();
    }

    private void Update()
    {
        currentVelocityDown = rb.velocity.y;
        if (isGrounded)
        {
            hasJumped = false;
            if (isQuickFalling)
            {
                quickFallSound.Stop();
                isQuickFalling = false;
                playerAnim.SetPlayerQuickFall(false);
            }
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded && !jumpRequest)
        {
            jumpRequest = true;
        }
        else if (isHanging)
        {
            if (!isFlipped)
            {
                if (!spawner.closeToGround && !spawner.stuckDown)
                {
                    transform.position = spawner.cloneSword.transform.GetChild(0).transform.position;
                }
                else if (spawner.closeToGround && !spawner.stuckDown)
                {
                    transform.position = spawner.cloneSword.transform.GetChild(0).transform.position + new Vector3(0, 1f, 0);
                }
                else if (spawner.closeToGround && spawner.stuckDown || !spawner.closeToGround && spawner.stuckDown)
                {
                    transform.position = spawner.cloneSword.transform.GetChild(0).transform.position + new Vector3(0, 0.8f, 0);
                }
            }
            else if (isFlipped)
            {
                if (!spawner.closeToRoof && !spawner.stuckUp)
                {
                    transform.position = spawner.cloneSword.transform.GetChild(0).transform.position + new Vector3(0, 0.7f, 0);
                }
                else if (spawner.closeToRoof && !spawner.stuckUp)
                {
                    transform.position = spawner.cloneSword.transform.GetChild(0).transform.position + new Vector3(0, 1f, 0);
                }
                else if (spawner.closeToRoof && spawner.stuckUp || !spawner.closeToRoof && spawner.stuckUp)
                {
                    transform.position = spawner.cloneSword.transform.GetChild(0).transform.position - new Vector3(0, 0.8f, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                ResetGravity();
                Jump();                
                isHanging = false;
                DestroySword();
                CursorManager.Instance.ChangeCursor(false);
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
            DestroySword();
            CursorManager.Instance.ChangeCursor(false);
        }

        if (isGrounded == false && Input.GetKey(KeyCode.S))
        {
            if (!isQuickFalling)
            {
                quickFallSound.Play();
            }
            isQuickFalling = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(currentVelocityDown, quickFallMaxVelocityDown, t));
            t += 0.5f * Time.deltaTime;
            playerAnim.SetPlayerQuickFall(true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            quickFallSound.Stop();
            isQuickFalling = false;
            playerAnim.SetPlayerQuickFall(false);
        }

        //Debug
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerFlip();
        }
    }

    private void FixedUpdate()
    {
        if (jumpRequest == true)
        {
            Jump();
            jumpRequest = false;
        }
        else if(doubleJumpRequest == true)
        {
            ResetGravity();
            DoubleJump();
        } else
        {
            //GROUND CHECK
            isGrounded = Physics2D.OverlapBox(feetPos.position, boxSize, 0f, mask) != null;
            playerAnim.SetPlayerGrounded(isGrounded);
        }

        if (isHanging)
        {
            maxVelocityDown = 0f;
        }
        else if (isFlipped)
        {
            maxVelocityDown = 20f;
        }
        else
        {
            maxVelocityDown = -20f;
        }
        
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
       
        if (rb.velocity.y < maxVelocityDown && !isQuickFalling && !isHanging && !isFlipped)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocityDown);
        }
        else if (rb.velocity.y > maxVelocityDown && !isQuickFalling && !isHanging && isFlipped)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocityDown);
        }
        else if (isHanging)
        {
            rb.velocity = Vector2.zero;
        }
        //Anim
        playerAnim.SetPlayerYVelocity(rb.velocity.y);
    }

    public void GhostJump()
    {
        ResetGravity();
        Jump();
        isHanging = false;
        DestroySword();
        CursorManager.Instance.ChangeCursor(false);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        isGrounded = false;
        doubleJumpReady = true;
        PlayerMovementV2.instance.canMove = true;
        hasJumped = true;
        t = 0;

        //Anim and sound
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
        //Anim and sound
        AudioManager.instance.Play("DJump");
        playerAnim.PlayerDoubleJumpTrig();
        Instantiate(dJumpPFX, transform);
    }

    public void ResetGravity()
    {
        PlayerMovementV2.instance.canMove = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = defaultGrav;
        t = 0f;
    }

    void DestroySword()
    {
        Destroy(spawner.cloneSword);
        spawner.cloneSword = null;
        spawner.swordSpawned = false;
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
        quickFallSound.Stop();
        isQuickFalling = false;
        //Anim
        playerAnim.SetPlayerQuickFall(false);
    } 

    public void PlayerFlip()
    {
        isFlipped = !isFlipped;
        jumpVelocity = -jumpVelocity;
        doubleJumpVelocity = -doubleJumpVelocity;
        fallMultiplier = -fallMultiplier;
        lowJumpMultiplier = -lowJumpMultiplier;
        defaultGrav = -defaultGrav;
        maxVelocityDown = -maxVelocityDown;
        quickFallMaxVelocityDown = -quickFallMaxVelocityDown;
        gameObject.GetComponent<Collider2D>().offset = new Vector2(gameObject.GetComponent<Collider2D>().offset.x, -gameObject.GetComponent<Collider2D>().offset.y);
        if (isFlipped)
        {
            playerAnim.spriteRend.flipY = true;
            feetPos.localPosition = new Vector3(0.043f, 1.032f, 0);
        }
        else if (!isFlipped)
        {
            playerAnim.spriteRend.flipY = false;
            feetPos.localPosition = new Vector3(0.043f, -1.032f, 0);
        }
        AudioManager.instance.Play("GravFlip");
    }
}
