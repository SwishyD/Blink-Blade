using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatrolDir { Left, Right, Up, Down}

public class FlyingSidePatrol : MonoBehaviour
{
    public PatrolDir direction;
    public float patrolTime;
    public float maxPatrolTime;
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        patrolTime += Time.deltaTime;
        if(patrolTime >= maxPatrolTime)
        {
            if(direction == PatrolDir.Left || direction == PatrolDir.Up)
            {
                direction++;
                patrolTime = 0;
            }
            else if(direction == PatrolDir.Right || direction == PatrolDir.Down)
            {
                direction--;
                patrolTime = 0;
            }
        }

        switch (direction)
        {
            case PatrolDir.Left:
                transform.position -= new Vector3(speed, 0,0);
                break;

            case PatrolDir.Right:
                transform.position += new Vector3(speed, 0, 0);
                break;

            case PatrolDir.Up:
                transform.position += new Vector3(0, speed, 0);
                break;

            case PatrolDir.Down:
                transform.position -= new Vector3(0, speed, 0);
                break;
        }
    }
}
