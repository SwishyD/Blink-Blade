using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGargoyleAI : MonoBehaviour, IEnemyDeath
{
    public bool inRange;
    [Tooltip("Number of tiles away from the Gargoyle to detect the player")]
    public float detectionRange;
    [Tooltip("Number in Seconds between each shockwave being released")]
    public float timeBetweenShots;
    public float chargeUpTime;


    public Transform leftSide;
    public Transform rightSide;

    public bool active;

    public GameObject shockWave;
    public GameObject respawnPFX;
    [SerializeField] AudioSource respawnSFX;

    Animator anim;
    ParticleSystem smashPFX;

    private void Start()
    {
        StartCoroutine("ShockWave");
        anim = GetComponent<Animator>();
        smashPFX = GetComponentInChildren<ParticleSystem>();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        active = false;
    }

    private void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, GameObject.Find("PlayerV2").transform.position) <= detectionRange)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
        //Debug.Log(inRange);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerSpawnPoint>().Respawn();
        }
    }

    IEnumerator ShockWave()
    {
        while (true)
        {
            if (inRange && active)
            {
                yield return new WaitForSeconds(chargeUpTime);
                anim.SetTrigger("Smash");
            }
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public void Spawn()
    {
        Instantiate(respawnPFX, gameObject.transform);
        respawnSFX.Play();
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        gameObject.layer = 8;
        active = true;
    }

    public void OnHit()
    {
        Destroy(SwordSpawner.instance.cloneSword);
        SwordSpawner.instance.cloneSword = null;
        SwordSpawner.instance.closeToGround = false;
        SwordSpawner.instance.swordSpawned = false;
        //CursorManager.Instance.ChangeCursorState(false);
    }

    public void SpawnShockwaves()
    {
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.5f, 0.8f, .5f);
        Instantiate(shockWave, leftSide.position, Quaternion.identity);
        var rightShock = Instantiate(shockWave, rightSide.position, Quaternion.identity);
        rightShock.GetComponent<SpriteRenderer>().flipX = true;
        rightShock.GetComponent<ShockwaveMovement>().isRight = true;
        AudioManager.instance.Play("HeavyLand");
        AudioManager.instance.Play("BassImpact");
        smashPFX.Play();
    }
}