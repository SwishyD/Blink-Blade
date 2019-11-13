using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourEnemy : MonoBehaviour, IEnemyDeath
{
    public GameObject soul;
    public GameObject soulSpawner;

    public bool isHit;
    public float respawnTimer;

    public void OnHit()
    {
        GetComponent<Collider2D>().enabled = false;
        isHit = true;
        GetComponent<SpriteRenderer>().color = Color.blue;
        var Soul = Instantiate(soul, soulSpawner.transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(Soul.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        isHit = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !isHit)
        {
            col.GetComponent<PlayerSpawnPoint>().Respawn();
        }
    }
}
