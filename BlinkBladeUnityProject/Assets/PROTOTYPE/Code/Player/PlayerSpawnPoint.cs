using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        gameObject.transform.position = spawnPoint;
    }
}
