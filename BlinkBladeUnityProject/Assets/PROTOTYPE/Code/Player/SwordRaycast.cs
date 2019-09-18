﻿using System.Collections;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, rayMask);
        Debug.DrawLine(transform.position, hit.point, Color.yellow);
        Debug.Log(hit.point);

        if(hit.collider != null)
        {
            SwordSpawner.instance.closeToGround = true;
        }
    }
}
