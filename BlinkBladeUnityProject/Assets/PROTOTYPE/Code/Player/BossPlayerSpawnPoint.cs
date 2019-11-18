using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossPlayerSpawnPoint : MonoBehaviour
{
    public Vector2 spawnPoint;

    public int deathCount;
    public TMP_Text deathCountText;
    public ParticleSystem deathPFX;

    private Timer timer;
    private PlayerFlipManager flipTrigger;
    public PauseMenu pauseMenu;
    public WaypointCamera cameraActive;

    private bool respawning = false;

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
        if (cameraActive.active && !pauseMenu.pauseMenuUI.activeSelf)
        {
            deathCount = Random.Range(0, 999);
            timer.timeStart = Random.Range(0, 99999);
            deathCountText.text = "DEATHS : " + deathCount.ToString();
        }
    }

    public void Respawn()
    {
        if (!respawning)
        {
            CursorManager.Instance.ChangeCursor(false);
            Instantiate(deathPFX, gameObject.transform.position, Quaternion.identity);
            Destroy(SwordSpawner.instance.cloneSword);
            PlayerJumpV2.instance.ResetGravity();
            PlayerJumpV2.instance.PlayerNormal();
            SwordSpawner.instance.cloneSword = null;
            SwordSpawner.instance.swordSpawned = false;
            FindObjectOfType<AudioManager>().Play("Death");
            FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.5f, 1f, .5f);
            respawning = true;
            pauseMenu.RestartLevel();
        }
    }
}
