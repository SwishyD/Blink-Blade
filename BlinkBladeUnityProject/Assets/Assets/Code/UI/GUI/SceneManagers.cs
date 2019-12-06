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

    public void MoveToScene(string nameOfScene)
    {
        StartCoroutine(SceneTransitionToScene(nameOfScene));
    }

    public IEnumerator SceneTransitionToScene(string sceneName)
    {
        if (sceneTransitionAnimator == null)
        {
            sceneTransitionAnimator = GameObject.Find("SceneTransitionCanvas").GetComponentInChildren<Animator>();
            if (sceneTransitionAnimator == null)
            {
                Debug.LogWarning("No SceneTransitionCanvas Found!");
            }
        }
        sceneTransitionAnimator.SetBool("FadingToBlack", true);
        StartCoroutine(MusicManager.instance.FadeOut("", 5f));
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
        sceneTransitionAnimator.SetBool("FadingToBlack", false);
    }
}
