using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_10_01 : MonoBehaviour
{
    GameObject skill10;
    int Skilltime;
    int prevMainturn;

    void Start()
    {
        skill10 = Resources.Load("Skill Effect/Skill_10_01") as GameObject;

        Skilltime = 4;
    }

    void Update()
    {
        //if (GameManager.Instance.mainTurn == 1)
        if(Skilltime == 0)
        {
            GameObject ins = Instantiate(skill10, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity) as GameObject;
            Destroy(this.gameObject);
        }
        else
        {
            if (GameManager.Instance.mainTurn - prevMainturn != 0)
            {
                Skilltime -= 1;
            }
        }
        prevMainturn = GameManager.Instance.mainTurn;
    }
}
