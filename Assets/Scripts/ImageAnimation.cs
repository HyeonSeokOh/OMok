using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageAnimation : MonoBehaviour
{
    Sprite ActCost;
    Animator Ani;

    void Start()
    {
        Ani = GetComponent<Animator>();
    }

    void Update()
    {
        ActCost = this.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<Image>().sprite = ActCost;
    }

}
