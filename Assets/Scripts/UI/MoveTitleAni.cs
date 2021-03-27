using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTitleAni : MonoBehaviour
{
    public RectTransform Stone;
    public Vector2 MovePos;
    int MoveSpeed;

    private void Start()
    {
        MoveSpeed = 2;
    }

    void Update()
    {
        Stone.anchoredPosition = Vector2.Lerp(Stone.anchoredPosition, MovePos, Time.deltaTime * MoveSpeed);
    }

}
