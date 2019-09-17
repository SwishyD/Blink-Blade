using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwordSpawner : MonoBehaviour
{

    public static SwordSpawner instance = null;

    private PlayerJumpV2 plJump;

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

    [SerializeField] Animator throwFXAnim;

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
        if(SceneManager.GetActiveScene().name == "TUTORIAL")
        {
            this.gameObject.SetActive(false);
        }
        plJump = player.GetComponent<PlayerJumpV2>();
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
            if (Input.GetMouseButtonDown(0) && swordSpawned == false && plJump.isHanging == false)
            {
                ThrowSword();
            }
            else if (Input.GetMouseButtonDown(0) && swordSpawned == true && plJump.isHanging == true)
            {
                plJump.ResetGravity();
                plJump.isHanging = false;
                ThrowSword();
            }
            else if (Input.GetMouseButtonDown(0) && swordSpawned == true && cloneSword.name.Contains("Sword"))
            {
                SwordReturn();
            }
        }
        else if(hit.collider != null)
        {
           if(hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 28)
           {
                if (Input.GetMouseButtonDown(0) && swordSpawned == false && plJump.isHanging == false)
                {
                    ThrowSword();
                    cloneSword.GetComponent<SwordProjectile>().speed = 10;
                }
                else if (Input.GetMouseButtonDown(0) && swordSpawned == true && plJump.isHanging == true)
                {
                    plJump.ResetGravity();
                    plJump.isHanging = false;
                    ThrowSword();
                    cloneSword.GetComponent<SwordProjectile>().speed = 10;
                }
                else if (Input.GetMouseButtonDown(0) && swordSpawned == true && cloneSword.name.Contains("Sword"))
                {
                    SwordReturn();
                }
           }
           else if(hit.collider.gameObject.layer == 8)
           {
                if (Input.GetMouseButtonDown(0) && swordSpawned == true && cloneSword.name.Contains("Sword"))
                {
                    SwordReturn();
                }
           }
        }
        #endregion
        #region Right Click Options
        //Blinking to the Blade
        if (Input.GetMouseButtonDown(1) && swordSpawned == true && cloneSword.name.Contains("StuckSword"))
        {
            if (!plJump.isHanging)
            {
                plJump.ResetGravity();
                if (!closeToGround && !stuckDown)
                {
                    transform.parent.transform.position = cloneSword.transform.GetChild(0).transform.position;
                }
                else if (closeToGround && !stuckDown)
                {
                    transform.parent.transform.position = cloneSword.transform.GetChild(0).transform.position + new Vector3(0, 1f, 0);
                }
                else if (closeToGround && stuckDown || !closeToGround && stuckDown)
                {
                    transform.parent.transform.position = cloneSword.transform.GetChild(0).transform.position + new Vector3(0, 0.8f, 0);
                }
                plJump.FreezePos();
                if (stuckLeft)
                {
                    GameObject.Find("PlayerV2/Sprite").GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (stuckRight)
                {
                    GameObject.Find("PlayerV2/Sprite").GetComponent<SpriteRenderer>().flipX = false;
                }
                FindObjectOfType<AudioManager>().Play("Blink");
            }
        }
        #endregion
    }

    void ThrowSword()
    {
        Destroy(cloneSword);
        cloneSword = Instantiate(sword, shotPoint.position, transform.rotation);
        Debug.Log("SwordSpawned");
        swordSpawned = true;
        closeToGround = false;
        CursorManager.Instance.ChangeCursor(false);
        throwFXAnim.SetTrigger("Throw");
        AudioManager.instance.Play("SwordThrow");
        AudioManager.instance.Play("SwordShwing");
        AudioManager.instance.Play("SwordSwing_02");
    }

    void SwordReturn()
    {
        var swordPoint = cloneSword.transform.position;
        Destroy(cloneSword);
        cloneSword = null;
        var returnSword = Instantiate(sword, swordPoint, transform.rotation);
        returnSword.GetComponent<SwordProjectile>().enabled = false;
        returnSword.GetComponent<ReturnSword>().enabled = true;
        CursorManager.Instance.ChangeCursor(false);
        plJump.isHanging = false;
        closeToGround = false;
        swordSpawned = false;
    }
}