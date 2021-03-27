using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_02 : SkillMom
{
    GameObject targetDoll;

    void Start()
    {
        Heel = 10; //힐
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
                    targetDoll.GetComponent<Ability>().PlusHp(Heel);
                AudioManager.Instance.PlaySkillClip(1);
            }
        }
    }
}
