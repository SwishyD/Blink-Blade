using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossPhases { Neutral, Shooting, Gravity, Spawning, Walls, Finale}

public class FinalBossScript : MonoBehaviour, IEnemyDeath
{
    public BossPhases phases;
    public bool alive;
    Animator anim;

    [System.Serializable]
    public class ShooterVariable
    {
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
    }
    public ShooterVariable shooterVariable;

    [System.Serializable]
    public class SpawnerVariables
    {
        [Header("Spawner Variables")]
        #region Spawning
        [SerializeField]
        public EnemyWave[] enemyWaves;
        public int waveNumber = 0;
        public bool[] waveSpawned;
        public GameObject spawnPFX;
        #endregion
    }
    public SpawnerVariables spawnerVariables;

    [System.Serializable]
    public class WallVariables
    {
        [Header("Wall Movement Variables")]
        #region Wall Movement
        public GameObject deathWall;
        public Vector2 wallScale;
        public float wallZoomSpeed;
        public bool allWallsActive;
        public bool wallMove;
        public Transform[] wallWaypoints;
        public float wallMovementSpeed;
        public int moveWallTo;
        #endregion
    }
    public WallVariables wallVariables;

    [System.Serializable]
    public class GravityVariables
    {
        [Header("Gravity Variables")]
        #region Gravity
        public PlayerFlipManager gravityManager;
        public bool flipActive;
        public bool gravityReset;
        #endregion
    }
    public GravityVariables gravityVariables;

    [System.Serializable]
    public class FinaleVariables
    {
        [Header("Finale Variables")]
        #region Boss Finale
        public bool parented;
        public GameObject parryBox;
        public bool killable;
        public bool attacking;
        public float timeBetweenAttacks;
        public float bossSpeed;
        public float bossSpeedMax;
        public float bossSpeedUpTime;
        public float bossRiseSpeed;
        public bool riseUp;
        public bool findPlayer;
        public Vector2 playerPos;
        public float minY;
        public float maxY;

        public GameObject player;
        public LayerMask diveMask;
        public RaycastHit2D hit;
        #endregion
    }
    public FinaleVariables finaleVariables;

