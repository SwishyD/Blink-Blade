using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraShake : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float power;
    [SerializeField] float decayRate;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(duration, power, decayRate);
        }
    }
}
