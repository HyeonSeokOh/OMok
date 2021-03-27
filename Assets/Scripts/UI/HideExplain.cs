using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HideExplain : MonoBehaviour, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.isexplain = false;
        GameManager.Instance.ExplainCheck(false);
    }
}
