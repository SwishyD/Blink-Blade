using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPaused : MonoBehaviour
{
    public AudioSource audioSound;
    public float normalVolume;

    // Start is called before the first frame update
    void Start()
    {
        normalVolume = audioSound.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.gameIsPaused)
        {
            audioSound.volume = 0;
        }
        if (!PauseMenu.gameIsPaused)
        {
            audioSound.volume = normalVolume;
        }
    }
}
