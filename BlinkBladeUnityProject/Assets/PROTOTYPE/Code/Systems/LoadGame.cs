using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadData(string moveToLevel)
    {
        LevelData data = SaveSystem.LoadData();
        VolumeSaveSystem.SaveVolume(VolumeSettings.instance);

        for (int i = 0; i < LevelManager.instance.levelComplete.Length; i++)
        {
            LevelManager.instance.levelComplete[i] = data.levels[i];
            LevelManager.instance.levelUnlocked[i] = data.levelUnlock[i];
            LevelManager.instance.time[i] = data.levelTimes[i];
            LevelManager.instance.deaths[i] = data.levelDeaths[i];
            LevelManager.instance.grade[i] = data.levelGrades[i];
        }
        for (int i = 0; i < LevelManager.instance.dogTreatCollected.Length; i++)
        {
            LevelManager.instance.dogTreatCollected[i] = data.dogTreat[i];
        }
        LevelManager.instance.playerPos.x = data.playerPosition[0];
        LevelManager.instance.playerPos.y = data.playerPosition[1];

        SceneManagers.instance.MoveToScene(moveToLevel);
    }
}
