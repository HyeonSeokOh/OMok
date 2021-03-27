using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Data
{
    //public int[,] pre_Card = new int[4, 5];
    public List<int> pre_Card = new List<int>();
    public List<int> pre_Skill = new List<int>();
    public int[] count = new int[3];
    public List<int> stat = new List<int>();

    public void setting1(int[,] card, int[,] skill)
    {
        pre_Card.Clear();
        pre_Skill.Clear();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                pre_Card.Add(card[i, j]);
            }

            for (int j = 0; j < 10; j++)
            {
                pre_Skill.Add(skill[i, j]);
            }
        }
    }

    public void setting2(int num1, int num2, int num3)
    {
        count[0] = num1;
        count[1] = num2;
        count[2] = num3;
    }

    public void setting3(int num1, int num2, int num3, int i)
    {


        stat.Add(num1);
        stat.Add(num2);
        stat.Add(num3);
    }

    public int GetCard(int i, int j)
    {
        return pre_Card[i * 5 + j];
    }

    public int GetSkill(int i, int j)
    {
        return pre_Skill[i * 10 + j];
    }

    public int GetCnt(int i)
    {
        return count[i];
    }

    public int GetStat(int i, int j)
    {
        return stat[i * 3 + j];
    }
}

public class SaveData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        Data data = new Data();
        data.setting1(LoadGame.Instance.preset_Block_Card, LoadGame.Instance.preset_Skill_Card);
        data.setting2(LoadGame.Instance.unlockStageAndBlock[0].unlockStageCount,
                      LoadGame.Instance.unlockStageAndBlock[0].unlockStoneCount,
                      LoadGame.Instance.unlockStageAndBlock[0].unlockSkillCount);
        for (int i = 0; i < 10; i++)
            data.setting3(LoadGame.Instance.stage_Stat[i].win_Count,
                          LoadGame.Instance.stage_Stat[i].lose_Count,
                          LoadGame.Instance.stage_Stat[i].afk_Count,
                          i);

        File.WriteAllText(Application.dataPath + "/saveFileJson.json", JsonUtility.ToJson(data));

    }

    public void Load()
    {

        try //예외처리
        {
            File.ReadAllText(Application.dataPath + "/saveFileJson.json");
        }
        catch (FileNotFoundException e) // 파일없으면 실행
        {
            
            return;
        }

        string str = File.ReadAllText(Application.dataPath + "/saveFileJson.json");
       


        Data data2 = JsonUtility.FromJson<Data>(str);

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                LoadGame.Instance.preset_Block_Card[i, j] = data2.GetCard(i, j);
            }

            for (int j = 0; j < 10; j++)
            {
                LoadGame.Instance.preset_Skill_Card[i, j] = data2.GetSkill(i, j);
            }
        }

        LoadGame.Instance.unlockStageAndBlock[0].unlockStageCount = data2.GetCnt(0);
        LoadGame.Instance.unlockStageAndBlock[0].unlockStoneCount = data2.GetCnt(1);
        LoadGame.Instance.unlockStageAndBlock[0].unlockSkillCount = data2.GetCnt(2);

        for (int i = 0; i < 10; i++)
        {
            LoadGame.Instance.stage_Stat[i].win_Count = data2.GetStat(i, 0);
            LoadGame.Instance.stage_Stat[i].lose_Count = data2.GetStat(i, 1);
            LoadGame.Instance.stage_Stat[i].afk_Count = data2.GetStat(i, 2);
        }
    }
}
