using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private static CameraFocus instance_;
    public static CameraFocus Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<CameraFocus>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("Main Camera").AddComponent<CameraFocus>();
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
        var objs = FindObjectsOfType<CameraFocus>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }

    Vector3 CameraPos;
    public Vector3 MovePos;
    public bool moveLock;

    // Start is called before the first frame update
    void Start()
    {
        moveLock = true;
        CameraPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveLock)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, MovePos, Time.deltaTime * 10f);
        }
    }
    void SetMovePos(float x, float y)
    {
        MovePos = new Vector3(x, y, 0);
    }
}
