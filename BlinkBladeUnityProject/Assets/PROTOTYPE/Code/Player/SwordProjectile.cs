using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{

    public GameObject stuckSword;
    public float speed;

    public float throwDistance;
    public Vector3 hitPoint;

    public bool stuckInObject = false;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
    }

    private void Start()
    {
        Invoke("DestroySword", 2f);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position + transform.right, throwDistance);
        Debug.DrawLine(transform.position, transform.position + transform.right * throwDistance, Color.yellow);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.layer == 9)
            {
                hitPoint = hit.point;
                Debug.Log("HitPoint: " + hitPoint);
            }
        }
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
                    Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 270));
                    //Destroy(gameObject);
                }
                else if (hitPos.normal.y < 0)
                {
                    Debug.Log("Hit the Bottom");
                    Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 90));
                    //Destroy(gameObject);
                }
                else if(hitPos.normal.x > 0)
                {
                    Debug.Log("Hit the Right");
                    Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 180));
                    //Destroy(gameObject);
                }
                else if(hitPos.normal.x < 0)
                {
                    Debug.Log("Hit the Left");
                    Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 0));
                    //Destroy(gameObject);
                }
            }
            speed = 0;
            stuckInObject = true;
        }
        
        if(col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<IEnemyDeath>().OnDeath();
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
