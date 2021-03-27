using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TurnButton : MonoBehaviour, IPointerClickHandler
{
    Sprite [] button_Image = new Sprite[4];
    GameObject img;
    GameObject panel;
    void Start()
    {
        for (int i = 0; i < 4; i ++)
        {
            string str = "Turn_Button_" + i.ToString();
            img = Resources.Load(str) as GameObject;
            button_Image[i] = img.GetComponent<SpriteRenderer>().sprite;
        }

        panel = this.transform.GetChild(0).gameObject;
    }


    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (panel.activeSelf == false)
        {
            if (GameManager.Instance.mainTurn <= 2)
            {
                GameManager.Instance.mainTurn++;   // 누르면 턴을 바꿈
                GameManager.Instance.turn++;
            }


            if (GameManager.Instance.mainTurn == 5) { GameManager.Instance.mainTurn = 1; }
            if (GameManager.Instance.mainTurn == 1) { GameManager.Instance.MainCost = 5; }
            else if (GameManager.Instance.mainTurn == 3) { GameManager.Instance.MainCost = 5; }
            GameManager.Instance.BuilDollstate = false;

            TileScan2.Instance.MapScanning();
        }
    }

    void Update()
    {
        if (GameManager.Instance.mainTurn <= 4)
        {
            this.GetComponent<Image>().sprite = button_Image[GameManager.Instance.mainTurn - 1];
        }
    }
}
