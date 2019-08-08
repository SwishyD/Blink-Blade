using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawner : MonoBehaviour
{

    public static SwordSpawner instance = null;

    public float offset;

    public Transform shotPoint;

    public GameObject cloneSword;
    public GameObject sword;
    public GameObject player;

    public bool swordSpawned;

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

        if (Input.GetMouseButtonDown(0) && swordSpawned == false && player.GetComponent<PlayerJumpV2>().isHanging == false)
        {
            cloneSword = Instantiate(sword, shotPoint.position, transform.rotation);
            swordSpawned = true;
        }
        else if(Input.GetMouseButtonDown(0) && swordSpawned == true && player.GetComponent<PlayerJumpV2>().isHanging == true)
        {
            Destroy(cloneSword);
            cloneSword = null;
            player.GetComponent<PlayerJumpV2>().ResetGravity();
            player.GetComponent<PlayerJumpV2>().isHanging = false;
            cloneSword = Instantiate(sword, shotPoint.position, transform.rotation);
            swordSpawned = true;
        }

        if(Input.GetMouseButton(1) && swordSpawned == true && cloneSword.GetComponent<SwordProjectile>().StuckinObject == false)
        {
            PlayerJumpV2.instance.ResetGravity();
            PlayerJumpV2.instance.PlayerNormal();
            transform.parent.transform.position = cloneSword.transform.position;
            swordSpawned = false;
            Destroy(cloneSword);
            cloneSword = null;
        }
        else if (Input.GetMouseButton(1) && swordSpawned == true && cloneSword.GetComponent<SwordProjectile>().StuckinObject == true)
        {
            PlayerJumpV2.instance.ResetGravity();
            transform.parent.transform.position = cloneSword.transform.GetChild(0).transform.position;
            player.GetComponent<PlayerJumpV2>().FreezePos();
        }
    }
}
