using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlocks : MonoBehaviour
{
    public int posY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DeckManager.Instance.mod)
        {
            this.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(this.GetComponent<RectTransform>().anchoredPosition, new Vector2(0, 0), Time.deltaTime * 10);
        }
        else
        {
            this.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(this.GetComponent<RectTransform>().anchoredPosition, new Vector2(0, posY), Time.deltaTime * 10);
        }
    }
}
