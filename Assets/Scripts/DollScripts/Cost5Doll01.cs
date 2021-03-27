using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cost5Doll01 : MonoBehaviour
{

    bool isdie = false;
    public GameObject targetDoll;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Ability>() !=null)
            targetDoll = collision.gameObject;

        if (targetDoll != null)
        {

            if (targetDoll.tag != "DefaultTile" && targetDoll.tag != "ForestTile" && targetDoll.tag != "IceTile" && targetDoll.tag != "WaterTile")
            {
                if (targetDoll.GetComponent<Ability>() != null)
                {
                        targetDoll.GetComponent<Ability>().onoffBigAmplify(true);
                }
            }
        }
    }
    public void dead()
    {
        isdie = true;

        if (targetDoll != null && isdie)
        {
            if (targetDoll.GetComponent<Ability>() != null)
            {
                targetDoll.GetComponent<Ability>().onoffBigAmplify(false);
            }
            
        }
        Destroy(this.gameObject);
    }
}
