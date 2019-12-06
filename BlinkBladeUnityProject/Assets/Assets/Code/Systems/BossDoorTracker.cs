using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorTracker : MonoBehaviour
{
    public bool levelCheck;
    public bool levelCheckComplete;
    public CameraFollow camFollow;
    public GameObject player;

    [SerializeField] GameObject lockPiece1;
    [SerializeField] GameObject lockPiece2;

    [SerializeField] ParticleSystem doorUnlockPFX;
    [SerializeField] GameObject chains;

    // Update is called once per frame
    void Update()
    {
        if (levelCheck && !LevelManager.instance.levelUnlocked[10])
        {
            levelCheck = false;
            if (CheckCompletion())
            {
                StartCoroutine("UnlockLevel");
            }
        }
    }

    IEnumerator UnlockLevel()
    {
        yield return new WaitForSeconds(0.1f);
        camFollow.target = this.transform;
        PlayerScriptManager.instance.PlayerScriptDisable();
        yield return new WaitForSeconds(2f);
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.5f, 0.4f, .5f);
        AudioManager.instance.Play("HeavyLand");
        yield return new WaitForSeconds(1f);
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.5f, 0.6f, .5f);
        AudioManager.instance.Play("HeavyLand");
        yield return new WaitForSeconds(1f);
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.5f, 0.8f, .5f);
        AudioManager.instance.Play("HeavyLand");
        yield return new WaitForSeconds(1f);
        Instantiate(lockPiece1, transform.position, Quaternion.identity); //Spawn lock piece 1
        Instantiate(lockPiece2, transform.position, Quaternion.identity); //Spawn lock piece 2
        Instantiate(chains, transform.position, Quaternion.identity); //Spawn chain pieces
        FindObjectOfType<CameraShaker>().StartCamShakeCoroutine(0.5f, 0.8f, .5f);
        AudioManager.instance.Play("Explosion");
        AudioManager.instance.Play("BassImpact");
        AudioManager.instance.Play("Laugh");
        Instantiate(doorUnlockPFX, new Vector3(transform.position.x, transform.position.y, 5), Quaternion.identity);
        GetComponent<SpriteRenderer>().sprite = GetComponent<BossLevelTransition>().unlockedDoor;
        LevelManager.instance.levelUnlocked[10] = true;
        yield return new WaitForSeconds(2f);
        camFollow.target = player.transform;
        PlayerScriptManager.instance.PlayerScriptEnable();
        levelCheckComplete = true;
    }

    bool CheckCompletion()
    {
        for (int i = 0; i < LevelManager.instance.levelComplete.Length; i++)
        {
            if (LevelManager.instance.levelComplete[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}
