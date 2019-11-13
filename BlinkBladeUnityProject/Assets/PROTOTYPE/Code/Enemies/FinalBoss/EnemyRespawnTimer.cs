using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnTimer : MonoBehaviour
{
    public float respawnTimer;

    public void startCoroutine()
    {
        StartCoroutine("ResetRespawnTimer");
    }

    IEnumerator ResetRespawnTimer()
    {
        respawnTimer = 0;
        StopCoroutine(GetComponent<IEnemyDeath>().Respawn());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(GetComponent<IEnemyDeath>().Respawn());
        yield return new WaitForSeconds(0.5f);
        respawnTimer = 99999;
    }
}
