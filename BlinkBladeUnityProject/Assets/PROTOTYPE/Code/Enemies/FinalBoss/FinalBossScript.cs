using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossPhases { Neutral, Shooting, Gravity, Spawning, Walls, Finale}

public class FinalBossScript : MonoBehaviour
{
    public BossPhases phases;
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
    public bool[] wallActive;
    #endregion


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

        }

        if (phases != BossPhases.Shooting)
        {
            shootActive = false;
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

    [System.Serializable]
    public class EnemyWave{
        public GameObject[] enemies;
    }
}
