using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMom : MonoBehaviour
{
    public int Damage;
    public int Heel;
    //public int SkillCost;

    protected bool DontTouchTile(string tag)
    {
        if (tag == "DefaultTile" || tag == "ForestTile" || tag == "IceTile" || tag == "WaterTile" || tag == null)
        {
            return false;
        }
        return true;
    }
}
