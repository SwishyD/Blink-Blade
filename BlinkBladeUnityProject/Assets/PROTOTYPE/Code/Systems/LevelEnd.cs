using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public int moveToLevel;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            SceneManagers.instance.MoveToScene(moveToLevel);
        }
    }
}
