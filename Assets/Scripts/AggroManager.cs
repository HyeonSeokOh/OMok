using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;

public class AggroManager : MonoBehaviour
{
    private static AggroManager instance_;
    public static AggroManager Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<AggroManager>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("GoBoard").AddComponent<AggroManager>();
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
        var objs = FindObjectsOfType<AggroManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public struct Point
    {
        public int x;
        public int y;
        public Point(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
    }

    public Point [] aggroArray = new Point[8];  //어그로 걸린 돌 좌표
    public bool[] isAggroArray = new bool[8];   // 어그로 여부
    public int aggroCount;  // 어그로 개수 세기
    public void aggroInit()
    {
        // aggroArray가 null이 아니면 그리고 다시 new로 재 할당
        // null이면 할당
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FindAggro()
    {
        //aggroCount = 0;   // 어그로 개수 세기

        //for (int i = 0; i < 8; i++) // 어그로 재검사
        //{
        //    if (aggroArray[i].x != null || aggroArray[i].y != null)
        //    {
        //        if (TileManager.TileMain[aggroArray[i].x, aggroArray[i].y].isAggro = false)
        //        {
        //            //isAggroArray[i] = false;
        //        }
        //    }
        //}

        // 어그로 찾기 및 배열에 저장
        for (int i = 0; i < TileManager.Instance.BoardSizeX; i++)
        {
            for (int j = 0; i < TileManager.Instance.BoardSizeY; j++)
            {
                if (TileManager.TileMain[i, j].isAggro == true)
                {
                    //if (!aggroArray.Contains(new Point(i,j)))   // 리스트에 없는 녀석이 어그로면 리스트에 추가함
                    //aggroArray.Add(new Point(i, j));
                    //isAggroArray[aggroCount] = true;    // 어그로 여부 저장
                    //aggroArray[aggroCount] = new Point(i, j);   // 좌표 저장
                    //aggroCount++;   // 개수를 셌다.
                }
            }
        }
    }
}
