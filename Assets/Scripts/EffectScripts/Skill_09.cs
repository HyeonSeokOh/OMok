using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_09 : SkillMom
{
    GameObject targetDoll;

    void Start()
    {
        Damage = 25;
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        targetDoll = collision.gameObject;

        if (targetDoll != null)
        {
            if (DontTouchTile(targetDoll.tag))
            {
                if (targetDoll.GetComponent<Ability>() != null)
                    targetDoll.GetComponent<Ability>().MinusHp(Damage);
                //targetDoll = null;
                AudioManager.Instance.PlaySkillClip(8);
            }
        }
    }
}
