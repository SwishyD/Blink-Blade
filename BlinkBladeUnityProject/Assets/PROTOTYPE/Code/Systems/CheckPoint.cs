using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool triggered;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            triggered = true;
            PlayerMovement.instance.spawnPoint = this.transform.position;
        }
    }
}
