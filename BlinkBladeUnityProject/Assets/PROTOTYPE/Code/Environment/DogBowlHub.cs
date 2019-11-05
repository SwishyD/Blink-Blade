using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBowlHub : MonoBehaviour
{
    public int noTreatsCollected;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < LevelManager.instance.dogTreatCollected.Length; i++)
        {
            if (LevelManager.instance.dogTreatCollected[i])
            {
                noTreatsCollected++;
            }
        }   
    }
}
