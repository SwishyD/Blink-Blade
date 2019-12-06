using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnable : MonoBehaviour
{
    public SwordSpawner swordActivator;
    public ParticleSystem collectPFX;

    public SpriteRenderer aimArrow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PlayerV2")
        {
            PlayerScriptManager.instance.tutorialCheck = false;
            swordActivator.enabled = true;
            aimArrow.enabled = true;
            PlayerScriptManager.instance.swordScriptActive = true;
            collectPFX.gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("SwordShwing_02");
            FindObjectOfType<AudioManager>().Play("FireWhoosh");
            Destroy(gameObject);
        }
    }
}
