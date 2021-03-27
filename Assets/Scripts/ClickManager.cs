using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour, IPointerClickHandler
{

    public bool isClick;
    bool isMove;
    GameObject mask_Preset;
    GameObject stat;
    // Start is called before the first frame update
    void Start()
    {
        isClick = false;
        isMove = false;
        mask_Preset = GameObject.Find("Preset_Bundle");
        stat = GameObject.Find("Statistics").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            mask_Preset.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(mask_Preset.GetComponent<RectTransform>().anchoredPosition, new Vector2(-140, 0), Time.deltaTime * 10f);
        }
        else
        {
            mask_Preset.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(mask_Preset.GetComponent<RectTransform>().anchoredPosition, new Vector2(0, 0), Time.deltaTime * 10f);
        }
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        MovePrePareWIndow.Instance.CloseWindow();
        CameraFocus.Instance.moveLock = true;
    }

    public void movePresetStat()
    {
        if (isMove)
        {
            isMove = false;
            stat.GetComponent<UnityEngine.UI.Text>().text = "Statistics";
        }
        else
        {
            isMove = true;
            stat.GetComponent<UnityEngine.UI.Text>().text = "Preset";

        }
    }

}
