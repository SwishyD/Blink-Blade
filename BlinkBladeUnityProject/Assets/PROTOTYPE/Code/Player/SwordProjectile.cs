using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    public SwordSpawner spawner;
    public GameObject stuckSword;
    public float speed;
    public float throwDistance;

    private float swordRotation;
    private Transform swordHit;

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
                swordHit = hit.collider.transform;
                if (Mathf.Abs(hit.normal.x) > Mathf.Abs(hit.normal.y))
                {
                    //If Sword hits right side of collider
                    if (hit.normal.x > 0)
                    {
                        swordRotation = 180;
                        SpawnStuckSword();
                        spawner.stuckLeft = true;                       
                    }
                    //If sword hits left side of collider
                    else if (hit.normal.x < 0)
                    {
                        swordRotation = 0;
                        SpawnStuckSword();
                        spawner.stuckRight = true;
                    }
                }
                else if(Mathf.Abs(hit.normal.x) < Mathf.Abs(hit.normal.y))
                {
                    //If sword hits bottom of collider
                    if (hit.normal.y < 0)
                    {
                        swordRotation = 90;
                        SpawnStuckSword();
                    }
                    else if (hit.normal.y > 0)
                    {
                        //If sword hits top of collider
                        {
                            swordRotation = 270;
                            SpawnStuckSword();
                            spawner.stuckDown = true;
                        }
                    }
                }
                trail1.transform.parent = null;
                trail2.transform.parent = null;
                trail1.transform.position = hit.point;
                trail2.transform.position = hit.point;
                CursorManager.Instance.ChangeCursor(true);
                Destroy(gameObject);
            }
            else if (hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 29)
            {
                Instantiate(swordBreakPFX, hit.point, Quaternion.identity);
                AudioManager.instance.Play("SwordBreak");
                DestroySword();
            }

            if (hit.transform.name.Contains("Bullet"))
            {
                Destroy(hit.collider.gameObject);
            }
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.gameObject.GetComponent<IEnemyDeath>().OnHit();
                hit.transform.gameObject.layer = 10;
                speed = 0;
                stuckInObject = true;
            }
        }
    }

    void SpawnStuckSword()
    {
        var CloneSword = Instantiate(stuckSword, hitPoint, Quaternion.Euler(0, 0, swordRotation));
        spawner.cloneSword = CloneSword;
        ResetDirection();
        CloneSword.transform.parent = swordHit;
        stuckInObject = true;
    }

    void ResetDirection()
    {
        spawner.stuckLeft = false;
        spawner.stuckRight = false;
        spawner.stuckDown = false;
    }

    public void DestroySword()
    {
        SwordSpawner.instance.swordSpawned = false;
        spawner.cloneSword = null;
        CursorManager.Instance.ChangeCursor(false);
        Destroy(gameObject);
    }
}
