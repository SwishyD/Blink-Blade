using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraActive : MonoBehaviour
{
    public WaypointCamera wayCam;
    public Timer timer;
    public BossPlayerSpawnPoint bossSpawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wayCam.active = true;
            timer.timerActive = true;
            timer.levelStarted = true;
            bossSpawn.active = true;
        }
    }
}
