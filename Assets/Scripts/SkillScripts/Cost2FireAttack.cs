using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cost2FireAttack : Skills //4번 보조 아이템
{
    int x;
    int y;

    void Start()
    {
        SkillCost = 2; //코스트
        myTag = System.Convert.ToInt32(this.gameObject.tag);
        number = 1004;
        thisWeight = -50;
    }

    public override void Init()
    {
        Start();
    }

    void Update()
    {
        //Debug.Log("a");
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
                    if (indexRange(x + 1, y + 1))
                    {
                        TileManager.TileMain[x + 1, y + 1].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    if (indexRange(x + 1, y - 1))
                    {
                        TileManager.TileMain[x + 1, y - 1].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    if (indexRange(x - 1, y + 1))
                    {
                        TileManager.TileMain[x - 1, y + 1].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    if (indexRange(x - 1, y - 1))
                    {
                        TileManager.TileMain[x - 1, y - 1].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
            }
        }
    }

    public override void SetPoint(int x, int y)
    {
        pointList.Clear();
        pointList.Add(new Vector2(x + 1, y + 1));
        pointList.Add(new Vector2(x + 1, y - 1));
        pointList.Add(new Vector2(x - 1, y + 1));
        pointList.Add(new Vector2(x - 1, y - 1));
    }
}