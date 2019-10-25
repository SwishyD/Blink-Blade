using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    //GameObject name MUST include "GravitySwitch"
    public void FlipGravity()
    {
        PlayerJumpV2.instance.PlayerFlip();    
    }

    private void Update()
    {
        if (PlayerJumpV2.instance.isFlipped)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (!PlayerJumpV2.instance.isFlipped)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
}
