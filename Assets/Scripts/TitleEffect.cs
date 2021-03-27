using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleEffect : MonoBehaviour
{
    float time;

    float MoveSpeed;
    float ColorChangeSpeed;

    Title StoneType;
    Image image;
    void Start()
    {
        image = this.GetComponent<Image>();
        StoneType = GameObject.Find("Canvas").GetComponent<Title>();
        MoveSpeed = 0.7f; // 이펙트가 지속되는 시간
        ColorChangeSpeed = 0.01f; // 알파값이 낮아지는 속도
        this.transform.localScale = Vector3.one;
        this.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
    }

    void FixedUpdate()
    {
        switch (StoneType.FindRandomType) // 스톤의 돌 색상에 따라 색상 맞추기    *Title 스크립트에서 1줄 추가(69라인)
        {
            case 0:
                this.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/black_Doll", typeof(Sprite)) as Sprite;
                break;
            case 1:
                this.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/white_Doll", typeof(Sprite)) as Sprite;
                break;
        }
        time = time + Time.deltaTime;

        if (time < MoveSpeed) 
        {
            Color color = image.color;

            color.a -= ColorChangeSpeed;
            image.color = color;

            this.transform.localScale = Vector2.Lerp(this.transform.localScale, new Vector2(3, 3), Time.deltaTime * 2);
        }
        else
            Destroy(this.gameObject);
    }

}
