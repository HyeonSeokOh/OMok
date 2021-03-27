using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallowHPbar : Ability
{
    public Slider Pslider;
    public Slider slider;
    public Text text;
    Vector3 sliderPos;
    void Start()
    {
        sliderPos = new Vector3(0, 0.4f, 0);

    }

    void Update()
    {
        Pslider.transform.position = Camera.main.WorldToScreenPoint(transform.position + sliderPos);
        if (this.gameObject.tag == "BlackCost01" || this.gameObject.tag == "WhiteCost01") { Cost01setting(); }
        else if (this.gameObject.tag == "BlackCost02" || this.gameObject.tag == "WhiteCost02") { Cost02setting(); }
        else if (this.gameObject.tag == "BlackCost03" || this.gameObject.tag == "WhiteCost03") { Cost03setting(); }
        else if (this.gameObject.tag == "BlackCost04" || this.gameObject.tag == "WhiteCost04") { Cost04setting(); }
        else if (this.gameObject.tag == "BlackCost05" || this.gameObject.tag == "WhiteCost05") { Cost05setting(); }

    }

    void Cost01setting()
    {
        Cost1Doll cost1doll = gameObject.GetComponent<Cost1Doll>();
        slider.maxValue = cost1doll.MaxHp;
        slider.value = cost1doll.GetHp();
    }
    void Cost02setting()
    {
        Cost2Doll cost2doll = gameObject.GetComponent<Cost2Doll>();
        slider.maxValue = cost2doll.MaxHp;
        slider.value = cost2doll.GetHp();
    }
    void Cost03setting()
    {
        Cost3Doll cost3doll = gameObject.GetComponent<Cost3Doll>();
        slider.maxValue = cost3doll.MaxHp;
        slider.value = cost3doll.GetHp();
    }
    void Cost04setting()
    {
        Cost4Doll cost4doll = gameObject.GetComponent<Cost4Doll>();
        slider.maxValue = cost4doll.MaxHp;
        slider.value = cost4doll.GetHp();
    }
    void Cost05setting()
    {
        Cost5Doll cost5doll = gameObject.GetComponent<Cost5Doll>();
        slider.maxValue = cost5doll.MaxHp;
        slider.value = cost5doll.GetHp();
    }
}
