using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineColourChanger : MonoBehaviour
{
    private Animator anim;
    private bool currentlyHovering;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlyHovering)
        {
            if (anim.GetFloat("mouseHover") > 0)
            {
                anim.SetFloat("mouseHover", anim.GetFloat("mouseHover") - Time.deltaTime);
            }
        }
    }

    private void OnMouseEnter()
    {
        anim.SetFloat("mouseHover", 1);
        currentlyHovering = true;
    }

    private void OnMouseExit()
    {
        //anim.SetFloat("mouseHover", 0);
        currentlyHovering = false;
    }
}
