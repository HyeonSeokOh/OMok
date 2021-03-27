using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveManager : MonoBehaviour
{
    private static SceneMoveManager instance_;
    public static SceneMoveManager Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<SceneMoveManager>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("SceneMoveManager").AddComponent<SceneMoveManager>();
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
        var objs = FindObjectsOfType<SceneMoveManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
