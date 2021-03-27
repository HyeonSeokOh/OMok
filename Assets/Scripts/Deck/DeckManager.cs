using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DeckManager : MonoBehaviour
{
    private static DeckManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        page = 0;
        preset = 0;
        mod = true;
        skill_Image = Resources.LoadAll<Sprite>("Item");
        stone_Image = Resources.LoadAll<Sprite>("Doll");
        lockImage = Resources.Load("LockImage") as GameObject;

        skill_Block = new GameObject[10];
        save_Skill = new int[4, 10];
        skill_Preset = new Sprite[4, 10];

        for (int i = 0; i < 10; i++)
        {
            string str = "Skill_Block" + (i + 1).ToString();
            skill_Block[i] = GameObject.Find(str);
        }

        stone_Block = new GameObject[5];
        save_Stone = new int[4, 5];
        stone_Preset = new Sprite[4, 5];

        for (int i = 0; i < 5; i++)
        {
            string str = "Stone_Block" + (i + 1).ToString();
            stone_Block[i] = GameObject.Find(str);
        }

        GameObject obj = Resources.Load("BlankImage") as GameObject;
        blank = obj.GetComponent<Image>();

        //여기다 스킬, 돌 코드 불러오기

        for (int i2 = 0; i2 < 4; i2++)
        {
            for (int i = 0; i < 10; i++)
            {
                if (LoadGame.Instance.preset_Skill_Card[i2, i] > 0)
                {
                    Debug.Log(LoadGame.Instance.preset_Skill_Card[i2, i]);
                    Sprite spr = skill_Image[LoadGame.Instance.preset_Skill_Card[i2, i] - 1 - 1000];
                    DeckManager.Instance.skill_Preset[i2, i] = spr;
                    save_Skill[i2, i] = LoadGame.Instance.preset_Skill_Card[i2, i];
                    if (i2 == 0)
                        DeckManager.Instance.skill_Block[i].transform.GetChild(0).GetComponent<Image>().sprite = spr;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (LoadGame.Instance.preset_Block_Card[i2, i] > 0)
                {
                    Sprite spr = stone_Image[LoadGame.Instance.preset_Block_Card[i2, i] - 1];
                    DeckManager.Instance.stone_Preset[i2, i] = spr;
                    save_Stone[i2, i] = LoadGame.Instance.preset_Block_Card[i2, i];
                    if (i2 == 0)
                        DeckManager.Instance.stone_Block[i].transform.GetChild(0).GetComponent<Image>().sprite = spr;
                }
            }
        }

    }
    public static DeckManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public int preset;
    public int page;
    public bool mod;    // 참이면 돌, 거짓이면 스킬
    public Sprite[] skill_Image;    // 스킬 이미지 담고있다.
    public Sprite[] stone_Image;    // 돌 이미지 담고있다.
    public int[,] save_Skill;   // 스킬 코드 저장중
    public int[,] save_Stone;   // 돌 코드 저장중
    public Sprite[,] skill_Preset;
    public Sprite[,] stone_Preset;
    public GameObject[] skill_Block;
    public GameObject[] stone_Block;
    public bool Deckisexplain = false;
    public int DeckTextType = 0;
    public GameObject DeckRightUI;
    public GameObject lockImage;
    public Image blank; // 빈칸 이미지
    public void ModBlock()
    {
        mod = true;
        page = 0;
    }

    public void ModSkill()
    {
        mod = false;
        page = 0;
    }

    public void NextPage()
    {
        int skill_page = (skill_Image.Length) / 8;
        int stone_page = (stone_Image.Length) / 8;

        if (!mod && page < skill_page)
        {
            page++;
        }
        else if (mod && page < stone_page)
        {
            page++;
        }
    }

    public void PreviousPage()
    {
        if (!mod && page > 0)
        {
            page--;
        }
    }

}