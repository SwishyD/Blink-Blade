using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawner : MonoBehaviour
{

    public static SwordSpawner instance = null;

    public float offset;

    public Transform shotPoint;

    public float throwDistance;

    public GameObject cloneSword;
    public GameObject sword;
    public GameObject player;

    public bool stuckLeft;
    public bool stuckRight;
    public bool stuckDown;

    public bool swordSpawned;

    public bool closeToGround;
    public LayerMask rayMask;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
        cloneSword = null;
        swordSpawned = false;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        #region Left Click Options
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, throwDistance, rayMask);
        Debug.DrawLine(transform.position, hit.point, Color.yellow);
        if(hit.collider == null)
        {
            if (Input.GetMouseButtonDown(0) && swordSpawned == false && player.GetComponent<PlayerJumpV2>().isHanging == false)
            {
                Destroy(cloneSword);
                cloneSword = Instantiate(sword, shotPoint.position, transform.rotation);
                Debug.Log("SwordSpawned");
                swordSpawned = true;
                closeToGround = false;
                CursorManager.Instance.ChangeCursorState(false);
            }
            else if (Input.GetMouseButtonDown(0) && swordSpawned == true && player.GetComponent<PlayerJumpV2>().isHanging == true)
            {
                Destroy(cloneSword);
                cloneSword = null;
                player.GetComponent<PlayerJumpV2>().ResetGravity();
                player.GetComponent<PlayerJumpV2>().isHanging = false;
                cloneSword = Instantiate(sword, shotPoint.position, transform.rotation);
                swordSpawned = true;
                closeToGround = false;
                CursorManager.Instance.ChangeCursorState(false);
            }
            else if (Input.GetMouseButtonDown(0) && swordSpawned == true && cloneSword.name.Contains("ThrownSword"))
            {
                var swordPoint = cloneSword.transform.position;
                Destroy(cloneSword);
                cloneSword = null;
                var returnSword = Instantiate(sword, swordPoint, transform.rotation);
                returnSword.GetComponent<SwordProjectile>().enabled = false;
                returnSword.GetComponent<ReturnSword>().enabled = true;
                //player.GetComponent<PlayerJumpV2>().ResetGravity();
                player.GetComponent<PlayerJumpV2>().isHanging = false;
                closeToGround = false;
                swordSpawned = false;
                CursorManager.Instance.ChangeCursorState(false);
            }
            else if (Input.GetMouseButtonDown(0) && swordSpawned == true && cloneSword.name.Contains("StuckSword"))
            {
                var swordPoint = cloneSword.transform.position;
                Destroy(cloneSword);
                cloneSword = null;
                var returnSword = Instantiate(sword, swordPoint, transform.rotation);
                returnSword.GetComponent<SwordProjectile>().enabled = false;
                returnSword.GetComponent<ReturnSword>().enabled = true;
                //player.GetComponent<PlayerJumpV2>().ResetGravity();
                player.GetComponent<PlayerJumpV2>().isHanging = false;
                closeToGround = false;
                swordSpawned = false;
                CursorManager.Instance.ChangeCursorState(false);
            }
        }
        else if(hit.collider != null)
        {
           if(hit.collider.gameObject.layer == 9 /* || hit.collider.tag == "Enemy"*/)
           {
                if (Input.GetMouseButtonDown(0) && swordSpawned == false && player.GetComponent<PlayerJumpV2>().isHanging == false)
                {
                    Destroy(cloneSword);
                    cloneSword = Instantiate(sword, shotPoint.position, transform.rotation);
                    cloneSword.GetComponent<SwordProjectile>().speed = 10;
                    Debug.Log("SwordSpawned");
                    swordSpawned = true;
                    closeToGround = false;
                    CursorManager.Instance.ChangeCursorState(false);
                }
                else if (Input.GetMouseButtonDown(0) && swordSpawned == true && player.GetComponent<PlayerJumpV2>().isHanging == true)
                {
                    Destroy(cloneSword);
                    cloneSword = null;
                    player.GetComponent<PlayerJumpV2>().ResetGravity();
                    player.GetComponent<PlayerJumpV2>().isHanging = false;
                    cloneSword = Instantiate(sword, shotPoint.position, transform.rotation);
                    cloneSword.GetComponent<SwordProjectile>().speed = 10;
                    swordSpawned = true;
                    closeToGround = false;
                    CursorManager.Instance.ChangeCursorState(false);
                }
                else if (Input.GetMouseButtonDown(0) && swordSpawned == true && cloneSword.name.Contains("ThrownSword"))
                {
                    var swordPoint = cloneSword.transform.position;
                    Destroy(cloneSword);
                    cloneSword = null;
                    var returnSword = Instantiate(sword, swordPoint, transform.rotation);
                    returnSword.GetComponent<SwordProjectile>().enabled = false;
                    returnSword.GetComponent<ReturnSword>().enabled = true;
                    //player.GetComponent<PlayerJumpV2>().ResetGravity();
                    player.GetComponent<PlayerJumpV2>().isHanging = false;
                    closeToGround = false;
                    swordSpawned = false;
                    CursorManager.Instance.ChangeCursorState(false);
                }
                else if (Input.GetMouseButtonDown(0) && swordSpawned == true && cloneSword.name.Contains("StuckSword"))
                {
                    var swordPoint = cloneSword.transform.position;
                    Destroy(cloneSword);
                    cloneSword = null;
                    var returnSword = Instantiate(sword, swordPoint, transform.rotation);
                    returnSword.GetComponent<SwordProjectile>().enabled = false;
                    returnSword.GetComponent<ReturnSword>().enabled = true;
                    //player.GetComponent<PlayerJumpV2>().ResetGravity();
                    player.GetComponent<PlayerJumpV2>().isHanging = false;
                    closeToGround = false;
                    swordSpawned = false;
                    CursorManager.Instance.ChangeCursorState(false);
                }
          
           }
           /*if(hit.collider.gameObject.layer == 9 || hit.collider.tag == "Enemy")
           {
               if (hit.normal.x > 0)
               {
                   cloneSword = Instantiate(stuckSword, hit.point, Quaternion.Euler(0, 0, 180));
                   cloneSword.transform.parent = hit.collider.transform;
                   swordSpawned = true;
                   if (cursorManager != null)
                   {
                       cursorManager.ChangeCursorState(true);
                   }
               }
               else if (hit.normal.x < 0)
               {
                   cloneSword = Instantiate(stuckSword, hit.point, Quaternion.Euler(0, 0, 0));
                   cloneSword.transform.parent = hit.collider.transform;
                   swordSpawned = true;
                   if (cursorManager != null)
                   {
                       cursorManager.ChangeCursorState(true);
                   }
               }
               else if (hit.normal.y < 0)
               {
                   cloneSword = Instantiate(stuckSword, hit.point, Quaternion.Euler(0, 0, 90));
                   cloneSword.transform.parent = hit.collider.transform;
                   swordSpawned = true;
                   if (cursorManager != null)
                   {
                       cursorManager.ChangeCursorState(true);
                   }
               }
               else if (hit.normal.y > 0)
               {
                   cloneSword = Instantiate(stuckSword, hit.point, Quaternion.Euler(0, 0, 270));
                   cloneSword.transform.parent = hit.collider.transform;
                   swordSpawned = true;
                   if (cursorManager != null)
                   {
                       cursorManager.ChangeCursorState(true);
                   }
               }
           }*/
        }
        #endregion
        #region Right Click Options
        if (Input.GetMouseButtonDown(1) && swordSpawned == true && cloneSword.name.Contains("StuckSword"))
        {
            PlayerJumpV2.instance.ResetGravity();
            if (!closeToGround && !stuckDown)
            {
                transform.parent.transform.position = cloneSword.transform.GetChild(0).transform.position;
            }
            else if (closeToGround && !stuckDown)
            {
                transform.parent.transform.position = cloneSword.transform.GetChild(0).transform.position + new Vector3(0,1f,0);
            }
            else if(closeToGround && stuckDown)
            {
                transform.parent.transform.position = cloneSword.transform.GetChild(0).transform.position + new Vector3(0, 0.8f, 0);
            }
            player.GetComponent<PlayerJumpV2>().FreezePos();
            if (stuckLeft)
            {
                GameObject.Find("PlayerV2/Sprite").GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (stuckRight)
            {
                GameObject.Find("PlayerV2/Sprite").GetComponent<SpriteRenderer>().flipX = false;
            }
            CursorManager.Instance.ChangeCursorState(false);
            FindObjectOfType<AudioManager>().Play("Blink");
        }
        #endregion
    }
}
