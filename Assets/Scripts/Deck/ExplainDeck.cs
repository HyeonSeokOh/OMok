using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplainDeck : MonoBehaviour
{
    public Text text;
    string explainDeckStr;

    private static ExplainDeck instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static ExplainDeck Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public string CreateDeckText()
    {

        switch (DeckManager.Instance.DeckTextType)
        {
            //흑돌 텍스트 입력

            case 0:
                explainDeckStr = "";
                break;
            case 1:
                explainDeckStr = "비용 : 1 \n체력 : 10 \n아무 효과 없음 ";
                break;
            case 2:
                explainDeckStr = "비용 : 2 \n체력 : 20 \n돌을 놓을 시, 일정 확률로 최대 체력이 10 증가합니다.";
                break;
            case 3:
                explainDeckStr = "비용 : 3 \n체력 : 30 \n2턴에 한번 씩, 주위에 비용이 1인 기본 돌을 생성합니다.";
                break;
            case 4:
                explainDeckStr = "비용 : 4 \n체력 : 40 \n돌을 놓을 시, 자기 자신에게 실드를 생성합니다. 실드의 지속시간은 주위에 존재하는 아군 돌의 수만큼입니다.";
                break;
            case 5:
                explainDeckStr = "비용 : 5 \n체력 : 150 \n돌을 놓을 시, 피해를 2배 입도록 하는 영역을 생성합니다.";
                break;

            //스킬 텍스트 입력
            case 1001:
                explainDeckStr = "비용 : 1 \n피해를 6 줍니다.";
                break;
            case 1002:
                explainDeckStr = "비용 : 1 \n체력을 10 회복 시킵니다.";
                break;
            case 1003:
                explainDeckStr = "비용 : 2 \n표식을 부여합니다. 부여된 돌은 1회에 한해서 피해를 2배로 받습니다.";
                break;
            case 1004:
                explainDeckStr = "비용 : 2 \n피해를 10 줍니다.";
                break;
            case 1005:
                explainDeckStr = "비용 : 2 \n피해를 10 줍니다.";
                break;
            case 1006:
                explainDeckStr = "비용 : 3 \n표식을 부여합니다. 부여할 때의 체력을 기준으로 체력을 2턴간 2배로 올립니다. 원상태로 돌아와도 체력이 1 아래로 떨어지진 않습니다.";
                break;
            case 1007:
                explainDeckStr = "비용 : 3 \n실드를 부여합니다. 1회에 한해서 피해를 막습니다.";
                break;
            case 1008:
                explainDeckStr = "비용 : 4 \n피해를 25 줍니다.";
                break;
            case 1009:
                explainDeckStr = "비용 : 4 \n피해를 25 줍니다. 물속성 타일에서만 사용이 가능합니다.";
                break;
            case 1010:
                explainDeckStr = "비용 : 5 \n피해를 40 줍니다. 1턴 후에 발동되며, 발동 위치는 범위 내에서 랜덤하게 정해집니다.";
                break;
        }

        text.text = explainDeckStr;

        return explainDeckStr;
    }

    public void DeckInGameText(int DecknowHp, int DecktagNum)
    {
        DeckManager.Instance.DeckTextType = DecktagNum;
        string str = CreateDeckText();
    }
}
