using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_06_Turn : MonoBehaviour
{
    public GameObject target;
    int turn = 0;
    int heal = 0;    // 너가 갚아야될 체력이다 시발아
    // Start is called before the first frame update
    void Start()
    {
        if (turn == 0)
            turn = GameManager.Instance.turn + 8;
    }

    // Update is called once per frame
    void Update()
    {

        if (turn == GameManager.Instance.turn)
        {
            if (target != null)
            {
                if (target.GetComponent<Ability>() != null)
                {
                    target.GetComponent<Ability>().RemoveMortage(turn);
                    //target.GetComponent<Ability>().MinusHp(heal, 1);
                    Destroy(this.gameObject);
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ability>() != null)
        {
            target = collision.gameObject;
            if (heal == 0)
            heal = target.GetComponent<Ability>().GetMortageHp(turn) / 2;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTrun(int turn_)
    {
        turn = turn_;
    }
    
    public void SetHeal(int heal_)
    {
        heal = heal_;
    }
    
}
