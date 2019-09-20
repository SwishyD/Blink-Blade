using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickFallCamShake : MonoBehaviour
{
    [SerializeField] PlayerJumpV2 pJump;

    public void CheckIfQuickFalling()
    {
        if (Input.GetKey(KeyCode.S))
        {
            CamShake();
        }
    }

    void CamShake()
    {
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.5f, 0.8f, .5f);
    }
}
