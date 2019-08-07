using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMovement : MonoBehaviour
{
    public float speed;

    private void Awake()
    {
        Debug.Log("Soul");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
