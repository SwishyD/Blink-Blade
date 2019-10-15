using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipTrigger : MonoBehaviour
{
    public bool flipActive;
    public float flipTimer;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PlayerV2" && !flipActive)
        {
            flipActive = true;
            StartCoroutine("FlipTimer");
        }
    }

    IEnumerator FlipTimer()
    {
        while (true)
        {
            if (flipActive)
            {
                PlayerJumpV2.instance.PlayerFlip();
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(flipTimer);
        }
    }
}
