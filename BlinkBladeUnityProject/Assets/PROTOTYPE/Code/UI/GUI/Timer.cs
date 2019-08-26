using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour

{
    public float timeStart;
    public TMP_Text textBox;


    bool timerActive = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timeStart = Mathf.Min(timeStart + Time.deltaTime, 300);
            var timeSpan = TimeSpan.FromSeconds(timeStart);
            textBox.text = "<mspace=0.38em>" + timeSpan.ToString("mm':'ss'.'fff") + "</mspace>";
        }
    }

    public void TimerToggle()
    {
        timerActive = !timerActive;
    }
}
