//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cost5Attack : Skills //10번 보조 아이템
{
    int x;
    int y;

    void Start()
    {
        SkillCost = 5; //코스트
        myTag = System.Convert.ToInt32(this.gameObject.tag);
        number = 1010;
        randomAtk = 12;
        thisWeight = -200;
    }
    public override void Init()
    {
        Start();
    }
    void Update()
    {
        int TurnPlus = GameManager.Instance.mainTurn;
        ChangeAlpha();
        if (GameManager.Instance.Dragging && DollMove.Instance.move_Object_Tag == myTag)
        {
            int i, j;
            x = DollMove.Instance.Xup;
            y = DollMove.Instance.Yup;
            if (x != -1 || y != -1)
            {
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 5; j++)
                    {
                        if (indexRange(x + i, y + j))
                        {
                            if (TileManager.TileMain != null)
                            {
                                int ran = Random.Range(0, 25);
                                if (ran < randomAtk)
                                    TileManager.TileMain[x + i, y + j].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                                else
                                    TileManager.TileMain[x + i, y + j].TileStyle.GetComponent<SpriteRenderer>().color = new Color(1.000f, 0.020f, 0.020f, 1.000f);
                            }
                        }
                    }
                }
            }
        }
    }

    public override void SetPoint(int x, int y)
    {
        pointList.Clear();
        for (int i = 0; i < 5; i++ )
        {
            for (int j = 0;  j < 5; j++)
            {
                pointList.Add(new Vector2(x + i, y + j));
            }
        }
    }
}