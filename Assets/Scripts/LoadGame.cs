using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    private static LoadGame instance_;
    public static LoadGame Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<LoadGame>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("LoadManager").AddComponent<LoadGame>();
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
        var objs = FindObjectsOfType<LoadGame>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        Select_Preset = 0;
        stageCount = 10;
        loadCount = 0;  //나중에 4로 바꿀거다.

        unlockStageAndBlock = new UnlockStageAndBlock[4];
        stage_Stat = new statistics[stageCount];

        for (int i = 0; i < 5; i++) // 디폴트 돌 덱
        {
            if (preset_Block_Card[0, i] <= 0)
            preset_Block_Card[0, i] = i+1; 
        }

        for (int i = 0; i < 10; i++)    // 디폴트 스킬 덱
        {
            if (preset_Skill_Card[0, i] <= 0)
            preset_Skill_Card[0, i] = i + 1 + 1000;
        }

        if (unlockStageAndBlock[loadCount].unlockStageCount == 0)  // 초기 해금 스테이지는 1개
            unlockStageAndBlock[loadCount].unlockStageCount = 1;

        if (unlockStageAndBlock[loadCount].unlockStoneCount == 0)  // 초기 해금 돌은 3개
            unlockStageAndBlock[loadCount].unlockStoneCount = 3;

        if (unlockStageAndBlock[loadCount].unlockSkillCount == 0)  // 초기 해금 스킬은 4개
            unlockStageAndBlock[loadCount].unlockSkillCount = 4;

        
    }
    public struct statistics
    {
        public int win_Count;
        public int lose_Count;
        public int afk_Count;
    }

    public struct UnlockStageAndBlock
    {
        public int unlockStageCount;
        public int unlockStoneCount;
        public int unlockSkillCount;
    }

    public UnlockStageAndBlock []unlockStageAndBlock;   // 해금할 스테이지, 돌, 스킬 저장할거임 4개, 저장할거
    public statistics[] stage_Stat; // 통계, 저장할거

    public int stageCount { get; private set; } // 총 스테이지 수
    public int stage_Number { get; private set; } // 스테이지 번호
    public int tile_Variety { get; private set; } // 타일 종류
    public int limit_Block { get; private set; } // 블록 제한 수
    public int limit_Skill { get; private set; } // 스킬 제한 수
    public int loadCount { get; private set; } // 총 저장 수

    public int[,] preset_Block_Card = new int[4, 5];    //저장할거
    public int[,] preset_Skill_Card = new int[4, 10];   //저장할거

    public int Select_Preset { get; private set; }  // 선택한 프리셋

    public void SetStageCode(int num, int var, int block, int skill)
    {
        stage_Number = num;
        tile_Variety = var;
        limit_Block = block;
        limit_Skill = skill;

        //Debug.Log(stage_Number);
        //Debug.Log(tile_Variety);
        //Debug.Log(limit_Block);
        //Debug.Log(limit_Skill);
    }

    public void changePresetNum(int num)
    {
        Select_Preset = num;
    }

    
    public void SetPresetBlock(int[,] block_array)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                preset_Block_Card[i, j] = block_array[i, j];
                Debug.Log(i + "번" + j + "칸" + preset_Block_Card[i, j] + "번 돌");
            }
        }
    }

    public void SetPresetSkill(int[,] skill_array)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                preset_Skill_Card[i, j] = skill_array[i, j];
                Debug.Log(i + "번" + j + "칸" + preset_Skill_Card[i, j] + "번 스킬");
            }
        }
    }

    public void PlusWin()
    {
        stage_Stat[stage_Number].win_Count++;
    }

    public void PlusLose()
    {
        stage_Stat[stage_Number].lose_Count++;
    }

    public void PlusAFK()
    {
        stage_Stat[stage_Number].afk_Count++;
    }

    public void SetStageCount(int num)
    {
        unlockStageAndBlock[loadCount].unlockStageCount = num;
    }

    public void SetSkillCount(int num)
    {
        unlockStageAndBlock[loadCount].unlockSkillCount = num;
    }

    public void SetStoneCount(int num)
    {
        unlockStageAndBlock[loadCount].unlockStoneCount = num;
    }

    public void SaveFile()
    {
        this.GetComponent<SaveData>().Save();
    }

    public void LoadFile()
    {
        this.GetComponent<SaveData>().Load();
    }
}
