using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootTrigger : MonoBehaviour, IEnemyDeath
{
    public void Spawn()
    {
        GetComponentInChildren<BossEnemyShoot>().Spawn();
    }

    public void OnHit()
    {
    }
}
