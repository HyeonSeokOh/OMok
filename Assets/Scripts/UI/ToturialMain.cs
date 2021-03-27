using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ToturialMain : MonoBehaviour, IPointerClickHandler
{
    public Text MainText;

    GameObject Toturial;
    GameObject select;
    public GameObject BuildBackGround;
    public GameObject SkillBackGround01;
    public GameObject Turn_Panel;
    GameObject Help;
    GameObject HelpImage;
    GameObject TurnButton;
    GameObject CostBackGround;

    private int TextNumber = 0;
    int IdleCost;

    void Start()
    {
        Toturial = GameObject.Find("Toturial");
        select = GameObject.Find("Select");
        //BuildBackGround = GameObject.Find("BuildBackGround");
        //SkillBackGround01 = GameObject.Find("SkillBackGround01");
        HelpImage = GameObject.Find("HelpImage");
        Help = GameObject.Find("Help");
        TurnButton = GameObject.Find("TurnButton");
        CostBackGround = GameObject.Find("CostBackGround");

        //Debug.Log(Toturial);
        //Debug.Log(select);
        //Debug.Log(BuildBackGround);
        //Debug.Log(SkillBackGround01);
        //Debug.Log(HelpImage);
        //Debug.Log(Help);
        //Debug.Log(TurnButton);
        //Debug.Log(CostBackGround);

        MainText.fontSize = 200;
        Toturial.SetActive(false);
        select.SetActive(false);
        HelpImage.SetActive(false);
        Turn_Panel.SetActive(true);

        MainText.text = "O!mok에 오신걸 환영합니다.";
        IdleCost = GameManager.Instance.MainCost;
        if (LoadGame.Instance.stage_Number == 1) { Toturial.SetActive(true); }
        else { SkipButton(); };
        ToturialText();
        BreakNumber();
    }

    void Update()
    {
        if (Toturial.activeSelf)
            BreakNumber();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (Toturial.activeSelf)
        {
            if (TextNumber == 5 || TextNumber == 8 || TextNumber == 12) return;
            else TextNumber++;

            ToturialText();
        }
    }

    void BreakNumber()
    {
        if (TextNumber == 5)
        {
            if (IdleCost > GameManager.Instance.MainCost)
            {
                TextNumber = 6;
                select.SetActive(false);
                MainText.text = "잘 하셨습니다!";
                IdleCost = GameManager.Instance.MainCost;
            }
        }
        else if (TextNumber == 8)
        {
            if (GameManager.Instance.mainTurn == 2)
            {
                TextNumber = 9;
                select.SetActive(false);
                MainText.text = "잘 하셨습니다!";
            }
        }
        else if (TextNumber == 12)
        {
            if (IdleCost > GameManager.Instance.MainCost)
            {
                TextNumber = 13;
                select.SetActive(false);
                MainText.text = "잘 하셨습니다!";
            }
        }
        else if (GameManager.Instance.mainTurn == 3)
        {
            Toturial.SetActive(false);
        }
    }

    void ToturialText()
    {
        switch (TextNumber)
        {
            case 1:
                MainText.text = "O!mok의 게임방법을 설명드리겠습니다.";
                break;
            case 2:
                MainText.fontSize = 150;
                MainText.text = "O!mok은 가로,세로, 대각선 중 한 방향으로 같은 색 돌 다섯개를\n연속적으로 먼저 늘어놓으면 승리하는 게임입니다.";
                break;
            case 3:
                MainText.fontSize = 200;
                MainText.text = "마우스를 드래그하여 돌을 놓을 수 있습니다. ";
                break;
            case 4:
                MainText.text = "돌은 한 차례에 한 번만 놓을 수 있습니다.";
                break;
            case 5:
                select.GetComponent<UpDown>().SetparentObject(BuildBackGround);
                select.SetActive(true);
                MainText.text = "돌을 놓아볼까요?";
                break;
                //
            case 6:
                select.SetActive(false);
                MainText.text = "잘 하셨습니다!";
                break;
            case 7:
                MainText.fontSize = 150;
                MainText.text = "각 돌이나 스킬을 누르면 설명을 볼 수 있습니다.";
                break;
            case 8:
                select.GetComponent<UpDown>().SetparentObject(TurnButton);
                select.SetActive(true);
                MainText.fontSize = 200;
                Turn_Panel.SetActive(false);
                MainText.text = "이번엔 턴을 넘겨 볼까요?";
                break;
                //
            case 9:
                select.SetActive(false);
                Turn_Panel.SetActive(true);
                MainText.text = "잘 하셨습니다!";
                break;
            case 10:
                MainText.text = "이번에는 스킬을 써 보겠습니다!";
                break;
            case 11:
                MainText.text = "스킬은 여러번 사용이 가능합니다!";
                break;
            case 12:
                select.GetComponent<UpDown>().SetparentObject(SkillBackGround01);
                select.SetActive(true);
                MainText.text = "마우스를 드래그하여 스킬을 사용하세요!";
                break;
                //
            case 13:
                select.SetActive(false);
                MainText.text = "잘 하셨습니다!";
                break;
            case 14:
                select.GetComponent<UpDown>().SetparentObject(CostBackGround);
                select.SetActive(true);
                MainText.fontSize = 150;
                MainText.text = "돌이나 스킬을 두려면 그에 맞는 비용이 필요하니 신중히 골라야 합니다!";
                break;
            case 15:
                select.GetComponent<UpDown>().SetparentObject(Help);
                MainText.text = "게임 중 궁금한것이 있으면 도움말을 눌러서 확인하세요!";
                break;
            case 16:
                select.SetActive(false);
                MainText.fontSize = 200;
                MainText.text = "그럼 게임을 시작해볼까요?";
                break;
            case 17:
                select.GetComponent<UpDown>().SetparentObject(TurnButton);
                select.SetActive(true);
                Turn_Panel.SetActive(false);
                MainText.text = "다음 턴을 눌러서 게임을 계속 진행하세요!";
                break;
        }
    }

    public void SkipButton()
    {
        TextNumber = 2000;
        Turn_Panel.SetActive(false);
        select.SetActive(false);
        Toturial.SetActive(false);
    }
    public void HelpButton()
    {
        HelpImage.SetActive(true);
    }
    public void HelpExitButton()
    {
        HelpImage.SetActive(false);
    }
}
