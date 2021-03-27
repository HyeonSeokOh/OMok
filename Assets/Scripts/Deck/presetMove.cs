using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class presetMove : MonoBehaviour
{
    int num;

    // Start is called before the first frame update
    void Start()
    {
        num = int.Parse(this.name) - 1;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PresetChanger()
    {
        DeckManager.Instance.preset = num;

        for (int i = 0; i < 5; i++)
        {
            if (DeckManager.Instance.stone_Preset[DeckManager.Instance.preset, i] != null)
                DeckManager.Instance.stone_Block[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite =
                    DeckManager.Instance.stone_Preset[DeckManager.Instance.preset, i];
            else
                DeckManager.Instance.stone_Block[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite =
                    DeckManager.Instance.blank.sprite;
        }

        for (int i = 0; i < 10; i++)
        {
            if (DeckManager.Instance.skill_Preset[DeckManager.Instance.preset, i] != null)
                DeckManager.Instance.skill_Block[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite =
                DeckManager.Instance.skill_Preset[DeckManager.Instance.preset, i];
            else
                DeckManager.Instance.skill_Block[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite =
                     DeckManager.Instance.blank.sprite;
        }
    }
}
