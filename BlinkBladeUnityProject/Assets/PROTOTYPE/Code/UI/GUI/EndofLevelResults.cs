using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EndofLevelResults : MonoBehaviour
{
    public PlayerSpawnPoint resultsDeaths;
    public Timer resultsTimer;
    private float unRoundedTime;

    public TMP_Text resultDeathCount;
    public TMP_Text resultTime;
    public TMP_Text resultsGrade;

    public string grade;

    private void OnEnable()
    {
        unRoundedTime = resultsTimer.timeStart;
        StartCoroutine(ShowResults());
        CalculateGrade();
    }

    void CalculateGrade()
    {
        if(unRoundedTime <= 60 && resultsDeaths.deathCount <= 1)
        {
            grade = "S+";
        }
    }

    IEnumerator ShowResults()
    {
        yield return new WaitForSeconds(1f);
        resultDeathCount.text = "Deaths: " + resultsDeaths.deathCount;
        yield return new WaitForSeconds(1f);
        unRoundedTime = resultsTimer.timeStart;
        var roundedTime = TimeSpan.FromSeconds(unRoundedTime);
        resultTime.text = "Time: " + "<mspace=0.38em>" + roundedTime.ToString("mm':'ss'.'fff") + "</mspace>";
        yield return new WaitForSeconds(1f);
        resultsGrade.text = "Grade: " + grade;
    }
}
