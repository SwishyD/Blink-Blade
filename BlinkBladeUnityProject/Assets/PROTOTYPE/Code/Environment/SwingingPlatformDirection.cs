using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingPlatformDirection : MonoBehaviour
{
    public SwingingPlatform swing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(swing.rotateLeft && collision.name == "Swinger")
        {
            swing.midLeft = true;
            swing.speedUp = false;
            swing.hitLimit = false;
        }
        else if(!swing.rotateLeft && collision.name == "Swinger")
        {
            swing.midRight = true;
            swing.speedUp = false;
            swing.hitLimit = false;
        }
    }
}