    void Start()
    {
        gravityVariables.gravityManager = PlayerFlipManager.instance;
        finaleVariables.player = GameObject.Find("PlayerV2");
        finaleVariables.findPlayer = false;
        alive = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (phases)
        {
            #region Neutral
            case BossPhases.Neutral:

                anim.SetInteger("BossPhase", 0);

                break;
            #endregion

            #region Shooting
            case BossPhases.Shooting:

                anim.SetInteger("BossPhase", 1);

                Vector3 difference = finaleVariables.player.transform.position - shooterVariable.bulletAimer.transform.position;
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                shooterVariable.bulletAimer.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + shooterVariable.offset);
                if (!shooterVariable.shootActive)
                {
                    shooterVariable.shootActive = true;
                    //StartCoroutine("Shoot");
                }
                break;
            #endregion

            #region Spawning
            case BossPhases.Spawning:

                anim.SetInteger("BossPhase", 3);

                if (spawnerVariables.waveNumber > 0)
                {
                    if (!spawnerVariables.waveSpawned[spawnerVariables.waveNumber - 1])
                    {
                        spawnerVariables.waveSpawned[spawnerVariables.waveNumber - 1] = true;
                        anim.SetTrigger("Summon");
                        //for (int i = 0; i < spawnerVariables.enemyWaves[spawnerVariables.waveNumber - 1].enemies.Length; i++)
                        //{
                        //    Instantiate(spawnerVariables.spawnPFX, spawnerVariables.enemyWaves[spawnerVariables.waveNumber - 1].enemies[i].transform.position, Quaternion.identity);
                        //    spawnerVariables.enemyWaves[spawnerVariables.waveNumber - 1].enemies[i].GetComponent<IEnemyDeath>().Spawn();
                        //}
                    }
                }
                break;
            #endregion

            #region Walls
            case BossPhases.Walls:

                anim.SetInteger("BossPhase", -1);

                if (wallVariables.allWallsActive)
                {

                    wallVariables.deathWall.transform.localScale = Vector2.Lerp(wallVariables.deathWall.transform.localScale, wallVariables.wallScale, wallVariables.wallZoomSpeed * Time.deltaTime);

                    if (wallVariables.wallMove)
                    {
                        wallVariables.deathWall.transform.localPosition = Vector2.MoveTowards(wallVariables.deathWall.transform.localPosition, wallVariables.wallWaypoints[wallVariables.moveWallTo].localPosition, wallVariables.wallMovementSpeed * Time.deltaTime);
                        if (Vector2.Distance(wallVariables.deathWall.transform.localPosition, wallVariables.wallWaypoints[wallVariables.moveWallTo].localPosition) < 0.2f)
                        {
                            if (wallVariables.wallWaypoints.Length > wallVariables.moveWallTo + 1)
                            {
                                wallVariables.moveWallTo++;
                            }
                            else
                            {
                                wallVariables.wallMove = false;
                            }
                        }
                    }
                    else
                    {
                        wallVariables.deathWall.transform.localPosition = Vector2.MoveTowards(wallVariables.deathWall.transform.localPosition, Vector2.zero, wallVariables.wallMovementSpeed * Time.deltaTime);
                    }
                }
                else
                {
                    wallVariables.deathWall.transform.localScale = Vector2.Lerp(wallVariables.deathWall.transform.localScale, Vector2.one, wallVariables.wallZoomSpeed * Time.deltaTime);
                }
                break;
            #endregion

            #region Gravity
            case BossPhases.Gravity:

                anim.SetInteger("BossPhase", 2);

                if (!gravityVariables.flipActive)
                {
                    gravityVariables.gravityReset = false;
                    gravityVariables.flipActive = true;
                    anim.SetBool("FlippingActive", true);
                    gravityVariables.gravityManager.FlipEnabler(true);
                }
                break;
            #endregion

            #region Finale
            case BossPhases.Finale:

                anim.SetInteger("BossPhase", 0);

                if (alive)
                {
                    if (finaleVariables.parented)
                    {
                        finaleVariables.parented = false;
                        this.transform.parent = null;
                    }
                    finaleVariables.parryBox.SetActive(true);
                    finaleVariables.killable = true;
                    Vector3 difference2 = finaleVariables.player.transform.position - shooterVariable.bulletAimer.transform.position;
                    float rotZ2 = Mathf.Atan2(difference2.y, difference2.x) * Mathf.Rad2Deg;
                    shooterVariable.bulletAimer.transform.rotation = Quaternion.Euler(0f, 0f, rotZ2 + shooterVariable.offset);
                    if (finaleVariables.attacking)
                    {
                        anim.SetBool("Diving", true);
                        if (!finaleVariables.findPlayer)
                        {
                            finaleVariables.bossSpeedUpTime = 0;
                            finaleVariables.hit = Physics2D.Raycast(shooterVariable.bulletAimer.transform.position, finaleVariables.player.transform.position - shooterVariable.bulletAimer.transform.position, 50f, finaleVariables.diveMask);
                            finaleVariables.findPlayer = true;
                        }
                        if (finaleVariables.findPlayer)
                        {
                            finaleVariables.bossSpeed = Mathf.Lerp(0, finaleVariables.bossSpeedMax, finaleVariables.bossSpeedUpTime);
                            finaleVariables.bossSpeedUpTime += 2f * Time.deltaTime;
                            this.transform.localPosition = Vector2.MoveTowards(transform.localPosition, finaleVariables.hit.point, finaleVariables.bossSpeed * Time.deltaTime);
                        }

                        if (Vector2.Distance(transform.localPosition, finaleVariables.hit.point) < 1f || this.transform.localPosition.y <= finaleVariables.minY)
                        {
                            finaleVariables.attacking = false;
                            finaleVariables.riseUp = true;
                            StartCoroutine("RiseUp");
                        }
                    }
                    if (this.transform.localPosition.y >= finaleVariables.maxY && finaleVariables.riseUp)
                    {
                        Debug.Log("Max Y");
                        StartCoroutine("Attack");
                    }
                    if (finaleVariables.riseUp)
                    {
                        transform.Translate(Vector2.up * finaleVariables.bossRiseSpeed * Time.deltaTime);
                    }
                }
                Debug.Log(finaleVariables.hit.point);
                Debug.DrawRay(shooterVariable.bulletAimer.transform.position, finaleVariables.hit.point);
                break;
                #endregion
        }

