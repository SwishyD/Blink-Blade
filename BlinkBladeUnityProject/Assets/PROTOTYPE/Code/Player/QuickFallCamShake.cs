using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickFallCamShake : MonoBehaviour
{
    [SerializeField] PlayerJumpV2 pJump;
    [SerializeField] ParticleSystem heavyLandPFX;

    public void CheckIfQuickFalling()
    {
        if (Input.GetKey(KeyCode.S))
        {
            HeavyLand();
        }
    }

    void HeavyLand()
    {
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.5f, 0.8f, .5f);
        Instantiate(heavyLandPFX, transform);
        AudioManager.instance.Play("HeavyLand");
    }
}
