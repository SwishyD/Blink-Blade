using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    Transform cameraTransform;
    Vector3 startPosition;
    
    void Start()
    {
        cameraTransform = gameObject.transform;
        startPosition = cameraTransform.localPosition;
    }

    public void StartCamShakeCoroutine(float duration, float power, float decayRate)
    {
        StartCoroutine(CameraShake(duration, power, decayRate));
    }
    
    IEnumerator CameraShake(float duration, float power, float decayRate)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0f;

        while (timer < duration)
        {
            cameraTransform.localPosition = startPosition + Random.insideUnitSphere * power;
            power -= (power * power) * decayRate; 
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        cameraTransform.localPosition = startPosition;
    }
}
