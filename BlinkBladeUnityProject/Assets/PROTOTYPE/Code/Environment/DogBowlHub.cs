using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBowlHub : MonoBehaviour
{
    public Sprite[] bowlFullness;
    public int[] requiredTreats;
    [Space]
    public int noTreatsCollected;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetDogBowl", 0.1f);
    }

    void SetDogBowl()
    {
        noTreatsCollected = 0;
        for (int i = 0; i < LevelManager.instance.dogTreatCollected.Length; i++)
        {
            if (LevelManager.instance.dogTreatCollected[i])
            {
                noTreatsCollected++;
            }
        }

        if (noTreatsCollected == requiredTreats[0])
        {
            GetComponent<SpriteRenderer>().sprite = bowlFullness[1];
        }
        else if (requiredTreats[0] < noTreatsCollected && noTreatsCollected <= requiredTreats[1])
        {
            GetComponent<SpriteRenderer>().sprite = bowlFullness[2];
        }
        else if (requiredTreats[1] < noTreatsCollected && noTreatsCollected <= requiredTreats[2])
        {
            GetComponent<SpriteRenderer>().sprite = bowlFullness[3];
        }
        else if (noTreatsCollected == requiredTreats[3])
        {
            GetComponent<SpriteRenderer>().sprite = bowlFullness[4];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = bowlFullness[0];
        }
    }
}
