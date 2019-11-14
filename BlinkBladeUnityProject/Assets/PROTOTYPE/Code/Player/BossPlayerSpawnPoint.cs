using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossPlayerSpawnPoint : MonoBehaviour
{
    public Vector2 spawnPoint;

    public int deathCount;
    public TMP_Text deathCountText;

    private Timer timer;
    private PlayerFlipManager flipTrigger;
    public PauseMenu pauseMenu;
    public WaypointCamera cameraActive;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
        timer = GameObject.Find("GUI").GetComponentInChildren<Timer>();
        if(GameObject.Find("FlipManager") != null)
        {
            flipTrigger = PlayerFlipManager.instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraActive.active)
        {
            deathCount = Random.Range(0, 999);
            timer.timeStart = Random.Range(0, 99999);
            deathCountText.text = "DEATHS : " + deathCount.ToString();
        }
    }

    public void Respawn()
    {
        pauseMenu.RestartLevel();
    }
}
