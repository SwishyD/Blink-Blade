using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPetterRaycast : MonoBehaviour
{
    public LayerMask rayMask;
    [SerializeField] PetTheDog[] dogPetScripts;
    [SerializeField] float rayDist = 1;
    public int[] dirMult;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        for (int i = 0; i < dogPetScripts.Length; i++)
        {
            dirMult[i] = dogPetScripts[i].dirMult;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0,-0.5f,0), transform.right * dirMult[0], rayDist, rayMask, -Mathf.Infinity, Mathf.Infinity);
        Debug.DrawLine(transform.position, hit.point, Color.green);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == 12)
            {
                hit.collider.gameObject.GetComponent<PetTheDog>().SetDogPettability(true);
                //dogPetScripts.SetDogPettability(true);
            }
        }
        else if (hit.collider == null)
        {
            for (int i = 0; i < dogPetScripts.Length; i++)
            {
                dogPetScripts[i].SetDogPettability(false);
            }
            //dogPetScripts.SetDogPettability(false);
        }
    }
}
