using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public GameObject cuff1;
    public GameObject cuff2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndScene();
        }
    }

    public void CamShake()
    {
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(1f, 0.1f, .7f);
    }

    public void SpawnCuffs()
    {
        cuff1.SetActive(true);
        cuff2.SetActive(true);
        cuff1.GetComponent<Rigidbody2D>().angularVelocity = 1080;
        cuff1.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 300));
        cuff2.GetComponent<Rigidbody2D>().angularVelocity = -827;
        cuff2.GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 300));
    }

    public void EndScene()
    {
        SceneManagers.instance.MoveToScene("TUTORIAL");
    }
}
