using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance_;
    public static TurnManager Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<TurnManager>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("TurnBnutton").AddComponent<TurnManager>();
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
        var objs = FindObjectsOfType<TurnManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    public int mainTurn;
    public int GameTurn = 0;
    void Start()
    {

    }
}
