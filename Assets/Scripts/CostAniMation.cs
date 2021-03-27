using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostAniMation : MonoBehaviour
{
    Sprite ActCost;

    Animator Ani;

    GameObject CostImage;
    string Costtag;
    private void Start()
    {
        Costtag = this.tag;
        Ani = GetComponent<Animator>();
    }

    void Update()
    {

        ShowCost();
        ShowCostEffect();
    }


    public void ShowCostEffect()
    {
        ActCost = this.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<Image>().sprite = ActCost;
    }

    
    void ShowCost()
    {
        switch (GameManager.Instance.MainCost)
        {
            case 0:
                if (this.tag == "Cost05") { Ani.SetBool("DesCost", true); }
                if (this.tag == "Cost04") { Ani.SetBool("DesCost", true); }
                if (this.tag == "Cost03") { Ani.SetBool("DesCost", true); }
                if (this.tag == "Cost02") { Ani.SetBool("DesCost", true); }
                if (this.tag == "Cost01") { Ani.SetBool("DesCost", true); }
                break;

            case 1:
                if (this.tag == "Cost05") { Ani.SetBool("DesCost", true); }
                if (this.tag == "Cost04") { Ani.SetBool("DesCost", true); }
                if (this.tag == "Cost03") { Ani.SetBool("DesCost", true); }
                if (this.tag == "Cost02") { Ani.SetBool("DesCost", true); }
                break;

            case 2:
                if (this.tag == "Cost05") { Ani.SetBool("DesCost", true); }
                if (this.tag == "Cost04") { Ani.SetBool("DesCost", true); }
                if (this.tag == "Cost03") { Ani.SetBool("DesCost", true); }
                break;

            case 3:
                if (this.tag == "Cost05") { Ani.SetBool("DesCost", true); }
                if (this.tag == "Cost04") { Ani.SetBool("DesCost", true); }
                break;

            case 4:
                if (this.tag == "Cost05") { Ani.SetBool("DesCost", true); }
                break;

            case 5:
                if (this.tag == "Cost01") { Ani.SetBool("DesCost", false); }
                if (this.tag == "Cost02") { Ani.SetBool("DesCost", false); }
                if (this.tag == "Cost03") { Ani.SetBool("DesCost", false); }
                if (this.tag == "Cost04") { Ani.SetBool("DesCost", false); }
                if (this.tag == "Cost05") { Ani.SetBool("DesCost", false); }
                break;
        }

    }
}
