using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    public float Speed;

    public bool StuckinObject = false;

    private void Start()
    {
        Invoke("DestroySword", 2f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(StuckinObject == false)
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 8)
        {
            DestroySword();
        }
        if(col.gameObject.layer == 9)
        {
            StuckinObject = true;
        }
        if(col.gameObject.tag == "FlyingEnemy")
        {
            StuckinObject = true;
            col.gameObject.GetComponent<FlyingEnemy>().isHit = true;
            this.transform.parent = col.gameObject.transform;
        }
    }

    void DestroySword()
    {
        if(StuckinObject == false)
        {
            SwordSpawner.instance.swordSpawned = false;
            Destroy(gameObject);
        }
    }
}
