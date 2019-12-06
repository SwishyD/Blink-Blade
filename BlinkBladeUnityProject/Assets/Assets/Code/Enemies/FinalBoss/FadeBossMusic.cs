using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBossMusic : MonoBehaviour
{
    public void FadeMusic()
    {
        StartCoroutine(MusicManager.instance.FadeOut("Boss", 5f));
    }
}
