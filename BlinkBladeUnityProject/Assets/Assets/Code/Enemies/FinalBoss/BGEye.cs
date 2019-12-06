using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGEye : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject pupilNull;
    GameObject player;

    [SerializeField] float eyeOpenMin;
    [SerializeField] float eyeOpenMax;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerJumpV2>().gameObject;
        Invoke("CloseEye", Random.Range(eyeOpenMin, eyeOpenMax));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = player.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        pupilNull.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    void CloseEye()
    {
        anim.SetTrigger("Close");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
