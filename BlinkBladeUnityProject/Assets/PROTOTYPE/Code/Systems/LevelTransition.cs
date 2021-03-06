﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string moveToLevel;

    public bool unlocked;

    public Sprite lockedDoor;
    public Sprite unlockedDoor;
    [Space]
    public Vector2 playerSpawn;

    bool inDoor;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "HUB" && this.name != "Tutorial Door" && this.name != "1-1 Door" && this.name != "Secret Door")
        {
            /*if (LevelManager.instance.levelComplete[GetComponent<Tooltip>().level - 1])
            {
                GetComponent<SpriteRenderer>().sprite = unlockedDoor;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = lockedDoor;
            }*/
            if (LevelManager.instance.levelUnlocked[GetComponent<Tooltip>().level])
            {
                GetComponent<SpriteRenderer>().sprite = unlockedDoor;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "PlayerV2")
        {
            if(gameObject.name.Contains("Door"))
            {
                inDoor = true;
            }
            else if (!gameObject.name.Contains("Door"))
            {
                SceneManagers.instance.MoveToScene(moveToLevel);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "PlayerV2")
        {
            if (gameObject.name.Contains("Door"))
            {
                inDoor = false;
            }
        }
    }

    private void Update()
    {
        if(inDoor && Input.GetKeyDown(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().name == "HUB")
            {
                if (GetComponent<SpriteRenderer>().sprite == unlockedDoor)
                {
                    LevelManager.instance.playerPos = playerSpawn;
                    SceneManagers.instance.MoveToScene(moveToLevel);
                }
            }
            else
            {
                SceneManagers.instance.MoveToScene(moveToLevel);
            }
        }
    }
}
