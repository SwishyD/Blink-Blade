﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour

{
    public float timeStart;
    public TMP_Text textBox;


    public bool timerActive;
    public bool levelStarted;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timeStart = Mathf.Min(timeStart + Time.deltaTime, 359999);
            var timeSpan = TimeSpan.FromSeconds(timeStart);
            textBox.text = "<mspace=0.38em>" + timeSpan.ToString("mm':'ss'.'fff") + "</mspace>";
        }
    }

    public void TimerToggleOff()
    {
        timerActive = false;
    }
    public void TimerToggleOn()
    {
        if (levelStarted)
        {
            timerActive = true;
        }
    }
}
