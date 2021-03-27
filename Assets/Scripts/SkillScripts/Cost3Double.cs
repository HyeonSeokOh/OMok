using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class Cost3Double : Skills //6번 보조 아이템
{
    int x;
    int y;
    public GameObject aa;

    void Start()
    {
        SkillCost = 3; //코스트
        myTag = Convert.ToInt32(this.gameObject.tag);
        number = 1006;
        thisWeight = 150;
    }
    public override void Init()
    {
        Start();
    }
    void Update()
    {
        //int TurnPlus = TurnManager.Instance.GameTurn;
        ChangeAlpha();
        if (GameManager.Instance.Dragging && DollMove.Instance.move_Object_Tag == myTag)
        {
            x = DollMove.Instance.Xup;
            y = DollMove.Instance.Yup;

            if (indexRange(x, y))
            {
                if (TileManager.TileMain != null)
                    TileManager.TileMain[x, y].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
    public override void SetPoint(int x, int y)
    {
        pointList.Clear();
        pointList.Add(new Vector2(x, y));
    }
}
