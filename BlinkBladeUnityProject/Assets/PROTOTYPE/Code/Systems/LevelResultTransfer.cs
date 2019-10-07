using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResultTransfer : MonoBehaviour
{
    public Timer levelTime;
    public PlayerSpawnPoint levelDeaths;
    public EndofLevelResults levelGrades;

    public int levelNo;
    
    public void SetResults()
    {
        LevelManager.instance.levelComplete[levelNo] = true;
        if(LevelManager.instance.time[levelNo] == 0)
        {
            LevelManager.instance.time[levelNo] = levelTime.timeStart;
        }
        else if(LevelManager.instance.time[levelNo] > levelTime.timeStart)
        {
            LevelManager.instance.time[levelNo] = levelTime.timeStart;
        }
        else
        {
            return;
        }

        if(LevelManager.instance.deaths[levelNo] > levelDeaths.deathCount)
        {
            LevelManager.instance.deaths[levelNo] = levelDeaths.deathCount;
        }
        else
        {
            return;
        }

        if(LevelManager.instance.grade[levelNo] == "")
        {
            LevelManager.instance.grade[levelNo] = levelGrades.grade;
        }
        else if(LevelManager.instance.grade[levelNo] == "D")
        {
            LevelManager.instance.grade[levelNo] = levelGrades.grade;
        }
        else if (LevelManager.instance.grade[levelNo] == "C" && levelGrades.grade != "D")
        {
            LevelManager.instance.grade[levelNo] = levelGrades.grade;
        }
        else if (LevelManager.instance.grade[levelNo] == "B" && levelGrades.grade != "D" && levelGrades.grade != "C")
        {
            LevelManager.instance.grade[levelNo] = levelGrades.grade;
        }
        else if (LevelManager.instance.grade[levelNo] == "A" && levelGrades.grade != "D" && levelGrades.grade != "C" && levelGrades.grade != "B")
        {
            LevelManager.instance.grade[levelNo] = levelGrades.grade;
        }
        else if(LevelManager.instance.grade[levelNo] == "S")
        {
            LevelManager.instance.grade[levelNo] = levelGrades.grade;
        }
    }
}
