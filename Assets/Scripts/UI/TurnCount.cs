using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnCount : MonoBehaviour
{
    public Sprite[] Turncount = new Sprite[4];


    void Update()
    {
        if (GameManager.Instance.mainTurn == 1)
        {
            gameObject.GetComponent<Image>().sprite = Turncount[0];
        }
        else if( GameManager.Instance.mainTurn == 2)
        {
            gameObject.GetComponent<Image>().sprite = Turncount[1];
        }
        else if(GameManager.Instance.mainTurn == 3)
        {
            gameObject.GetComponent<Image>().sprite = Turncount[2];
        }
        else if(GameManager.Instance.mainTurn==4)
        {
            gameObject.GetComponent<Image>().sprite = Turncount[3];
        }
    }
}
