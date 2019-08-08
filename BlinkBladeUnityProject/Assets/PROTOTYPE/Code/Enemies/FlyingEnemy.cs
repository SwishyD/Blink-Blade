using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public bool isHit;
    public float fallSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHit)
        {
            this.transform.position -= new Vector3(0, fallSpeed, 0);
            this.gameObject.layer = 10;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if((col.gameObject.layer == 8 || col.gameObject.layer == 9) && isHit)
        {          
            Destroy(gameObject);
            if(gameObject.transform.childCount > 0)
            {
                SwordSpawner.instance.CloneSword = null;
                SwordSpawner.instance.swordSpawned = false;
            }
            PlayerMovement.instance.PlayerNormal();
            PlayerMovement.instance.ResetGravity();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.Contains("Sword"))
        {
            isHit = true;
            col.gameObject.GetComponent<SwordProjectile>().stuckInObject = gameObject;
        }
    }
}
