using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingPlatform : MonoBehaviour
{
    public float rotateLength;
    public float angleLimitLeft;
    public float angleLimitRight;
    public float maxSpeed;
    public float speed;

    public float t;

    public bool rotateLeft;
    private bool hitLimit;


    public GameObject rotator;
    public GameObject rotatedObject;
    public GameObject spriteHolder;

    // Start is called before the first frame update
    void Start()
    {
        rotateLeft = true;
        speed = maxSpeed;
        rotatedObject.transform.position = transform.position - new Vector3(0, rotateLength, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rotator.transform.rotation.z);
        spriteHolder.transform.position = rotatedObject.transform.position;
        rotator.transform.eulerAngles += new Vector3(0, 0, speed * Time.deltaTime);
        if (rotateLeft)
        {
            if (rotator.transform.eulerAngles.z >= angleLimitLeft && rotator.transform.eulerAngles.z <= angleLimitRight && !hitLimit)
            {
                hitLimit = true;
                StartCoroutine("ChangeRotation");
            }
        }
        else if (!rotateLeft)
        {
            if (rotator.transform.eulerAngles.z <= angleLimitRight && rotator.transform.eulerAngles.z >= angleLimitLeft && !hitLimit)
            {
                hitLimit = true;
                StartCoroutine("ChangeRotation");
            }
        }

        if(rotator.transform.eulerAngles.z == 0)
        {
            Debug.Log("Bottom");
        }
    }

    IEnumerator ChangeRotation()
    {
        rotateLeft = !rotateLeft;
        speed = 0;
        maxSpeed = -maxSpeed;
        yield return new WaitForSeconds(1f);
        speed = maxSpeed;
        hitLimit = false;
    }
}
