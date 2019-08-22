﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatrolDir { Left, Right, Up, Down}

public class FlyingSidePatrol : MonoBehaviour, IEnemyDeath
{
    public PatrolDir direction;
    [Tooltip("(Seconds) Time that the Enemy pauses for on each side")]
    public float pauseTimer;
    public bool active;
    private float patrolTime;
    [Tooltip("(Seconds) Time that the Enemy moves to each side")]
    public float maxPatrolTime;
    public float speed;
    [Tooltip("(Seconds) Time it takes for the Soul to disappear")]
    public float deathTimer;
    [Tooltip("(Seconds) Time it takes for the Enemy to Respawn")]
    public float respawnTimer;
    [Tooltip("(Seconds) Time that the Enemy doesn't have a hitbox")]
    public float iFrameTimer;

    public Sprite soul;
    public Sprite normal;
    public bool isHit;

    // Update is called once per frame
    void FixedUpdate()
    {
            if (active)
            {
                patrolTime += Time.deltaTime;
            }
            if (patrolTime >= maxPatrolTime)
            {
                StartCoroutine("EdgePause");
                if (direction == PatrolDir.Left || direction == PatrolDir.Up)
                {
                    direction++;
                    patrolTime = 0;
                }
                else if (direction == PatrolDir.Right || direction == PatrolDir.Down)
                {
                    direction--;
                    patrolTime = 0;
                }
            }

        switch (direction)
        {
            case PatrolDir.Left:
                transform.position -= new Vector3(speed, 0,0);
                break;

            case PatrolDir.Right:
                transform.position += new Vector3(speed, 0, 0);
                break;

            case PatrolDir.Up:
                transform.position += new Vector3(0, speed, 0);
                break;

            case PatrolDir.Down:
                transform.position -= new Vector3(0, speed, 0);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && !isHit)
        {
            col.GetComponent<PlayerSpawnPoint>().Respawn();
        }  
    }

    IEnumerator EdgePause()
    {
            active = false;
            var normalSpeed = speed;
            speed = 0;
            yield return new WaitForSeconds(pauseTimer);
            active = true;
            speed = normalSpeed;
    }

    public void OnHit()
    {
        GetComponent<SpriteRenderer>().sprite = soul;
        isHit = true;
        Invoke("OnDeath", deathTimer);
    }

    public void OnDeath()
    {
        if (transform.childCount > 0)
        {
            Destroy(SwordSpawner.instance.cloneSword);
            PlayerJumpV2.instance.ResetGravity();
            PlayerJumpV2.instance.PlayerNormal();
            SwordSpawner.instance.swordSpawned = false;
            SwordSpawner.instance.cloneSword = null;
        }
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = normal;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(iFrameTimer);
        GetComponent<SpriteRenderer>().color = Color.white;
        isHit = false;
        active = true;
    }
}