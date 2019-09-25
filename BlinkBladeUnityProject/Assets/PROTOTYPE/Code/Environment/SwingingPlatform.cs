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

    public float speedUpSpeed;
    public float slowDownSpeed;

    public float t;

    public bool rotateLeft;
    public bool hitLimit;

    public bool midLeft;
    public bool midRight;
    public bool speedUp;


    public GameObject rotator;
    public GameObject rotatedObject;
    public GameObject spriteHolder;
    public GameObject midPoint;

    // Start is called before the first frame update
    void Start()
    {
        rotateLeft = true;
        speed = maxSpeed;
        rotatedObject.transform.position = transform.position - new Vector3(0, rotateLength, 0);
        midPoint.transform.position = transform.position - new Vector3(0, rotateLength, 0);
    }

    // Update is called once per frame
    void Update()
    {
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

        if (midLeft || midRight)
        {
            speed = Mathf.Lerp(speed, 0, t);
            t = slowDownSpeed * Time.deltaTime;
        }
        if(speedUp)
        {
            speed = Mathf.Lerp(speed, maxSpeed, t);
            t = speedUpSpeed * Time.deltaTime;
        }
    }

    IEnumerator ChangeRotation()
    {
        rotateLeft = !rotateLeft;
        speed = 0;
        maxSpeed = -maxSpeed;
        yield return new WaitForSeconds(0.1f);
        //speed = maxSpeed;
        speedUp = true;
        midRight = false;
        midLeft = false;
    }
}
