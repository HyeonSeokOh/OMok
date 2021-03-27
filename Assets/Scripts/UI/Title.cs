using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Title : MonoBehaviour, IPointerClickHandler
{

    public GameObject GroundBack;
    public GameObject ForestBack;
    public GameObject WaterBack;
    public GameObject IceBack;
    public RectTransform ClickMove;
    public GameObject Clone; // 클릭시 생성할 이펙트 생성
    public int FindRandomType; // TitleEffect에서 RandomOmok값 가져오기

    GameObject NewGame;
    GameObject LoadGame;
    GameObject Option;
    GameObject Omok;
    GameObject Stone;
    GameObject exit;
    GameObject CloneArea;
    bool State = false;
    bool OmokType;

    public Vector2 MovePos;
    Vector2 PrevPos;
    void Start()
    {
        int RandomBackGround = Random.Range(0, 4);
        int RandomOmok = Random.Range(0, 2);
        NewGame = GameObject.Find("NewGame");
        LoadGame = GameObject.Find("LoadGame");
        Option = GameObject.Find("Option");
        Omok = GameObject.Find("Omok");
        Stone = GameObject.Find("Stone");
        exit = GameObject.Find("Exit");
        CloneArea = GameObject.Find("CloneArea");

        PrevPos = ClickMove.anchoredPosition;


        GroundBack.SetActive(false);
        ForestBack.SetActive(false);
        WaterBack.SetActive(false);
        IceBack.SetActive(false);


        switch (RandomBackGround)
        {
            case 0: GroundBack.SetActive(true); break;
            case 1: ForestBack.SetActive(true); break;
            case 2: WaterBack.SetActive(true); break;
            case 3: IceBack.SetActive(true); break;
        }

        switch (RandomOmok)
        {
            case 0: Stone.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/black_Doll", typeof(Sprite)) as Sprite; OmokType = true; break;
            case 1: Stone.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/white_Doll", typeof(Sprite)) as Sprite; OmokType = false; break;
        }

        if(OmokType == false) // 오목 글자가 처음부터 파란색으로 나올 때
        {
            Omok.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/OmokB", typeof(Sprite)) as Sprite;
            NewGame.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/NewGameB", typeof(Sprite)) as Sprite;
            LoadGame.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/LoadGameB", typeof(Sprite)) as Sprite;
            Option.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/OptionB", typeof(Sprite)) as Sprite;
            exit.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(99f / 255f, 155f / 255f, 255f / 255f, 255f / 255f);
        }
        FindRandomType = RandomOmok;

    }

    void Update()
    {
        if(State)
        {
            ClickMove.anchoredPosition = Vector3.Lerp(ClickMove.anchoredPosition, PrevPos + MovePos, 5f * Time.deltaTime);
        }
        else
            ClickMove.anchoredPosition = Vector3.Lerp(ClickMove.anchoredPosition, PrevPos, 2.5f * Time.deltaTime);
    }


    public void ChangeNewGame()
    {
        if (OmokType == true)
        { NewGame.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/NewGameB", typeof(Sprite)) as Sprite; }
        else NewGame.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/NewGameR", typeof(Sprite)) as Sprite;
    }
    public void DefaultNewGame()
    {
        if (OmokType == true)
        { NewGame.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/NewGameR", typeof(Sprite)) as Sprite; }
        else NewGame.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/NewGameB", typeof(Sprite)) as Sprite;
    }

    public void ChangeLoadGame()
    {
        if (OmokType == true)
        { LoadGame.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/LoadGameB", typeof(Sprite)) as Sprite; }
        else LoadGame.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/LoadGameR", typeof(Sprite)) as Sprite;
    }
    public void DefaultRoadGame()
    {
        if (OmokType == true)
        { LoadGame.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/LoadGameR", typeof(Sprite)) as Sprite; }
        else LoadGame.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/LoadGameB", typeof(Sprite)) as Sprite;
    }

    public void ChangeOption()
    {
        if (OmokType == true)
        { Option.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/OptionB", typeof(Sprite)) as Sprite; }
        else Option.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/OptionR", typeof(Sprite)) as Sprite;
    }
    public void DefaultOption()
    {
        if (OmokType == true)
        { Option.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/OptionR", typeof(Sprite)) as Sprite; }
        else Option.GetComponent<Image>().sprite = Resources.Load("TitleBackGround/Other/OptionB", typeof(Sprite)) as Sprite;
    }

    public void exitColor()
    {
        if (exit.transform.GetChild(0).gameObject.GetComponent<Text>().color == new Color(99f / 255f, 155f / 255f, 255f / 255f, 255f / 255f))
            exit.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(170f / 255f, 8f / 255f, 8f / 255f, 255f / 255f);
        else
            exit.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(99f / 255f, 155f / 255f, 255f / 255f, 255f / 255f);

    }

    public void DonwExitColor()
    {
        exit.transform.GetChild(0).gameObject.GetComponent<Text>().color = exit.transform.GetChild(0).gameObject.GetComponent<Text>().color + new Color(30f / 255f, 30f / 255f, 30f / 255f);
    }
    public void UpExitColor()
    {
        exit.transform.GetChild(0).gameObject.GetComponent<Text>().color = exit.transform.GetChild(0).gameObject.GetComponent<Text>().color - new Color(30f / 255f, 30f / 255f, 30f / 255f);
    }

    //캔버스의 클릭 카운터이기 때문에 어느곳을 클릭해도 바뀜
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        State = !State;
        GameObject a = Instantiate(Clone, new Vector2(Stone.transform.position.x, Stone.transform.position.y), Quaternion.identity) as GameObject;
        a.transform.SetParent(CloneArea.transform);
    }

    public void end()
    {
        Application.Quit();
    }
}
