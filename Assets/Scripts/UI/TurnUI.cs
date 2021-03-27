using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUI : MonoBehaviour
{

    GameObject BlackTurn;
    GameObject WhiteTurn;
    GameObject SkillTurn;

    GameObject Skill01;

    void Start()
    {
        BlackTurn = GameObject.FindGameObjectWithTag("101");
        WhiteTurn = GameObject.FindGameObjectWithTag("102");
        SkillTurn = GameObject.FindGameObjectWithTag("103");

        BlackTurn.SetActive(false);
        WhiteTurn.SetActive(false);
        SkillTurn.SetActive(false);
    }

    void Update()
    {
        if(GameManager.Instance.mainTurn == 1)
        {
            BlackTurn.SetActive(true);
            WhiteTurn.SetActive(false);
            SkillTurn.SetActive(false);
        }
        else if (GameManager.Instance.mainTurn == 2)
        {
            BlackTurn.SetActive(false);
            WhiteTurn.SetActive(false);
            SkillTurn.SetActive(true);
        }
        //else if(GameManager.Instance.mainTurn == 3)
        //{
        //    BlackTurn.SetActive(false);
        //    WhiteTurn.SetActive(true);
        //    SkillTurn.SetActive(false);
        //}
        //else if(GameManager.Instance.mainTurn == 4)
        //{
        //    BlackTurn.SetActive(false);
        //    WhiteTurn.SetActive(false);
        //    SkillTurn.SetActive(true);
        //}
    }
}
