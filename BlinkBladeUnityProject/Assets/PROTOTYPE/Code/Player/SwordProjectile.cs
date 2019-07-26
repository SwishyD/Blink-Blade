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

    private void OnCollisionEnter2D(Collision2D col)
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
            GameObject.Find("Aim Ring").GetComponent<SwordSpawner>().swordSpawned = false;
            Destroy(gameObject);
        }
    }
}
