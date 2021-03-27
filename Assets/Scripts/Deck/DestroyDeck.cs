using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DestroyDeck : MonoBehaviour, IPointerDownHandler
{
    public Image nowImage;
    public Sprite ChangeSprite;


    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)  // 버튼을 클릭하는 순간 실행
    {
        

        int tag = int.Parse(this.transform.parent.tag) - 1;

        if (DeckManager.Instance.mod)
        {
            if (DeckManager.Instance.stone_Block[tag].transform.GetChild(0).GetComponent<Image>().sprite != ChangeSprite)
            {
                DeckManager.Instance.stone_Block[tag].transform.GetChild(0).GetComponent<Image>().sprite = ChangeSprite;
                DeckManager.Instance.stone_Preset[DeckManager.Instance.preset, tag] = ChangeSprite;
                DeckManager.Instance.save_Stone[DeckManager.Instance.preset, tag] = -2000;
                Debug.Log(tag);
            }
        }
        else if (!DeckManager.Instance.mod)
        {
            if (DeckManager.Instance.skill_Block[tag].transform.GetChild(0).GetComponent<Image>().sprite != ChangeSprite)
            {
                DeckManager.Instance.skill_Block[tag].transform.GetChild(0).GetComponent<Image>().sprite = ChangeSprite;
                DeckManager.Instance.skill_Preset[DeckManager.Instance.preset, tag] = ChangeSprite;
                DeckManager.Instance.save_Skill[DeckManager.Instance.preset, tag] = -2000;
                Debug.Log(tag);
            }
        }


        nowImage.sprite = ChangeSprite;
    }
}
