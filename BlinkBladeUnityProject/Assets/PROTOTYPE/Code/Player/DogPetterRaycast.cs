using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPetterRaycast : MonoBehaviour
{
    public LayerMask rayMask;
    [SerializeField] PetTheDog dogPetScript;
    [SerializeField] float rayDist = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayDist, rayMask, -Mathf.Infinity, Mathf.Infinity);
        Debug.DrawLine(transform.position, hit.point, Color.green);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 10)
            {
                dogPetScript.SetDogPettability(true);
            }
        }
        else if (hit.collider == null)
        {
            dogPetScript.SetDogPettability(false);
        }
    }
}
