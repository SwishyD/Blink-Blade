using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] string musicName;
    [SerializeField] bool fadeIn = true;
    [SerializeField] float fadeSpeed = 0.5f;
    
    void Start()
    {
        if (fadeIn)
        {
            StartCoroutine(MusicManager.instance.FadeIn(musicName, fadeSpeed));
        }
        else
        {
            MusicManager.instance.Play(musicName);
        }
    }
}
