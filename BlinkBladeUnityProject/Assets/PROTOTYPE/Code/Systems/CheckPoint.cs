using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool triggered;
    public bool isEnd;
    public bool isStart;

    Animator anim;
    ParticleSystem triggerPFX;

    public GameObject timer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (triggerPFX == null)
        {
            triggerPFX = GetComponentInChildren<ParticleSystem>();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if (!triggered)
            {
                if (isEnd)
                {
                    timer.GetComponent<Timer>().TimerToggleOff();
                }
                else if (isStart)
                {
                    timer.GetComponent<Timer>().levelStarted = true;
                    timer.GetComponent<Timer>().TimerToggleOn();
                }
                ActivateCheckpoint();
            }
            if (triggered)
            {
                //anim.SetTrigger("Shake");
                //Play Sounds
            }
        }
    }

    void ActivateCheckpoint()
    {
        triggered = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpawnPoint>().spawnPoint = this.transform.position;
        anim.SetBool("Activated", true);
        triggerPFX.Play();
        //Play Sounds
    }
}
