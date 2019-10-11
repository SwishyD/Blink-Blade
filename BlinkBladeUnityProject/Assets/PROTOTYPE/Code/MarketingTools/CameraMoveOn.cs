using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveOn : MonoBehaviour
{
    private bool cameraEnabled = false;

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
        {
            cameraEnabled = !cameraEnabled;
            var cameraRig = GameObject.Find("CameraRig");
            cameraRig.GetComponent<CameraMovement>().enabled = cameraEnabled;
        }
    }
}
