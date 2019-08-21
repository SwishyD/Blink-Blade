using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatrolDir { Left, Right, Up, Down}

public class FlyingSidePatrol : MonoBehaviour, IEnemyDeath
{
    public PatrolDir direction;
    public bool active;
    public float patrolTime;
    public float maxPatrolTime;
    public float speed;
    public float deathTimer;

    public Vector2 respawnPoint;
    public float respawnTimer;
    private float respawnSpeed;
    private PatrolDir respawnDir;

    void Start()
    {
        respawnPoint = transform.position;
        respawnSpeed = speed;
        respawnDir = direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            patrolTime += Time.deltaTime;
        }
        if(patrolTime >= maxPatrolTime)
        {
            StartCoroutine("EdgePause");
            if(direction == PatrolDir.Left || direction == PatrolDir.Up)
            {
                direction++;
                patrolTime = 0;
            }
            else if(direction == PatrolDir.Right || direction == PatrolDir.Down)
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

    IEnumerator EdgePause()
    {
        if (active)
        {
            active = false;
            var normalSpeed = speed;
            speed = 0;
            yield return new WaitForSeconds(1f);
            active = true;
            speed = normalSpeed;
        }
    }

    public void OnDeath()
    {
        active = false;
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("Destroy", deathTimer);
        speed = 0;
    }

    public void Destroy()
    {
        Destroy(SwordSpawner.instance.cloneSword);
        if (transform.childCount > 0)
        {
            PlayerJumpV2.instance.ResetGravity();
            PlayerJumpV2.instance.PlayerNormal();
            SwordSpawner.instance.swordSpawned = false;
            SwordSpawner.instance.cloneSword = null;
        }
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Invoke("Respawn", respawnTimer);
    }

    public void Respawn()
    {
        Debug.Log(respawnSpeed);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        transform.position = respawnPoint;
        speed = respawnSpeed;
        direction = respawnDir;
        patrolTime = 0;
        active = true;
    }
}
