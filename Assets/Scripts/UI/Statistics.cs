using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    Text Win_Text;
    Text Lose_Text;
    Text AFK_Text;

    // Start is called before the first frame update
    void Start()
    {
        Win_Text = GameObject.Find("Win").transform.GetChild(0).GetComponent<Text>();
        Lose_Text = GameObject.Find("Lose").transform.GetChild(0).GetComponent<Text>();
        AFK_Text = GameObject.Find("AFK").transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStatistics()
    {
        Win_Text.text = "승리 횟수 : " + LoadGame.Instance.stage_Stat[LoadGame.Instance.stage_Number].win_Count.ToString();
        Lose_Text.text = "패배 횟수 : " + LoadGame.Instance.stage_Stat[LoadGame.Instance.stage_Number].lose_Count.ToString();
        AFK_Text.text = "도망친 횟수 : " + LoadGame.Instance.stage_Stat[LoadGame.Instance.stage_Number].afk_Count.ToString();
    }
}
