﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipTrigger : MonoBehaviour
{
    private PlayerFlipManager flipManager;

    public bool enable;

    private void Start()
    {
        flipManager = PlayerFlipManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PlayerV2")
        {
            if (flipManager.flipActive != enable)
            {
                flipManager.FlipEnabler(enable);
                if (!enable)
                {
                    flipManager.CancelWarning();
                    if (PlayerJumpV2.instance.isFlipped)
                    {
                        PlayerJumpV2.instance.PlayerFlip();
                    }
                }
            }
            else if (!enable)
            {
                if (PlayerJumpV2.instance.isFlipped)
                {
                    PlayerJumpV2.instance.PlayerFlip();
                }
            }
        }
    }
}
