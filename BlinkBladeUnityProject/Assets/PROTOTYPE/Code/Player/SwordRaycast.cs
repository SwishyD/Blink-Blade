using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRaycast : MonoBehaviour
{
    public float groundDistance;
    public LayerMask rayMask;

    private void Start()
    {
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.2f, 0.2f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerJumpV2.instance.isFlipped)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, rayMask);
            Debug.DrawLine(transform.position, hit.point, Color.yellow);
            SwordSpawner.instance.closeToRoof = false;

            if (hit.collider != null)
            {
                SwordSpawner.instance.closeToGround = true;
            }
        }
        else if (PlayerJumpV2.instance.isFlipped)
        {
            RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, groundDistance, rayMask);
            Debug.DrawLine(transform.position, hitUp.point, Color.yellow);
            SwordSpawner.instance.closeToGround = false;

            if (hitUp.collider != null)
            {
                SwordSpawner.instance.closeToRoof = true;
            }
        }
    }
}
