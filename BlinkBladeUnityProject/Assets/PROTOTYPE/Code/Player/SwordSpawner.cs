using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawner : MonoBehaviour
{
    public float offset;

    public GameObject sword;
    public Transform shotPoint;

    public GameObject CloneSword;

    public GameObject player;

    public bool swordSpawned = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (Input.GetMouseButtonDown(0) && swordSpawned == false && player.GetComponent<PlayerMovement>().isHanging == false)
        {
            CloneSword = Instantiate(sword, shotPoint.position, transform.rotation);
            swordSpawned = true;
        }
        else if(Input.GetMouseButtonDown(0) && swordSpawned == true && player.GetComponent<PlayerMovement>().isHanging == true)
        {
            Destroy(CloneSword);
            CloneSword = null;
            player.GetComponent<PlayerMovement>().ResetGravity();
            player.GetComponent<PlayerMovement>().isHanging = false;
            CloneSword = Instantiate(sword, shotPoint.position, transform.rotation);
            swordSpawned = true;
        }

        if(Input.GetMouseButton(1) && swordSpawned == true && CloneSword.GetComponent<SwordProjectile>().StuckinObject == false)
        {
            transform.parent.transform.position = CloneSword.transform.position;
            //player.GetComponent<PlayerMovement>().doubleJumpReady = true;
            swordSpawned = false;
            Destroy(CloneSword);
            CloneSword = null;
        }
        else if (Input.GetMouseButton(1) && swordSpawned == true && CloneSword.GetComponent<SwordProjectile>().StuckinObject == true)
        {
            transform.parent.transform.position = CloneSword.transform.GetChild(0).transform.position;
            player.GetComponent<PlayerMovement>().FreezePos();
        }
    }
}
