using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckRandom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectRandom()  // 랜덤으로 덱 만들기
    {
        if (DeckManager.Instance.mod)
            Stone_Random();
        else
            SKill_Random();
    }

    void SKill_Random()
    {
        List<int> skillArray = new List<int>();
        for (int i = 0; i < 10; i++)
            skillArray.Add(i);

        for (int i = 0; i < 10; i++)
        {
            int RandomSkill = Random.Range(0, skillArray.Count);

            

            DeckManager.Instance.skill_Block[i].transform.GetChild(0).GetComponent<Image>().sprite = DeckManager.Instance.skill_Image[skillArray[RandomSkill]];
            DeckManager.Instance.skill_Preset[DeckManager.Instance.preset, i] = DeckManager.Instance.skill_Image[skillArray[RandomSkill]];
            DeckManager.Instance.save_Skill[DeckManager.Instance.preset, i] = skillArray[RandomSkill] + 1 + 1000;

            skillArray.Remove(skillArray[RandomSkill]);
        }

        skillArray.Clear();
    }

    void Stone_Random()
    {
        List<int> stoneArray = new List<int>();
        for (int i = 0; i < 5; i++)
            stoneArray.Add(i);

        for (int i = 0; i < 5; i++)
        {
            int RandomStone = Random.Range(0, stoneArray.Count);

            

            DeckManager.Instance.stone_Block[i].transform.GetChild(0).GetComponent<Image>().sprite = DeckManager.Instance.stone_Image[stoneArray[RandomStone]];
            DeckManager.Instance.stone_Preset[DeckManager.Instance.preset, i] = DeckManager.Instance.stone_Image[stoneArray[RandomStone]];
            DeckManager.Instance.save_Stone[DeckManager.Instance.preset, i] = stoneArray[RandomStone] + 1;

            stoneArray.Remove(stoneArray[RandomStone]);
        }

        stoneArray.Clear();
    }
}
