using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    public Texture2D cursorNoBlink;
    public Texture2D cursorYesBlink;
    private bool cursorIsCanBlink;
    Vector2 hotSpot;

    // Start is called before the first frame update
    void Start()
    {
        hotSpot = new Vector2(17, 17);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            ChangeCursorState(true);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            ChangeCursorState(false);
        }
    }

    public void ChangeCursorState(bool canBlink)
    {
        if (canBlink)
        {
            Debug.Log("CAN BLINK");
            Cursor.SetCursor(cursorYesBlink, hotSpot, CursorMode.Auto);
            cursorIsCanBlink = true;
        }
        if (!canBlink)
        {
            Debug.Log("CAN NOT BLINK");
            Cursor.SetCursor(cursorNoBlink, hotSpot, CursorMode.Auto);
            cursorIsCanBlink = false;
        }
    }
}
