using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool[] levelComplete;
    public bool[] levelUnlocked;
    public float[] time;
    public int[] deaths;
    public string[] grade;
    public bool[] dogTreatCollected;

    private int debugUnlock;

    public Vector2 playerPos;

    public static LevelManager instance;
    public bool devActive;

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
        for (int i = 0; i < dogTreatCollected.Length; i++)
        {
            dogTreatCollected[i] = false;
        }
        for (int i = 0; i < levelUnlocked.Length; i++)
        {
            levelUnlocked[i] = false;
        }
        levelUnlocked[0] = true;
        playerPos = Vector2.zero;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("HUB"))
        {
            if (devActive)
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Equals))
                {
                    for (int i = 0; i < levelComplete.Length; i++)
                    {
                        levelComplete[i] = true;
                        levelUnlocked[i] = true;
                    }
                }

                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
                {
                    for (int i = 0; i < dogTreatCollected.Length; i++)
                    {
                        dogTreatCollected[i] = true;
                    }
                }
            }
        }
    }
}
