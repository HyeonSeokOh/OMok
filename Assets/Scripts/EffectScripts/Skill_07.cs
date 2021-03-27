using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_07 : SkillMom // 지속효과라 스킬 이펙트말고 지속 이펙트에 넣었음(Skill07_Mu)
{
    public GameObject targetDoll;
    GameObject Shield_Effect;
    GameObject ins;
    public GameObject doll;
    int Skill07Cost;
    public Collider2D col;

    void Start()
    {
        //SkillCost = 3; //코스트
        Shield_Effect = Resources.Load("Doll Effect/Cost_3_skill_shield") as GameObject;
        Debug.Log(Shield_Effect);
    }

    void Update()
    {
        if (ins != null && doll != null)
        {
            Debug.Log(doll.gameObject);
            ins.GetComponent<Skill_07_Shield>().target = doll.gameObject;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        targetDoll = collision.gameObject;

        if (targetDoll != null)
        {
            if (DontTouchTile(targetDoll.tag))
            {
                if (targetDoll.GetComponent<Ability>() != null)
                {
                    doll = targetDoll.gameObject;
                    targetDoll.GetComponent<Ability>().onoffShield();
                }
            }

            if (!DontTouchTile(targetDoll.tag))
                ins = Instantiate(Shield_Effect, new Vector2(targetDoll.transform.position.x, targetDoll.transform.position.y), Quaternion.identity) as GameObject;
            {
            }

            AudioManager.Instance.PlaySkillClip(6);
        }
    }
}
