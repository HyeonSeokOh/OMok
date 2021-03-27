using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStage : MonoBehaviour
{
    int [] stone_Cnt = new int[9] { 4, 5, 5, 5, 5, 5, 5, 5, 5};
    int[] skill_Cnt = new int[9] { 5, 7, 9, 10, 10, 10, 10, 10, 10 };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear(int stage)
    {
        LoadGame.Instance.SetStoneCount(stone_Cnt[stage]);
        LoadGame.Instance.SetSkillCount(skill_Cnt[stage]);
    }
}
