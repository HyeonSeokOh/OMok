using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Master : MonoBehaviour
{
    int nowPage;
    bool isPage;    // 페이지 넘어갓는지 확인
    bool isLock;    // 자물쇠가 채워져있는지 확인
    int skill_Code;       // 스킬 코드
    Sprite skill_Image;
    Sprite mask;
    GameObject lock_obj;
    int number; // 이 새1끼가 스킬 코드를 가지고 있음 !

    void Start()
    {
        nowPage = DeckManager.Instance.page;
        isPage = false;
        isLock = false;
        mask = this.GetComponent<Image>().sprite;   // 투명이미지
        number = 0; 
        number = (nowPage * 8) + (int.Parse(this.transform.parent.tag) - 1);

        if (DeckManager.Instance.skill_Image.Length - 1 >= number)
        {
            skill_Image = DeckManager.Instance.skill_Image[number];
            skill_Code = number + 1 + 1000; 
        }
        else
            skill_Image = mask;

        this.GetComponent<Image>().sprite = skill_Image;

        lock_obj = Instantiate(DeckManager.Instance.lockImage, this.transform.position, Quaternion.identity);
        lock_obj.GetComponent<RectTransform>().transform.SetParent(this.GetComponent<RectTransform>().transform);
        lock_obj.GetComponent<RectTransform>().localScale = new Vector2(2, 2);
        lock_obj.SetActive(false);
        Skill_Lock();
    }

    void Update()
    {
        if (nowPage != DeckManager.Instance.page)
            isPage = true;
        

        if (isPage)
        {
            nowPage = DeckManager.Instance.page;

            number = (nowPage * 8) + (int.Parse(this.transform.parent.tag) - 1);

            if (DeckManager.Instance.skill_Image.Length - 1 >= number)
            {
                skill_Image = DeckManager.Instance.skill_Image[number];
                skill_Code = number + 1 + 1000; 
            }
            else
                skill_Image = mask; // 빈 곳은 마스크

            this.GetComponent<Image>().sprite = skill_Image;

            isPage = false;
            Skill_Lock();
        }
    }

    void Skill_Lock()
    {
        if (LoadGame.Instance.unlockStageAndBlock[LoadGame.Instance.loadCount].unlockSkillCount <= number)    // 자물쇠 채운다.
        {
            this.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            lock_obj.SetActive(true);
            isLock = true;
        }
        else
        {
            this.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            lock_obj.SetActive(false);
            isLock = false;
        }    
    }

    public void Click_Button()
    {
        if (!isLock)
        {
            for (int i = 0; i < 10; i++)
            {
                if (DeckManager.Instance.skill_Block[i].transform.GetChild(0).GetComponent<Image>().sprite == mask)
                {
                    DeckManager.Instance.skill_Block[i].transform.GetChild(0).GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
                    DeckManager.Instance.skill_Preset[DeckManager.Instance.preset, i] = this.GetComponent<Image>().sprite;
                    DeckManager.Instance.save_Skill[DeckManager.Instance.preset, i] = skill_Code;
                    break;
                }
            }
        }
    }

    public void Skill_Reset()
    {
        if (DeckManager.Instance.mod == false)
        {
            for (int i = 0; i < 10; i++)
            {
                if (DeckManager.Instance.skill_Block[i].transform.GetChild(0).GetComponent<Image>().sprite != mask)
                {
                    DeckManager.Instance.skill_Block[i].transform.GetChild(0).GetComponent<Image>().sprite = mask;
                    DeckManager.Instance.skill_Preset[DeckManager.Instance.preset, i] = mask;
                    DeckManager.Instance.save_Skill[DeckManager.Instance.preset, i] = -2000;
                }
            }
        }
    }


}
