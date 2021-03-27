using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{

    //public GameObject OptionScene;
    GameObject StopCamera;

    private void Start()
    {
        StopCamera = GameObject.Find("Main Camera");
    }

    private void Update()
    {

    }

    public void OptionButton() //옵션버튼
    {
        //OptionScene.SetActive(true);
        if (StopCamera.GetComponent<MoveCamera>() != null)
            StopCamera.GetComponent<MoveCamera>().enabled = false;

        AudioManager.Instance.openOption();
        //HideUI.Instance.state = false;
    }

    public void ExitButton() //나가기 버튼
    {
        Application.Quit(); // 어플리케이션 종료
    }

    public void Back() //옵션끄기 버튼
    {
        //OptionScene.SetActive(false);
        if (StopCamera.GetComponent<MoveCamera>() != null)
            StopCamera.GetComponent<MoveCamera>().enabled = true;

        AudioManager.Instance.closeOption();
        //HideUI.Instance.state = true;
    }
}
