using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour
{
    //float Count = 0.0f;
    //bool state = false;


    GameObject CostBackground;

    private Text showTurn;
    private Text timeCount;

    Vector2 MousePosition;
    
    public Transform Cam;

    GameObject SkillBackGround01;
    GameObject SkillBackGround02;


    void Start()
    {
        CostBackground = GameObject.Find("CostBackGround");
        Cam = Camera.main.transform;

        SkillBackGround01 = GameObject.Find("SkillBackGround01");
        SkillBackGround02 = GameObject.Find("SkillBackGround02");

        SkillBackGround01.SetActive(false);
        SkillBackGround02.SetActive(false);
        SkillBackGround01.SetActive(true);

    }
    public void ChangeSkill1()
    {
        SkillBackGround01.SetActive(true);
        SkillBackGround02.SetActive(false);
    }
    
    public void ChangeSkill2()
    {
        SkillBackGround01.SetActive(false);
        SkillBackGround02.SetActive(true);
    }

   
}
