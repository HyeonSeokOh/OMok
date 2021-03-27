using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
//using UnityEngine.Tilemaps;

public class Skill_06 : SkillMom
{
    public GameObject targetDoll;
    GameObject health_Effect;
    GameObject doll;
    GameObject ins;
    int Skill06Shield;
    void Start()
    {
        health_Effect = Resources.Load("Doll Effect/Cost_3_Health_Effect") as GameObject;
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
                {
                    targetDoll.GetComponent<Ability>().AddMortage(targetDoll.GetComponent<Ability>().GetHp());   // 자기 체력 함 저장하고
                }
            }

            if (!DontTouchTile(targetDoll.tag))
            {
                ins = Instantiate(health_Effect, new Vector2(targetDoll.transform.position.x, targetDoll.transform.position.y), Quaternion.identity) as GameObject;
            }
        }
        AudioManager.Instance.PlaySkillClip(5);
    }
}
