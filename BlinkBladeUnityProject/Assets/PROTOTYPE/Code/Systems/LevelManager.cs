using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float time;
    public int deaths;
    public string grade;
    public bool levelComplete;

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
        playerPos = GameObject.Find("PlayerV2").transform.position;
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveSystem.SavePlayer(this);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LevelData data = SaveSystem.LoadData();

            time = data.levelTimes;
            deaths = data.levelDeaths;
            grade = data.levelGrades;
            levelComplete = data.levels;

            Vector2 position;
            position.x = data.playerPosition[0];
            position.y = data.playerPosition[1];
            GameObject.Find("PlayerV2").transform.position = position;
        }
    }
}
