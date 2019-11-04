using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTreat : MonoBehaviour
{
    public EndofLevelResults endResults;
    public Timer timer;
    public float requiredTime;

    private void Start()
    {
        requiredTime = endResults.requiredTime[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(requiredTime < timer.timeStart)
        {
            Destroy(this.gameObject);
        }
    }
}
