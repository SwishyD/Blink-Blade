﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TopSide2 { TopBottom, Sideways }

public class BossDeflect : MonoBehaviour
{
    public TopSide2 direction;
    private SwordSpawner spawner;
    Animator anim;

    private void Start()
    {
        spawner = SwordSpawner.instance;
        anim = GetComponentInParent<Animator>();
    }

    public void ChooseDirection()
    {
        if (direction == TopSide2.TopBottom)
        {
            TopBounce();
        }
        else if (direction == TopSide2.Sideways)
        {
            SideBounce();
        }
    }

    void TopBounce()
    {
        spawner.cloneSword.transform.rotation = Quaternion.Euler(spawner.cloneSword.transform.eulerAngles.x, spawner.cloneSword.transform.eulerAngles.y, -spawner.cloneSword.transform.eulerAngles.z);
        //AudioManager.instance.Play("Bounce");
        AudioManager.instance.Play("SwordThrow");
        AudioManager.instance.Play("SwordShwing");
        AudioManager.instance.Play("HardStep");
        anim.SetTrigger("Deflect");
    }

    void SideBounce()
    {
        spawner.cloneSword.transform.rotation = Quaternion.Euler(spawner.cloneSword.transform.eulerAngles.x, spawner.cloneSword.transform.eulerAngles.y, -spawner.cloneSword.transform.eulerAngles.z);
        spawner.cloneSword.transform.rotation = Quaternion.Euler(spawner.cloneSword.transform.eulerAngles.x, spawner.cloneSword.transform.eulerAngles.y, spawner.cloneSword.transform.eulerAngles.z + 180f);
        //AudioManager.instance.Play("Bounce");
        AudioManager.instance.Play("SwordThrow");
        AudioManager.instance.Play("SwordShwing");
        AudioManager.instance.Play("HardStep");
        anim.SetTrigger("Deflect");
    }
}