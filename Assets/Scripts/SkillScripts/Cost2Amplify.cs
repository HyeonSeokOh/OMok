using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cost2Amplify : Skills
{
    int x;
    int y;

    void Start()
    {
        SkillCost = 2; //코스트

        myTag = System.Convert.ToInt32(this.gameObject.tag);
        number = 1003;
        thisWeight = -75;
    }

    public override void Init()
    {
        Start();
    }

    // Update is called once per frame
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
