using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNumber : MonoBehaviour
{
    GameObject [] numberArray = new GameObject[10];
    GameObject Lock;
    List<GameObject> numberList = new List<GameObject>();

    private int number = -1;

    int stage_number;
    int tile_variety;
    int limit_block;
    int limit_skill;

    // Start is called before the first frame update
    void Start()
    {
        Lock = Resources.Load("Stage_Select/Lock_Sprite") as GameObject;

        if (number == -1000)
        {
            GameObject ins = Instantiate(Lock, Vector2.zero, Quaternion.identity);
            ins.transform.parent = this.transform;
            ins.transform.localPosition = Vector2.zero;
            ins.transform.localScale = new Vector2(2f, 2f);
            return;
        }

        for (int i = 0; i < 10; i++)
        {
            string str = "Stage_Select/number_" + i.ToString();
            numberArray[i] = Resources.Load(str) as GameObject;
        }

        //Debug.Log(number);

        if (number < 10)
            numberList.Add(numberArray[number]);
        else if (number < 100)
        {
            numberList.Add(numberArray[number / 10]);
            numberList.Add(numberArray[number % 10]);
        }
        else if (number < 1000)
        {
            numberList.Add(numberArray[number / 100]);
            numberList.Add(numberArray[(number / 10) % 10]);
            numberList.Add(numberArray[number % 10]);
        }

        if (numberList.Count == 1)
        {
            GameObject ins = Instantiate(numberList[0], Vector2.zero, Quaternion.identity);
            ins.transform.parent = this.transform;
            ins.transform.localPosition = Vector2.zero;
        }
        else if (numberList.Count == 2)
        {
            GameObject ins = Instantiate(numberList[0], Vector2.zero, Quaternion.identity);
            GameObject ins2 = Instantiate(numberList[1], Vector2.zero, Quaternion.identity);
            ins.transform.parent = this.transform;
            ins2.transform.parent = this.transform;
            ins.transform.localPosition = new Vector2(-0.3f, 0);
            ins2.transform.localPosition = new Vector2(0.3f, 0);
        }
        else if (numberList.Count == 3)
        {
            GameObject ins = Instantiate(numberList[0], new Vector2(-0.4f, 0), Quaternion.identity);
            GameObject ins2 = Instantiate(numberList[1], new Vector2(-0.1f, 0), Quaternion.identity);
            GameObject ins3 = Instantiate(numberList[2], new Vector2(0.2f, 0), Quaternion.identity);
            ins.transform.parent = this.transform;
            ins2.transform.parent = this.transform;
            ins3.transform.parent = this.transform;
            ins.transform.localPosition = new Vector2(-0.6f, 0);
            ins2.transform.localPosition = new Vector2(0, 0);
            ins3.transform.localPosition = new Vector2(0.6f, 0);
        }

        numberList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNumber(int num)
    {
        number = num;
    }

    public void SetStage(int num, int var, int block, int skill)
    {
        stage_number = num;
        tile_variety = var;
        limit_block = block;
        limit_skill = skill;
    }

    public void SetStageVar()
    {
        LoadGame.Instance.SetStageCode(stage_number,tile_variety,limit_block,limit_skill);
    }

    public int GetStageNumber()
    {
        return stage_number;
    }
}
