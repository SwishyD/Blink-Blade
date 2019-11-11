using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VolumeData
{
    public float volume;

    public VolumeData (VolumeSettings volumeManager)
    {
        volume = volumeManager.volumeSlider.value;
    }
}
