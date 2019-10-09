using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubStart : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(LevelManager.instance.playerPos != Vector2.zero)
        {
            player.transform.position = LevelManager.instance.playerPos;
        }

        SaveSystem.SavePlayer(LevelManager.instance);
    }
}
