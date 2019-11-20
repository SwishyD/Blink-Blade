using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeManager : MonoBehaviour
{
    public GameObject[] eyes;

    public float rateOfSpawn = 0.5f;

    private float nextSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEyes");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEyes()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(rateOfSpawn);
            Vector3 rndPosWithin;
            rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
            var name = Instantiate(eyes[Random.Range(0, eyes.Length)], rndPosWithin, Quaternion.identity);
            name.transform.parent = gameObject.transform;
        }

        yield return null;
    }
}
