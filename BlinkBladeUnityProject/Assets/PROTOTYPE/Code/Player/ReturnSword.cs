using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnSword : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(transform.position, GameObject.Find("PlayerV2").transform.position, step);
        if (Vector2.Distance(this.transform.position, GameObject.Find("PlayerV2").transform.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
