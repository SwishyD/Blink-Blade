using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenResolutionSettings : MonoBehaviour
{
    public TMP_Dropdown resDropdown;

    public Toggle fullScreenToggle;
    private bool fullScreen;

    // Update is called once per frame
    public void ResolutionChange()
    {
        if(resDropdown.value == 0)
        {
            Screen.SetResolution(1920, 1080, fullScreen);
        }
        else if (resDropdown.value == 1)
        {
            Screen.SetResolution(1280, 720, fullScreen);
        }
        else if (resDropdown.value == 2)
        {
            Screen.SetResolution(1024, 768, fullScreen);
        }
        else if (resDropdown.value == 3)
        {
            Screen.SetResolution(960, 640, fullScreen);
        }
    }

    public void FullScreen()
    {
        if (fullScreenToggle.isOn)
        {
            fullScreen = true;
        }
        else if(!fullScreenToggle.isOn)
        {
            fullScreen = false;
        }
        ResolutionChange();
    }
}
