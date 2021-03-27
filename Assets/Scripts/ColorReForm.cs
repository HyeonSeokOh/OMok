using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorReForm : MonoBehaviour
{
    Color reformColor;

    void Start()
    {
        reformColor = this.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (this.GetComponent<SpriteRenderer>().color != reformColor)
        {
            this.GetComponent<SpriteRenderer>().color = Color.Lerp(this.GetComponent<SpriteRenderer>().color, reformColor, Time.deltaTime * 5f);
        }
    }
}
