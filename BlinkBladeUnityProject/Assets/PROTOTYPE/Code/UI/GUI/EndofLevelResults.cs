using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class EndofLevelResults : MonoBehaviour
{
    public GameObject gameGUI;

    public PlayerSpawnPoint resultsDeaths;
    public Timer resultsTimer;
    private float unRoundedTime;

    public TMP_Text resultDeathCount;
    public TMP_Text resultTime;
    public TMP_Text resultsGrade;
    public GameObject resultsToHub;
    public GameObject resultsRestart;

    public float[] requiredTime;
    public float[] requiredDeaths;
    public string[] gradeLetter;

    private string grade;

    private void OnEnable()
    {
        PauseMenu.gameIsPaused = true;
        resultsDeaths.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameGUI.SetActive(false);
        unRoundedTime = resultsTimer.timeStart;
        CalculateGrade();
        StartCoroutine(ShowResults());
    }

    private void OnDisable()
    {
        PauseMenu.gameIsPaused = false;
        if(gameGUI != null)
        {
            gameGUI.SetActive(true);
        }
        resultDeathCount.text = "";
        resultTime.text = "";
        resultsGrade.text = "";
        resultsToHub.SetActive(false);
        resultsRestart.SetActive(false);
    }

    void CalculateGrade()
    {
        if(unRoundedTime <= requiredTime[0] && resultsDeaths.deathCount <= requiredDeaths[0])
        {
            grade = gradeLetter[0];
        }
        else if(unRoundedTime <= requiredTime[1] && resultsDeaths.deathCount <= requiredDeaths[1])
        {
            grade = gradeLetter[1];
        }
        else if (unRoundedTime <= requiredTime[2] && resultsDeaths.deathCount <= requiredDeaths[2])
        {
            grade = gradeLetter[2];
        }
        else if (unRoundedTime <= requiredTime[3] && resultsDeaths.deathCount <= requiredDeaths[3])
        {
            grade = gradeLetter[3];
        }
        else
        {
            grade = gradeLetter[4];
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
        yield return new WaitForSeconds(1f);
        resultsToHub.SetActive(true);
        resultsRestart.SetActive(true);
    }

    public void BackToHub()
    {
        SceneManagers.instance.MoveToScene("HUB");
    }

    public void RestartLevel()
    {
        string nameOfScene = SceneManager.GetActiveScene().name;
        SceneManagers.instance.MoveToScene(nameOfScene);
    }
}