﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnable : MonoBehaviour
{
    public GameObject swordActivator;
    public ParticleSystem collectPFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PlayerV2")
        {
            swordActivator.SetActive(true);
            GameObject.Find("PauseMenuHolder").GetComponent<PauseMenu>().swordScriptActive = true;
            collectPFX.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}