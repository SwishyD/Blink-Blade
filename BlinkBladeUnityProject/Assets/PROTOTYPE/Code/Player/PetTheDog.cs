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
        if (Input.GetKeyDown(KeyCode.E) && canPetDog && !beingPet)
        {
            RetrieveAnims();
            dogAnimScript.PetDog();
            beingPet = true;
            playerAnim.PlayerPetDog();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canPetDog = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canPetDog = false;
        }
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

    public void SetDogPetToFalse()
    {
        beingPet = false;
    }
}
