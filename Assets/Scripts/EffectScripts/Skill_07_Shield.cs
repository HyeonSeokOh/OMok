using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_07_Shield : MonoBehaviour
{
    public GameObject target;

    void Start()
    {

    }

    void Update()
    {
        if (target != null)
        {
            if (!target.GetComponent<Ability>().GetSmallShield())
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
