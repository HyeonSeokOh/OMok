using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPreset_Num : MonoBehaviour
{
    GameObject[] preset = new GameObject[4];
    Sprite[] preset_Button;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0 ; i < 4; i++)
        {
            string str = "Preset_" + (i + 1).ToString();
            preset[i] = GameObject.Find(str);
        }

        preset_Button = Resources.LoadAll<Sprite>("Select_Preset");
    }

    public void ChagnePresetButton(int num)
    {
        for (int i = 0 ; i < 4; i ++)
        {
            if (preset[i].name == "Preset_" + (num + 1).ToString())
            {
                preset[i].GetComponent<Image>().sprite = preset_Button[1]; 
            }
            else
            {
                preset[i].GetComponent<Image>().sprite = preset_Button[0];
            }
        }
        LoadGame.Instance.changePresetNum(num);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
