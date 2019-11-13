using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    public float originalSpeed;
    private float speed;

    public float maxY;
    private bool moveScene;

    public Animator playerAnim;
    public GameObject pfx;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.S))
        {
            playerAnim.SetBool("QuickFalling", true);
            pfx.SetActive(true);
            speed = originalSpeed * 20;
        }
        else
        {
            pfx.SetActive(false);
            playerAnim.SetBool("QuickFalling", false);
            speed = originalSpeed;
        }

        if(this.transform.position.y >= maxY && !moveScene)
        {
            moveScene = true;
            SceneManagers.instance.MoveToScene("MainMenu");
        }
    }
}
