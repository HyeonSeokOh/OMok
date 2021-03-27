using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HideUI : MonoBehaviour, IPointerClickHandler
{
    public RectTransform up, down, left;
    public Vector2 upTargetPos;
    public Vector2 downTargetPos;
    public Vector2 leftTargetPos;
    

    private Vector2 prevPosUp, prevPosDown, prevPosLeft;

    bool state = false;

    void Start()
    {
        prevPosDown = down.anchoredPosition;
        prevPosUp = up.anchoredPosition;
        prevPosLeft = left.anchoredPosition;
    }

    void Update()
    {
        if (state)
        {
            up.anchoredPosition = Vector3.Lerp(up.anchoredPosition, prevPosUp + upTargetPos, 5f * Time.deltaTime);
            down.anchoredPosition = Vector3.Lerp(down.anchoredPosition, prevPosDown + downTargetPos, 5f * Time.deltaTime);
            left.anchoredPosition = Vector3.Lerp(left.anchoredPosition, prevPosLeft + leftTargetPos, 5f * Time.deltaTime);
        }
        else
        {
            up.anchoredPosition = Vector3.Lerp(up.anchoredPosition, prevPosUp, 2.5f * Time.deltaTime);
            down.anchoredPosition = Vector3.Lerp(down.anchoredPosition, prevPosDown, 2.5f * Time.deltaTime);
            left.anchoredPosition = Vector3.Lerp(left.anchoredPosition, prevPosLeft, 2.5f * Time.deltaTime);
        }

    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount % 2 == 0)   
        {
            state = !state;
        }    
    }
}
