using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWeight : MonoBehaviour
{
    private static SkillWeight instance_;
    public static SkillWeight Instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = FindObjectOfType<SkillWeight>();
                if (obj != null)
                {
                    instance_ = obj;
                }
                else
                {
                    var newSingleton = new GameObject("MapScan").AddComponent<SkillWeight>();
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
        var objs = FindObjectsOfType<SkillWeight>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    int weight;
    int skil_num;

    struct Point    // 포인트 구조체
    {
        public int x;
        public int y;
        public Point(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
    }

    Vector2 location;

    GameObject[] skillObj = new GameObject[10];
    GameObject[] skillSkills = new GameObject[10];
    List<Vector2> insPoint = new List<Vector2>();

    int cost = 0;

    // Start is called before the first frame update
    void Start()
    {
        weight = -20000;

        for (int i = 0; i < 10; i++)
        {
            string str = "Skill/Skill_0" + (i+1).ToString();
            string str2 = "Skill Effect/Skill_0" + (i + 1).ToString();

            skillObj[i] = Resources.Load(str) as GameObject;
            skillObj[i].GetComponent<Skills>().Init();
            skillSkills[i] = Resources.Load(str2) as GameObject;
        }
    }

    public void skillWeight()
    {

        weight = -20000;    // 가중치
        int skillnum = 0;

        for (int i = 0; i < TileManager.Instance.BoardSizeX; i++)
        {
            if (GameManager.Instance.MainCost == 0) break;  // 돈 없으면 스킵

            for (int j = 0; j < TileManager.Instance.BoardSizeY; j++)
            {
                // 아무런 가중치가 없는 곳에는 관심 없음
                if (TileManager.TileMain[i,j].RullTile == 0 && TileManager.TileMain[i, j].weight == 0 &&
                    TileManager.TileMain[i, j].attackPoint == TileManager.attack_defense_Point.ZERO &&
                    TileManager.TileMain[i, j].defensePoint == TileManager.attack_defense_Point.ZERO)
                    continue;

                //남은 코스트 값에 맞춰서 스킬 가중치 계산
                for (int k = 1; k <= GameManager.Instance.MainCost; k++)
                {
                    for (int l = 0; l < LoadGame.Instance.limit_Skill; l++)
                    {
                        if (GameManager.Instance.MainCost < skillObj[l].GetComponent<Skills>().GetSkillCost()) continue;
                        int wei = skillObj[l].GetComponent<Skills>().skillRange(i, j, skillObj[l].GetComponent<Skills>().GetSkillWeight(), skillObj[l].GetComponent<Skills>().GetSkillWeight() * (-1))
                             * (60 / skillObj[l].GetComponent<Skills>().GetSkillCost());

                        if (weight < wei)
                        {
                            weight = wei;
                            skillnum = skillObj[l].GetComponent<Skills>().getSkillNumber();
                            location = new Vector2(i, j);
                            cost = skillObj[l].GetComponent<Skills>().GetSkillCost();
                        }
                    }
                }
            }
        }
        if (skillnum != 0)
            CreateSkill(skillnum);
    }

    void CreateSkill(int skillnum)  // 스킬 생성
    {
        skillObj[skillnum - 1- 1000].GetComponent<Skills>().SetPoint(Mathf.FloorToInt(location.x), Mathf.FloorToInt(location.y));
        insPoint = skillObj[skillnum - 1- 1000].GetComponent<Skills>().GetPoint();
        for (int i = 0; i < insPoint.Count; i++)
        {
            GameObject ins = Instantiate(skillSkills[skillnum - 1 - 1000], insPoint[i], Quaternion.identity) as GameObject;
            //ins.GetComponent<DragEffect>().EnemyCreateSkill();
        }
        GameManager.Instance.MainCost -= cost;
    }
}
