using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnim : MonoBehaviour
{
    Animator anim;
    AudioSource dogBark;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        dogBark = GetComponent<AudioSource>();
    }

    public void SetNewAnimIndex()
    {
        int newIndex;
        newIndex = Random.Range(0, 5); // 0-2 loop, 3 tail wag, 4 bark
        anim.SetInteger("IdleAnimIndex", newIndex);
    }

    public void DogBark()
    {
        dogBark.Play();
    }

    public void PetDog()
    {
        anim.SetTrigger("Pet");
    }
}
