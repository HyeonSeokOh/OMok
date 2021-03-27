using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_05 : SkillMom
{
    GameObject targetDoll;

    void Start()
    {
        Damage = 10; //데미지
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        targetDoll = collision.gameObject;

        if (targetDoll != null)
        {
            if (DontTouchTile(targetDoll.tag)) // 검은돌과 흰돌의 태그
            {
                if (targetDoll.GetComponent<Ability>() != null)
                {
                    targetDoll.GetComponent<Ability>().MinusHp(Damage);
                    AudioManager.Instance.PlaySkillClip(4);
                    targetDoll = null;
                }
            }
        }
    }
}
