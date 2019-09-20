using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnim : MonoBehaviour
{
    Animator anim;
    [SerializeField] float timeUntilTrig;
    AudioSource barkSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SetNewTime();
        barkSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilTrig > 0)
        {
            timeUntilTrig -= Time.deltaTime;
        }
        if (timeUntilTrig <= 0)
        {
            int newIndex = Random.Range(1, 3);
            anim.SetInteger("IdleAnimIndex", newIndex);
            Debug.Log(newIndex);
            anim.SetTrigger("IdleAnim");
            if (newIndex == 2)
            {
                barkSound.Play();
            }
            SetNewTime();
        }
    }

    void SetNewTime()
    {
        timeUntilTrig = Mathf.CeilToInt(Random.Range(1, 5));
    }
}
