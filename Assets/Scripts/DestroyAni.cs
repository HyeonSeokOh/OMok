using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAni : MonoBehaviour
{
    Animator ani;

    void Start()
    {
        if (this.GetComponent<Animator>() != null)
        {
            ani = this.GetComponent<Animator>();
        }

    }

    void Update()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(this.gameObject);
        }
    }
}
