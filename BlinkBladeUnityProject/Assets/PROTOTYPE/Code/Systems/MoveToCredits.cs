using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCredits : MonoBehaviour
{
    public float pauseTime;

    public void InvokeCredits()
    {
        Invoke("MoveCredits", pauseTime);
    }

    void MoveCredits()
    {
        SceneManagers.instance.MoveToScene("Credits");
    }
}
