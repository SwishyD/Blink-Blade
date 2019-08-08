using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        Debug.Log("BulletSpawned");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {

        }
        else if(!col.name.Contains("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
