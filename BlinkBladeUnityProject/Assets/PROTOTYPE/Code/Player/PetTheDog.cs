using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetTheDog : MonoBehaviour
{
    [SerializeField] bool canPetDog;
    [SerializeField] bool beingPet;
    DogAnim dogAnimScript;
    PlayerAnimator playerAnim;

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
        if (dogAnimScript == null)
        {
            dogAnimScript = GetComponent<DogAnim>();
        }
    }

    void SetDogPetToTrue()
    {
        RetrieveAnims();
        PlayerScriptManager.instance.PlayerScriptDisable();
        beingPet = true;
        playerAnim.PlayerPetDog();
        dogAnimScript.PetDog();
    }

    public void SetDogPetToFalse()
    {
        beingPet = false;
        PlayerScriptManager.instance.PlayerScriptEnable();
    }
}
