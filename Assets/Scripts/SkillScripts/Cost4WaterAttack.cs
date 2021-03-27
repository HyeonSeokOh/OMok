using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cost4WaterAttack : Skills //9번 보조 아이템
{
    GameObject aa;
    int x;
    int y;

    Point[] dollDir = new Point[4] { new Point( 0 , 1 ),
                                     new Point( -1, 0 ),
                                     new Point( 1, 0 ),
                                     new Point( 0, -1 ) };
    void Start()
    {
        SkillCost = 4; //코스트
        myTag = Convert.ToInt32(this.gameObject.tag);
        number = 1009;
        thisWeight = -125;
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
            x = DollMove.Instance.Xup;
            y = DollMove.Instance.Yup;

            if (TileManager.TileMain != null)
            {
                if (indexRange(x, y) && TileManager.TileMain[x, y].TileAttribute == 3)
                {
                    Find_Water(x, y);
                }
            }
        }
    }
    int value = 0;

    void Find_Water(int x, int y)   // 물범위. 이거는 공부합시다.
    {
        for (int i = 0; i < 4; i++)
        {
            if (indexRange(x + dollDir[i].x, y + dollDir[i].y))
            {
                if (TileManager.TileMain[x + dollDir[i].x, y + dollDir[i].y].TileAttribute == 3 &&
                    TileManager.TileMain[x + dollDir[i].x, y + dollDir[i].y].TileStyle.GetComponent<SpriteRenderer>().color != Color.red)
                {
                    TileManager.TileMain[x + dollDir[i].x, y + dollDir[i].y].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                    value++;
                    Find_Water(x + dollDir[i].x, y + dollDir[i].y);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        aa = collision.gameObject;
    }


    public override void SetPoint(int x, int y)
    {
        pointList.Clear();
        if (TileManager.TileMain != null)
        {
            if (indexRange(x, y) && TileManager.TileMain[x, y].TileAttribute == 3)
            {
                Find_Weight(x, y);
            }
        }
    }

    void Find_Weight(int x, int y)
    {
        for (int i = 0; i < 4; i++)
        {
            if (indexRange(x + dollDir[i].x, y + dollDir[i].y))
            {
                if (TileManager.TileMain[x + dollDir[i].x, y + dollDir[i].y].TileAttribute == 3 && !pointList.Contains(new Vector2(x, y)))
                    pointList.Add(new Vector2(x, y));
            }
        }
    }
}