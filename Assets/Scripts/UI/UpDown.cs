using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpDown : MonoBehaviour
{
    float move;
    float time;
    Vector2 pos;
    void Start()
    {
        move = 10f;
        pos = this.GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        Vector2 dir = new Vector2(0, move);

        time = time + Time.deltaTime;

        if (time <= 0.7f)
        {
            this.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(this.GetComponent<RectTransform>().anchoredPosition, pos + dir, Time.deltaTime * 5f);
        }
        else
        {
            time = 0;
            move = move * -1;
        }
    }

    public void SetparentObject(GameObject obj)
    {
        this.GetComponent<RectTransform>().transform.SetParent(obj.GetComponent<RectTransform>().transform);
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 40);
        pos = this.GetComponent<RectTransform>().anchoredPosition;
    }
}
