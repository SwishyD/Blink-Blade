using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour, IEnemyDeath
{
    public bool active;
    public GameObject bullet;
    public int numberOfShots;

    public GameObject bulletAimer;
    public GameObject bulletSpawn;
    public float offset;

    public float deathTimer;
    public Sprite soul;
    public Sprite normal;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Shoot");
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            active = true;
        }

        Vector3 difference = GameObject.FindGameObjectWithTag("Player").transform.position - bulletAimer.transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        bulletAimer.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && active)
        {
            col.GetComponent<PlayerSpawnPoint>().Respawn();
        }
    }

    IEnumerator Shoot()
    {
        while (enabled)
        {
            Debug.Log("Shoot");
            if (active)
            {
                for (int i = 0; i < numberOfShots; i++)
                {
                    var Bullet = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                    Physics2D.IgnoreCollision(Bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public void OnHit()
    {
        GetComponent<SpriteRenderer>().sprite = soul;
        Invoke("OnDeath", deathTimer);
        active = false;
    }

    public void OnDeath()
    {
        if (transform.childCount > 1)
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
        yield return new WaitForSeconds(2f);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = normal;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        active = true;
    }
}