        if (phases != BossPhases.Shooting)
        {
            shooterVariable.shootActive = false;
        }
        if (phases != BossPhases.Gravity)
        {
            if (!gravityVariables.gravityReset)
            {
                gravityVariables.gravityReset = true;
                gravityVariables.flipActive = false;
                anim.SetBool("FlippingActive", false);
                anim.SetInteger("Gravity", -1);
                gravityVariables.gravityManager.FlipEnabler(false);
            }
        }
    }

    public IEnumerator Shoot()
    {
        while (enabled)
        {
            if (shooterVariable.shootActive)
            {
                Debug.Log("Shooty");
                if (shooterVariable.spray)
                {
                    for (int i = 0; i < shooterVariable.numberOfShots; i++)
                    {
                        shooterVariable.offset = Random.Range(0, 20);
                        var Bullet = Instantiate(shooterVariable.bullet, shooterVariable.bulletSpawn.transform.position, shooterVariable.bulletSpawn.transform.rotation);
                        anim.SetBool("Shooting", true);
                        Physics2D.IgnoreCollision(Bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                        yield return new WaitForSeconds(shooterVariable.timeBetweenMultiShots);
                    }
                }
                else if (!shooterVariable.spray)
                {
                    shooterVariable.offset = 0;
                    var Bullet = Instantiate(shooterVariable.bullet, shooterVariable.bulletSpawn.transform.position, shooterVariable.bulletSpawn.transform.rotation);
                    anim.SetBool("Shooting", true);
                    Physics2D.IgnoreCollision(Bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
                }
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(shooterVariable.timeBetweenShots);
        }
        anim.SetBool("Shooting", false);
    }

    public IEnumerator RiseUp()
    {
        if(finaleVariables.player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(finaleVariables.player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        var normalSpeed = finaleVariables.bossRiseSpeed;
        finaleVariables.bossRiseSpeed = 0;
        finaleVariables.riseUp = true;
        anim.SetBool("Diving", false);
        yield return new WaitForSeconds(1f);
        finaleVariables.bossRiseSpeed = normalSpeed;
    }

    public IEnumerator Attack()
    {
        finaleVariables.riseUp = false;
        Debug.Log("StopRise");
        yield return new WaitForSeconds(finaleVariables.timeBetweenAttacks);
        finaleVariables.findPlayer = false;
        finaleVariables.attacking = true;
        if (finaleVariables.player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (finaleVariables.player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnerVariables.enemyWaves[spawnerVariables.waveNumber - 1].enemies.Length; i++)
        {
            Instantiate(spawnerVariables.spawnPFX, spawnerVariables.enemyWaves[spawnerVariables.waveNumber - 1].enemies[i].transform.position, Quaternion.identity);
            spawnerVariables.enemyWaves[spawnerVariables.waveNumber - 1].enemies[i].GetComponent<IEnemyDeath>().Spawn();
        }
    }

    public void OnHit()
    {
        if (finaleVariables.killable)
        {
            alive = false;
            Debug.Log("Dead");
            anim.SetTrigger("Death");
            GetComponentInChildren<ParticleSystem>().Play(); //Blood PFX
            //Play sounds here
            StopAllCoroutines();
            StartCoroutine("DeathBuildUp");
        }
    }

    public IEnumerator DeathBuildUp()
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(1f, 0.1f, 0.9f);
        yield return new WaitForSeconds(1);
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(1f, 0.3f, 0.7f);
        yield return new WaitForSeconds(1);
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(1f, 0.5f, 0.4f);
        yield return new WaitForSeconds(1);
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(1f, 0.7f, 0.1f);
        yield return new WaitForSeconds(1);
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(10f, 1f, 0.2f);
        gameObject.SetActive(false);
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(wallVariables.wallWaypoints.Length > 0)
        {
            Gizmos.DrawLine(wallVariables.deathWall.transform.position, wallVariables.wallWaypoints[0].position);
        }
        for (int i = 0; i < wallVariables.wallWaypoints.Length; i++)
        {
            if (wallVariables.wallWaypoints.Length > i + 1)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(wallVariables.wallWaypoints[i].position, wallVariables.wallWaypoints[i + 1].position);
            }
        }
    }

    [System.Serializable]
    public class EnemyWave{
        public GameObject[] enemies;
    }
}