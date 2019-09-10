using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.5f,0.3f,0.8f);
        }
    }
}
