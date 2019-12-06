using UnityEngine;

public class AnimSound : MonoBehaviour
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
