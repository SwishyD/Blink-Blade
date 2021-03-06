﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerSpawnPoint : MonoBehaviour
{
    public Vector2 spawnPoint;
    public ParticleSystem deathPFX;

    public int deathCount;
    public TMP_Text deathCountText;

    private Timer timer;
    private PlayerFlipManager flipTrigger;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
        if (SceneManager.GetActiveScene().name.Contains("LEVEL"))
        {
            timer = GameObject.Find("GUI").GetComponentInChildren<Timer>();
        }
        if(GameObject.Find("FlipManager") != null)
        {
            flipTrigger = PlayerFlipManager.instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
        if (SceneManager.GetActiveScene().name.Contains("LEVEL"))
        {
            deathCountText.text = "DEATHS : " + deathCount.ToString();
        }
    }

    public void Respawn()
    {
        CursorManager.Instance.ChangeCursor(false);
        Instantiate(deathPFX, gameObject.transform.position, Quaternion.identity);
        gameObject.transform.position = spawnPoint;
        Destroy(SwordSpawner.instance.cloneSword);
        SwordSpawner.instance.cloneSword = null;
        SwordSpawner.instance.swordSpawned = false;
        PlayerJumpV2.instance.ResetGravity();
        PlayerJumpV2.instance.PlayerNormal();
        if(flipTrigger != null)
        {
            flipTrigger.FlipEnabler(false);
        }
        if (PlayerJumpV2.instance.isFlipped)
        {
            PlayerJumpV2.instance.PlayerFlip();
        }
        Instantiate(deathPFX, transform);
        if (SceneManager.GetActiveScene().name.Contains("LEVEL"))
        {
            if (timer.timerActive)
            {
                deathCount++;
            }
        }
        //CursorManager.Instance.ChangeCursorState(false);
        FindObjectOfType<AudioManager>().Play("Death");
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.5f, 1f, .5f);
    }
}
