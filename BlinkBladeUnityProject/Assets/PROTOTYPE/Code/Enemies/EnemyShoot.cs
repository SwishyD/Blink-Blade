using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public bool active;
    public GameObject bullet;
    public int numberOfShots;

    public GameObject bulletAimer;
    public GameObject bulletSpawn;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Shoot");
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            active = true;
        }
        if (gameObject.GetComponent<FlyingEnemy>().isHit)
        {
            active = false;
        }

        Vector3 difference = GameObject.FindGameObjectWithTag("Player").transform.position - bulletAimer.transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        bulletAimer.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

    IEnumerator Shoot()
    {
        while (enabled)
        {
            Debug.Log("Shoot");
            if (active)
            {
                for (int i = 0; i < numberOfShots; i++)
                {
                    Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
