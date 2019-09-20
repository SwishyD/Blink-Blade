using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnim : MonoBehaviour
{
    Animator anim;
    float timeUntilTrig;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            anim.SetFloat("IdleAnimIndex", Random.Range(1, 2));
        }
    }


}
