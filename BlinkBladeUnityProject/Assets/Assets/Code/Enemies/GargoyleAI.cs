using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GargoyleAI : MonoBehaviour, IEnemyDeath
{
    public bool inRange;
    [Tooltip("Number of tiles away from the Gargoyle to detect the player")]
    public float detectionRange;
    [Tooltip("Number in Seconds between each shockwave being released")]
    public float timeBetweenShots;
    public float chargeUpTime;


    public Transform leftSide;
    public Transform rightSide;

    public GameObject shockWave;

    Animator anim;
    ParticleSystem smashPFX;

    private void Start()
    {
        StartCoroutine("ShockWave");
        anim = GetComponent<Animator>();
        smashPFX = GetComponentInChildren<ParticleSystem>();
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
            if (SceneManager.GetActiveScene().name.Contains("BOSS"))
            {
                col.GetComponent<BossPlayerSpawnPoint>().Respawn();
            }
            else
            {
                col.GetComponent<PlayerSpawnPoint>().Respawn();
            }
        }
    }

    IEnumerator ShockWave()
    {
        while (true)
        {
            if (inRange)
            {
                yield return new WaitForSeconds(chargeUpTime);
                anim.SetTrigger("Smash");
            }
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public void Spawn()
    {
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