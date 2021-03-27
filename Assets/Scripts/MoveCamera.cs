using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCamera : MonoBehaviour
{
    private float dist;
    private Vector3 MouseStart;
    private Vector3 derp;
    
    Vector3 CameraPos;

    public float MinPosX = -10f;
    public float MaxPosX = 10f;
    public float MinPosY = -10f;
    public float MaxPosY = 10;
    public bool showGizmos = true;
    public Vector2 vectr;
    void Start()
    {
        this.transform.position = vectr;
        dist = transform.position.z;
        CameraPos = this.transform.position;
    }

    void Update()
    {
        CameraMove();
        MaxCamera();
    }

    void MaxCamera()
    {
        Vector3 temp;
        temp.x = Mathf.Clamp(transform.position.x, MinPosX, MaxPosX);
        temp.y = Mathf.Clamp(transform.position.y, MinPosY, MaxPosY);
        temp.z = -10;
        transform.position = temp;
    }
    



    void CameraMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
            MouseStart.z = transform.position.z;

        }
        else if (Input.GetMouseButton(0))
        {
            var MouseMove = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
            MouseMove.z = transform.position.z;
            transform.position = transform.position - (MouseMove - MouseStart);
        }

    }
}
