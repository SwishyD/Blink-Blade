using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTooltip : MonoBehaviour
{
    string tagName = "Player";

    [SerializeField] bool tooltipActiveState;

    [SerializeField] GameObject tooltip;

    [SerializeField] AudioSource bark;
    bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        //tooltipAnim = tooltip.GetComponentInParent<Animator>();
        tooltip = gameObject.transform.GetChild(0).gameObject;
        SwitchTooltipActivationState(tooltipActiveState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagName)
        {
            SwitchTooltipActivationState(true);
            if (firstTime)
            {
                if (bark != null)
                {
                    bark.Play();
                }
                firstTime = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == tagName)
        {
            SwitchTooltipActivationState(false);
        }
    }

    void SwitchTooltipActivationState(bool newState)
    {
        tooltipActiveState = newState;
        tooltip.SetActive(newState);
    }
}
