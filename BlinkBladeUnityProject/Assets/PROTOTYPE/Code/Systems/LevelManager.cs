using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool[] levelComplete;
    public float[] time;
    public int[] deaths;
    public string[] grade;

    private int debugUnlock;

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

    public void ResetVariables()
    {
        for (int i = 0; i < levelComplete.Length; i++)
        {
            levelComplete[i] = false;
            time[i] = 9999;
            deaths[i] = 9999;
            grade[i] = "";
        }
        playerPos = Vector2.zero;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Equals))
        {
            for (int i = 0; i < levelComplete.Length; i++)
            {
                levelComplete[i] = true;
            }
        }
    }
}
