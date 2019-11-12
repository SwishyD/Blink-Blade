using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShoot : MonoBehaviour, IEnemyDeath
{
    public bool active;
    bool routineStarted;
    public GameObject bullet;
    [Tooltip("Number of shots in each Volley")]
    public int numberOfShots;
    [Tooltip("This determines time between shots and multi shots")]
    public float timeBetweenShots;
    [Tooltip("If Number of Shots > 1, then this determines time between those shots")]
    public float timeBetweenMultiShots;
    [Tooltip("How long between charge anim and shoot")]
    public float chargeTime;

    public GameObject bulletAimer;
    public GameObject bulletSpawn;
    public float offset;

    [Tooltip("(Seconds) Time it takes for the Soul to disappear")]
    public float deathTimer;
    [Tooltip("(Seconds) Time it takes for the Enemy to Respawn")]
    public float respawnTimer;
    [Tooltip("(Seconds) Time that the Enemy doesn't have a hitbox")]
    public float iFrameTimer;

    public ImpRadius impRadius;

    Animator anim;
    public ParticleSystem deathPFX;
    public ParticleSystem respawnPFX;
    public ParticleSystem soulDisappearPFX;
    public ParticleSystem chargeBulletPFX;

    bool canTriggerHit = true;

    [SerializeField] AudioSource ghostVanishSFX;
    [SerializeField] AudioSource respawnSFX;
    [SerializeField] AudioSource summonSFX;
    [SerializeField] AnimationClip popAnim;

    // Start is called before the first frame update
    void Start()
    {
        routineStarted = false;
        anim = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name.Contains("BOSS"))
        {
            OnHit();
        }
    }

    private void FixedUpdate()
    {
        Vector3 difference = GameObject.FindGameObjectWithTag("Player").transform.position - bulletAimer.transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        bulletAimer.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (!routineStarted && active && impRadius.inRange)
        {
            routineStarted = true;
            StartCoroutine("Shoot");
        }
        if (SceneManager.GetActiveScene().name.Contains("BOSS"))
        {
            respawnTimer = GetComponent<EnemyRespawnTimer>().respawnTimer;
        }
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
            if (active && impRadius.inRange)
            {
                for (int i = 0; i < numberOfShots; i++)
                {
                    if (active && impRadius.inRange)
                    {
                        anim.SetBool("shooting", true);
                        summonSFX.Play();
                        chargeBulletPFX.Play();
                        yield return new WaitForSeconds(chargeTime);
                        var Bullet = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                        Physics2D.IgnoreCollision(Bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                        yield return new WaitForSeconds(timeBetweenMultiShots);
                        anim.SetBool("shooting", false);
                    }
                    else
                    {
                        routineStarted = false;
                        yield break;
                    }
                }
            }
            else
            {
                routineStarted = false;
                yield break;
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
            StopCoroutine("Shoot");
            routineStarted = false;
            anim.SetBool("shooting", false);
            Invoke("OnDeath", deathTimer);
            FindObjectOfType<AudioManager>().Play("EyebatSquelch");
            FindObjectOfType<AudioManager>().Play("EyebatSquelch_02");
            FindObjectOfType<AudioManager>().Play("Squeal");
            Invoke("soulPop", deathTimer - popAnim.length);
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

    void soulPop()
    {
        anim.SetTrigger("warnPop");
    }

    public IEnumerator Respawn()
    {
        anim.SetBool("showSoul", false);
        yield return new WaitForSeconds(respawnTimer);
        Instantiate(respawnPFX, gameObject.transform);
        respawnSFX.Play();
        yield return new WaitForSeconds(iFrameTimer);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        gameObject.layer = 28;
        active = true;
        canTriggerHit = true;
    }
}
