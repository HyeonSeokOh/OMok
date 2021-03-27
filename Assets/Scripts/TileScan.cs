using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TileScan : MonoBehaviour
{
    private static TileScan instance_;
    public static TileScan Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<TileScan>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("MapScan").AddComponent<TileScan>();
                    instance_ = newSingleton;
                }
            }
            return instance_;
        }
        private set
        {
            instance_ = value;
        }
    }
    
    private void Awake()
    {
        var objs = FindObjectsOfType<TileScan>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    struct Point    // 포인트 구조체
    {
        public int x;
        public int y;
        public Point(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
    }

    // 방향을 정해줄 배열
    Point[] dirArray = new Point[4] { new Point ( 1, 0 ), new Point(1, -1), new Point(0, -1), new Point(-1, -1) };
    // 8방향에 가중치를 더함
    Point[] plusWeightDir = new Point[9] {
        new Point (1 , 0), new Point(1 , -1), new Point (0 , -1), new Point(-1 , -1),
        new Point (-1 , 0), new Point(-1 , 1), new Point (0 , 1), new Point(1 , 1),
        new Point (0, 0)};
    //어느 좌표로 몇목인지를 구하기 위함
    Point[] tilepoint = new Point[5];

 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void MapScanning()
    {
        for (int i = 0; i < TileManager.Instance.BoardSizeX; i++)
        {
            for (int j = 0; j < TileManager.Instance.BoardSizeY; j++)
            {
                //가중치 초기화
                if (indexRange(i, j))
                {
                    TileManager.TileMain[i, j].weight = 0;
                    TileManager.TileMain[i, j].attackPoint = TileManager.attack_defense_Point.ZERO;
                    TileManager.TileMain[i, j].defensePoint = TileManager.attack_defense_Point.ZERO;
                }
            }
        }
        
        for (int i = 0; i < TileManager.Instance.BoardSizeX; i++)
        {
            for (int j = 0; j < TileManager.Instance.BoardSizeY; j++)
            {
                //오목알의 색깔 확인
                int dollColor = TileManager.TileMain[i, j].RullTile;
                if (dollColor == 0) continue;   // 오목알이 없으면 넘김

                //오목알의 combo 확인
                ComboScan(dollColor, i, j);
            }
        }

        for (int i = 0; i < TileManager.Instance.BoardSizeX; i++)
        {
            for (int j = 0; j < TileManager.Instance.BoardSizeY; j++)
            {
                int dollColor = TileManager.TileMain[i, j].RullTile;
                if (dollColor == 0) continue;   // 오목알이 없으면 넘김

                //combo와 색깔에 맞게 가중치 주기
                PlusWeight(dollColor, i, j);
            }
        }

        weight.Instance.detone();
    }

    void PlusWeight(int dollColor, int TileX, int TileY)    // 돌색깔과 현재 x,y 좌표 가져옴
    {
        for (int i = 0; i < 8; i++) // 8방향에 가중치 추가
        {   // 범위 내
            if (TileX + plusWeightDir[i].x < TileManager.Instance.BoardSizeX && TileX + plusWeightDir[i].x >= 0 && TileY + plusWeightDir[i].y < TileManager.Instance.BoardSizeY && TileY + plusWeightDir[i].y >= 0)
            {   // 돌이 놓여져 있으면 안됨
                if (TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].RullTile == 0)
                {
                    if (dollColor == 1) // 적의 돌(흑돌)이면 가중치로 -1 추가
                        TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].weight--;
                    else if (dollColor == 2)    // 나의 돌(백돌)이면 가중치로 +1 추가
                        TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].weight++;
                }
            }
        }

        // 돌이 놓여져 있어도 일단 공격, 방어 포인트를 줌 (다른쪽에서 돌이 놓여져 있는지 여부를 검사)
        for (int i = 0; i < 4; i++) // 공격과 방어포인트 정하기 
        {
            int com = TileManager.TileMain[TileX, TileY].combo[i];
            int bPoint = 0;

            //경우에 따라 얼만큼의 포인트를 줄 것인지 결정
            if (com == 0 || com == 11 || com == 12 || com == 22 || com == 21 || com == 23 || com == 24) { bPoint = 0; }
            else if (com == 1 || com == 2 || com == 13) { bPoint = 1; }
            else if (com == 3 || com == 14 || com == 4) { bPoint = 2; }

            //string str = TileX + " , " + TileY + ",,," + TileManager.TileMain[TileX, TileY].combo[i];
            //Debug.Log(str);

            if (dollColor == 1) // 적의 돌(흑돌)이면 방어 포인트를 쌓음
            {
                Defense_Point(i, bPoint, TileX, TileY);
                Defense_Point(i + 4, bPoint, TileX, TileY);
                Defense_Point(8, bPoint, TileX, TileY);
            }
            else if (dollColor == 2)    // 나의 돌(백돌)이면 공격 포인트를 쌓음
            {
                Attack_Point(i, bPoint, TileX, TileY);
                Attack_Point(i + 4, bPoint, TileX, TileY);
                Attack_Point(8, bPoint, TileX, TileY);
            }
        }


    }

    void Attack_Point(int i, int point, int TileX, int TileY)   // 공격포인트 쌓기
    {
        if (TileX + plusWeightDir[i].x < TileManager.Instance.BoardSizeX && TileX + plusWeightDir[i].x >= 0 && TileY + plusWeightDir[i].y < TileManager.Instance.BoardSizeY && TileY + plusWeightDir[i].y >= 0)
        {
            switch(point)
            {
                case 0:
                    if (TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].attackPoint == TileManager.attack_defense_Point.ZERO)
                        TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].attackPoint = TileManager.attack_defense_Point.ZERO;
                    break;  
                case 1:
                    if (TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].attackPoint != TileManager.attack_defense_Point.TWO)
                        TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].attackPoint = TileManager.attack_defense_Point.ONE;
                    break;
                case 2:
                    TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].attackPoint = TileManager.attack_defense_Point.TWO;
                    break;
            }
        }
    }

    void Defense_Point(int i, int point, int TileX, int TileY)  // 방어 포인트 쌓기
    {
        if (TileX + plusWeightDir[i].x < TileManager.Instance.BoardSizeX && TileX + plusWeightDir[i].x >= 0 && TileY + plusWeightDir[i].y < TileManager.Instance.BoardSizeY && TileY + plusWeightDir[i].y >= 0)
        {
            switch (point)
            {
                case 0:
                    if (TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].defensePoint == TileManager.attack_defense_Point.ZERO)
                        TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].defensePoint = TileManager.attack_defense_Point.ZERO;
                    break;
                case 1:
                    if (TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].defensePoint != TileManager.attack_defense_Point.TWO)
                        TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].defensePoint = TileManager.attack_defense_Point.ONE;
                    break;
                case 2:
                    TileManager.TileMain[TileX + plusWeightDir[i].x, TileY + plusWeightDir[i].y].defensePoint = TileManager.attack_defense_Point.TWO;
                    break;
            }
        }
    }

    void ComboScan(int dollColor, int TileX, int TileY) // 돌색깔과 현재 x,y 좌표 가져옴
    {
        tilepoint[0].x = TileX;
        tilepoint[0].y = TileY;

        for (int i = 0; i < 4; i++)
        {
            int compoint = 0;   // 몇 목이고, 시작과 끝 방향이 막혔는지 알려줌
            int comboCnt = 0;   // 몇 목(comboCnt + 1)인지 확인해줌
            int life = 1;   // 한칸 정돈 떨어져 있어도 됨
            Point blank;   // 빈공간의 좌표 저장
            bool isLoop = true; // 돌이 있을 때까지 루프 돌리기

            blank.x = -1; blank.y = -1;

            //이미 검사한 방향 스킵
            if ((tilepoint[0].x - dirArray[i].x >= 0 && tilepoint[0].x - dirArray[i].x  < TileManager.Instance.BoardSizeX && tilepoint[0].y - dirArray[i].y < TileManager.Instance.BoardSizeY) // 범위 안
                && (TileManager.TileMain[tilepoint[0].x - dirArray[i].x, tilepoint[0].y - dirArray[i].y].RullTile == dollColor)) // 이전번에 이미 검사한 방향이면 스킵함
                    continue;

            while (isLoop)  // 방향에 따라 몇 목인지 검사
            {   // TileX, TileY좌표가 테두리인지 확인. 그렇다면 막힘포인트 추가
                if (TileX + dirArray[i].x >= TileManager.Instance.BoardSizeX || TileX + dirArray[i].x < 0 || TileY + dirArray[i].y < 0)  // 판에 끝자락에 오면 그 즉시 막힘 포인트(+10) 추가 후 루프문 종료
                {
                    compoint = compoint + 10;
                    isLoop = false;
                }
                else if (TileManager.TileMain[TileX + dirArray[i].x, TileY + dirArray[i].y].RullTile == dollColor)   // 같은색이면 계속 그 방향으로 돌아감
                {
                    TileX = TileX + dirArray[i].x;
                    TileY = TileY + dirArray[i].y;
                    comboCnt++;
                    if (comboCnt == 4)  // 다섯번째 돌이 들어서는 순간
                    {
                        if (life == 0)  // 한번 뚫린 곳이 있었고, 다섯번째 돌이 들어온다는 건 5목 이상의 목이 될 수 있으므로 빈공간이 있는 곳까지 다시 돌아감
                        {
                            //comboCnt = blankCom - 1;
                            break;
                        }
                    }
                    tilepoint[comboCnt].x = TileX;
                    tilepoint[comboCnt].y = TileY;
                }
                else if (TileManager.TileMain[TileX + dirArray[i].x, TileY + dirArray[i].y].RullTile == 0)  // 한번까지는 건너 뛰어도 됨
                {
                    life--;
                }
                else if (TileManager.TileMain[TileX + dirArray[i].x, TileY + dirArray[i].y].RullTile != dollColor) // 다른 색이면 막힘 포인트(+10) 추가
                {
                    compoint = compoint + 10;
                    isLoop = false;
                }

                if (life == -1) // 두번 건너 뛰면 루프문 탈출
                    isLoop = false;
            };

            compoint = compoint +(comboCnt+ 1);

            // 반대편에도 막힘이 있는지 확인
            if ( (tilepoint[0].x - dirArray[i].x < 0 || tilepoint[0].x - dirArray[i].x >= TileManager.Instance.BoardSizeX || tilepoint[0].y - dirArray[i].y >= TileManager.Instance.BoardSizeY) ||
                (TileManager.TileMain[tilepoint[0].x - dirArray[i].x, tilepoint[0].y - dirArray[i].y].RullTile != 0  
                && TileManager.TileMain[tilepoint[0].x - dirArray[i].x, tilepoint[0].y - dirArray[i].y].RullTile != dollColor))
                compoint = compoint + 10;   // 있으면 막힘 포인트(+10) 추가

            for (int j = 0; j < comboCnt+1; j++)
            {
                TileManager.TileMain[tilepoint[j].x, tilepoint[j].y].combo[i] = compoint;   // combo[i]는 방향에 따른 목과 막힘정도
            }

        }


    }

    bool indexRange(int x, int y)   // 이거는 배열의 인덱스를 넘어가는지 아닌지 확인합니다.
    {
        if (x >= 0 && x < TileManager.Instance.BoardSizeX && y >= 0 && y < TileManager.Instance.BoardSizeY)
            return true;
        return false;
    }
}
