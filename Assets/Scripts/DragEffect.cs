using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragEffect : CreateDoll, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static Vector3 defaultPosition;
    GameObject StopCamera;
    GameObject ins;
    CostType drag_Cost;
    string Obj_name;
    int cost = 0;
    bool costLock = false;
    bool CreateLock = false;
    public void Start()
    {
        StopCamera = GameObject.Find("Main Camera");


        if (System.Convert.ToInt32(this.tag) > 1000)
            Obj_name = "Skill Effect/" + this.name;
        else
            Obj_name = this.name;
        //this.tag = "1"; // 임시로 줘봤음 된다.

        Object_Name = Resources.Load(Obj_name) as GameObject;
        DollMove.Instance.skill_obj = Object_Name;

        SettingImage();
    }
    public void Update()
    {
        
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) // 드래그 시작할때
    {
        defaultPosition = this.transform.position;
        if (this.GetComponent<Skills>() != null)
            cost = this.GetComponent<Skills>().GetSkillCost();
    }

    //범위 내에서 생성 막기
    void OnTriggerEnter2D(Collider2D other) { if (other.name == "Panel2") CreateLock = true; }
    void OnTriggerExit2D(Collider2D other) { if (other.name == "Panel2") CreateLock = false; }

    void IDragHandler.OnDrag(PointerEventData eventData) //드래그 중일때
    {
        DollMove.Instance.move_Object_Tag = System.Convert.ToInt32(this.tag);

        Vector2 currentPos = Input.mousePosition;
        this.transform.position = currentPos;
        GameManager.Instance.Dragging = true;
        //Debug.Log(DollMove.Instance.move_Object_Tag);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) //드래그 끝날 때
    {
        //GameManager.Instance.colorLock = true;
        StopCamera.GetComponent<MoveCamera>().enabled = true;   // 카메라의 이동을 풀음
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        this.transform.position = defaultPosition;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.Dragging = false;
        }

        if (CreateLock == false)
        {
            ListBlackDoll();
            //ListWhiteDoll();
            if (System.Convert.ToInt32(this.tag) > 1000)
            {
                if (GameManager.Instance.MainCost - cost >= 0 && GameManager.Instance.mainTurn < 3)
                    CreateSkill();
            }
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)  // 버튼을 클릭하는 순간 실행
    {
        StopCamera.GetComponent<MoveCamera>().enabled = false;  // 카메라의 이동을 막음
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        StopCamera.GetComponent<MoveCamera>().enabled = true;

        FindTextType();
        GameManager.Instance.isexplain = true;
        GameManager.Instance.ExplainCheck(true);
        TextManager.Instance.CreateText();
    }

    void ListBlackDoll()
    {
        if (this.gameObject == GameObject.Find("BlackCost01")) { drag_Cost = CostType.Cost01;  CreateBlackdoll(drag_Cost); }
        else if (this.gameObject == GameObject.Find("BlackCost02")) { drag_Cost = CostType.Cost02; CreateBlackdoll(drag_Cost); }
        else if (this.gameObject == GameObject.Find("BlackCost03")) { drag_Cost = CostType.Cost03; CreateBlackdoll(drag_Cost); }
        else if (this.gameObject == GameObject.Find("BlackCost04")) { drag_Cost = CostType.Cost04; CreateBlackdoll(drag_Cost); }
        else if (this.gameObject == GameObject.Find("BlackCost05")) { drag_Cost = CostType.Cost05; CreateBlackdoll(drag_Cost); }
    }

    //void ListWhiteDoll()
    //{
    //    if (this.gameObject == GameObject.Find("WhiteCost01")) { drag_Cost = CostType.Cost01; CreateWhitedoll(drag_Cost); }
    //    else if (this.gameObject == GameObject.Find("WhiteCost02")) { drag_Cost = CostType.Cost02; CreateWhitedoll(drag_Cost); }
    //    else if (this.gameObject == GameObject.Find("WhiteCost03")) { drag_Cost = CostType.Cost03; CreateWhitedoll(drag_Cost); }
    //    else if (this.gameObject == GameObject.Find("WhiteCost04")) { drag_Cost = CostType.Cost04; CreateWhitedoll(drag_Cost); }
    //    else if (this.gameObject == GameObject.Find("WhiteCost05")) { drag_Cost = CostType.Cost05; CreateWhitedoll(drag_Cost); }
    //}


    void CreateSkill()
    {
        for (int i = 0; i < TileManager.Instance.BoardSizeX; i++)
        {
            for (int j = 0; j < TileManager.Instance.BoardSizeY; j++)
            {
                if (TileManager.TileMain[i, j].TileStyle.GetComponent<SpriteRenderer>().color == Color.red)
                {
                    ins = Instantiate(Object_Name, new Vector2(i, j), Quaternion.identity) as GameObject;
                    costLock = true;
                }
            }
        }

        if (costLock)
        {
            GameManager.Instance.MainCost -= cost;
            costLock = false;
        }
    }

    void FindTextType()
    {
        if (this.gameObject == GameObject.Find("BlackCost01")) { GameManager.Instance.TextType = int.Parse(this.gameObject.tag); }
        else if (this.gameObject == GameObject.Find("BlackCost02")) { GameManager.Instance.TextType = int.Parse(this.gameObject.tag); }
        else if (this.gameObject == GameObject.Find("BlackCost03")) { GameManager.Instance.TextType = int.Parse(this.gameObject.tag); }
        else if (this.gameObject == GameObject.Find("BlackCost04")) { GameManager.Instance.TextType = int.Parse(this.gameObject.tag); }
        else if (this.gameObject == GameObject.Find("BlackCost05")) { GameManager.Instance.TextType = int.Parse(this.gameObject.tag); }

        //else if (this.gameObject == GameObject.Find("WhiteCost01")) { GameManager.Instance.TextType = 1; }
        //else if (this.gameObject == GameObject.Find("WhiteCost02")) { GameManager.Instance.TextType = 2; }
        //else if (this.gameObject == GameObject.Find("WhiteCost03")) { GameManager.Instance.TextType = 3; }
        //else if (this.gameObject == GameObject.Find("WhiteCost04")) { GameManager.Instance.TextType = 4; }
        //else if (this.gameObject == GameObject.Find("WhiteCost05")) { GameManager.Instance.TextType = 5; }

        else if (this.gameObject == GameObject.FindGameObjectWithTag("1001")) { GameManager.Instance.TextType = this.gameObject.GetComponent<Skills>().getSkillNumber(); }
        else if (this.gameObject == GameObject.FindGameObjectWithTag("1002")) { GameManager.Instance.TextType = this.gameObject.GetComponent<Skills>().getSkillNumber(); }
        else if (this.gameObject == GameObject.FindGameObjectWithTag("1003")) { GameManager.Instance.TextType = this.gameObject.GetComponent<Skills>().getSkillNumber(); }
        else if (this.gameObject == GameObject.FindGameObjectWithTag("1004")) { GameManager.Instance.TextType = this.gameObject.GetComponent<Skills>().getSkillNumber(); }
        else if (this.gameObject == GameObject.FindGameObjectWithTag("1005")) { GameManager.Instance.TextType = this.gameObject.GetComponent<Skills>().getSkillNumber(); }

        else if (this.gameObject == GameObject.FindGameObjectWithTag("1006")) { GameManager.Instance.TextType = this.gameObject.GetComponent<Skills>().getSkillNumber(); }
        else if (this.gameObject == GameObject.FindGameObjectWithTag("1007")) { GameManager.Instance.TextType = this.gameObject.GetComponent<Skills>().getSkillNumber(); }
        else if (this.gameObject == GameObject.FindGameObjectWithTag("1008")) { GameManager.Instance.TextType = this.gameObject.GetComponent<Skills>().getSkillNumber(); }
        else if (this.gameObject == GameObject.FindGameObjectWithTag("1009")) { GameManager.Instance.TextType = this.gameObject.GetComponent<Skills>().getSkillNumber(); }
        else if (this.gameObject == GameObject.FindGameObjectWithTag("1010")) { GameManager.Instance.TextType = this.gameObject.GetComponent<Skills>().getSkillNumber(); }

    }

    void SettingImage()
    {
        CircleCollider2D col = this.gameObject.AddComponent<CircleCollider2D>();
        Rigidbody2D rig = this.gameObject.AddComponent<Rigidbody2D>();
        col.radius = 12;
        col.isTrigger = true;

        rig.gravityScale = 0;
    }
}