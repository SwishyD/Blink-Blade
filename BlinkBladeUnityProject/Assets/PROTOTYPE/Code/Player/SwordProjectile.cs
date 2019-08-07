using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    public float Speed;

    public bool StuckinObject = false;
    public GameObject objectStuckIn;

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
        if (StuckinObject)
        {
            this.transform.position = objectStuckIn.transform.position;
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
