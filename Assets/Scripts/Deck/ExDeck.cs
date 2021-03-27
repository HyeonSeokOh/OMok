using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExDeck : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler    // 돌 설명
{
    string GrandMother;

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        FindTextType2();
        DeckManager.Instance.Deckisexplain = true;
        ExplainDeck.Instance.CreateDeckText();
    }

    void FindTextType2()
    {
        GrandMother = this.transform.parent.gameObject.transform.parent.name; //부모의 부모 오브젝트의 이름

        if (GrandMother == "Stones")
        {
            if (this.transform.parent.tag == "1") { DeckManager.Instance.DeckTextType = 1; }
            else if (this.transform.parent.tag == "2") { DeckManager.Instance.DeckTextType = 2; }
            else if (this.transform.parent.tag == "3") { DeckManager.Instance.DeckTextType = 3; }
            else if (this.transform.parent.tag == "4") { DeckManager.Instance.DeckTextType = 4; }
            else if (this.transform.parent.tag == "5") { DeckManager.Instance.DeckTextType = 5; }
        }

        if (GrandMother == "Skills")
        {
            if (DeckManager.Instance.page == 0)
            {
                if (this.transform.parent.tag == "1") { DeckManager.Instance.DeckTextType = 1001; }
                else if (this.transform.parent.tag == "2") { DeckManager.Instance.DeckTextType = 1002; }
                else if (this.transform.parent.tag == "3") { DeckManager.Instance.DeckTextType = 1003; }
                else if (this.transform.parent.tag == "4") { DeckManager.Instance.DeckTextType = 1004; }
                else if (this.transform.parent.tag == "5") { DeckManager.Instance.DeckTextType = 1005; }
                else if (this.transform.parent.tag == "6") { DeckManager.Instance.DeckTextType = 1006; }
                else if (this.transform.parent.tag == "7") { DeckManager.Instance.DeckTextType = 1007; }
                else if (this.transform.parent.tag == "8") { DeckManager.Instance.DeckTextType = 1008; }
            }

            if (DeckManager.Instance.page == 1)
            {
                if (this.transform.parent.tag == "1") { DeckManager.Instance.DeckTextType = 1009; }
                else if (this.transform.parent.tag == "2") { DeckManager.Instance.DeckTextType = 1010; }
            }
        }
    }
}
