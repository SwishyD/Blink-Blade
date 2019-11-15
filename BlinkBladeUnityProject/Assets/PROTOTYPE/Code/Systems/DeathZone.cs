using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name.Contains("BOSS"))
            {
                col.gameObject.GetComponent<BossPlayerSpawnPoint>().Respawn();
            }
            else
            {
                col.gameObject.GetComponent<PlayerSpawnPoint>().Respawn();
            }
        }
    }
}
