using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    //GameObject name MUST include "GravitySwitch"

    public Sprite gravUp;
    public Sprite gravDown;
    public ParticleSystem warnPFX;

    private void Start()
    {
        warnPFX = GetComponentInChildren<ParticleSystem>();
    }

    public void FlipGravity()
    {
        PlayerJumpV2.instance.PlayerFlip();
        if (PlayerJumpV2.instance.isFlipped)
        {
            warnPFX.startColor = new Color32(237, 122, 111, 255);
        }
        else
        {
            warnPFX.startColor = new Color32(86, 112, 245, 255);
        }
        warnPFX.Play();
    }

    private void Update()
    {
        if (PlayerJumpV2.instance.isFlipped)
        {
            GetComponent<SpriteRenderer>().sprite = gravUp;
        }
        else if (!PlayerJumpV2.instance.isFlipped)
        {
            GetComponent<SpriteRenderer>().sprite = gravDown;
        }
    }
}
