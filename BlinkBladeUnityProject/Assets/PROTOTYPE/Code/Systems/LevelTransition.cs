using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string moveToLevel;

    bool inDoor;

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
            if(SceneManager.GetActiveScene().name == "HUB")
            {
                LevelManager.instance.playerPos = GameObject.Find("PlayerV2").transform.position;
            }
            SceneManagers.instance.MoveToScene(moveToLevel);
        }
    }
}
