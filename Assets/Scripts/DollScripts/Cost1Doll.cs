using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cost1Doll : Ability
{
    void Start()
    {
        DollHp = 10;
        DollCost = 1;
        dollTag = 1;
        MaxHp = DollHp;
        Create_turn = GameManager.Instance.turn;
    }

    void Update()
    {
        if (DollHp > MaxHp) { MaxHp = DollHp; }
        Collider2D hit2 = Physics2D.OverlapBox(transform.position, new Vector2(0.5f, 0.5f), 0);
        //transform.position에서 사이즈 (0.5,0.5)에 회전안한(0) 상자에 충돌한 콜라이더를 반환한다

        if (hit2) //범위를 지정
        {
            int x = Mathf.RoundToInt(hit2.transform.position.x);
            int y = Mathf.RoundToInt(hit2.transform.position.y);

            if (indexRange(x, y))
            {
                if (this.tag == "BlackCost01")
                {
                    TileManager.TileMain[x, y].RullTile = 1;
                }
                else
                    TileManager.TileMain[x, y].RullTile = 2;
            }
            die();							
        }
    }
}
