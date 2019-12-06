using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CameraFollow camFollow;
    public PlayerScriptManager scripts;
    public GameObject GUI;
    public GameObject aimArrow;

    private bool guiActive;

    public float speed;

    private void OnEnable()
    {
        scripts = GameObject.Find("PlayerScriptManager").GetComponent<PlayerScriptManager>();
        camFollow.enabled = false;
        scripts.PlayerScriptDisable();
        guiActive = true;
    }

    private void OnDisable()
    {
        camFollow.enabled = true;
        scripts.PlayerScriptEnable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.Equals))
        {
            speed++;
        }
        if (Input.GetKey(KeyCode.Minus))
        {
            speed--;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            guiActive = !guiActive;
            GUI.SetActive(guiActive);
            aimArrow.SetActive(guiActive);
        }

        if (Input.GetKey(KeyCode.RightShift))
        {
            var spawner = SwordSpawner.instance;
            if (spawner.cloneSword != null)
            {
                transform.position = new Vector3(spawner.cloneSword.transform.position.x, spawner.cloneSword.transform.position.y, -10);
            }
            else
            {
                Debug.Log("No Sword is spawned");
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            var player = PlayerJumpV2.instance.gameObject;
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            var player = PlayerJumpV2.instance.gameObject;
            player.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        }
    }
}
