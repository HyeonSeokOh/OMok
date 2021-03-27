using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_06_Turn2 : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////폐기 스크립트///////////////////////////////////////////////////////////
    GameObject targetDoll;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        targetDoll = collision.gameObject;

        if (targetDoll != null)
        {
            if (targetDoll.tag == "11"/* || targetDoll.tag == "흰돌태그"*/) // 검은돌과 흰돌의 태그
            {
                if (targetDoll.GetComponent<Ability>() != null)
                {
                    //targetDoll.GetComponent<Ability>().DollHp = 10;
                    targetDoll = null;
                }
            }
        }
    }
}
