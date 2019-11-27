using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorTracker : MonoBehaviour
{
    public bool levelCheck;
    public CameraFollow camFollow;
    public GameObject player;
    public BossDoorTracker bossDoorTrans;

    [SerializeField] ParticleSystem doorUnlockPFX;

    // Update is called once per frame
    void Update()
    {
        if (levelCheck && !LevelManager.instance.levelUnlocked[11])
        {
            if(!bossDoorTrans.levelCheck && bossDoorTrans.levelCheckComplete)
            {
                levelCheck = false;
                if (CheckCompletion())
                {
                    StartCoroutine("UnlockLevel");
                }
            }
            else if(LevelManager.instance.levelUnlocked[10] && camFollow.target == player.transform)
            {
                levelCheck = false;
                if (CheckCompletion())
                {
                    StartCoroutine("UnlockLevel");
                }
            }
        }
    }

    IEnumerator UnlockLevel()
    {
        yield return new WaitForSeconds(0.1f);
        camFollow.target = this.transform;
        PlayerScriptManager.instance.PlayerScriptDisable();
        yield return new WaitForSeconds(1f);
        AudioManager.instance.Play("Lock");
        yield return new WaitForSeconds(1f);
        Instantiate(doorUnlockPFX, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
        AudioManager.instance.Play("Chord");
        AudioManager.instance.Play("Zoom");
        AudioManager.instance.Play("Whoosh");
        yield return new WaitForSeconds(0.1f);
        LevelManager.instance.levelUnlocked[11] = true;
        yield return new WaitForSeconds(2f);
        camFollow.target = player.transform;
        PlayerScriptManager.instance.PlayerScriptEnable();
    }

    bool CheckCompletion()
    {
        for (int i = 0; i < LevelManager.instance.dogTreatCollected.Length; i++)
        {
            if (LevelManager.instance.dogTreatCollected[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}
