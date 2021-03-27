using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWeight : MonoBehaviour
{
    int i;
    // Start is called before the first frame update
    void Start()
    {
        i = GameManager.Instance.mainTurn;
    }

    // Update is called once per frame
    void Update()
    {
        if (i != GameManager.Instance.mainTurn)
        {
            Destroy(this.gameObject);
        }
    }
}
