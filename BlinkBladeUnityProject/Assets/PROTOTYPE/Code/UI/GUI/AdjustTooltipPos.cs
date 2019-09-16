using UnityEngine;

public class AdjustTooltipPos : MonoBehaviour
{
    [SerializeField] Transform tooltipTransform;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 tooltipPos = Camera.main.WorldToScreenPoint(transform.position);
        tooltipTransform.transform.position = tooltipPos;
    }
}
