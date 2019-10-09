﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour {

    public GameObject menuScreenUI;
    public GameObject quitConfirmUI;
    public GameObject continueButton;
    public static bool quitActive = false;
    

    

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (quitActive == true)
            {
                ReturnToMain();
            }
            else
            {
                QuitGameConfirmation();
            }
        }
        if (SaveSystem.DataExists())
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftControl))
        {
            SaveSystem.DeleteSave();
        }
	}

    public void StartGame(string moveToLevel)
    {
        //SceneManager.LoadScene(1);
        SceneManagers.instance.MoveToScene(moveToLevel);
    }
       
    public void QuitGameConfirmation()
    {
        quitActive = true;
        menuScreenUI.SetActive(false);
        quitConfirmUI.SetActive(true);
    }

    public void ReturnToMain()
    {
        quitActive = false;
        menuScreenUI.SetActive(true);
        quitConfirmUI.SetActive(false);
    }


   

    public void QuitGame()
    {
        Application.Quit();
    }

}
