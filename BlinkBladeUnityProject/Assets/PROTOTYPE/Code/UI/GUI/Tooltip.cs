using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RevealType
{
    GameObject,
    Mouse
}

public class Tooltip : MonoBehaviour
{
    public RevealType revealType;
    string tagName = "Player";
    [SerializeField] bool tooltipActiveState;

    public GameObject tooltip;
    Animator tooltipAnim;

    // Start is called before the first frame update
    void Start()
    {
        tooltipAnim = tooltip.GetComponent<Animator>();
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

    private void OnMouseOver(RevealType type)
    {
        if (type == RevealType.Mouse)
        {
            SwitchTooltipActivationState(true);
        }
    }

    private void OnMouseExit(RevealType type)
    {
        if (type == RevealType.Mouse)
        {
            SwitchTooltipActivationState(false);
        }
    }

    void SwitchTooltipActivationState(bool newState)
    {
        tooltipActiveState = newState;
        tooltipAnim.SetBool("ActiveState", newState);
    }

    
}
