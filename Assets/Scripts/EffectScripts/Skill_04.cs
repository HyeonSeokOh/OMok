using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_04 : SkillMom
{
    GameObject targetDoll;

    void Start()
    {
        //SkillCost = 2;

        Damage = 10; //데미지

        if (TileManager.TileMain != null)
        {
            if (TileManager.TileMain[(int)this.transform.position.x, (int)this.transform.position.y].TileAttribute == 2)
            {
                Damage = (int)(10 * 1.5);
            }

            if (TileManager.TileMain[(int)this.transform.position.x, (int)this.transform.position.y].TileAttribute == 3)
            {
                Damage = (int)(10 * 0.5);
            }
        }
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
                //targetDoll.GetComponent<Ability>().DollHp -= Skill04AttackDamage;
                if (targetDoll.GetComponent<Ability>() != null)
                    targetDoll.GetComponent<Ability>().MinusHp(Damage);
                //targetDoll = null;
                AudioManager.Instance.PlaySkillClip(3);
            }
        }
    }
}
