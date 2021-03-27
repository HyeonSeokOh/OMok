using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClick : MonoBehaviour // 스테이지 클릭 스크립트
{
    float MaxDistance = 15f;
    public static Vector3 MousePosition;
    Camera cam;
    GameObject select;
    GameObject Prepare;

    // Start is called before the first frame update
    void Start()
    {
        select = GameObject.Find("Select");
        select.SetActive(false);
        cam = GetComponent<Camera>();
        Prepare = GameObject.Find("GamePrepare");
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition = Input.mousePosition;
        MousePosition = cam.ScreenToWorldPoint(MousePosition);

        RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "stage" && Input.GetMouseButtonUp(0))
            {
                bool islock = false;

                if (Prepare.GetComponent<RectTransform>().anchoredPosition.x < 200) // 가려지면 스테이지 클릭 안됨
                {
                    if (hit.collider.transform.position.x < 4)
                    {
                        islock = true;
                    }
                }
                else if (hit.collider.GetComponent<CreateNumber>().GetStageNumber() == 0)
                {
                    islock = false;
                }
                else
                    islock = true;

                if (islock)
                {
                    MovePrePareWIndow.Instance.OpenWindow();
                    CameraFocus.Instance.MovePos = hit.collider.transform.position;
                    CameraFocus.Instance.moveLock = false;
                    hit.collider.gameObject.GetComponent<CreateNumber>().SetStageVar();

                    select.SetActive(true);
                    select.GetComponent<MoveUpDown>().SetparentObject(hit.collider.gameObject);
                }
            }
        }
        
    }

}
