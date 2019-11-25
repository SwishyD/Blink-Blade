using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTracker : MonoBehaviour
{
    public static DoorTracker instance;

    public bool levelCheck;
    public int levelNo;

    public CameraFollow camFollow;
    public GameObject player;

    public List<GameObject> doors = new List<GameObject>();

    void Awake()
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

    private void Start()
    {
        levelNo = doors[0].GetComponent<Tooltip>().level;
        levelCheck = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelCheck && levelNo < doors.Count)
        {
            if (LevelManager.instance.levelComplete[levelNo - 1] && !LevelManager.instance.levelUnlocked[levelNo])
            {
                levelCheck = false;
                StartCoroutine("UnlockLevel");
            }
            else
            {
                if(levelNo < doors.Count)
                {
                    levelNo++;
                }
                else
                {
                    levelCheck = false;
                }
            }
        }
    }

    IEnumerator UnlockLevel()
    {
        yield return new WaitForSeconds(0.1f);
        camFollow.target = doors[levelNo - 1].transform;
        PlayerScriptManager.instance.PlayerScriptDisable();
        yield return new WaitForSeconds(2f);
        doors[levelNo - 1].GetComponent<SpriteRenderer>().sprite = doors[levelNo - 1].GetComponent<LevelTransition>().unlockedDoor;
        LevelManager.instance.levelUnlocked[levelNo] = true;
        yield return new WaitForSeconds(2f);
        camFollow.target = player.transform;
        PlayerScriptManager.instance.PlayerScriptEnable();
    }
}
