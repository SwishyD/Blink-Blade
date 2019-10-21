using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPFXManager : MonoBehaviour
{
    [SerializeField] ParticleSystem gravPFXCeil;
    [SerializeField] ParticleSystem gravPFXFloor;

    [SerializeField] bool gravFlipped;

    // Start is called before the first frame update
    void Start()
    {
        //ChangeGravPFX(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Temporary gravity change, should eventually be tied to gravity state proper
        {
            gravFlipped = !gravFlipped;
            ChangeGravPFX(gravFlipped);
        }
    }

    void ChangeGravPFX(bool gravFlipped)
    {
        //var ceilEmission = gravPFXCeil.emission;
        //var floorEmission = gravPFXFloor.emission;
        if (gravFlipped)
        {
            //ceilEmission.rateOverTime = 0;
            //floorEmission.rateOverTime = 20;
            gravPFXFloor.Play();
        }
        if (!gravFlipped)
        {
            //ceilEmission.rateOverTime = 20;
            //floorEmission.rateOverTime = 0;
            gravPFXCeil.Play();
        }
    }
}
