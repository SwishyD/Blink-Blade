using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveMovement : MonoBehaviour
{

    public float speed;
    //public float timeBeforeMoving;
    public int numberOfTiles;
    public bool isRight;
    public Vector2 startPosition;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("ShockMove");
        startPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerSpawnPoint>().Respawn();
        }
        if(col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            anim.SetTrigger("Kill");
        }
    }

    private void Update()
    {
        if (isRight)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (!isRight)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if(Vector2.Distance(startPosition, this.transform.position) >= numberOfTiles)
        {
            anim.SetTrigger("Kill");
        }
    }

    public void KillShockwave()
    {
        Destroy(gameObject);
    }

    /*IEnumerator ShockMove()
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
    }*/
}
