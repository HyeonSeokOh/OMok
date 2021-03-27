using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExplain : MonoBehaviour
{
    public static Vector3 MousePosition;
    float MaxDistance = 15f;
    Camera cam;


    struct DollStatus
    {
        public int nowHp;
        public int tagNum;
    };

    DollStatus dollStatus;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition = Input.mousePosition;
        MousePosition = cam.ScreenToWorldPoint(MousePosition);

        RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "BlackCost01" || hit.collider.tag == "BlackCost02" || hit.collider.tag == "BlackCost03" || hit.collider.tag == "BlackCost04" || hit.collider.tag == "BlackCost05" ||
                hit.collider.tag == "WhiteCost01" || hit.collider.tag == "WhiteCost02" || hit.collider.tag == "WhiteCost03" || hit.collider.tag == "WhiteCost04" || hit.collider.tag == "WhiteCost05")
            {
                dollStatus.nowHp = hit.collider.GetComponent<Ability>().GetHp();
                dollStatus.tagNum = hit.collider.GetComponent<Ability>().GetDollTag();
                GameManager.Instance.isexplain = false;
                GameManager.Instance.ExplainCheck(true);

                TextManager.Instance.InGameText(dollStatus.nowHp, dollStatus.tagNum);
            }
            else
                GameManager.Instance.ExplainCheck(false);
        }
    }

}
