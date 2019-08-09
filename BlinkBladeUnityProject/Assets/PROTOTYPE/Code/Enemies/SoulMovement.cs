using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMovement : MonoBehaviour
{
    public float speed;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
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
