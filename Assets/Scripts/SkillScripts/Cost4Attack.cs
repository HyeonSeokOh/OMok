using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;

public class Cost4Attack : Skills
{
    int x;
    int y;
    int a;
    int b;

    void Start()
    {
        SkillCost = 4; //코스트
        myTag = Convert.ToInt32(this.gameObject.tag);
        number = 1008;
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
            int i, j;


            if (indexRange(x, y))
            {
                if (TileManager.TileMain != null)
                {
                    for (i = 0; i < TileManager.Instance.BoardSizeX; i++)
                    {
                        if (indexRange(x + i, y))
                            TileManager.TileMain[x + i, y].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                        if (indexRange(x - i, y))
                            TileManager.TileMain[x - i, y].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;

                        for (j = 0; j < TileManager.Instance.BoardSizeY; j++)
                        {
                            if (indexRange(x, y + j))
                                TileManager.TileMain[x, y + j].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                            if (indexRange(x, y - j))
                                TileManager.TileMain[x, y - j].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                        }
                    }
                }

            }
        }
    }
    public override void SetPoint(int x, int y)
    {
        pointList.Clear();

        for (int i = 0; i < TileManager.Instance.BoardSizeX; i++ )
            pointList.Add(new Vector2(i, y));

        pointList.Remove(new Vector2(x, y));

        for (int i = 0; i < TileManager.Instance.BoardSizeY; i++)
            pointList.Add(new Vector2(x, i));
    }
}
