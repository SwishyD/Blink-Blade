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
        if (LevelManager.instance.levelUnlocked[11])
        {
            GetComponent<SpriteRenderer>().sprite = unlockedDoor;
        }
    }
}
