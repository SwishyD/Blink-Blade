using UnityEngine;

public class AdjustTooltipPos : MonoBehaviour
{
    [SerializeField] Transform tooltipTransform;
    [SerializeField] float upDist;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 tooltipPos = Camera.main.WorldToScreenPoint(transform.position);
        tooltipTransform.transform.position = new Vector3(tooltipPos.x, tooltipPos.y + upDist, tooltipPos.z);
    }
}
