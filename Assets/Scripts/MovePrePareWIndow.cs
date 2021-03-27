using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePrePareWIndow : MonoBehaviour
{
    private static MovePrePareWIndow instance_;
    public static MovePrePareWIndow Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<MovePrePareWIndow>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("GamePrepare").AddComponent<MovePrePareWIndow>();
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
        var objs = FindObjectsOfType<MovePrePareWIndow>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
        isMove = false;
        page_button[0] = Resources.Load("Stage_Select/page_0") as GameObject;
        page_button[1] = Resources.Load("Stage_Select/page_1") as GameObject;

        isMove = false;
        page_image = this.transform.GetChild(4).GetComponent<Image>();
        page[0] = MovePrePareWIndow.Instance.page_button[0];
        page[1] = MovePrePareWIndow.Instance.page_button[1];
        page_image.sprite = page[0].GetComponent<SpriteRenderer>().sprite;
    }
    GameObject[] page = new GameObject[2];
    Image page_image;

    public bool isMove;
    public GameObject[] page_button = new GameObject[2];
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (isMove)
            this.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(this.GetComponent<RectTransform>().anchoredPosition, new Vector3(178, 0, 0), Time.deltaTime * 10f);
        else
            this.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(this.GetComponent<RectTransform>().anchoredPosition, new Vector3(336, 0, 0), Time.deltaTime * 10f);

    }

    public void ChangeImage()
    {
        if (page_image.sprite == page[0].GetComponent<SpriteRenderer>().sprite)
        {
            OpenWindow();
        }
        else if (page_image.sprite == page[1].GetComponent<SpriteRenderer>().sprite)
        {
            CloseWindow();
        }
    }

    public void OpenWindow()
    {
        page_image.sprite = page[1].GetComponent<SpriteRenderer>().sprite;
        MovePrePareWIndow.Instance.isMove = true;
    }
    public void CloseWindow()
    {
        page_image.sprite = page[0].GetComponent<SpriteRenderer>().sprite;
        MovePrePareWIndow.Instance.isMove = false;
    }
}
