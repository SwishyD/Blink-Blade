﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool triggered;
    public bool isEnd;
    public bool isStart;

    public GameObject timer;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && triggered == false)
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
            GetComponent<SpriteRenderer>().color = Color.red;
            triggered = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpawnPoint>().spawnPoint = this.transform.position;
        }
    }
}
