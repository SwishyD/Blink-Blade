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

    [ConditionalEnumHide("bossPhases", 1, 1, true)]
    public float timeBetweenVolleys;
    [ConditionalEnumHide("bossPhases", 1, 1, true)]
    public bool spray;
    [ConditionalEnumHide("bossPhases", 1, 1, true)]
    public int bulletsInSpray;
    [ConditionalEnumHide("bossPhases", 1, 1, true)]
    public float timeBetweenMultiShots;

    [ConditionalEnumHide("bossPhases", 2, 2, true)]
    public float gravTimer;

    [ConditionalEnumHide("bossPhases", 4, 4, true)]
    public bool wallsActive;
    [ConditionalEnumHide("bossPhases", 4, 4, true)]
    public Vector2 wallScale;
    [ConditionalEnumHide("bossPhases", 4, 4, true)]
    public float wallZoomSpeed;
    [ConditionalEnumHide("bossPhases", 4, 4, true)]
    public bool wallMovement;
    [ConditionalEnumHide("bossPhases", 4, 4, true)]
    public float movingWallSpeed;

    [ConditionalEnumHide("bossPhases", 5, 5, true)]
    public GameObject blocker;

    public void PhaseChange()
    {
        if(bossPhases == BossPhases.Shooting)
        {
            bossScript.shooterVariable.spray = spray;
            bossScript.shooterVariable.numberOfShots = bulletsInSpray;
            bossScript.shooterVariable.timeBetweenShots = timeBetweenVolleys;
            bossScript.shooterVariable.timeBetweenMultiShots = timeBetweenMultiShots;
        }
        if(bossPhases == BossPhases.Spawning)
        {
            bossScript.spawnerVariables.waveNumber++;
        }
        if(bossPhases == BossPhases.Walls)
        {
            bossScript.wallVariables.allWallsActive = wallsActive;
            bossScript.wallVariables.wallScale = wallScale;
            bossScript.wallVariables.wallZoomSpeed = wallZoomSpeed;
            bossScript.wallVariables.wallMove = wallMovement;
            bossScript.wallVariables.wallMovementSpeed = movingWallSpeed;
        }
        if(bossPhases == BossPhases.Gravity)
        {
            PlayerFlipManager.instance.flipTimer = gravTimer;
        }
        if (bossPhases == BossPhases.Finale)
        {
           blocker.SetActive(true);
        }
        bossScript.phases = bossPhases;
    }
}
