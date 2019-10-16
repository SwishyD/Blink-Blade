using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCamera : MonoBehaviour
{
    public Transform[] wayPoints;
    public int moveToward;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (wayPoints.Length > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[moveToward].position, speed * Time.deltaTime);
        }
    }
}
