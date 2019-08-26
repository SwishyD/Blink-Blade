using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveMovement : MonoBehaviour
{
    public float timeBeforeMoving;
    public int numberOfTiles;
    public bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShockMove");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerSpawnPoint>().Respawn();
        }
    }

    IEnumerator ShockMove()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (isRight)
            {
                transform.position += Vector3.right;
            }
            else if (!isRight)
            {
                transform.position += Vector3.left;
            }
            yield return new WaitForSeconds(timeBeforeMoving);
        }
        Destroy(gameObject);
    }
}
