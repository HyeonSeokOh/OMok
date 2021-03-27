using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private static TileManager instance_;
    public static TileManager Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<TileManager>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("GoBoard").AddComponent<TileManager>();
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
        var objs = FindObjectsOfType<TileManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }

    public static MainTile[,] TileMain;

    public int BoardSizeX = 0;
    public int BoardSizeY = 0;

    GameObject DefaultTile;
    GameObject ForestTile;
    GameObject WaterTile;
    GameObject IceTile;

    Vector2 TilePos;
    MainTile mainTile;

    public enum attack_defense_Point { ZERO = 0, ONE, TWO, THREE };

    public struct MainTile
    {
        public int PosX, PosY; // 좌표를 기억하는 변수
        public int TileAttribute; // 속성을 기억하는 변수
        public GameObject TileStyle; // 타일 프리펩 가져오기
        public int RullTile; // 게임의 룰 지정 0,1,2
        public int DollValue;    // 돌의 실가치
        public int weight; // 가중치
        public Transform TileChild;
        // 어느 방향으로 몇번 연속 같은색 돌이 나열되었는지
        // 0이면 동쪽, 1이면 동남쪽, 2이면 남쪽, 3이면 남서쪽
        // 3목이면 3, 4목이면 4, 2목이면 2 이런식으로...
        // 전부 뚫려 있으면 +0, 한쪽만이면 + 10, 두쪽 다 막혔으면 +20
        public int[] combo;
        public attack_defense_Point attackPoint; // 공격 우선권
        public attack_defense_Point defensePoint; // 방어 우선권
        public bool isAggro;    // 어그로. false면 어그로 아님. true면 어그로임
    }



    void Start()
    {
        //각 타일의 속성 리소스 불러오기
        DefaultTile = Resources.Load("DefaultTile") as GameObject;
        ForestTile = Resources.Load("ForestTile") as GameObject;
        WaterTile = Resources.Load("WaterTile") as GameObject;
        IceTile = Resources.Load("IceTile") as GameObject;

        GameStage(); // 게임 스테이지 지정
        CreateBoard(); // 게임 보드 만들기
    }

    void GameStage() // 스테이지별 사이즈 설정
    {
        switch (GameManager.Instance.stage)
        {
            case 1: // 1스테이지
                BoardSizeX = 10;
                BoardSizeY = 10;
                break;
            case 2: // 2스테이지
                BoardSizeX = 5;
                BoardSizeY = 5;
                break;
            case 3: // 3스테이지
                BoardSizeX = 5;
                BoardSizeY = 5;
                break;
            case 4: // 4스테이지
                BoardSizeX = 5;
                BoardSizeY = 5;
                break;

        }

    }

    void CreateBoard()  // 보드를 생성해요
    {
        mainTile.TileAttribute = 1;
        TileMain = new MainTile[BoardSizeX, BoardSizeY]; // 배열 동적 할당

        mainTile.PosX = 0;
        mainTile.PosY = 0;

        //보드 사이즈만큼 디폴트값(땅 속성) 지정
        for (mainTile.PosX = 0; mainTile.PosX < BoardSizeX; mainTile.PosX++)
        {
            for (mainTile.PosY = 0; mainTile.PosY < BoardSizeY; mainTile.PosY++)
            {
                TileMain[mainTile.PosX, mainTile.PosY].TileAttribute = 1;
                TileMain[mainTile.PosX, mainTile.PosY].RullTile = 0;

                //타일의 연속 여부 
                TileMain[mainTile.PosX, mainTile.PosY].combo = new int[4];
                //타일의 가중치
                TileMain[mainTile.PosX, mainTile.PosY].weight = 0;
                //타일 위의 돌의 실가치
                TileMain[mainTile.PosX, mainTile.PosY].DollValue = 0;
            }
        }

        //30개의 랜덤 좌표지정(시작 좌표)
        for (int i = 0; i < 30; i++)
        {
            int RandomTileX = Random.Range(0, BoardSizeX - 1);
            int RandomTileY = Random.Range(0, BoardSizeY - 1);
            int RandomTile = Random.Range(1, LoadGame.Instance.tile_Variety+1);

            // 시작좌표 기준 7X7의 타일 지정
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    // 오목판의 초과범위 방지 조건문
                    if (RandomTileX + x < BoardSizeX && RandomTileY + y < BoardSizeY)
                    {
                        TileMain[x + RandomTileX, y + RandomTileY].TileAttribute = RandomTile; // 시작좌표 랜덤값 지정
                    }
                }
            }
        }

        // 전체 좌표스캔용 2중 for문
        for (mainTile.PosX = 0; mainTile.PosX < BoardSizeX; mainTile.PosX++)
        {
            for (mainTile.PosY = 0; mainTile.PosY < BoardSizeY; mainTile.PosY++)
            {
                //타일의 위치값
                Vector2 TilePos = new Vector2(mainTile.PosX, mainTile.PosY);

                //타일 속성에 따른 오브젝트 출력
                switch (TileMain[mainTile.PosX, mainTile.PosY].TileAttribute)
                {
                    case 1:
                        TileMain[mainTile.PosX, mainTile.PosY].TileStyle = Instantiate(DefaultTile);
                        break;
                    case 2:
                        TileMain[mainTile.PosX, mainTile.PosY].TileStyle = Instantiate(ForestTile);
                        break;
                    case 3:
                        TileMain[mainTile.PosX, mainTile.PosY].TileStyle = Instantiate(WaterTile);
                        break;
                    case 4:
                        TileMain[mainTile.PosX, mainTile.PosY].TileStyle = Instantiate(IceTile);
                        break;
                }
                TileMain[mainTile.PosX, mainTile.PosY].TileStyle.AddComponent<ColorReForm>();
                TileMain[mainTile.PosX, mainTile.PosY].TileStyle.transform.position = TilePos; //타일의 위치지정

                TileMain[mainTile.PosX, mainTile.PosY].TileStyle.transform.parent =
                    GameObject.Find("GoBoard").transform; //GoBoard 오브젝트 안에 생성된 타일 자식으로 넣음

                //좌표값 찾기
                TileMain[mainTile.PosX, mainTile.PosY].PosX = mainTile.PosX;
                TileMain[mainTile.PosX, mainTile.PosY].PosY = mainTile.PosY;
            }
        }
    }


   
}
