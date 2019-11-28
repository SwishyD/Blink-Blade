using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTracker : MonoBehaviour
{
    public bool levelCheck;
    public int levelNo;

    public CameraFollow camFollow;
    public GameObject player;

    public List<GameObject> doors = new List<GameObject>();
    public GameObject door1;

    [SerializeField] ParticleSystem doorUnlockPFX;

    private void Start()
    {
        levelNo = doors[0].GetComponent<Tooltip>().level;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelCheck)
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
        door1.GetComponent<LevelTransition>().enabled = false;
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].GetComponent<LevelTransition>().enabled = false;
        }
        yield return new WaitForSeconds(0.05f);
        PlayerScriptManager.instance.PlayerScriptDisable();
        camFollow.target = doors[levelNo - 1].transform;
        PlayerScriptManager.instance.PlayerScriptDisable();
        yield return new WaitForSeconds(1f);
        AudioManager.instance.Play("Lock");
        yield return new WaitForSeconds(1f);
        Instantiate(doorUnlockPFX, new Vector3(doors[levelNo - 1].transform.position.x, doors[levelNo - 1].transform.position.y, -3), Quaternion.identity);
        AudioManager.instance.Play("Chord");
        AudioManager.instance.Play("Zoom");
        AudioManager.instance.Play("Whoosh");
        yield return new WaitForSeconds(0.1f);
        doors[levelNo - 1].GetComponent<SpriteRenderer>().sprite = doors[levelNo - 1].GetComponent<LevelTransition>().unlockedDoor;
        LevelManager.instance.levelUnlocked[levelNo] = true;
        yield return new WaitForSeconds(2f);
        camFollow.target = player.transform;
        PlayerScriptManager.instance.PlayerScriptEnable();
        door1.GetComponent<LevelTransition>().enabled = true;
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].GetComponent<LevelTransition>().enabled = true;
        }
    }
}
