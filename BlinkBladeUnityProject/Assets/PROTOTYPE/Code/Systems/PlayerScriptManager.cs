using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScriptManager : MonoBehaviour
{
    public static PlayerScriptManager instance;

    public GameObject player;
    public bool foundPlayer;

    public bool scriptsActive;

    public bool swordScriptActive = true;
    public bool tutorialCheck;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame

    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Main Menu")
        {
            if (!foundPlayer)
            {
                player = GameObject.Find("PlayerV2");
                if (player == null)
                {
                    player = null;
                }
                else
                {
                    foundPlayer = true;
                }
            }
            if (player == null)
            {
                foundPlayer = false;
            }
        }
        if (SceneManager.GetActiveScene().name == "TUTORIAL" && tutorialCheck)
        {
            swordScriptActive = false;
        }
        else
        {
            swordScriptActive = true;
        }
    }

    public void PlayerScriptEnable()
    {
        scriptsActive = true;
        player.GetComponent<PlayerMovementV2>().enabled = true;
        player.GetComponent<PlayerMovementV2>().canMove = true;
        player.GetComponent<PlayerJumpV2>().enabled = true;
        player.GetComponent<PlayerAnimator>().enabled = true;
        if (!SceneManager.GetActiveScene().name.Contains("BOSS"))
        {
            player.GetComponent<PlayerSpawnPoint>().enabled = true;
        }
        if (swordScriptActive)
        {
            player.GetComponentInChildren<SwordSpawner>().enabled = true;
        }
    }

    public void PlayerScriptDisable()
    {
        scriptsActive = false;
        player.GetComponent<PlayerMovementV2>().enabled = false;
        player.GetComponent<PlayerJumpV2>().enabled = false;
        player.GetComponent<PlayerAnimator>().enabled = false;
        if (!SceneManager.GetActiveScene().name.Contains("BOSS"))
        {
            player.GetComponent<PlayerSpawnPoint>().enabled = false;
        }
        player.GetComponentInChildren<SwordSpawner>().enabled = false;
    }
}
