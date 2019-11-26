using UnityEngine;
using UnityEngine.SceneManagement;

public enum RevealType
{
    GameObject,
    Mouse
}

public class Tooltip : MonoBehaviour
{
    public RevealType revealType;
    string tagName = "Player";
    public int level;
    [SerializeField] bool tooltipActiveState;
    [SerializeField] bool eTooltipActiveState;

    [SerializeField] GameObject tooltip;
    [SerializeField] GameObject pressE;
    //Animator tooltipAnim;

    // Start is called before the first frame update
    void Start()
    {
        //tooltipAnim = tooltip.GetComponentInParent<Animator>();
        SwitchTooltipActivationState(tooltipActiveState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagName && LevelManager.instance != null)
        {
            if(SceneManager.GetActiveScene().name == "HUB" && !gameObject.name.Contains("DogBowl") && !gameObject.name.Contains("bossDoor"))
            {
                if (LevelManager.instance.levelComplete[level])
                {
                    SwitchTooltipActivationState(true);
                }
                if(GetComponent<SpriteRenderer>().sprite == GetComponent<LevelTransition>().unlockedDoor)
                {
                    PressEActivationState(true);
                }
            }
            else if (gameObject.name.Contains("bossDoor") && LevelManager.instance.levelUnlocked[10])
            {
                SwitchTooltipActivationState(true);
                PressEActivationState(true);
            }
            else
            {
                SwitchTooltipActivationState(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == tagName)
        {
            SwitchTooltipActivationState(false);
            PressEActivationState(false);
        }
    }

    private void OnMouseOver()
    {
        if (revealType == RevealType.Mouse)
        {
            SwitchTooltipActivationState(true);
        }
    }

    private void OnMouseExit()
    {
        if (revealType == RevealType.Mouse)
        {
            SwitchTooltipActivationState(false);
        }
    }

    void SwitchTooltipActivationState(bool newState)
    {
        tooltipActiveState = newState;
        //tooltipAnim.SetBool("ActiveState", newState);
        tooltip.SetActive(newState);
    }

    void PressEActivationState(bool newEState)
    {
        eTooltipActiveState = newEState;
        pressE.SetActive(newEState);
    }

    
}
