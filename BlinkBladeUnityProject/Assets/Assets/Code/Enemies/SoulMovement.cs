using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMovement : MonoBehaviour
{
    public float speed;
    public float throwDistance;
    public LayerMask rayMask;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            if(transform.childCount > 0)
            {
                PlayerJumpV2.instance.ResetGravity();
                PlayerJumpV2.instance.PlayerNormal();
                SwordSpawner.instance.swordSpawned = false;
                SwordSpawner.instance.cloneSword = null;
            }
            Destroy(this.gameObject);
        }
        if (col.gameObject.name.Contains("Sword"))
        {
            col.transform.position = transform.position;
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
