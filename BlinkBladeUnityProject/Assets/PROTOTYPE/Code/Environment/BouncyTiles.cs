using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TopSide { TopBottom, Sideways}

public class BouncyTiles : MonoBehaviour
{
    public TopSide direction;
    private SwordSpawner spawner;

    private void Start()
    {
        spawner = SwordSpawner.instance;
    }

    public void ChooseDirection()
    {
        if (direction == TopSide.TopBottom)
        {
            TopBounce();
        }
        else if(direction == TopSide.Sideways)
        {
            SideBounce();
        }
    }

    void TopBounce()
    {
        spawner.cloneSword.transform.rotation = Quaternion.Euler(spawner.cloneSword.transform.eulerAngles.x, spawner.cloneSword.transform.eulerAngles.y, -spawner.cloneSword.transform.eulerAngles.z);
    }

    void SideBounce()
    {
        spawner.cloneSword.transform.rotation = Quaternion.Euler(spawner.cloneSword.transform.eulerAngles.x, spawner.cloneSword.transform.eulerAngles.y, -spawner.cloneSword.transform.eulerAngles.z);
        spawner.cloneSword.transform.rotation = Quaternion.Euler(spawner.cloneSword.transform.eulerAngles.x, spawner.cloneSword.transform.eulerAngles.y, spawner.cloneSword.transform.eulerAngles.z + 180f);
    }
}