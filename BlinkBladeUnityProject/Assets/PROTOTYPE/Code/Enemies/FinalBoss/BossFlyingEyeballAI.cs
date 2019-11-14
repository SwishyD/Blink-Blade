using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyingEyeballAI : MonoBehaviour, IEnemyDeath
{
    public Transform[] wayPoints;
    public int moveToward;

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

    public bool isHit;

    Animator anim;
    public ParticleSystem deathPFX;
    public ParticleSystem respawnPFX;
    public ParticleSystem soulDisappearPFX;

    bool canTriggerHit = true;

    [SerializeField] AudioSource ghostVanishSFX;
    [SerializeField] AudioSource respawnSFX;
    [SerializeField] AnimationClip popAnim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(wayPoints.Length > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[moveToward].position, speed * Time.deltaTime);
        }
        else
        {
            speed = 0;
        }

        if(wayPoints.Length > 1 && Vector2.Distance(this.transform.position, wayPoints[moveToward].position) < 0.2f)
        {
            StartCoroutine("EdgePause");
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
        moveToward++;
        if(moveToward > wayPoints.Length - 1)
        {
            moveToward = 0;
        }
        yield return new WaitForSeconds(pauseTimer);
        active = true;
        speed = normalSpeed;
    }

    public void OnHit()
    {
        if (canTriggerHit)
        {
            anim.SetBool("showSoul", true);
            Instantiate(deathPFX, gameObject.transform);
            isHit = true;
            Invoke("OnDeath", deathTimer);
            FindObjectOfType<AudioManager>().Play("EyebatSquelch");
            FindObjectOfType<AudioManager>().Play("EyebatSquelch_02");
            FindObjectOfType<AudioManager>().Play("Squeal");
            Invoke("soulPop", deathTimer - popAnim.length);
            //FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.3f,0.5f,0.5f);
        }
        canTriggerHit = false;
    }

    public void OnDeath()
    {
        foreach(Transform child in transform)
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

    public void Spawn()
    {
        StartCoroutine("Create");
    }

    IEnumerator Respawn()
    {
        anim.SetBool("showSoul", false);
        yield return new WaitForSeconds(respawnTimer);
        StartCoroutine("Create");
    }

    IEnumerator Create()
    {
        Instantiate(respawnPFX, gameObject.transform);
        respawnSFX.Play();
        yield return new WaitForSeconds(iFrameTimer);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        gameObject.layer = 28;
        isHit = false;
        canTriggerHit = true;
    }
}