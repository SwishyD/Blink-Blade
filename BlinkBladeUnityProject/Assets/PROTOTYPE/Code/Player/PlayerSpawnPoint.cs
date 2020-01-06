using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerSpawnPoint : MonoBehaviour
{
    public Vector2 spawnPoint;
    public ParticleSystem deathPFX;

    public int deathCount;
    public TMP_Text deathCountText;

    private Timer timer;
    private PlayerFlipManager flipTrigger;

    SpriteRenderer spriteRend;
    SpriteRenderer aimRing;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
        if (SceneManager.GetActiveScene().name.Contains("LEVEL"))
        {
            timer = GameObject.Find("GUI").GetComponentInChildren<Timer>();
        }
        if(GameObject.Find("FlipManager") != null)
        {
            flipTrigger = PlayerFlipManager.instance;
        }
        spriteRend = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        aimRing = gameObject.transform.GetChild(2).transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
        if (SceneManager.GetActiveScene().name.Contains("LEVEL"))
        {
            deathCountText.text = "DEATHS : " + deathCount.ToString();
        }
    }

    public void Respawn()
    {
        StartCoroutine("DelayRespawn");
    }

    IEnumerator DelayRespawn()
    {
        FindObjectOfType<AudioManager>().Play("Death");
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.5f, 1f, .5f);
        PlayerScriptManager.instance.PlayerScriptDisable();
        CursorManager.Instance.ChangeCursor(false);
        Instantiate(deathPFX, gameObject.transform.position, Quaternion.identity);
        spriteRend.enabled = false;
        aimRing.enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        Destroy(SwordSpawner.instance.cloneSword);
        SwordSpawner.instance.cloneSword = null;
        SwordSpawner.instance.swordSpawned = false;
        if (SceneManager.GetActiveScene().name.Contains("LEVEL"))
        {
            if (timer.timerActive)
            {
                deathCount++;
            }
        }
        yield return new WaitForSeconds(.5f);
        PlayerScriptManager.instance.PlayerScriptEnable();
        gameObject.transform.position = spawnPoint;
        spriteRend.enabled = true;
        aimRing.enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        PlayerJumpV2.instance.ResetGravity();
        PlayerJumpV2.instance.PlayerNormal();
        if (flipTrigger != null)
        {
            flipTrigger.FlipEnabler(false);
        }
        if (PlayerJumpV2.instance.isFlipped)
        {
            PlayerJumpV2.instance.PlayerFlip();
        }
        Instantiate(deathPFX, transform);
        //CursorManager.Instance.ChangeCursorState(false);
    }
}
