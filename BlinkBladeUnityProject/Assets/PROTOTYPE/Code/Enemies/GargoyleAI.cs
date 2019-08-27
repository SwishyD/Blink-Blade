using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleAI : MonoBehaviour
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

    private void Start()
    {
        StartCoroutine("ShockWave");
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
        }
        Debug.Log(inRange);
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
            if (inRange)
            {
                yield return new WaitForSeconds(chargeUpTime);
                GetComponent<SpriteRenderer>().color = Color.yellow;
                yield return new WaitForSeconds(0.2f);
                Instantiate(shockWave, leftSide.position, Quaternion.identity);
                var rightShock = Instantiate(shockWave, rightSide.position, Quaternion.identity);
                rightShock.GetComponent<SpriteRenderer>().flipX = true;
                rightShock.GetComponent<ShockwaveMovement>().isRight = true;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
