using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cost3Doll : Ability //3코스트 돌
{
    int Hturn = 2;
    bool turnLock = true;
    public GameObject BoxaDoll;
    public GameObject cost1Doll;
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
        DollCost = 3;
        DollHp = 30;
        cost1Doll = Resources.Load("Doll Effect/Cost_3_Create_Effect0") as GameObject;
        MaxHp = DollHp;
        dollTag = 3;

        Create_turn = GameManager.Instance.turn;
    }

    // Update is called once per frame
    void Update()
    {
        if (DollHp > MaxHp) { MaxHp = DollHp; }

        Collider2D hit2 = Physics2D.OverlapBox(transform.position, new Vector2(0.5f, 0.5f), 0);
        //transform.position에서 사이즈 (0.5,0.5)에 회전안한(0) 상자에 충돌한 콜라이더를 반환한다

        if (hit2) //범위를 지정
        {
            int x = Mathf.RoundToInt(hit2.transform.position.x);
            int y = Mathf.RoundToInt(hit2.transform.position.y);

            if (this.tag == "BlackCost03")
            {
                TileManager.TileMain[x, y].RullTile = 1;
            }
            else
                TileManager.TileMain[x, y].RullTile = 2;

            if (GameManager.Instance.mainTurn == 4)
            {
                turnLock = false;
            }

            if (GameManager.Instance.mainTurn == 1 && !turnLock)
            {
                Hturn -= 1;
                turnLock = true;
            }


            if (Hturn == 0)
            {
                for (int i = 0; i < dollDir.Length; i++)
                {
                    if (indexRange(x + dollDir[i].x, y + dollDir[i].y))
                    {
                        if (TileManager.TileMain[x + dollDir[i].x, y + dollDir[i].y].RullTile == 0)
                        {
                            Hturn = 0;
                            break;
                        }
                    }
                    Hturn = -200;
                }
                while (Hturn == 0) // 빈자리 나올때까지 무한루프 돌리기
                {
                    int R = Random.Range(0, 8);
                    if (indexRange(x + dollDir[R].x, y + dollDir[R].y)) // 범위 내
                    {
                        if (TileManager.TileMain[x + dollDir[R].x, y + dollDir[R].y].RullTile == 0)
                        {
                            BoxaDoll = Instantiate(cost1Doll, new Vector2(x + dollDir[R].x, y + dollDir[R].y), Quaternion.identity) as GameObject;
                            if (this.tag == "BlackCost03")
                            {
                                BoxaDoll.GetComponent<Cost3Doll_01>().WhatColor(true);
                                TileManager.TileMain[x + dollDir[R].x, y + dollDir[R].y].RullTile = 1;
                                //Debug.Log("a");
                            }
                            else
                            {
                                BoxaDoll.GetComponent<Cost3Doll_01>().WhatColor(false);
                                TileManager.TileMain[x + dollDir[R].x, y + dollDir[R].y].RullTile = 2;
                                //Debug.Log("b");
                            }
                            Hturn = 2;
                            break;
                        }
                    }
                }
            }
        }

        die();
    }
}