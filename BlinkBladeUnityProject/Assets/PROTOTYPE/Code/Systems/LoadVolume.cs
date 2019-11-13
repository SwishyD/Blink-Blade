using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVolume : MonoBehaviour
{
    public void LoadData()
    {
        VolumeData data = VolumeSaveSystem.LoadVolume();

        VolumeSettings.instance.volumeSlider.value = data.volume;
    }
}
