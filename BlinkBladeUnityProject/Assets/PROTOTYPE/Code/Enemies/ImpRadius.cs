using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpRadius : MonoBehaviour
{
    public bool inRange;

    private void Start()
    {
        transform.GetComponentInChildren<EnemyShoot>().impRadius = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
