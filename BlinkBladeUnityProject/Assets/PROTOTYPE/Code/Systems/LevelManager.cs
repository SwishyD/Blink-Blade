using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool[] levelComplete;
    public float[] time;
    public int[] deaths;
    public string[] grade;

    public Vector2 playerPos;

    public static LevelManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.O))
        {
            SaveSystem.SavePlayer(this);
        }*/

        if (Input.GetKeyDown(KeyCode.P))
        {
            LevelData data = SaveSystem.LoadData();

            for (int i = 0; i < levelComplete.Length; i++)
            {
                levelComplete[i] = data.levels[i];
                time[i] = data.levelTimes[i];
                deaths[i] = data.levelDeaths[i];
                grade[i] = data.levelGrades[i];
            }
            playerPos.x = data.playerPosition[0];
            playerPos.y = data.playerPosition[1];
            //GameObject.Find("PlayerV2").transform.position = position;
        }
    }
}
