﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public Vector2 spawnPoint;
    public ParticleSystem deathPFX;

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
        Instantiate(deathPFX, gameObject.transform.position, Quaternion.identity);
        gameObject.transform.position = spawnPoint;
        Destroy(SwordSpawner.instance.cloneSword);
        SwordSpawner.instance.cloneSword = null;
        SwordSpawner.instance.swordSpawned = false;
        PlayerJumpV2.instance.ResetGravity();
        PlayerJumpV2.instance.PlayerNormal();
        Instantiate(deathPFX, transform);
    }
}
