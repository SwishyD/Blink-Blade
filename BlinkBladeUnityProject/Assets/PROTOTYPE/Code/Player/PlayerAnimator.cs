using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    public SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("PlayerHorizontalSpeed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
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

    public void SetPlayerGrounded(bool newState)
    {
        anim.SetBool("PlayerIsGrounded", newState);
    }

    public void PlayerJumpTrigger()
    {
        anim.SetTrigger("PlayerStartJump");
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
}
