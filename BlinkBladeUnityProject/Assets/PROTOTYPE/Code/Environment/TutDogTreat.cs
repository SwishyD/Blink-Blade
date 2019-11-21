using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutDogTreat : MonoBehaviour
{
    //public LevelResultTransfer transfer;

    public int tutorialNo;

    private float requiredTime;

    [SerializeField] ParticleSystem grabPFX;

    private void Start()
    {
        //requiredTime = endResults.requiredTime[0] + addDebugTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.instance.dogTreatCollected[tutorialNo] = true;
            Instantiate(grabPFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}

