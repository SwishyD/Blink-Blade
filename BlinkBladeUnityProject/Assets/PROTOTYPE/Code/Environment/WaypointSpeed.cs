using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSpeed : MonoBehaviour
{
    [Tooltip("Speed the Camera moves to next Waypoint")]
    public float speed;
    [Tooltip("(seconds) Time the camera waits here until moving to next waypoint")]
    public float waitTime;
}
