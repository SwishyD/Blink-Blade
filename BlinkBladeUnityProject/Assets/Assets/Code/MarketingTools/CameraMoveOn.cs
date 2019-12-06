using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraMoveOn : MonoBehaviour
{
    private bool cameraEnabled = false;
    bool frozen = false;
    bool devModeActive = false;
    TMP_Text devText;

    private void Start()
    {
        devText = GameObject.Find("SceneTransitionCanvas").GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.DownArrow))
        {
            ActivateDevMode();
        }

        if (devModeActive)
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
            {
                cameraEnabled = !cameraEnabled;
                var cameraRig = GameObject.Find("CameraRig");
                cameraRig.GetComponent<CameraMovement>().enabled = cameraEnabled;
            }

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
            {
                frozen = !frozen;
                if (frozen)
                {
                    Time.timeScale = 0f;
                }
                else if (!frozen)
                {
                    Time.timeScale = 1f;
                }
            }
        }
    }

    void ActivateDevMode()
    {
        CancelInvoke("TextOff");
        devModeActive = !devModeActive;
        LevelManager.instance.devActive = devModeActive;
        devText.text = "Dev Mode Active = " + devModeActive;
        devText.gameObject.SetActive(true);
        Invoke("TextOff", 2);
    }

    void TextOff()
    {
        devText.gameObject.SetActive(false);
    }
}
