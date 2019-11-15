using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelTransition : MonoBehaviour
{
    public string moveToLevel;

    public Sprite lockedDoor;
    public Sprite unlockedDoor;

    bool inDoor;

    private void Start()
    {
        CheckCompletion();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckCompletion())
        {
            GetComponent<SpriteRenderer>().sprite = unlockedDoor;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = lockedDoor;
        }

        if (inDoor && Input.GetKeyDown(KeyCode.E))
        {
                if (GetComponent<SpriteRenderer>().sprite == unlockedDoor)
                {
                    LevelManager.instance.playerPos = GameObject.Find("PlayerV2").transform.position;
                    SceneManagers.instance.MoveToScene(moveToLevel);
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "PlayerV2")
        {
            inDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "PlayerV2")
        {
            inDoor = false;
        }
    }


    bool CheckCompletion()
    {
        for (int i = 0; i < LevelManager.instance.levelComplete.Length; i++)
        {
            if(LevelManager.instance.levelComplete[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}
