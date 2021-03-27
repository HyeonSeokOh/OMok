using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    protected int SkillCost;
    protected int myTag;
    protected int number;   //스킬 넘버
    protected int randomAtk;

    protected int thisWeight;

    protected struct Point    // 포인트 구조체
    {
        public int x;
        public int y;
        public Point(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
    }

    protected List<Vector2> pointList = new List<Vector2>();  // 공격할 좌표 저장

    public int GetSkillCost()
    {
        return SkillCost;
    }

    protected bool indexRange(int x, int y)   // 이거는 배열의 인덱스를 넘어가는지 아닌지 확인합니다.
    {
        if (x >= 0 && x <= 9 && y >= 0 && y <= 9)
            return true;
        return false;
    }

    protected void ChangeAlpha() // 알파값 변경 함수
    {
            if(GameManager.Instance.MainCost >= SkillCost)
                this.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 255, 1f);
            else 
                this.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 255, 0.5f);
    }

    public int skillRange(int x, int y, int enemy, int mine)    // 가중치 계산. 흑돌을 공격하면 mine을 더하고 백돌을 공격하면 enemy를 더함
    {
        int wei = 0;
        SetPoint(x, y);
        
        for (int i = 0; i < pointList.Count; i++)
        {
            //Debug.Log(pointList[i]);
            int xx = Mathf.FloorToInt(pointList[i].x);
            int yy = Mathf.FloorToInt(pointList[i].y);
            //Debug.Log(pointList[i]);
            if (indexRange(xx, yy))
            {
                if (TileManager.TileMain[xx, yy].RullTile == 1)   // 흑돌
                    wei = wei + mine;
                else if (TileManager.TileMain[xx, yy].RullTile == 2)   // 백돌
                    wei = wei + enemy;

                if (TileManager.TileMain[xx, yy].defensePoint == TileManager.attack_defense_Point.ONE) wei = wei + ((mine * 2) / 5);
                else if (TileManager.TileMain[xx, yy].defensePoint == TileManager.attack_defense_Point.TWO) wei = wei + ((mine * 3) / 5);
                else if (TileManager.TileMain[xx, yy].defensePoint == TileManager.attack_defense_Point.THREE) wei = wei + ((mine * 4) / 5);

                if (TileManager.TileMain[xx, yy].attackPoint == TileManager.attack_defense_Point.ONE) wei = wei + ((enemy * 2) / 5);
                else if (TileManager.TileMain[xx, yy].attackPoint == TileManager.attack_defense_Point.TWO) wei = wei + ((enemy * 3) / 5);
                else if (TileManager.TileMain[xx, yy].attackPoint == TileManager.attack_defense_Point.THREE) wei = wei + ((enemy * 4) / 5);
            }
            //Debug.Log(wei);
        }
        return wei;
    }

    public virtual void SetPoint(int x, int y)
    {
        return;
    }
    
    public virtual void Init()
    {

    }

    public List<Vector2> GetPoint()
    {
        return pointList;
    }

    public int getSkillNumber()
    {
        return number;
    }

    public int GetSkillWeight()
    {
        return thisWeight;
    }
}
