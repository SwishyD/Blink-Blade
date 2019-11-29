using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTooltip : MonoBehaviour
{
    string tagName = "Player";

    [SerializeField] bool tooltipActiveState;

    [SerializeField] GameObject tooltip;

    // Start is called before the first frame update
    void Start()
    {
        //tooltipAnim = tooltip.GetComponentInParent<Animator>();
        SwitchTooltipActivationState(tooltipActiveState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagName)
        {
            SwitchTooltipActivationState(true);
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
