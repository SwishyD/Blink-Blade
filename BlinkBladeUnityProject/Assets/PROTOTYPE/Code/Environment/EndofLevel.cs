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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inDoor)
        {
            endOfLevelResults.SetActive(true);
        }
    }
}
