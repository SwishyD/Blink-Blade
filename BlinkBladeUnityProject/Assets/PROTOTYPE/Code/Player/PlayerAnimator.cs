using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    public SpriteRenderer spriteRend;

    private PlayerMovementV2 pMoveScript;
    [HideInInspector]
    public PlayerJumpV2 pJumpScript;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        pMoveScript = GetComponentInChildren<PlayerMovementV2>();
        pJumpScript = GetComponentInChildren<PlayerJumpV2>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!pJumpScript.isHanging) // Prevents the player from changing direction while hanging.
        {
            anim.SetFloat("PlayerHorizontalSpeed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
            SetPlayerDirection();

            if (anim.GetBool("PlayerHangingFromSword"))
            {
                anim.SetBool("PlayerHangingFromSword", false);
            }
        }

        if (pJumpScript.isHanging)
        {
            if (!pJumpScript.isFlipped)
            {
                if (pJumpScript.spawner.closeToGround)
                {
                    if (!anim.GetBool("PlayerHangingCloseToGround"))
                    {
                        anim.SetBool("PlayerHangingCloseToGround", true);
                    }
                }
                else if (pJumpScript.spawner.stuckDown) //Sword is in the ground
                {
                    if (!anim.GetBool("PlayerHangingCloseToGround"))
                    {
                        anim.SetBool("PlayerHangingCloseToGround", true);
                    }
                }
                else if (!pJumpScript.spawner.closeToGround)
                {
                    if (anim.GetBool("PlayerHangingCloseToGround"))
                    {
                        anim.SetBool("PlayerHangingCloseToGround", false);
                    }
                }

                if (!anim.GetBool("PlayerHangingFromSword"))
                {
                    anim.SetBool("PlayerHangingFromSword", true);
                }
            }
            else if (pJumpScript.isFlipped)
            {
                if (pJumpScript.spawner.closeToRoof)
                {
                    if (!anim.GetBool("PlayerHangingCloseToGround"))
                    {
                        anim.SetBool("PlayerHangingCloseToGround", true);
                    }
                }
                else if (pJumpScript.spawner.stuckUp) //Sword is in the ground
                {
                    if (!anim.GetBool("PlayerHangingCloseToGround"))
                    {
                        anim.SetBool("PlayerHangingCloseToGround", true);
                    }
                }
                else if (!pJumpScript.spawner.closeToRoof)
                {
                    if (anim.GetBool("PlayerHangingCloseToGround"))
                    {
                        anim.SetBool("PlayerHangingCloseToGround", false);
                    }
                }

                if (!anim.GetBool("PlayerHangingFromSword"))
                {
                    anim.SetBool("PlayerHangingFromSword", true);
                }
            }
        }
    }

    public void SetPlayerDirection()
    {
        if (pJumpScript.isHanging)
        {
            //Make sure that player is facing sword when hanging.
        }
        else
        {
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
        }
    }

    public void SetPlayerGrounded(bool newState)
    {
        anim.SetBool("PlayerIsGrounded", newState);
    }

    public void PlayerJumpTrigger()
    {
        anim.SetTrigger("PlayerStartJump");
    }

    public void PlayerDoubleJumpTrig()
    {
        anim.SetTrigger("PlayerDoubleJump");
    }

    public void SetPlayerYVelocity(float yVel)
    {
        anim.SetFloat("PlayerYVelocity", -yVel);
    }

    public void SetPlayerQuickFall(bool newState)
    {
        if (anim.GetBool("PlayerQuickFalling") != newState)
        {
            anim.SetBool("PlayerQuickFalling", newState);
        }
    }

    public void PlayerPetDog()
    {
        anim.SetTrigger("PlayerPet");
    }
}
