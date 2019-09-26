using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIndicator : MonoBehaviour
{
    public Texture2D indicator;
    public float indicatorSize;

    public GUIStyle swordBox;
    private Vector2 indicatorRange;
    private float indicatorScale = Screen.width / 500;
    private Camera cam;
    public bool visible;

    // Start is called before the first frame update
    void Start()
    {
        visible = GetComponentInChildren<SpriteRenderer>().isVisible;

        cam = Camera.main;

        indicatorRange.x = Screen.width - (Screen.width / 6);
        indicatorRange.y = Screen.height - (Screen.height / 7);
        indicatorRange /= 2f;

        swordBox.normal.textColor = new Vector4(0, 0, 0, 0);
    }

    void OnGUI()
    {
        if (!visible)
        {
            Vector3 direction = transform.position - cam.transform.position;
            direction = Vector3.Normalize(direction);
            direction.y *= -1f;

            Vector2 indicatorPos = new Vector2(indicatorRange.x * direction.x, indicatorRange.y * direction.y);
            indicatorPos = new Vector2((Screen.width / 2) + indicatorPos.x, (Screen.height / 2) + indicatorPos.y);

            Vector3 posDir = transform.position - cam.ScreenToWorldPoint(new Vector3(indicatorPos.x, indicatorPos.y, transform.position.z));
            posDir = Vector3.Normalize(posDir);

            float angle = Mathf.Atan2(posDir.x, posDir.y) * Mathf.Rad2Deg;

            GUIUtility.RotateAroundPivot(angle, indicatorPos);
            GUI.Box(new Rect(indicatorPos.x, indicatorPos.y, indicatorScale * indicatorSize, indicatorScale * indicatorSize), indicator, swordBox);
            GUIUtility.RotateAroundPivot(0, indicatorPos);
        }
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }

    private void OnBecameVisible()
    {
        visible = true;
    }
}
