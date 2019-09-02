using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    public SwordSpawner spawner;
    public GameObject stuckSword;
    public float speed;
    public float throwDistance;

    public float hitPointRay;

    public float distanceFromWall;
    public Vector2 hitPoint;
    public LayerMask rayMask;

    public GameObject trail1;
    public GameObject trail2;

    public GameObject swordBreakPFX;

    public bool stuckInObject = false;

    private void Start()
    {
        spawner = GameObject.Find("Aim Ring").GetComponent<SwordSpawner>();
        Invoke("DestroyUnstuckSword", 2f);
    }

    private void FixedUpdate()
    {
        if (stuckInObject == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (Vector2.Distance(this.transform.position, GameObject.Find("PlayerV2").transform.position) >= throwDistance)
        {
            DestroySword();
        }
        RaycastHit2D longHitPoint = Physics2D.Raycast(transform.position, transform.right, hitPointRay, rayMask);
        if (longHitPoint.collider != null)
        {
            if (longHitPoint.collider.gameObject.layer == 9 || longHitPoint.collider.gameObject.layer == 28)
            {
                hitPoint = longHitPoint.point;
            }
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distanceFromWall, rayMask);
        Debug.DrawLine(transform.position, hit.point, Color.yellow);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 28)
            {
                Debug.Log("HitPoint: " + hitPoint);
                if(Mathf.Abs(hit.normal.x) > Mathf.Abs(hit.normal.y))
                {
                    if (hit.normal.x > 0)
                    {
                        var CloneSword = Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 180));
                        spawner.cloneSword = CloneSword;
                        spawner.stuckLeft = true;
                        spawner.stuckRight = false;
                        spawner.stuckDown = false;
                        CloneSword.transform.parent = hit.collider.transform;
                        stuckInObject = true;
                        //CursorManager.Instance.ChangeCursorState(true);
                    }
                    else if (hit.normal.x < 0)
                    {
                        var CloneSword = Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 0));
                        spawner.cloneSword = CloneSword;
                        CloneSword.transform.parent = hit.collider.transform;
                        stuckInObject = true;
                        spawner.stuckLeft = false;
                        spawner.stuckRight = true;
                        spawner.stuckDown = false;
                        //CursorManager.Instance.ChangeCursorState(true);
                    }
                }
                else if(Mathf.Abs(hit.normal.x) < Mathf.Abs(hit.normal.y))
                {
                    if (hit.normal.y < 0)
                    {
                        var CloneSword = Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 90));
                        spawner.cloneSword = CloneSword;
                        CloneSword.transform.parent = hit.collider.transform;
                        stuckInObject = true;
                        spawner.stuckLeft = false;
                        spawner.stuckRight = false;
                        spawner.stuckDown = false;
                        //CursorManager.Instance.ChangeCursorState(true);
                    }
                    else if (hit.normal.y > 0)
                    {
                        if (hit.collider.name.Contains("Soul"))
                        {
                            var ClonerSword = Instantiate(stuckSword, hitPoint - new Vector2(0, 1), Quaternion.Euler(0, 0, 90));
                            spawner.cloneSword = ClonerSword;
                            ClonerSword.transform.parent = hit.collider.transform;
                            stuckInObject = true;
                            spawner.stuckLeft = false;
                            spawner.stuckRight = false;
                            spawner.stuckDown = false;
                            //CursorManager.Instance.ChangeCursorState(true);
                        }
                        else
                        {
                            var CloneSword = Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, 270));
                            spawner.cloneSword = CloneSword;
                            CloneSword.transform.parent = hit.collider.transform;
                            spawner.stuckLeft = false;
                            spawner.stuckRight = false;
                            spawner.stuckDown = true;
                            stuckInObject = true;
                            //CursorManager.Instance.ChangeCursorState(true);
                        }
                    }
                }
                trail1.transform.parent = null;
                trail2.transform.parent = null;
                trail1.transform.position = hit.point;
                trail2.transform.position = hit.point;
                Destroy(gameObject);
            }
            else if (hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 29)
            {
                Instantiate(swordBreakPFX, hit.point, Quaternion.identity);
                DestroySword();
            }

            if (hit.transform.name.Contains("Bullet"))
            {
                Debug.Log("BulletSplit");
                Destroy(hit.collider.gameObject);
            }
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.gameObject.GetComponent<IEnemyDeath>().OnHit();
                speed = 0;
                stuckInObject = true;
                //CursorManager.Instance.ChangeCursorState(true);
            }
        }
    }

    public void DestroyUnstuckSword()
    {
        if(stuckInObject == false)
        {
            SwordSpawner.instance.swordSpawned = false;
            spawner.cloneSword = null;
            //CursorManager.Instance.ChangeCursorState(false);
            Destroy(gameObject);
        }
    }
    public void DestroySword()
    {
        SwordSpawner.instance.swordSpawned = false;
        spawner.cloneSword = null;
        //CursorManager.Instance.ChangeCursorState(false);
        Destroy(gameObject);
    }
}
