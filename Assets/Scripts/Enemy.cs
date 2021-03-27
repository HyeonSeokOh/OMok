using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static Enemy instance_;
    public static Enemy Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<Enemy>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("Enemy").AddComponent<Enemy>();
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
        var objs = FindObjectsOfType<Enemy>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
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

    List<Point> weightList = new List<Point>(); // 가중치가 젤 높은 좌표 리스트
    List<Point> atk3List = new List<Point>();   // 0순위 공격포인트 좌표 리스트
    List<Point> atk2List = new List<Point>();    // 1순위 공격포인트 좌표 리스트
    List<Point> atk1List = new List<Point>();    // 2순위 공격포인트 좌표 리스트
    List<Point> def3List = new List<Point>();   // 0순위 방어포인트 좌표 리스트
    List<Point> def2List = new List<Point>();    // 1순위 방어포인트 좌표 리스트 
    List<Point> def1List = new List<Point>();    // 2순위 방어포인트 좌표 리스트

    Point GodHand;

    int atk;    // 공격적
    int def;    // 방어적
    int myf;    // 마이페이스
    float time;

    int[] behavior = new int[3];
    GameObject[] whiteDoll = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        GodHand = new Point(-1, -1);
        atk = 50;
        def = 20;
        myf = 10;

        whiteDoll[0] = Resources.Load("WhiteCost01") as GameObject;
        whiteDoll[1] = Resources.Load("WhiteCost02") as GameObject;
        whiteDoll[2] = Resources.Load("WhiteCost03") as GameObject;
        whiteDoll[3] = Resources.Load("WhiteCost04") as GameObject;
        whiteDoll[4] = Resources.Load("WhiteCost05") as GameObject;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.mainTurn >= 3)
            time += Time.deltaTime;

        if (GameManager.Instance.mainTurn == 3 && time >= 2.5f)
        {
            MakeList(); // 놓을 좌표들 모으기
            MakePont(); // 놓을 좌표 고르기
            SetDoll();// 돌 놓기

            time = 0;
            TileScan2.Instance.MapScanning();
            //weight.Instance.detone();
            GameManager.Instance.mainTurn++;// 턴 바꾸기
            GameManager.Instance.turn++;
            // 스킬 놓기
        }


        if (GameManager.Instance.mainTurn == 4 && time >= 2.5f)
        {
            time = 0;

            if (GameManager.Instance.MainCost >= 1) // 후에 1을 가지고 있는 최소비용의 스킬의 비용으로 바꿔쓰기
                SkillWeight.Instance.skillWeight();// 스킬 놓을 좌표 고르기
            else
                GameManager.Instance.mainTurn = 5;// 턴 바꾸기

            TileScan2.Instance.MapScanning();

            //weight.Instance.detone();
        }

        if (GameManager.Instance.mainTurn == 5 && time >= 2.5f)
        {
            time = 0;

            TileScan2.Instance.MapScanning();
            //weight.Instance.detone();
            GameManager.Instance.turn++;

            GameManager.Instance.mainTurn = 1;
            GameManager.Instance.MainCost = 5;
            GameManager.Instance.BuilDollstate = false;

            weightList.Clear();

            atk1List.Clear();
            atk2List.Clear();
            atk3List.Clear();

            def1List.Clear();
            def2List.Clear();
            def3List.Clear();
        }
    }

    void SetDoll()
    {
        int ran = Random.Range(0, LoadGame.Instance.limit_Block);

        CreateWhitedoll(ran);
    }

    void MakeList()
    {
        int wei = 0;
        for (int i = 0; i < TileManager.Instance.BoardSizeX; i++)
        {
            for (int j = 0; j < TileManager.Instance.BoardSizeY; j++)
            {
                if (TileManager.TileMain[i, j].weight > wei)
                {
                    wei = TileManager.TileMain[i, j].weight;
                    weightList.Clear();
                }

                if (TileManager.TileMain[i, j].RullTile == 0)   // 빈곳만 찾기 
                {
                    if (TileManager.TileMain[i, j].weight != 0)
                    {
                        weightList.Add(new Point(i, j));    // 가중치가 젤 높은 좌표들만 리스트에 넣음, 백돌이 많은 곳에 두게 됨
                    }


                    if (TileManager.TileMain[i, j].attackPoint == TileManager.attack_defense_Point.ONE)
                    {
                        atk1List.Add(new Point(i, j));  // 2순위 공격포인트 좌표들만 리스트에 넣음
                    }
                    else if (TileManager.TileMain[i, j].attackPoint == TileManager.attack_defense_Point.TWO)
                    {
                        atk2List.Add(new Point(i, j));  // 1순위 공격포인트 좌표들만 리스트에 넣음
                    }
                    else if (TileManager.TileMain[i, j].attackPoint == TileManager.attack_defense_Point.THREE)
                    {
                        atk3List.Add(new Point(i, j));  // 0순위 공격포인트 좌표들만 리스트에 넣음
                    }

                    if (TileManager.TileMain[i, j].defensePoint == TileManager.attack_defense_Point.ONE)
                    {
                        def1List.Add(new Point(i, j));  // 2순위 방어포인트 좌표들만 리스트에 넣음
                    }
                    else if (TileManager.TileMain[i, j].defensePoint == TileManager.attack_defense_Point.TWO)
                    {
                        def2List.Add(new Point(i, j));  // 1순위 방어포인트 좌표들만 리스트에 넣음
                    }
                    else if (TileManager.TileMain[i, j].defensePoint == TileManager.attack_defense_Point.THREE)
                    {
                        def3List.Add(new Point(i, j));  // 0순위 방어포인트 좌표들만 리스트에 넣음
                    }
                }
            }
        }
    }

    void MakePont()
    { 
        // 0순위로 봐야하는 방어 포인트와 공격 포인트
        if (def3List.Count != 0)
        {
            int ran = Random.Range(0, def3List.Count);
            int rr = 0;
            while (TileManager.TileMain[def3List[ran].x, def3List[ran].y].RullTile != 0)
            {
                ran = Random.Range(0, def3List.Count);
                rr++;
                if (rr > 100) { Debug.Log("버그 여기"); break; }
            }

            GodHand = def3List[ran];

            return;
        }
        else if (atk3List.Count != 0)
        {
            int ran = Random.Range(0, atk3List.Count);
            int rr = 0;
            while (TileManager.TileMain[atk3List[ran].x, atk3List[ran].y].RullTile != 0)
            {
                ran = Random.Range(0, atk3List.Count);
                rr++;
                if (rr > 100) { Debug.Log("버그 여기"); break; }
            }

            GodHand = atk3List[ran];

            return;
        }

        // 1순위로 봐야하는 방어 포인트와 공격 포인트
        if (def2List.Count != 0)
        {
            int ran = Random.Range(0, def2List.Count);
            int rr = 0;
            while(TileManager.TileMain[def2List[ran].x,def2List[ran].y].RullTile != 0)
            {
                ran = Random.Range(0, def2List.Count);
                rr++;
                if (rr > 100) { Debug.Log("버그 여기"); break; }
            }

            GodHand = def2List[ran];

            return;
        }
        else if (atk2List.Count != 0)
        {
            int ran = Random.Range(0, atk2List.Count);
            int rr = 0;
            while (TileManager.TileMain[atk2List[ran].x, atk2List[ran].y].RullTile != 0)
            {
                ran = Random.Range(0, atk2List.Count);
                rr++;
                if (rr > 100) { Debug.Log("버그 여기"); break; }
            }

            GodHand = atk2List[ran];

            return;
        }

        // 2순위로 봐야하는 방어 포인트와 공격 포인트

        atk = 55 + Random.Range(0, 101);
        def = 25 + Random.Range(0, 101);
        myf = 5 + Random.Range(0, 101);

        if (def1List.Count != 0 && def >= atk && def >= myf) // 방어함
        {
            int ran = Random.Range(0, def1List.Count);
            int rr = 0;
            while (TileManager.TileMain[def1List[ran].x, def1List[ran].y].RullTile != 0)
            {
                ran = Random.Range(0, def1List.Count);
                rr++;
                if (rr > 100) { Debug.Log("버그 여기"); break; }
            }

            GodHand = def1List[ran];

            return;
        }
        else if (atk1List.Count != 0 && atk >= def && atk >= myf) // 공격함
        {
            int ran = Random.Range(0, atk1List.Count);
            int rr = 0;
            while (TileManager.TileMain[atk1List[ran].x, atk1List[ran].y].RullTile != 0)
            {
                ran = Random.Range(0, atk1List.Count);
                rr++;
                if (rr > 100) { Debug.Log("버그 여기"); break; }
            }

            GodHand = atk1List[ran];

            return;
        }
        else if (myf >= atk && myf >= def) // 꼴리는 대로 둠
        {
            int ranX = Random.Range(0, TileManager.Instance.BoardSizeX);
            int ranY = Random.Range(0, TileManager.Instance.BoardSizeY);
            int rr = 0;
            while (TileManager.TileMain[ranX, ranY].RullTile != 0)
            {
                ranX = Random.Range(0, TileManager.Instance.BoardSizeX);
                ranY = Random.Range(0, TileManager.Instance.BoardSizeY);
                rr++;
                if (rr > 100) { Debug.Log("버그 여기"); break; }
            }

            GodHand = new Point(ranX, ranY);

            return;
        }
        else if (weightList.Count != 0)   // 가중치가 높은 곳에 둠
        {
            int ran = Random.Range(0, weightList.Count);
            int rr = 0;
            while (TileManager.TileMain[weightList[ran].x, weightList[ran].y].RullTile != 0)
            {
                ran = Random.Range(0, weightList.Count);
                rr++;
                if (rr > 100) { Debug.Log("버그 여기"); break; }
            }

            GodHand = weightList[ran];

            return;
        }
        else
        {
            int ranX = Random.Range(0, TileManager.Instance.BoardSizeX);
            int ranY = Random.Range(0, TileManager.Instance.BoardSizeY);
            int rr = 0;
            while (TileManager.TileMain[ranX, ranY].RullTile != 0)
            {
                ranX = Random.Range(0, TileManager.Instance.BoardSizeX);
                ranY = Random.Range(0, TileManager.Instance.BoardSizeY);
                rr++;
                if (rr > 100) { Debug.Log("버그 여기"); break; }
            }

            GodHand = new Point(ranX, ranY);

            return;
        }
    }

    //백돌 생성 함수
    void CreateWhitedoll(int num)
    {
        if (GameManager.Instance.mainTurn == 3)
        {
            if (TileManager.TileMain[GodHand.x, GodHand.y].RullTile != 0) { return; }

            else if (TileManager.TileMain[GodHand.x, GodHand.y].RullTile == 0)
            {
                if (GameManager.Instance.BuilDollstate == false)
                {
                    switch (num)
                    {
                        case 0:
                            GameManager.Instance.MainCost -= 1;
                            Instantiate(whiteDoll[0], new Vector3(GodHand.x, GodHand.y, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case 1:
                            GameManager.Instance.MainCost -= 2;
                            Instantiate(whiteDoll[1], new Vector3(GodHand.x, GodHand.y, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case 2:
                            GameManager.Instance.MainCost -= 3;
                            Instantiate(whiteDoll[2], new Vector3(GodHand.x, GodHand.y, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case 3:
                            GameManager.Instance.MainCost -= 4;
                            Instantiate(whiteDoll[3], new Vector3(GodHand.x, GodHand.y, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case 4:
                            GameManager.Instance.MainCost -= 5;
                            Instantiate(whiteDoll[4], new Vector3(GodHand.x, GodHand.y, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                    }
                    AudioManager.Instance.DollSt(0);
                    GameManager.Instance.BuilDollstate = true;  // 돌 더 놓지 못도로 막음.
                }
            }
        }
        else if (GameManager.Instance.mainTurn != 3)
        {
            // 중복 돌놓기 금지 텍스트
        }
    }

    //이펙트 표현 함수
    void ShowTileEffect()
    {
        if (TileManager.TileMain[GodHand.x, GodHand.y].RullTile == 0)
        {
            if (TileManager.TileMain[GodHand.x, GodHand.y].TileAttribute == 1)
            {
                GameObject EffectTile = Instantiate(GameManager.Instance.Tile_Effect[0], new Vector3(GodHand.x, GodHand.y, -1), Quaternion.identity);
                EffectTile.AddComponent<DestroyAni>();
            }
            else if (TileManager.TileMain[GodHand.x, GodHand.y].TileAttribute == 2)
            {
                GameObject EffectTile = Instantiate(GameManager.Instance.Tile_Effect[1], new Vector3(GodHand.x, GodHand.y, -1), Quaternion.identity);
                EffectTile.AddComponent<DestroyAni>();
            }
            else if (TileManager.TileMain[GodHand.x, GodHand.y].TileAttribute == 3)
            {
                GameObject EffectTile = Instantiate(GameManager.Instance.Tile_Effect[2], new Vector3(GodHand.x, GodHand.y, -1), Quaternion.identity);
                EffectTile.AddComponent<DestroyAni>();
            }
            else if (TileManager.TileMain[GodHand.x, GodHand.y].TileAttribute == 4)
            {
                GameObject EffectTile = Instantiate(GameManager.Instance.Tile_Effect[3], new Vector3(GodHand.x, GodHand.y, -1), Quaternion.identity);
                EffectTile.AddComponent<DestroyAni>();
            }
        }
    }

}
