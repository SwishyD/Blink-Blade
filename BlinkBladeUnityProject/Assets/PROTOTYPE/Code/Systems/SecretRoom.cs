using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoom : MonoBehaviour
{
    public Sprite lockedDoor;
    public Sprite unlockedDoor;
    public GameObject dogBowl;

    // Start is called before the first frame update
    void Update()
    {
        if (dogBowl.GetComponent<SpriteRenderer>().sprite == dogBowl.GetComponent<DogBowlHub>().bowlFullness[4])
        {
            GetComponent<SpriteRenderer>().sprite = unlockedDoor;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = lockedDoor;
        }
    }
}
