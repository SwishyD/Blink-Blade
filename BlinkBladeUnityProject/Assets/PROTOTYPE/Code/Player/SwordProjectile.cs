using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    public float speed;

    public bool stuckInObject = false;
    public GameObject objectStuckIn;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>());

    }

    private void Start()
    {
        Invoke("DestroySword", 2f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(stuckInObject == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.layer == 8)
        {
            DestroySword();
        }
        if(col.gameObject.layer == 9)
        {
            foreach(ContactPoint2D hitPos in col.contacts)
            {
                Debug.Log(hitPos.normal);

                if (hitPos.normal.y > 0)
                {
                    Debug.Log("Hit the Top");
                }
                else if (hitPos.normal.y < 0)
                {
                    Debug.Log("Hit the Bottom");
                }
                else if(hitPos.normal.x > 0)
                {
                    Debug.Log("Hit the Right");
                }
                else if(hitPos.normal.x < 0)
                {
                    Debug.Log("Hit the Left");
                }
            }
            speed = 0;
            stuckInObject = true;
        }
    }

    void DestroySword()
    {
        if(stuckInObject == false)
        {
            SwordSpawner.instance.swordSpawned = false;
            Destroy(gameObject);
        }
    }
}
