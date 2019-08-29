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

    private SwordProjectile swordScript;

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
        swordScript = FindObjectOfType<SwordProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (swordScript.stuckInObject)
        {
            //ChangeCursorState(true);
        }
        //else if (!swordScript.stuckInObject)
        {
            //ChangeCursorState(false);
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
