using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurnUIImage : MonoBehaviour
{
    public Image TurnCount;

    GameObject TurnUI;
    Animator TurnAni;
    //bool blackdollstate = false;
    //bool blackskillstate = false;
    //bool whitedollstate = false;
    //bool whiteskillstate = false;

    void Start()
    {
        TurnUI = GameObject.Find("TurnUIImage");
        TurnAni = GetComponent<Animator>();
    }

    void Update()
    {
        ChangeTurnImage();
    }
    void ChangeTurnImage()
    {
        if (GameManager.Instance.mainTurn == 1)
        {
            TurnAni.SetBool("WhiteSkill", false);
            TurnAni.SetBool("BlackDoll", true);
        }
        if (GameManager.Instance.mainTurn == 2)
        {
            TurnAni.SetBool("BlackDoll", false);
            TurnAni.SetBool("BlackSkill", true);
        }
        if (GameManager.Instance.mainTurn == 3)
        {
            TurnAni.SetBool("BlackSkill", false);
            TurnAni.SetBool("WhiteDoll", true);

        }
        if (GameManager.Instance.mainTurn == 4)
        {
            TurnAni.SetBool("WhiteDoll", false);
            TurnAni.SetBool("WhiteSkill", true);
        }
    }
}
