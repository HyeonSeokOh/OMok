using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Cost4Doll : Ability
{
    int shield_Gage = -2;
    bool whatcolor;
    public GameObject shield_Obj;
    public GameObject ins;
    int deadline;

    Ability.Point[] dollDir = new Point[8] { new Point( -1 , 1 ),
                                            new Point( 0 , 1 ),
                                            new Point( 1, 1 ),
                                            new Point( -1, 0 ),
                                            new Point( 1, 0 ),
                                            new Point( -1, -1 ),
                                            new Point( 0, -1 ),
                                            new Point( 1, -1 ) };

    void Start()
    {
        if (this.tag == "BlackCost04")
            whatcolor = true;   // 검정 돌일떄
        else
            whatcolor = false;  // 흰 돌일때

        DollCost = 4;
        DollHp = 40;

        shield_Obj = Resources.Load("Doll Effect/Cost_4_Doll_Shield_Effect") as GameObject;

        MaxHp = DollHp;

        dollTag = 4;

        Create_turn = GameManager.Instance.turn;

        //Shield_Time = -1000;
    }

    // Update is called once per frame
    void Update()
    {
        if (DollHp > MaxHp) { MaxHp = DollHp; }
        if (Shield_Time != null)
            Shield_Time = deadline - GameManager.Instance.turn;

        Collider2D hit2 = Physics2D.OverlapBox(transform.position, new Vector2(0.5f, 0.5f), 0);
        //transform.position에서 사이즈 (0.5,0.5)에 회전안한(0) 상자에 충돌한 콜라이더를 반환한다

        if (hit2) //범위를 지정
        {
            int x = Mathf.RoundToInt(hit2.transform.position.x);
            int y = Mathf.RoundToInt(hit2.transform.position.y);

            if (this.tag == "BlackCost04")
            {
                TileManager.TileMain[x, y].RullTile = 1;
            }
            else
                TileManager.TileMain[x, y].RullTile = 2;


            // 실드를 생성합니다.
            if (shield_Gage == -2)
            {
                shield_Gage = 0;
                for (int i = 0; i < dollDir.Length; i++)
                {
                    if (indexRange(x + dollDir[i].x, y + dollDir[i].y))  // 범위 확인
                    {
                        if ((TileManager.TileMain[x + dollDir[i].x, y + dollDir[i].y].RullTile == 1 && whatcolor == true) ||
                            TileManager.TileMain[x + dollDir[i].x, y + dollDir[i].y].RullTile == 2 && whatcolor == false)
                        {
                            shield_Gage++;  // 같은색 돌잇으면 실드 게이지 증가
                        }
                    }
                }
                Shield_Time = shield_Gage * 4;
                if (Shield_Time > 0)
                    ins = Instantiate(shield_Obj, new Vector2(x, y), Quaternion.identity) as GameObject;
                
                deadline = GameManager.Instance.turn + Shield_Time;
            }

            if (Shield_Time == 0)
            {
                if (ins != null)
                    Destroy(ins.gameObject);
            }

            die();
        }
    }
}
