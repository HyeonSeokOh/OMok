using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollMove : MonoBehaviour
{
    private static DollMove instance_;
    public static DollMove Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<DollMove>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("Main Camera").AddComponent<DollMove>();
                    instance_ = newSingleton;
                }
            }
            return instance_;
        }
        private set
        {
            instance_ = value;
        }
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<DollMove>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }

    float MaxDistance = 15f;
    public static Vector3 MousePosition;
    Camera cam;

    public int move_Object_Tag = 0;
    public GameObject skill_obj;

    public int Xup;
    public int Yup;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        MousePosition = Input.mousePosition;
        MousePosition = cam.ScreenToWorldPoint(MousePosition);

        RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);
        if (hit && GameManager.Instance.Dragging == true) // hit는 충돌이 감지된 블록 && 드래깅 체크
        {
            Xup = Mathf.RoundToInt(hit.point.x);
            Yup = Mathf.RoundToInt(hit.point.y);
            Vector3 point = new Vector3(Xup, Yup + 0.3f, 0);

            if (move_Object_Tag < 1000)
                TileManager.TileMain[Xup, Yup].TileStyle.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else    // 충돌감지 안되면 일루 넘어갑니다.
        {
            Xup = -1;
            Yup = -1;
        }


    }
}