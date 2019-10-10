using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ToolTipStats : MonoBehaviour
{
    public int level;
    public TMP_Text time;
    public TMP_Text deaths;
    public TMP_Text grade;

    // Start is called before the first frame update
    void Start()
    {
        var timeSpan = TimeSpan.FromSeconds(LevelManager.instance.time[level]);

        time.text = "Time: " + "<mspace=0.38em>" + timeSpan.ToString("mm':'ss'.'fff") + "</mspace>";
        deaths.text = "Deaths: " + LevelManager.instance.deaths[level].ToString();
        grade.text = "Grade: " + LevelManager.instance.grade[level];
    }
}
