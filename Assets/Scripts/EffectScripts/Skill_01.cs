using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_01 : SkillMom
{
    GameObject targetDoll;
    int Skill01AttackDamage;

    void Start()
    {
        Damage = 6; //데미지
        //SkillCost = 1;
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        targetDoll = collision.gameObject;
        //Debug.Log(System.Convert.ToInt32(targetDoll.tag));
        if (targetDoll != null)
        {
            if (DontTouchTile(targetDoll.tag)) // 검은돌과 흰돌의 태그
            {
                if (targetDoll.GetComponent<Ability>() != null)
                    targetDoll.GetComponent<Ability>().MinusHp(Damage);
                AudioManager.Instance.PlaySkillClip(0);
            }
        }
    }
}
