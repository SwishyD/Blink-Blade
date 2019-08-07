using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourEnemy : MonoBehaviour
{
    public GameObject soul;
    public GameObject soulSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Sword"))
        {
            other.gameObject.GetComponent<SwordProjectile>().StuckinObject = this.gameObject;
            GetComponent<SpriteRenderer>().color = Color.blue;
            var Soul = Instantiate(soul, soulSpawner.transform.position, Quaternion.identity);
            Physics2D.IgnoreCollision(Soul.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            this.gameObject.layer = 10;
        }
    }
}
