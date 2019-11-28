using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipManager : MonoBehaviour
{
    public bool flipActive;
    public float flipTimer;

    public static PlayerFlipManager instance;

    Animator anim;
    //[SerializeField] AnimationClip warnAnimClip;
    GravitySwitch[] gravSwitchArray;
    [SerializeField] Animator bossAnim;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        gravSwitchArray = FindObjectsOfType<GravitySwitch>();
        FlipEnabler(false);
        anim = GetComponentInChildren<Animator>();
        if (bossAnim == null)
        {
            return;
        }
    }

    public void FlipEnabler(bool enabled)
    {
        flipActive = enabled;
        StopCoroutine("FlipTimer");
        StartCoroutine("FlipTimer");

        if (!enabled && PlayerJumpV2.instance.isFlipped)
        {
            PlayerJumpV2.instance.PlayerFlip();
        }
    }

    IEnumerator FlipTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(flipTimer - 0.5f * 3);
            for (int i = 0; i < 3; i++)
            {
                if (flipActive)
                {
                    //anim.SetTrigger("Warn");
                    for (int e = 0; e < gravSwitchArray.Length; e++)
                    {
                        if (i == 2)
                        {
                            if (!PlayerJumpV2.instance.isFlipped)
                            {
                                gravSwitchArray[e].warnPFX.startColor = new Color32(237, 122, 111, 255);
                            }
                            else
                            {
                                gravSwitchArray[e].warnPFX.startColor = new Color32(86, 112, 245, 255);
                            }
                        }
                        else
                        {
                            gravSwitchArray[e].warnPFX.startColor = new Color(1, 1, 1);
                        }
                        gravSwitchArray[e].warnPFX.Play();
                        AudioManager.instance.Play("GravBeep");
                    }
                }
                yield return new WaitForSeconds(0.5f);
            }
            if (flipActive)
            {
                PlayerJumpV2.instance.PlayerFlip();
                for (int i = 0; i < gravSwitchArray.Length; i++)
                {
                    if (PlayerJumpV2.instance.isFlipped)
                    {
                        gravSwitchArray[i].warnPFX.startColor = new Color32(237, 122, 111, 255);
                    }
                    else
                    {
                        gravSwitchArray[i].warnPFX.startColor = new Color32(86, 112, 245, 255);
                    }
                    gravSwitchArray[i].warnPFX.Play();

                }
                
                if (bossAnim != null)
                {
                    if (PlayerJumpV2.instance.isFlipped)
                    {
                        bossAnim.SetInteger("Gravity", 1);
                    }
                    else
                    {
                        bossAnim.SetInteger("Gravity", -1);
                    }
                }
            }
            else
            {
                yield break;
            }
        }
    }

    public void CancelWarning()
    {
        anim.SetTrigger("Cancel");
    }
}
