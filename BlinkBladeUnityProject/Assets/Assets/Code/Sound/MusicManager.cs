using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public Music[] musicTracks;

    [SerializeField] Music currentlyPlaying;

    public static MusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Music m in musicTracks)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.loop = m.loop;
            m.source.volume = m.volume;
        }
    }

    public void Play(string name)
    {
        Music m = Array.Find(musicTracks, sound => sound.name == name);
        if (m == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }
        m.source.Play();
        currentlyPlaying = m;
    }

    public void Stop(string name)
    {
        Music m = Array.Find(musicTracks, sound => sound.name == name);
        if (m == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }
        m.source.Stop();
        currentlyPlaying = null;
    }

    public IEnumerator FadeIn(string name, float speed)
    {
        Music m = Array.Find(musicTracks, sound => sound.name == name);
        if (m == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            yield return null;
        }

        m.volume = 0;
        m.source.Play();
        currentlyPlaying = m;

        while (m.volume < 1)
        {
            m.volume += speed * Time.deltaTime;
            m.source.volume = m.volume;
            yield return null;
        }

        m.volume = 1;
    }

    public IEnumerator FadeOut(string name, float speed)
    {
        name = currentlyPlaying.name;
        Music m = Array.Find(musicTracks, sound => sound.name == name);
        if (m == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            yield return null;
        }

        m.volume = 1;

        while (m.volume > 0)
        {
            m.volume -= speed * Time.deltaTime;
            m.source.volume = m.volume;
            yield return null;
        }

        m.volume = 0;
        m.source.Stop();
        //currentlyPlaying = null;
    }
}
