using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_10 : SkillMom
{
    GameObject targetDoll;

    void Start()
    {

        Damage = 40; //데미지
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
                AudioManager.Instance.PlaySkillClip(9);
                //targetDoll = null;
            }
        }
    }
}
