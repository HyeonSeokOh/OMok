using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weight : MonoBehaviour
{
    private static weight instance_;
    public static weight Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<weight>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("MapScan").AddComponent<weight>();
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
        var objs = FindObjectsOfType<weight>();
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

    GameObject[] numberArray = new GameObject[9];
    GameObject[] attackArray = new GameObject[4];
    GameObject[] defenseArray = new GameObject[4];
    void Start()
    {
        
        for (int i = 0; i < 9; i ++)
        {
            string str = "Turn_Number_" + i.ToString();
            numberArray[i] = Resources.Load(str) as GameObject;
        }

        for (int i = 0; i < 4; i++)
        {
            string str = "Attack_Point_" + i.ToString();
            string str1 = "Defense_Point_" + i.ToString();
            attackArray[i] = Resources.Load(str) as GameObject;
            defenseArray[i] = Resources.Load(str1) as GameObject;
        }
    }

    public void detone()
    {
        for (int i = 0; i < TileManager.Instance.BoardSizeX; i++)
        {
            for (int j = 0; j < TileManager.Instance.BoardSizeY; j++)
            {
                int atk = 0;
                int def = 0;
                int k = TileManager.TileMain[i, j].weight;
                if (k < 0) k = k * -1;

                if (TileManager.TileMain[i, j].attackPoint == TileManager.attack_defense_Point.ZERO)
                    atk = 0;
                else if (TileManager.TileMain[i, j].attackPoint == TileManager.attack_defense_Point.ONE)
                    atk = 1;
                else if (TileManager.TileMain[i, j].attackPoint == TileManager.attack_defense_Point.TWO)
                    atk = 2;
                else if (TileManager.TileMain[i, j].attackPoint == TileManager.attack_defense_Point.THREE)
                    atk = 3;

                if (TileManager.TileMain[i, j].defensePoint == TileManager.attack_defense_Point.ZERO)
                    def = 0;
                else if (TileManager.TileMain[i, j].defensePoint == TileManager.attack_defense_Point.ONE)
                    def = 1;
                else if (TileManager.TileMain[i, j].defensePoint == TileManager.attack_defense_Point.TWO)
                    def = 2;
                else if (TileManager.TileMain[i, j].defensePoint == TileManager.attack_defense_Point.THREE)
                    def = 3;

                GameObject obj = Instantiate(numberArray[k], new Vector2(i, j), Quaternion.identity) as GameObject;
                obj.GetComponent<Transform>().localScale = new Vector3(2, 2, 2);
                obj.AddComponent<DeleteWeight>();

                GameObject obj1 = Instantiate(attackArray[atk], new Vector2(i - 0.2f, j + 0.2f), Quaternion.identity) as GameObject;
                obj1.AddComponent<DeleteWeight>();

                GameObject obj2 = Instantiate(defenseArray[def], new Vector2(i + 0.2f, j - 0.2f), Quaternion.identity) as GameObject;
                obj2.AddComponent<DeleteWeight>();
            }
        }
    }
}
