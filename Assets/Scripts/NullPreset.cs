using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullPreset : MonoBehaviour
{
    public GameObject[] iO;

    private void Start()
    {

    }

    void Update()
    {
        NullPre();      
    }

    public void NullPre()
    {
        for (int n = 0; n < 4; n++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (LoadGame.Instance.preset_Skill_Card[n, j] <= 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (LoadGame.Instance.preset_Block_Card[n, i] <= 0)
                        {
                            iO[n].SetActive(true);
                        }
                    }
                }
            }

            for (int j = 0; j < 10; j++)
            {
                if (LoadGame.Instance.preset_Skill_Card[n, j] > 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (LoadGame.Instance.preset_Block_Card[n, i] > 0)
                        {
                            iO[n].SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
