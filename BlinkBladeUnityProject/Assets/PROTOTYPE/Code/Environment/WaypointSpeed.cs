using System.Collections;
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
            bossScript.shooterVariable.spray = spray;
            bossScript.shooterVariable.numberOfShots = bulletsInSpray;
        }
        if(bossPhases == BossPhases.Spawning)
        {
            bossScript.spawnerVariables.waveNumber++;
        }
        if(bossPhases == BossPhases.Walls)
        {
            bossScript.wallVariables.allWallsActive = !bossScript.wallVariables.allWallsActive;
        }
        if(bossPhases == BossPhases.Gravity)
        {
            PlayerFlipManager.instance.flipTimer = gravTimer;
        }
        bossScript.phases = bossPhases;
    }
}
