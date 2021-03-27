using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;

public class LoadingScene : MonoBehaviour
{
    public Slider slider;
    bool IsDone = false;
    float fTime = 0f;
    AsyncOperation async_operation;
    float time;
    public Text loadingtext;
    public Text skilltext;
    string explainStr;

    public Image Skillimage;
    public Sprite[] sprites;
    int RandomSkillShow;
    void Start()
    {
        RandomSkillShow = Random.Range(1, 15);
        loadingtext.fontSize = 150;
        skilltext.fontSize = 150;
        StartCoroutine(StartLoad(SceneMoveManager.Instance.SceneName));
        switch (RandomSkillShow)
        {
            case 1:
                explainStr = "비용 : 1 체력 : 10 아무 효과 없음 ";
                Skillimage.sprite = sprites[0]; break;
            case 2:
                explainStr = "비용 : 2 체력 : 20 돌을 놓을 시, 일정 확률로 최대 체력이 10 증가합니다.";
                Skillimage.sprite = sprites[1]; break;
            case 3:
                explainStr = "비용 : 3 체력 : 30 2턴에 한번 씩, 주위에 비용이 1인 기본 돌을 생성합니다.";
                Skillimage.sprite = sprites[2]; break;
            case 4:
                explainStr = "비용 : 4 체력 : 40 돌을 놓을 시, 자기 자신에게 실드를 생성합니다. 실드의 지속시간은 주위에 존재하는 아군 돌의 수만큼입니다.";
                Skillimage.sprite = sprites[3]; break;
            case 5:
                explainStr = "비용 : 5 체력 : 150 돌을 놓을 시, 피해를 2배 입도록 하는 영역을 생성합니다.";
                Skillimage.sprite = sprites[4]; break;

            //스킬 텍스트 입력
            case 6:
                explainStr = "비용 : 1 피해를 6 줍니다.";
                Skillimage.sprite = sprites[5]; break;
            case 7:
                explainStr = "비용 : 1 체력을 10 회복 시킵니다.";
                Skillimage.sprite = sprites[6]; break;
            case 8:
                explainStr = "비용 : 2 표식을 부여합니다. 부여된 돌은 1회에 한해서 피해를 2배로 받습니다.";
                Skillimage.sprite = sprites[7]; break;
            case 9:
                explainStr = "비용 : 2 피해를 10 줍니다.";
                Skillimage.sprite = sprites[8]; break;
            case 10:
                explainStr = "비용 : 2 피해를 10 줍니다.";
                Skillimage.sprite = sprites[9]; break;
            case 11:
                explainStr = "비용 : 3 표식을 부여합니다. 부여할 때의 체력을 기준으로 체력을 2턴간 2배로 올립니다. 원상태로 돌아와도 체력이 1 아래로 떨어지진 않습니다.";
                Skillimage.sprite = sprites[10]; break;
            case 12:
                explainStr = "비용 : 3 실드를 부여합니다. 1회에 한해서 피해를 막습니다.";
                Skillimage.sprite = sprites[11]; break;
            case 13:
                explainStr = "비용 : 4 피해를 25 줍니다.";
                Skillimage.sprite = sprites[12]; break;
            case 14:
                explainStr = "비용 : 4 피해를 25 줍니다. 물속성 타일에서만 사용이 가능합니다.";
                Skillimage.sprite = sprites[13]; break;
            case 15:
                explainStr = "비용 : 5 피해를 40 줍니다. 1턴 후에 발동되며, 발동 위치는 범위 내에서 랜덤하게 정해집니다.";
                Skillimage.sprite = sprites[14]; break;
        }

    }

    void loading(string str)
    {
        // StartCoroutine(StartLoad(str));
    }

    // 92퍼에서 다음씬 넘어가기전 value 값 1로 지정후 넘기기
    void Update()
    {
        skilltext.text = explainStr;

        fTime += 0.005f;
        slider.value = fTime;
        if (fTime >= 3)
        {
            async_operation.allowSceneActivation = true;
        }
    }

    public IEnumerator StartLoad(string strSceneName)
    {
        async_operation = SceneManager.LoadSceneAsync(strSceneName);
        async_operation.allowSceneActivation = false;

        if (IsDone == false)
        {
            IsDone = true;

            while (async_operation.progress < 0.3f)
            {
                slider.value = async_operation.progress;

                yield return null;
            }
        }
        while (true)
        {
            loadingtext.text = "Loading" + ".";
            yield return new WaitForSeconds(0.5f);
            loadingtext.text = "Loading" + "..";
            yield return new WaitForSeconds(0.5f);
            loadingtext.text = "Loading" + "...";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
