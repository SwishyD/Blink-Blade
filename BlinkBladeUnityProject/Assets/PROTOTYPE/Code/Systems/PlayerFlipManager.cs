using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipManager : MonoBehaviour
{
    public bool flipActive;
    public float flipTimer;

    public static PlayerFlipManager instance;

    Animator anim;
    [SerializeField] AnimationClip warnAnimClip;

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
        FlipEnabler(false);
        anim = GetComponentInChildren<Animator>();
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
            yield return new WaitForSeconds(flipTimer - warnAnimClip.length);
            if (flipActive)
            {
                anim.SetTrigger("Warn");
            }
            yield return new WaitForSeconds(warnAnimClip.length);
            if (flipActive)
            {
                PlayerJumpV2.instance.PlayerFlip();
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
