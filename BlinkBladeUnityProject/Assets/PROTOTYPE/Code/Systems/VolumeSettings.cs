using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSettings : MonoBehaviour
{
    public static VolumeSettings instance;

    public Slider volumeSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        VolumeData data = VolumeSaveSystem.LoadVolume();
        volumeSlider.value = data.volume;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
