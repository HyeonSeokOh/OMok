﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using System;

public class Cost3Mu : Skills
{
    int x;
    int y;

    void Start()
    {
        SkillCost = 3; //코스트
        myTag = Convert.ToInt32(this.gameObject.tag);
        number = 1007;
        thisWeight = 125;
    }
    public override void Init()
    {
        Start();
    }
    void Update()
    {
        int TurnPlus = TurnManager.Instance.mainTurn;
        ChangeAlpha();
        if (GameManager.Instance.Dragging && DollMove.Instance.move_Object_Tag == myTag)
        {
            x = DollMove.Instance.Xup;
            y = DollMove.Instance.Yup;

            if (indexRange(x, y))
            {
                if (TileManager.TileMain != null)
                {
                    if (indexRange(x - 1, y))
                    {
                        TileManager.TileMain[x - 1, y].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    if (indexRange(x + 1, y))
                    {
                        TileManager.TileMain[x + 1, y].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
            }
        }
    }

    public override void SetPoint(int x, int y)
    {
        pointList.Clear();
        pointList.Add(new Vector2(x + 1, y));
        pointList.Add(new Vector2(x - 1, y));
    }
}