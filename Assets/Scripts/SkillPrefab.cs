using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPrefab : MonoBehaviour
{
    private static SkillPrefab instance_;
    public static SkillPrefab Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<SkillPrefab>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("SkillResources").AddComponent<SkillPrefab>();
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
        var objs = FindObjectsOfType<SkillPrefab>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public struct Skillpref
    {
        public GameObject skillObj;
        public int skillTag;
    };

    public Skillpref[] skillPref = new Skillpref[10];   // 10가지 스킬 프리팹과 UI상에 나타나는 스킬들의 태그를 가지고 있음

    //public GameObject[] SkillObj = new GameObject[10];

    // Start is called before the first frame update
    void Start()
    {
        string tagNum;

        for (int i = 0; i < 10; i++)
        {   //Resources 파일 안에 Skill Effect 파일 안에 있는 스킬 프리팹들을 담고 있음
            //추후 여러 스킬이 생겨 스킬조합이 바뀌면 UI상에 나와있는 스킬 이름을 실제로 프리팹으로 가져오는 스킬 이름과 동일하게 지으면 됨
            tagNum = (i + 1 + 1000).ToString();
            skillPref[i].skillTag = i + 1 + 1000;

            if (GameObject.FindGameObjectWithTag(tagNum) != null)
                tagNum = "Skill Effect/" + GameObject.FindGameObjectWithTag(tagNum).name;
            
            skillPref[i].skillObj = Resources.Load(tagNum) as GameObject;
            //Debug.Log(skillPref[i].skillObj.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
