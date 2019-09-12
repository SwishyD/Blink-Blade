using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatrolDir { Left, Right, Up, Down}

public class FlyingEyeballAI : MonoBehaviour, IEnemyDeath
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

    //public Sprite soul;
    //public Sprite normal;
    public bool isHit;

    Animator anim;
    public ParticleSystem deathPFX;
    public ParticleSystem respawnPFX;
    public ParticleSystem soulDisappearPFX;

    bool canTriggerHit = true;

    [SerializeField] AudioSource ghostVanishSFX;
    [SerializeField] AudioSource respawnSFX;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

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
        //GetComponent<SpriteRenderer>().sprite = soul;
        if (canTriggerHit)
        {
            anim.SetBool("showSoul", true);
            Instantiate(deathPFX, gameObject.transform);
            isHit = true;
            Invoke("OnDeath", deathTimer);
            FindObjectOfType<AudioManager>().Play("EyebatSquelch");
            FindObjectOfType<AudioManager>().Play("EyebatSquelch_02");
            FindObjectOfType<AudioManager>().Play("Squeal");
            FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.3f,0.5f,0.5f);
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
        //CursorManager.Instance.ChangeCursorState(false);
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
        isHit = false;
        canTriggerHit = true;
    }
}