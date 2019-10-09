using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    public Texture2D cursorNoBlink;
    public Texture2D cursorYesBlink;
    private bool cursorIsCanBlink;
    Vector2 hotSpot;
    public bool blinkCursorOn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hotSpot = new Vector2(17, 17);
    }

    public void ChangeCursor(bool active)
    {
        blinkCursorOn = active;
        if (active)
        {
            Cursor.SetCursor(cursorYesBlink, hotSpot, CursorMode.Auto);
        }
        else if (!active)
        {
            Cursor.SetCursor(cursorNoBlink, hotSpot, CursorMode.Auto);
        }
    }
}
