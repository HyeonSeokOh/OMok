using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_03_Amplify : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (!target.GetComponent<Ability>().GetSmallAmplify())
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
