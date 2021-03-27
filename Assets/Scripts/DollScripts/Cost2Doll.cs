using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cost2Doll : Ability
{
    public GameObject[] effect_Obj = new GameObject[4];
    GameObject ins;
    int R;
    bool isEffect;

    void Start()
    {
        effect_Obj[0] = Resources.Load("Doll Effect/Cost_2_Doll_Effect") as GameObject;
        effect_Obj[1] = Resources.Load("Doll Effect/Cost_2_Doll_Effect011") as GameObject;
        effect_Obj[2] = Resources.Load("Doll Effect/Cost_2_Doll_Effect022") as GameObject;
        effect_Obj[3] = Resources.Load("Doll Effect/Cost_2_Doll_Effect03") as GameObject;
        DollCost = 2;
        DollHp = 20;
        isEffect = false;
        R = Random.Range(1, 5);
        dollTag = 2;
        MaxHp = DollHp;
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
            //int x = (int)(this.transform.position.x);
            //int y = (int)(this.transform.position.y);
            //Debug.Log(x);
            //Debug.Log(y);
            if (this.tag == "BlackCost02")
            {
                TileManager.TileMain[x, y].RullTile = 1;
            }
            else
                TileManager.TileMain[x, y].RullTile = 2;


            if (TileManager.Instance != null)
            {
                if (GameManager.Instance.mainTurn != 0 && DollMove.Instance != null && !isEffect)   // 한턴 지나면
                {   // 0은 기본 // 1은 흙 // 2는 숲 // 3은 물 // 4는 얼음
                    if (indexRange(x, y))
                    {
                        if (TileManager.TileMain[x, y].TileAttribute == R)
                        {
                            switch (R)
                            {
                                case 1: ins = Instantiate(effect_Obj[1], new Vector2(x, y), Quaternion.identity) as GameObject; break;  // 흙
                                case 2: ins = Instantiate(effect_Obj[0], new Vector2(x, y), Quaternion.identity) as GameObject; break;  // 숲
                                case 3: ins = Instantiate(effect_Obj[3], new Vector2(x, y), Quaternion.identity) as GameObject; break;  // 물
                                case 4: ins = Instantiate(effect_Obj[2], new Vector2(x, y), Quaternion.identity) as GameObject; break;  // 얼음
                            }
                            PlusHp(10);
                        }
                    }
                    isEffect = true;
                }
            }
        }

        die();
    }
}
