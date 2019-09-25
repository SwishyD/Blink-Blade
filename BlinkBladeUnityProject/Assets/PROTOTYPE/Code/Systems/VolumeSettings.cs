using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSettings : MonoBehaviour
{
    public static VolumeSettings instance;

    public Slider volumeSlider;
    public TMP_Text volumeNumber;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        volumeNumber.text = volumeSlider.value.ToString();
        AudioListener.volume = volumeSlider.value / 100;
    }
}
