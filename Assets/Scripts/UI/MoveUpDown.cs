using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class MoveUpDown : MonoBehaviour
{
    private static MoveUpDown instance_;
    public static MoveUpDown Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<MoveUpDown>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("Select").AddComponent<MoveUpDown>();
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
        var objs = FindObjectsOfType<MoveUpDown>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    float move;
    float time;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        move = 0.1f;
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = new Vector2(0, move);

        time = time + Time.deltaTime;

        if (time <= 0.7f)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, pos + dir, Time.deltaTime * 5);
        }
        else
        {
            time = 0;
            move = move * -1;
        }
    }

    public void SetparentObject(GameObject obj)
    {
        this.transform.parent = obj.transform;
        this.transform.localPosition = new Vector2(0, 1);
        pos = this.transform.position;
    }
}
