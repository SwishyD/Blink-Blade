using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndofLevel : MonoBehaviour
{
    public GameObject endOfLevelResults;
    public bool inDoor;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "PlayerV2")
        {
            if (gameObject.name.Contains("Door"))
            {
                inDoor = true;
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
        if(Input.GetKeyDown(KeyCode.E) && inDoor)
        {
            endOfLevelResults.SetActive(true);
        }
    }
}
