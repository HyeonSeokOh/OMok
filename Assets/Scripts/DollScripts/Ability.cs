using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected int DollHp; //건설 돌의 체력
    protected int dollTag;
    public int MaxHp;

    protected struct mortage
    {
        public int mortage_Hp;
        public int mortage_Turn;
    }

    protected int DollAttribute; //건설 돌의 속성값
    protected int DollCost; //돌들의 코스트 값
    protected int mortgageHp = 0; // 저당잡힌 체력
    protected List<mortage> mortageList = new List<mortage>();

    protected int Shield_Time;
    protected bool smallShield = false; // 스킬로 부여된 실드
    protected bool smallAmplify = false;    // 스킬로 피해량 증폭시킴
    protected bool amplify = false;    // 피해량 증폭(돌 능력)

    protected int amplify_Val = 1;
    protected int shield_Val = 1;
    protected int Create_turn;
    protected int x;
    protected int y;

    protected struct Point
    {
        public int x;
        public int y;
        public Point(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
    }

    public void MinusHp(int damage)
    {
        if (!smallShield && Shield_Time <= 0)   // 실드가 없으면 데미지를 입음
        {
            if (smallAmplify || amplify)    // 피해량 증폭시킴
            {
                DollHp -= (damage * 2);
                if (smallAmplify)    // 이거때문이면 이제 지움
                    smallAmplify = false;
            }
            else
            {
                DollHp -= damage;
            }
        }
        else if (smallShield)
            smallShield = false;

        return;
    }

    public void MinusHp(int damage, int life)
    {
        if (DollHp - damage <= 0)
            DollHp = 1;
        else
            DollHp -= damage;
    }

    public void PlusHp(int Heel)
    {
        DollHp += Heel; // 체력 회복
    }

    protected void die()
    {

        if (DollHp <= 0)
        {
            Collider2D hit2 = Physics2D.OverlapBox(transform.position, new Vector2(0.5f, 0.5f), 0);
            //transform.position에서 사이즈 (0.5,0.5)에 회전안한(0) 상자에 충돌한 콜라이더를 반환한다

            if (hit2) //범위를 지정
            {
                int x = Mathf.RoundToInt(hit2.transform.position.x);
                int y = Mathf.RoundToInt(hit2.transform.position.y);

                TileManager.TileMain[x, y].RullTile = 0;
                
            }
            AudioManager.Instance.DollSt(1);
            Destroy(this.gameObject);
        }

    }
	public void onoffBigAmplify(bool select)
	{
		amplify = select;
	}
    public void onoffShield()
    {
        smallShield = true;
    }

    public void onoffamplify()
    {
        smallAmplify = true;
    }

    public bool GetSmallShield()
    {
        return smallShield;
    }

    public bool GetSmallAmplify()
    {
        return smallAmplify;
    }

    public bool GetonoffBigAmplify()
    {
        return amplify;
    }

    public int GetMortgageHp()
    {
        return mortgageHp;
    }

    public int GetHp()
    {
        return DollHp;
    }

    public int GetDollTag()
    {
        return dollTag;
    }

    public void SetMortgageHp(int mortgage)
    {
        mortgageHp = mortgage;
    }
    public void AddMortage(int mortagehp)
    {
        mortage mortage_new;

        mortage_new.mortage_Hp = mortagehp;
        mortage_new.mortage_Turn = GameManager.Instance.turn + 8;

        PlusHp(GetHp());

        mortageList.Add(mortage_new);
    }

    public int GetMortageHp(int deadline_Turn)
    {
        int j = 0;

        for (int i = 0; i < mortageList.Count; i ++)
        {
            if (mortageList[i].mortage_Turn == deadline_Turn)
            {
                j = mortageList[i].mortage_Hp;
            }
        }

        return j;
    }

    public void RemoveMortage(int deadline_Turn)
    {
        for (int i = 0; i < mortageList.Count; i++)
        {
            if (mortageList[i].mortage_Turn == deadline_Turn)
            {
                MinusHp(mortageList[i].mortage_Hp, 1);
                mortageList.Remove(mortageList[i]);
            }
        }
    }

    void Update()
    {
    }

    protected bool indexRange(int x, int y)   // 이거는 배열의 인덱스를 넘어가는지 아닌지 확인합니다.
    {
        if (x >= 0 && x <= 9 && y >= 0 && y <= 9)
            return true;
        return false;
    }


    
}
