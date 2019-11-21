using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetTheDevDog : MonoBehaviour
{
    [SerializeField] bool canPetDog;
    [SerializeField] bool beingPet;
    //DogAnim dogAnimScript;
    PlayerAnimator playerAnim;
    [SerializeField] AnimationClip petAnim;
    public int dirMult;
    [SerializeField] SpriteRenderer aimArrow;
    [SerializeField] ParticleSystem petPFX;

    // Start is called before the first frame update
    void Start()
    {
        RetrieveAnims();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPetDog && !beingPet && playerAnim.pJumpScript.isGrounded)
        {
            SetDogPetToTrue();
            Invoke("SetDogPetToFalse", petAnim.length);
        }
        if (playerAnim.spriteRend.flipX)
        {
            dirMult = -1;
        }
        else
        {
            dirMult = 1;
        }
    }

    public void SetDogPettability(bool newState)
    {
        canPetDog = newState;
    }

    void RetrieveAnims()
    {
        if (playerAnim == null)
        {
            playerAnim = FindObjectOfType<PlayerAnimator>();
        }
        //if (dogAnimScript == null)
        //{
        //    dogAnimScript = GetComponent<DogAnim>();
        //}
    }

    void SetDogPetToTrue()
    {
        RetrieveAnims();
        playerAnim.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        playerAnim.GetComponent<Rigidbody2D>().angularVelocity = 0;
        PlayerScriptManager.instance.swordScriptActive = true;
        PlayerScriptManager.instance.PlayerScriptDisable();
        beingPet = true;
        playerAnim.PlayerPetDog();
        //dogAnimScript.PetDog();
        Invoke("EnablePlayerMovement", petAnim.length);
        aimArrow.color = new Color(aimArrow.color.r, aimArrow.color.g, aimArrow.color.b, 0f);
    }

    public void SetDogPetToFalse()
    {
        beingPet = false;
        petPFX.Play();
    }

    public void EnablePlayerMovement()
    {
        PlayerScriptManager.instance.PlayerScriptEnable();
        aimArrow.color = new Color(aimArrow.color.r, aimArrow.color.g, aimArrow.color.b, 1f);

    }
}
