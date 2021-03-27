using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }

        turn = 1;
        isEnd = false;

        WinEffect.SetActive(false);
        LoseEffect.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            string str = "Stone__" + (i+ 1).ToString();
            Block[i] = GameObject.Find(str);
        }

        for (int i = 0; i < 10; i++)
        {
            string str = "Skill__" + (i + 1).ToString();
            Skill[i] = GameObject.Find(str);
        }

        Tile_Effect[0] = Resources.Load("GroundEffect") as GameObject;
        Tile_Effect[1] = Resources.Load("ForestEffect") as GameObject;
        Tile_Effect[2] = Resources.Load("WaterEffect") as GameObject;
        Tile_Effect[3] = Resources.Load("IceEffect") as GameObject;
        lockImage = Resources.Load("LockImage") as GameObject;
        preset_Num = LoadGame.Instance.Select_Preset;

        for (int i = 0; i < 5; i++)
        {
            if (i < LoadGame.Instance.limit_Block)
            {
                string str = "Stone/BlackCost0" + (LoadGame.Instance.preset_Block_Card[preset_Num, i]).ToString();
                GameObject obj = Resources.Load(str) as GameObject;
                if (obj == null) continue;

                GameObject obj2 = Instantiate(obj, Block[i].transform.position, Quaternion.identity);
                obj2.GetComponent<RectTransform>().transform.SetParent(Block[i].GetComponent<RectTransform>().transform);
                obj2.name = "BlackCost0" + (LoadGame.Instance.preset_Block_Card[preset_Num, i]).ToString();
            }
            else
            {
                GameObject obj2 = Instantiate(lockImage, Block[i].transform.position, Quaternion.identity);
                obj2.GetComponent<RectTransform>().transform.SetParent(Block[i].GetComponent<RectTransform>().transform);
                obj2.GetComponent<RectTransform>().transform.localScale = new Vector2(1.5f, 1.5f);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (i < LoadGame.Instance.limit_Skill)
            {
                string str = "Skill/Skill_0" + (LoadGame.Instance.preset_Skill_Card[preset_Num, i] - 1000).ToString();
                GameObject obj = Resources.Load(str) as GameObject;
                if (obj == null) continue;

                GameObject obj2 = Instantiate(obj, Skill[i].transform.position, Quaternion.identity);
                obj2.GetComponent<RectTransform>().transform.SetParent(Skill[i].GetComponent<RectTransform>().transform);
                obj2.name = "Skill_0" + (LoadGame.Instance.preset_Skill_Card[preset_Num, i] - 1000).ToString();
            }
            else
            {
                GameObject obj2 = Instantiate(lockImage, Skill[i].transform.position, Quaternion.identity);
                obj2.GetComponent<RectTransform>().transform.SetParent(Skill[i].GetComponent<RectTransform>().transform);
                obj2.GetComponent<RectTransform>().transform.localScale = new Vector2(1.5f, 1.5f);
            }
        }

    }
    public static GameManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    //변수 선언
    public int stage;
    public int[,] CreateRullBoard;
    public bool Dragging = false; // 드래깅 
    public bool NextTurn = false;
    public int mainTurn = 1;
    public int turn;
    public int MainCost = 5;
    public int objectTag;
    public bool BuilDollstate = false;
    public bool colorLock = false;
    public bool isexplain = false;
    public bool isEnd; // 게임이 끝났는지 아닌지
    public int TextType = 0;
    private int GameState = 0;
    private int preset_Num = 0;

    GameObject lockImage;
    GameObject[] Block = new GameObject[5];
    GameObject[] Skill = new GameObject[10];
    public GameObject[] Tile_Effect = new GameObject[4];
    public GameObject WinEffect;
    public GameObject LoseEffect;

    public GameObject leftUI;

    //함수 선언
    public void CostCheck()
    {
        if(MainCost <=0)
        {
            MainCost = 0;
        }

        if (MainCost >= 5)
        {
            MainCost = 5;
        }
    }

    public void ExplainCheck(bool isExplain)
    {
        if (isExplain)
            leftUI.SetActive(true);
        else if (!isExplain && !isexplain)
            leftUI.SetActive(false);
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (!isEnd)
            MainGame();
    }
    private void MainGame() // 게임 승리 조건이 담겨있다. 
    {
        //가로 5줄 검사 코드
        for (int i = 0; i < TileManager.Instance.BoardSizeX; i++)   //////////////////////검사 범위 막기 !!!!!!!!!!!!!!!!!
        {
            for (int j = 0; j < TileManager.Instance.BoardSizeY; j++)
            {

                //흑돌 승리 조건  
                if (i + 4 <= TileManager.Instance.BoardSizeX)
                {
                    if (TileManager.TileMain[i, j].RullTile == 1 && TileManager.TileMain[i + 1, j].RullTile == 1 && TileManager.TileMain[i + 2, j].RullTile == 1
                        && TileManager.TileMain[i + 3, j].RullTile == 1 && TileManager.TileMain[i + 4, j].RullTile == 1) { GameState = 1; EndGame(); return; }

                }
                if (j + 4 <= TileManager.Instance.BoardSizeY)
                {
                    if (TileManager.TileMain[i, j].RullTile == 1 && TileManager.TileMain[i, j + 1].RullTile == 1 && TileManager.TileMain[i, j + 2].RullTile == 1
                        && TileManager.TileMain[i, j + 3].RullTile == 1 && TileManager.TileMain[i, j + 4].RullTile == 1) { GameState = 1; EndGame(); return; }
                }
                if (i + 4 <= TileManager.Instance.BoardSizeX && j + 4 <= TileManager.Instance.BoardSizeY)
                { 
                    if (TileManager.TileMain[i, j].RullTile == 1 && TileManager.TileMain[i + 1, j + 1].RullTile == 1 && TileManager.TileMain[i + 2, j + 2].RullTile == 1
                    && TileManager.TileMain[i + 3, j + 3].RullTile == 1 && TileManager.TileMain[i + 4, j + 4].RullTile == 1) { GameState = 1; EndGame(); return; } 
                }

                if (i + 4 <= TileManager.Instance.BoardSizeX &&j-4 >= 0 )
                {
                    if (TileManager.TileMain[i, j].RullTile == 1 && TileManager.TileMain[i + 1, j - 1].RullTile == 1 && TileManager.TileMain[i + 2, j - 2].RullTile == 1
                    && TileManager.TileMain[i + 3, j - 3].RullTile == 1 && TileManager.TileMain[i + 4, j - 4].RullTile == 1) { GameState = 1; EndGame(); return; }
                }


                //백돌 승리 조건
                if (i + 4 <= TileManager.Instance.BoardSizeX)
                {
                    if (TileManager.TileMain[i, j].RullTile == 2 && TileManager.TileMain[i + 1, j].RullTile == 2 && TileManager.TileMain[i + 2, j].RullTile == 2
                    && TileManager.TileMain[i + 3, j].RullTile == 2 && TileManager.TileMain[i + 4, j].RullTile == 2) { GameState = 2; EndGame(); return; }
                }
                if (j + 4 <= TileManager.Instance.BoardSizeY)
                {
                    if (TileManager.TileMain[i, j].RullTile == 2 && TileManager.TileMain[i, j + 1].RullTile == 2 && TileManager.TileMain[i, j + 2].RullTile == 2
                    && TileManager.TileMain[i, j + 3].RullTile == 2 && TileManager.TileMain[i, j + 4].RullTile == 2) { GameState = 2; EndGame(); return; }
                }
                if (i + 4 <= TileManager.Instance.BoardSizeX && j + 4 <= TileManager.Instance.BoardSizeY)
                {
                    if (TileManager.TileMain[i, j].RullTile == 2 && TileManager.TileMain[i + 1, j + 1].RullTile == 2 && TileManager.TileMain[i + 2, j + 2].RullTile == 2
                    && TileManager.TileMain[i + 3, j + 3].RullTile == 2 && TileManager.TileMain[i + 4, j + 4].RullTile == 2) { GameState = 2; EndGame(); return; }
                }
                if (i + 4 <= TileManager.Instance.BoardSizeX && j - 4 >= 0)
                {
                    if (TileManager.TileMain[i, j].RullTile == 2 && TileManager.TileMain[i + 1, j - 1].RullTile == 2 && TileManager.TileMain[i + 2, j - 2].RullTile == 2
                    && TileManager.TileMain[i + 3, j - 3].RullTile == 2 && TileManager.TileMain[i + 4, j - 4].RullTile == 2) { GameState = 2; EndGame(); return; }
                }
            }
        }

    }

    private void EndGame()  // 한놈이 이기면 게임끝
    {
        isEnd = true;

        if (GameState == 1) // 흑이 이길 때
        {
            //AudioManager.Instance.WinLoseAudio(0);
            WinEffect.SetActive(true);
            LoadGame.Instance.PlusWin();    // 승리 통계 추가
            OpenNextStage();
        }
        else if (GameState == 2)    // 백이 이길 때
        {
            //AudioManager.Instance.WinLoseAudio(0);
            LoseEffect.SetActive(true);
            LoadGame.Instance.PlusLose();   // 패배 통계 추가
        }

        GameState = 0;

        
    }

    private void OpenNextStage()
    {
        int n = LoadGame.Instance.stage_Number;
        if (LoadGame.Instance.unlockStageAndBlock[LoadGame.Instance.loadCount].unlockStageCount > n)
        {
            return;
        }
        else
        {
            this.GetComponent<ClearStage>().Clear(n-1);
            LoadGame.Instance.SetStageCount(n + 1);
        }
    }

    public void yourAFK()
    {
        if (!isEnd)
        {
            LoadGame.Instance.PlusAFK();    // 도망 통계 추가
        }
    }
}
