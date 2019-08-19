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

    public bool swordSpawned;

    public bool closeToGround;

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
    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        #region Left Click Options
        if (Input.GetMouseButtonDown(0) && swordSpawned == false && player.GetComponent<PlayerJumpV2>().isHanging == false)
        {
            Destroy(cloneSword);
            cloneSword = Instantiate(sword, shotPoint.position, transform.rotation);
            Debug.Log("SwordSpawned");
            swordSpawned = true;
            closeToGround = false;
        }
        else if(Input.GetMouseButtonDown(0) && swordSpawned == true && player.GetComponent<PlayerJumpV2>().isHanging == true)
        {
            Destroy(cloneSword);
            cloneSword = null;
            player.GetComponent<PlayerJumpV2>().ResetGravity();
            player.GetComponent<PlayerJumpV2>().isHanging = false;
            cloneSword = Instantiate(sword, shotPoint.position, transform.rotation);
            swordSpawned = true;
            closeToGround = false;
        }
        else if (Input.GetMouseButtonDown(0) && swordSpawned == true && cloneSword.name.Contains("ThrownSword"))
        {
            Destroy(cloneSword);
            cloneSword = null;
            //player.GetComponent<PlayerJumpV2>().ResetGravity();
            player.GetComponent<PlayerJumpV2>().isHanging = false;
            closeToGround = false;
            swordSpawned = false;
        }
        else if (Input.GetMouseButtonDown(0) && swordSpawned == true && cloneSword.name.Contains("StuckSword"))
        {
            Destroy(cloneSword);
            cloneSword = null;
            //player.GetComponent<PlayerJumpV2>().ResetGravity();
            player.GetComponent<PlayerJumpV2>().isHanging = false;
            closeToGround = false;
            swordSpawned = false;
        }
        #endregion
        #region Right Click Options
        if (Input.GetMouseButton(1) && swordSpawned == true && cloneSword.name.Contains("StuckSword"))
        {
            PlayerJumpV2.instance.ResetGravity();
            if (!closeToGround)
            {
                transform.parent.transform.position = cloneSword.transform.GetChild(0).transform.position;
            }
            else if (closeToGround)
            {
                transform.parent.transform.position = cloneSword.transform.GetChild(0).transform.position + new Vector3(0,1.2f,0);
            }
            player.GetComponent<PlayerJumpV2>().FreezePos();
        }
        #endregion
    }
}
