﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSpeed : MonoBehaviour
{
    public FinalBossScript bossScript;

    [Tooltip("Speed the Camera moves to next Waypoint")]
    public float speed;
    [Tooltip("(seconds) Time the camera waits here until moving to next waypoint")]
    public float waitTime;

    public BossPhases bossPhases;

    [ConditionalEnumHide("bossPhases", 2, 2, true)]
    public float gravTimer;
    [ConditionalEnumHide("bossPhases", 1, 1, true)]
    public bool spray;
    [ConditionalEnumHide("bossPhases", 1, 1, true)]
    public int bulletsInSpray;

    public void PhaseChange()
    {
        if(bossPhases == BossPhases.Shooting)
        {
            bossScript.spray = spray;
            bossScript.numberOfShots = bulletsInSpray;
        }
        if(bossPhases == BossPhases.Spawning)
        {
            bossScript.waveNumber++;
        }
        if(bossPhases == BossPhases.Walls)
        {
            bossScript.allWallsActive = !bossScript.allWallsActive;
        }
        if(bossPhases == BossPhases.Gravity)
        {
            PlayerFlipManager.instance.flipTimer = gravTimer;
        }
        bossScript.phases = bossPhases;
    }
}
