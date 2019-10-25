using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossPhases { Neutral, Shooting, Gravity, Spawning, Walls, Finale}

public class FinalBossScript : MonoBehaviour, IEnemyDeath
{
    public BossPhases phases;
    public bool alive;
    [Header("Shooter Variables")]
    #region Shooting
    public bool shootActive;
    public bool spray;
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
    #endregion

    [Header("Spawner Variables")]
    #region Spawning
    [SerializeField]
    public EnemyWave[] enemyWaves;
    public int waveNumber = 0;
    public bool[] waveSpawned;
    public GameObject spawnPFX;
    #endregion

    [Header("Wall Movement Variables")]
    #region Wall Movement
    public GameObject deathWall;
    public Vector2 wallScale;
    public float wallSpeed;
    public bool allWallsActive;
    #endregion

    [Header("Gravity Variables")]
    #region Gravity
    private PlayerFlipManager gravityManager;
    private bool flipActive;
    private bool gravityReset;
    #endregion

    [Header("Finale Variables")]
    #region Boss Finale
    public GameObject parryBox;
    public bool killable;
    public bool attacking;
    public float timeBetweenAttacks;
    public float bossSpeed;
    public float bossRiseSpeed;
    public bool riseUp;
    private bool findPlayer;
    private Vector2 playerPos;

    private GameObject player;
    #endregion

    void Start()
    {
        gravityManager = PlayerFlipManager.instance;
        player = GameObject.Find("PlayerV2");
        findPlayer = false;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (phases)
        {
            case BossPhases.Neutral:

                break;

            case BossPhases.Shooting:

                Vector3 difference = GameObject.FindGameObjectWithTag("Player").transform.position - bulletAimer.transform.position;
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                bulletAimer.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
                if (!shootActive)
                {
                    shootActive = true;
                    StartCoroutine("Shoot");
                }
                break;

            case BossPhases.Spawning:
                if(waveNumber > 0)
                {
                    if (!waveSpawned[waveNumber - 1])
                    {
                        waveSpawned[waveNumber - 1] = true;
                        for (int i = 0; i < enemyWaves[waveNumber - 1].enemies.Length; i++)
                        {
                            Instantiate(spawnPFX, enemyWaves[waveNumber - 1].enemies[i].transform.position, Quaternion.identity);
                            enemyWaves[waveNumber - 1].enemies[i].SetActive(true);
                        }
                    }
                }
                break;

            case BossPhases.Walls:
                if (allWallsActive)
                {
                    deathWall.transform.localScale = Vector2.Lerp(deathWall.transform.localScale, wallScale, wallSpeed * Time.deltaTime);
                }
                else
                {
                    deathWall.transform.localScale = Vector2.Lerp(deathWall.transform.localScale, Vector2.one, wallSpeed * Time.deltaTime);
                }
                break;

            case BossPhases.Gravity:
                if (!flipActive)
                {
                    gravityReset = false;
                    flipActive = true;
                    gravityManager.FlipEnabler(true);
                }
                break;

            case BossPhases.Finale:
                if (alive)
                {
                    parryBox.SetActive(true);
                    killable = true;
                    if (attacking)
                    {
                        if (!findPlayer)
                        {
                            findPlayer = true;
                            playerPos = player.transform.position;
                        }
                        this.transform.position = Vector2.MoveTowards(transform.position, playerPos, bossSpeed * Time.deltaTime);

                        if (Vector2.Distance(transform.position, playerPos) < 0.5f)
                        {
                            attacking = false;
                            riseUp = true;
                            StartCoroutine("RiseUp");
                        }
                    }
                    if (riseUp)
                    {
                        transform.Translate(Vector2.up * bossRiseSpeed * Time.deltaTime);
                    }
                }
                break;
        }

        if (phases != BossPhases.Shooting)
        {
            shootActive = false;
        }
        if(phases != BossPhases.Gravity)
        {
            if (!gravityReset)
            {
                gravityReset = true;
                flipActive = false;
                gravityManager.FlipEnabler(false);
            }
        }
    }

    IEnumerator Shoot()
    {
        while (enabled)
        {
            if (shootActive)
            {
                Debug.Log("Shooty");
                if (spray)
                {
                    for (int i = 0; i < numberOfShots; i++)
                    {
                        offset = Random.Range(0, 20);
                        var Bullet = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                        //Physics2D.IgnoreCollision(Bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                        yield return new WaitForSeconds(timeBetweenMultiShots);
                    }
                }
                else if (!spray)
                {
                    offset = 0;
                    var Bullet = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                    //Physics2D.IgnoreCollision(Bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                }
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    IEnumerator RiseUp()
    {
        if(player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        var normalSpeed = bossRiseSpeed;
        bossRiseSpeed = 0;
        riseUp = true;
        yield return new WaitForSeconds(1f);
        bossRiseSpeed = normalSpeed;
        yield return new WaitForSeconds(timeBetweenAttacks);
        riseUp = false;
        findPlayer = false;
        attacking = true;
        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void OnHit()
    {
        if (killable)
        {
            alive = false;
            Debug.Log("Dead");
            StopAllCoroutines();
        }
    }

    [System.Serializable]
    public class EnemyWave{
        public GameObject[] enemies;
    }
}