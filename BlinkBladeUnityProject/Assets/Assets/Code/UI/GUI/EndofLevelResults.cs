using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class EndofLevelResults : MonoBehaviour
{
    public GameObject gameGUI;
    public PauseMenu pauseMenu;

    public PlayerSpawnPoint resultsDeaths;
    public Timer resultsTimer;
    private float unRoundedTime;

    public TMP_Text resultDeathCount;
    public TMP_Text resultTime;
    public TMP_Text resultsGrade;
    public TMP_Text nextGrade;
    public GameObject resultsToHub;
    public GameObject resultsRestart;

    public float[] requiredTime;
    public string[] gradeLetter;

    public string grade;

    public bool levelEnd;

    private void OnEnable()
    {
        pauseMenu.enabled = false;
        levelEnd = true;
        PlayerJumpV2.instance.ResetGravity();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimator>().canMove = false;
        PlayerScriptManager.instance.PlayerScriptDisable();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimator>().enabled = true;
        gameGUI.SetActive(false);
        unRoundedTime = resultsTimer.timeStart;
        CalculateGrade();
        StartCoroutine(ShowResults());
    }

    void CalculateGrade()
    {
        if(unRoundedTime <= requiredTime[0])
        {
            grade = gradeLetter[0];
        }
        else if(unRoundedTime <= requiredTime[1])
        {
            grade = gradeLetter[1];
        }
        else if (unRoundedTime <= requiredTime[2])
        {
            grade = gradeLetter[2];
        }
        else if (unRoundedTime <= requiredTime[3])
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
        AudioManager.instance.Play("HeavyLand");
        yield return new WaitForSeconds(1f);
        resultDeathCount.text = "Deaths: " + resultsDeaths.deathCount;
        AudioManager.instance.Play("HeavyLand");
        yield return new WaitForSeconds(1f);
        unRoundedTime = resultsTimer.timeStart;
        var roundedTime = TimeSpan.FromSeconds(unRoundedTime);
        resultTime.text = "Time: " + "<mspace=0.38em>" + roundedTime.ToString("mm':'ss'.'fff") + "</mspace>";
        AudioManager.instance.Play("HeavyLand");
        yield return new WaitForSeconds(1f);
        resultsGrade.text = "Grade: " + grade;
        NextGrade();
        AudioManager.instance.Play("HeavyLand");
        yield return new WaitForSeconds(1f);
        GetComponent<LevelResultTransfer>().SetResults();
        resultsToHub.SetActive(true);
        resultsRestart.SetActive(true);
    }

    void NextGrade()
    {
        if(grade == gradeLetter[0])
        {
            nextGrade.text = "Perfect!!";
        }
        else if(grade == gradeLetter[1])
        {
            nextGrade.text = "Next Grade: " + gradeLetter[0] + " = " + requiredTime[0].ToString() + " seconds";
        }
        else if (grade == gradeLetter[2])
        {
            nextGrade.text = "Next Grade: " + gradeLetter[1] + " = " + requiredTime[1].ToString() + " seconds";
        }
        else if (grade == gradeLetter[3])
        {
            nextGrade.text = "Next Grade: " + gradeLetter[2] + " = " + requiredTime[2].ToString() + " seconds";
        }
        else if (grade == gradeLetter[4])
        {
            nextGrade.text = "Next Grade: " + gradeLetter[3] + " = " + requiredTime[3].ToString() + " seconds";
        }
    }

    public void BackToHub()
    {
        levelEnd = false;
        SceneManagers.instance.MoveToScene("HUB");
    }

    public void RestartLevel()
    {
        levelEnd = false;
        string nameOfScene = SceneManager.GetActiveScene().name;
        SceneManagers.instance.MoveToScene(nameOfScene);
    }
}