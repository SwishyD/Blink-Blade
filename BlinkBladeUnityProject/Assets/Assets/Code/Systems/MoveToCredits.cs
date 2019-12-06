using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToCredits : MonoBehaviour
{
    public float pauseTime;
    public Animator _fadeAnim;
        
    public void InvokeCredits()
    {
        StartCoroutine("MoveCredits");
    }

    IEnumerator MoveCredits()
    {
        yield return new WaitForSeconds(0.5f);
        _fadeAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(pauseTime);
        SceneManagers.instance.MoveToScene("Credits");
    }
}
