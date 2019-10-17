using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCamera : MonoBehaviour
{
    public Transform[] wayPoints;
    public int moveToward;

    public float speed;

    public bool active;
    public float pauseTimer;
    private bool atEnd;

    // Update is called once per frame
    void Update()
    {
        if (!atEnd)
        {
            if (wayPoints.Length > 1 && active)
            {
                transform.position = Vector2.MoveTowards(transform.position, wayPoints[moveToward].position, speed * Time.deltaTime);
            }

            if (wayPoints.Length > 1 && Vector2.Distance(this.transform.position, wayPoints[moveToward].position) < 0.2f)
            {
                StartCoroutine("EdgePause");
            }
        }
    }

    IEnumerator EdgePause()
    {
        active = false;
        speed = 0;
        var newSpeed = wayPoints[moveToward].gameObject.GetComponent<WaypointSpeed>().speed;
        var waitTime = wayPoints[moveToward].gameObject.GetComponent<WaypointSpeed>().waitTime;
        moveToward++;
        if (moveToward > wayPoints.Length - 1)
        {
            atEnd = true;
            yield break;
        }
        yield return new WaitForSeconds(waitTime);
        active = true;
        speed = newSpeed;
    }
}
