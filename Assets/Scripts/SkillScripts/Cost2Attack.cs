using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cost2Attack : Skills //5번 보조 아이템
{
    int x;
    int y;

    void Start()
    {
        SkillCost = 2; //코스트
        myTag = System.Convert.ToInt32(this.tag);
        number = 1005;
        thisWeight = -50;
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
            if (indexRange(x, y))
            {
                if (TileManager.TileMain != null)
                {
                    if (indexRange(x, y))
                    {
                        TileManager.TileMain[x, y].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    if (indexRange(x + 1, y))
                    {
                        TileManager.TileMain[x + 1, y].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    if (indexRange(x, y + 1))
                    {
                        TileManager.TileMain[x, y + 1].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    if (indexRange(x + 1, y + 1))
                    {
                        TileManager.TileMain[x + 1, y + 1].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
            }
        }
    }

    public override void SetPoint(int x, int y)
    {
        pointList.Clear();
        pointList.Add(new Vector2(x, y));
        pointList.Add(new Vector2(x + 1, y));
        pointList.Add(new Vector2(x, y + 1));
        pointList.Add(new Vector2(x + 1, y + 1));
    }
}