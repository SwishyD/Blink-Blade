﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTreat : MonoBehaviour
{
    public EndofLevelResults endResults;
    public LevelResultTransfer transfer;
    public Timer timer;

    public int addDebugTime;

    private float requiredTime;
    private int levelNo;

    private void Start()
    {
        requiredTime = endResults.requiredTime[0] + addDebugTime;
        levelNo = transfer.levelNo;
    }

    // Update is called once per frame
    void Update()
    {
        if(requiredTime < timer.timeStart)
        {
            Destroy(this.gameObject.transform.parent);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.instance.dogTreatCollected[levelNo] = true;
            Destroy(this.gameObject.transform.parent);
        }
    }
}
