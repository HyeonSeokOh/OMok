using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button1234 : MonoBehaviour
{
    int num2;
    Sprite[] button1234Sprite;

    void Start()
    {
        button1234Sprite = Resources.LoadAll<Sprite>("Preset_20_18");
    }

    private void Update()
    {
        Chnage1234Button();
    }

    public void Chnage1234Button()
    {
        num2 = DeckManager.Instance.preset;

        if (num2 == 0)
            transform.GetChild(0).GetComponent<Image>().sprite = button1234Sprite[0];

        else if (num2 != 0)
            transform.GetChild(0).GetComponent<Image>().sprite = button1234Sprite[1];

        if (num2 == 1)
            transform.GetChild(1).GetComponent<Image>().sprite = button1234Sprite[2];

        else if (num2 != 1)
            transform.GetChild(1).GetComponent<Image>().sprite = button1234Sprite[3];

        if (num2 == 2)
            transform.GetChild(2).GetComponent<Image>().sprite = button1234Sprite[4];

        else if (num2 != 2)
            transform.GetChild(2).GetComponent<Image>().sprite = button1234Sprite[5];

        if (num2 == 3)
            transform.GetChild(3).GetComponent<Image>().sprite = button1234Sprite[6];

        else if (num2 != 3)
            transform.GetChild(3).GetComponent<Image>().sprite = button1234Sprite[7];
    }
}
