using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorTracker : MonoBehaviour
{
    public bool levelCheck;
    public bool levelCheckComplete;
    public CameraFollow camFollow;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (levelCheck && !LevelManager.instance.levelUnlocked[10])
        {
            levelCheck = false;
            if (CheckCompletion())
            {
                StartCoroutine("UnlockLevel");
            }
        }
    }

    IEnumerator UnlockLevel()
    {
        yield return new WaitForSeconds(0.1f);
        camFollow.target = this.transform;
        PlayerScriptManager.instance.PlayerScriptDisable();
        yield return new WaitForSeconds(2f);
        GetComponent<SpriteRenderer>().sprite = GetComponent<BossLevelTransition>().unlockedDoor;
        LevelManager.instance.levelUnlocked[10] = true;
        yield return new WaitForSeconds(2f);
        camFollow.target = player.transform;
        PlayerScriptManager.instance.PlayerScriptEnable();
        levelCheckComplete = true;
    }

    bool CheckCompletion()
    {
        for (int i = 0; i < LevelManager.instance.levelComplete.Length; i++)
        {
            if (LevelManager.instance.levelComplete[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}
