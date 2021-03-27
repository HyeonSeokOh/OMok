using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDeckCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveDeck()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (DeckManager.Instance.skill_Preset[i,j] == null)
                {
                    DeckManager.Instance.save_Skill[i, j] = -2000;
                }
            }

            for (int j = 0; j < 5; j++)
            {
                if (DeckManager.Instance.stone_Preset[i, j] == null)
                {
                    DeckManager.Instance.save_Stone[i, j] = -2000;
                }
            }
        }

        LoadGame.Instance.SetPresetBlock(DeckManager.Instance.save_Stone);
        LoadGame.Instance.SetPresetSkill(DeckManager.Instance.save_Skill);
    }
}
