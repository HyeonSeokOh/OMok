using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateStageButton : MonoBehaviour  // 버튼 생성 스크립트
{
    GameObject stage_Button;
    GameObject Line_Height;
    GameObject Line_Width;

    GameObject[] stageArray = new GameObject[10];

    Vector2[] pos = new Vector2[10] {new Vector2(0,0), new Vector2(4,-3), new Vector2(7,1), new Vector2(10,-5),
                                     new Vector2(8, -8), new Vector2(3, -6), new Vector2(-2, -9), new Vector2(0, -13),
                                     new Vector2(4, -10), new Vector2(8, -13)};

    int[,] stage_Setting = new int[10, 4] { {1, 1, 3, 3 }, {2, 2, 4, 5 }, {3, 3, 5, 8 }, {4, 4, 5, 10 }, {5, 4, 5, 10 },
                                            {6, 4, 5, 10 }, {7, 4, 5, 10 }, {8, 4, 5, 10 }, {9, 4, 5, 10 }, {10, 4, 5, 10 } }; 


    // Start is called before the first frame update
    void Start()
    {
        stage_Button = Resources.Load("Stage_Select/stage") as GameObject;
        Line_Height = Resources.Load("Stage_Select/Line_Height") as GameObject;
        Line_Width = Resources.Load("Stage_Select/Line_Width") as GameObject;

        Vector2 LineDir = Vector2.zero;
        Vector2 LineDir2 = Vector2.zero;
        for (int i = 0; i < 10; i++)    // 버튼 생성
        {
            stageArray[i] = Instantiate(stage_Button, Vector2.zero, Quaternion.identity) as GameObject;
            stageArray[i].transform.parent = this.transform;
            stageArray[i].transform.position = pos[i];
            if (LoadGame.Instance.unlockStageAndBlock[LoadGame.Instance.loadCount].unlockStageCount >= i + 1)
            {
                stageArray[i].GetComponent<CreateNumber>().SetNumber(i + 1);  // CreateNumber는 버튼에 숫자 박는 거
                stageArray[i].GetComponent<CreateNumber>().SetStage(stage_Setting[i, 0], stage_Setting[i, 1], stage_Setting[i, 2], stage_Setting[i, 3]);
            }
            else
            {
                stageArray[i].GetComponent<CreateNumber>().SetNumber(-1000);
            }
        }

        for (int i = 0; i < 10 - 1; i++)    // 막대기 생성
        {
            Vector2 dir = stageArray[i].transform.position - stageArray[i + 1].transform.position;

            if (dir.y < 0)
            {
                for (int j = 0; j > dir.y + 1; j--)
                {
                    GameObject obj = Instantiate(Line_Height, new Vector2(stageArray[i].transform.position.x, stageArray[i].transform.position.y - j + 1.5f), Quaternion.identity) as GameObject;
                    obj.transform.parent = stageArray[i].transform;
                }
            }
            else
            {
                for (int j = 1; j <= dir.y-1; j++)
                {
                    GameObject obj = Instantiate(Line_Height, new Vector2(stageArray[i].transform.position.x , stageArray[i].transform.position.y - j - 0.5f), Quaternion.identity) as GameObject;
                    obj.transform.parent = stageArray[i].transform;
                }
            }

            if (dir.x < 0)
            {
                for (int j = 0; j > dir.x; j--)
                {
                    GameObject obj = Instantiate(Line_Width, new Vector2(stageArray[i + 1].transform.position.x + j - 0.5f, stageArray[i + 1].transform.position.y), Quaternion.identity) as GameObject;
                    obj.transform.parent = stageArray[i].transform;
                }
            }
            else
            {
                for (int j = 1; j <= dir.x - 1; j++)
                {
                    GameObject obj = Instantiate(Line_Width, new Vector2(stageArray[i + 1].transform.position.x + j + 0.5f, stageArray[i + 1].transform.position.y), Quaternion.identity) as GameObject;
                    obj.transform.parent = stageArray[i].transform;
                }
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
