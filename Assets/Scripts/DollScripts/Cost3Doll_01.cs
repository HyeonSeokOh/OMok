using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cost3Doll_01 : MonoBehaviour
{
    public GameObject cost1Doll;
    bool isLock;
    public bool whatcolors;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        isLock = false;
        //whatcolors = false;
        if (this.GetComponent<Animator>() != null)
        {
            ani = this.GetComponent<Animator>();
        }

        cost1Doll = null;
    }

    public void WhatColor(bool color_)
    {
        if (color_)
            whatcolors = true;
        //cost1Doll = Resources.Load("BlackCost03") as GameObject;
        else
            whatcolors = false;
            //cost1Doll = Resources.Load("WhiteCost03") as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(whatcolors);

        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f && !isLock)
        {
            if (whatcolors)
                cost1Doll = Resources.Load("BlackCost01") as GameObject;
            else
                cost1Doll = Resources.Load("WhiteCost01") as GameObject;

            GameObject ins = Instantiate(cost1Doll, new Vector3(this.transform.position.x, this.transform.position.y, -1f), Quaternion.identity);
            isLock = true;
        }
    }
}
