using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourEnemy : MonoBehaviour, IEnemyDeath
{
    public GameObject soul;
    public GameObject soulSpawner;

    public void OnHit()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
        var Soul = Instantiate(soul, soulSpawner.transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(Soul.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        this.gameObject.layer = 10;
    }
}
