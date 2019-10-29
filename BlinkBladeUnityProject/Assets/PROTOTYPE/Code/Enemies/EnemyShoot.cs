using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour, IEnemyDeath
{
    public bool active;
    public GameObject bullet;
    [Tooltip("Number of shots in each Volley")]
    public int numberOfShots;
    [Tooltip("This determines time between shots and multi shots")]
    public float timeBetweenShots;
    [Tooltip("If Number of Shots > 1, then this determines time between those shots")]
    public float timeBetweenMultiShots;

    public GameObject bulletAimer;
    public GameObject bulletSpawn;
    public float offset;

    [Tooltip("(Seconds) Time it takes for the Soul to disappear")]
    public float deathTimer;
    [Tooltip("(Seconds) Time it takes for the Enemy to Respawn")]
    public float respawnTimer;
    [Tooltip("(Seconds) Time that the Enemy doesn't have a hitbox")]
    public float iFrameTimer;
    public Sprite soul;
    public Sprite normal;

    Animator anim;
    public ParticleSystem deathPFX;
    public ParticleSystem respawnPFX;
    public ParticleSystem soulDisappearPFX;

    bool canTriggerHit = true;

    [SerializeField] AudioSource ghostVanishSFX;
    [SerializeField] AudioSource respawnSFX;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            if (active)
            {
                for (int i = 0; i < numberOfShots; i++)
                {
                    if (active)
                    {
                        
                        var Bullet = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                        Physics2D.IgnoreCollision(Bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                        yield return new WaitForSeconds(timeBetweenMultiShots);
                    }
                }
            }
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public void OnHit()
    {
        if (canTriggerHit)
        {
            anim.SetBool("showSoul", true);
            Instantiate(deathPFX, gameObject.transform);
            active = false;
            Invoke("OnDeath", deathTimer);
            FindObjectOfType<AudioManager>().Play("EyebatSquelch");
            FindObjectOfType<AudioManager>().Play("EyebatSquelch_02");
            FindObjectOfType<AudioManager>().Play("Squeal");
        }
        canTriggerHit = false;
    }

    public void OnDeath()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Sword"))
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
        }
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(soulDisappearPFX, gameObject.transform);
        ghostVanishSFX.Play();
        CursorManager.Instance.ChangeCursor(false);
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        anim.SetBool("showSoul", false);
        yield return new WaitForSeconds(respawnTimer);
        Instantiate(respawnPFX, gameObject.transform);
        respawnSFX.Play();
        yield return new WaitForSeconds(iFrameTimer);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.layer = 28;
        active = true;
        canTriggerHit = true;
    }
}
