using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public bool[] levels;
    public bool[] levelUnlock;
    public float[] levelTimes;
    public int[] levelDeaths;
    public string[] levelGrades;
    public bool[] dogTreat;

    public float[] playerPosition;

    public LevelData (LevelManager manager)
    {
        levelDeaths = manager.deaths;
        levelTimes = manager.time;
        levels = manager.levelComplete;
        levelUnlock = manager.levelUnlocked;
        levelGrades = manager.grade;
        dogTreat = manager.dogTreatCollected;

        playerPosition = new float[2];
        playerPosition[0] = manager.playerPos.x;
        playerPosition[1] = manager.playerPos.y;
    }
}
