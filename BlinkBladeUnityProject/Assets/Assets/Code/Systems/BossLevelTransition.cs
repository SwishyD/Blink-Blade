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

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.instance.levelUnlocked[10])
        {
            GetComponent<SpriteRenderer>().sprite = unlockedDoor;
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
}
