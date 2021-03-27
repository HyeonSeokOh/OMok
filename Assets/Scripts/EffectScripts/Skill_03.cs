using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_03 : SkillMom
{
    public GameObject targetDoll;
    GameObject ampli;
    GameObject ins;
    public GameObject doll;
    // Start is called before the first frame update
    void Start()
    {
        ampli = Resources.Load("Doll Effect/Cost_2_Doll_Amplify_Effect") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (ins != null && doll != null)
        {
            //Debug.Log(doll.gameObject);
            ins.GetComponent<Skill_03_Amplify>().target = doll.gameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        targetDoll = collision.gameObject;

        if (targetDoll != null)
        {
            if (DontTouchTile(targetDoll.tag)) // 검은돌과 흰돌의 태그만 가져옴
            {   
                if (targetDoll.GetComponent<Ability>() != null)
                {
                    doll = targetDoll.gameObject;
                    targetDoll.GetComponent<Ability>().onoffamplify();
                }
            }

            if (!DontTouchTile(targetDoll.tag))
            {
                ins = Instantiate(ampli, new Vector2(targetDoll.transform.position.x, targetDoll.transform.position.y), Quaternion.identity) as GameObject;
                ins.AddComponent<Skill_03_Amplify>();
            }
            AudioManager.Instance.PlaySkillClip(2);
        }
    }
}
