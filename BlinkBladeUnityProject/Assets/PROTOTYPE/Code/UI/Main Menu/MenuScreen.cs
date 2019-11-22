using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour {

    public GameObject menuScreenUI;
    public GameObject quitConfirmUI;
    public GameObject continueButton;
    public GameObject newGameConfirmUI;
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

        /*if(Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftControl))
        {
            SaveSystem.DeleteSave();
        }*/
	}

    public void StartGame(string level)
    {
        //SceneManager.LoadScene(1);
        LevelManager.instance.ResetVariables();
        VolumeSaveSystem.SaveVolume(VolumeSettings.instance);
        SceneManagers.instance.MoveToScene(level);
    }

    public void NewGameConfirm(string moveToLevel)
    {
        if (SaveSystem.DataExists())
        {
            menuScreenUI.SetActive(false);
            newGameConfirmUI.SetActive(true);
        }
        else
        {
            StartGame(moveToLevel);
        }
    }

    public void ToCredits()
    {
        VolumeSaveSystem.SaveVolume(VolumeSettings.instance);
        SceneManagers.instance.MoveToScene("Credits");
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
        newGameConfirmUI.SetActive(false);
    }


   

    public void QuitGame()
    {
        Application.Quit();
    }

}
