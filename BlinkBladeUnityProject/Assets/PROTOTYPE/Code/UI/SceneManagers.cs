using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagers : MonoBehaviour {

    public static SceneManagers instance;
    public Animator sceneTransitionAnimator; //For scene transitions

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ToBattle()
    {
        SceneManager.LoadScene(2);
    }

    public void ToOverWorld()
    {
        SceneManager.LoadScene(1);
    }

    public void MoveToScene(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);
    }

    public IEnumerator SceneTransitionToScene(string sceneName)
    {
        sceneTransitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(sceneName);
    }
}
