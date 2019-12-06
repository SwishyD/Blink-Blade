using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPFXTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem pfx;

    public void TriggerPFX()
    {
        pfx.Play();
    }
}
