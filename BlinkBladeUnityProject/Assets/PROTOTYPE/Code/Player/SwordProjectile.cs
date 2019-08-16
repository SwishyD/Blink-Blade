using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    public SwordSpawner spawner;
    public GameObject stuckSword;
    public float speed;

    public float throwDistance;
    public Vector2 hitPoint;
    public LayerMask rayMask;

    public bool stuckInObject = false;

    private void Start()
    {
        spawner = GameObject.Find("Aim Ring").GetComponent<SwordSpawner>();
        Invoke("DestroyUnstuckSword", 2f);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, throwDistance, rayMask);
        Debug.DrawLine(transform.position, hit.point, Color.yellow);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 9 || hit.collider.gameObject.tag == "Enemy")
            {
                hitPoint = hit.point;
                Debug.Log("HitPoint: " + hitPoint);
                if (hit.normal.x > 0)
                {
                    Debug.Log("Hit the Right");
                    if (!stuckInObject)
                    {
                        var CloneSword = Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 180));
                        spawner.cloneSword = CloneSword;
                        CloneSword.transform.parent = hit.collider.transform;
                        stuckInObject = true;
                    }
                    Destroy(gameObject);
                }
                else if (hit.normal.x < 0)
                {
                    Debug.Log("Hit the Left");
                    if (!stuckInObject)
                    {
                        var CloneSword = Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 0));
                        spawner.cloneSword = CloneSword;
                        CloneSword.transform.parent = hit.collider.transform;
                        stuckInObject = true;
                    }
                    Destroy(gameObject);
                }
                else if (hit.normal.y < 0)
                {
                    Debug.Log("Hit the Bottom");
                    if (!stuckInObject)
                    {
                        var CloneSword = Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 90));
                        spawner.cloneSword = CloneSword;
                        CloneSword.transform.parent = hit.collider.transform;
                        stuckInObject = true;
                    }
                    Destroy(gameObject);
                }
                else if (hit.normal.y > 0)
                {
                    Debug.Log("Hit the Top");
                    if (!stuckInObject)
                    {
                        var CloneSword = Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 270));
                        spawner.cloneSword = CloneSword;
                        CloneSword.transform.parent = hit.collider.transform;
                        stuckInObject = true;
                    }
                    Destroy(gameObject);
                }
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<IEnemyDeath>().OnDeath();
                    speed = 0;
                    stuckInObject = true;
                }
            }
            else if (hit.collider.gameObject.layer == 8)
            {
                DestroySword();
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

    public void DestroyUnstuckSword()
    {
        if(stuckInObject == false)
        {
            SwordSpawner.instance.swordSpawned = false;
            spawner.cloneSword = null;
            Destroy(gameObject);
        }
    }
    public void DestroySword()
    {
        SwordSpawner.instance.swordSpawned = false;
        spawner.cloneSword = null;
        Destroy(gameObject);
    }
}
