using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 10;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
	}

    public void ChangeCameraTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ChangeCameraTargetNoSmooth(Transform newTarget) //If we don't want a smooth transition, use this one.
    {
        float savedSmoothSpeed = smoothSpeed;
        smoothSpeed = 100;
        target = newTarget;
        smoothSpeed = savedSmoothSpeed;
    }
}
