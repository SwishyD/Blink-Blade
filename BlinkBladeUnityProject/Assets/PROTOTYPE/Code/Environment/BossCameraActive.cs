using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraActive : MonoBehaviour
{
    public WaypointCamera wayCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wayCam.active = true;
        }
    }
}
