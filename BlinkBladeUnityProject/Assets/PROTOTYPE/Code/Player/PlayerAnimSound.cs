using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimSound : MonoBehaviour
{
    public void PlaySound(string soundName)
    {
        AudioManager.instance.Play(soundName);
    }

    public void StopSound(string soundName)
    {
        AudioManager.instance.Stop(soundName);
    }
}
