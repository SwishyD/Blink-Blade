using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPatroller : MonoBehaviour, IEnemyDeath
{
    public float speed;
    [Tooltip("How far the raycast can detect for ground")]
    public float distance;

    public bool isHit;
    public Sprite soul;
    public Sprite normal;

    [Tooltip("(Seconds) Time it takes for the Soul to disappear")]
    public float deathTimer;
    [Tooltip("(Seconds) Time it takes for the Enemy to Respawn")]
    public float respawnTimer;
    [Tooltip("(Seconds) Time that the Enemy doesn't have a hitbox")]
    public float iFrameTimer;

    private bool movingRight = true;

    public Transform groundDetection;

    private void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if(groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && isHit)
        {
            col.GetComponent<PlayerSpawnPoint>().Respawn();
        }
    }

    public void OnHit()
    {
        GetComponent<SpriteRenderer>().sprite = soul;
        isHit = true;
        Invoke("OnDeath", deathTimer);
    }

    public void OnDeath()
    {
        if (transform.childCount > 1)
        {
            Destroy(SwordSpawner.instance.cloneSword);
            if (PlayerJumpV2.instance.isHanging)
            {
                PlayerJumpV2.instance.ResetGravity();
                PlayerJumpV2.instance.PlayerNormal();
            }
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
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = normal;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(iFrameTimer);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        isHit = false;
    }
}
