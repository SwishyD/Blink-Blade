using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletMovement : MonoBehaviour
{
    public float speed;
    [SerializeField] GameObject destroyedBullet;

    private void Start()
    {
        //Debug.Log("BulletSpawned");
        if (destroyedBullet == null)
        {
            Debug.LogWarning("GameObject 'DestroyedBullet' is null! Use the prefab.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 31)
        {
            if (SceneManager.GetActiveScene().name.Contains("BOSS"))
            {
                col.GetComponent<BossPlayerSpawnPoint>().Respawn();
            }
            else
            {
                col.GetComponent<PlayerSpawnPoint>().Respawn();
            }
            DestroyBullet();
        }
        else if(col.gameObject.layer == 8 || col.gameObject.layer == 9 || col.gameObject.layer == 11)
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        Instantiate(destroyedBullet, gameObject.transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }
}
