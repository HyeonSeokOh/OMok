using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveScene(string str)
    {
        LoadGame.Instance.SaveFile();
        SceneManager.LoadScene("LoadingScene");
        SceneMoveManager.Instance.SceneName = str;
        
    }

    public void LoadMoveScene(string str)
    {
        LoadGame.Instance.LoadFile();
        SceneManager.LoadScene("LoadingScene");
        SceneMoveManager.Instance.SceneName = str;
        
    }

    public void changebackgroundSound(int i)
    {
        AudioManager.Instance.changeMusic(i);        
    }
}
