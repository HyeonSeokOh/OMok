using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cost5Doll : Ability
{
    public GameObject amplify_Effect;
    GameObject k;
    public List <GameObject> ins = new List<GameObject>();
    bool isLock = false;
    void Start()
    {
        DollCost = 5;
        DollHp = 150;
        MaxHp = DollHp;
        amplify_Effect = Resources.Load("Doll Effect/Cost_5_Doll_Amplify_Effect") as GameObject;

        dollTag = 5;

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

            if (this.tag == "BlackCost05")
            {
                TileManager.TileMain[x, y].RullTile = 1;
            }
            else
                TileManager.TileMain[x, y].RullTile = 2;

            if (!isLock)
            {
                for (int i = -2; i < 3; i++)
                {
                    for (int j = -2; j < 3; j++)
                    {
                        if (indexRange(x + i, y + j))
                        {
                                Debug.Log("a");
                                k = Instantiate(amplify_Effect, new Vector2(x + i, y + j), Quaternion.identity) as GameObject;
                                ins.Add(k);
                            Debug.Log(k.GetComponent<SpriteRenderer>().sprite);
                        }
                    }
                }
                isLock = true;
            }
        }

        if (DollHp <= 0)
        {
            foreach (GameObject obj in ins)
            {
                obj.gameObject.GetComponent<Cost5Doll01>().dead();
            }
            //ins.Clear();
            die();
        }

    }
}
