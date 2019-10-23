using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipManager : MonoBehaviour
{
    public bool flipActive;
    public float flipTimer;

    public static PlayerFlipManager instance;

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
            yield return new WaitForSeconds(flipTimer);
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
}
