using UnityEngine;
using System.Collections;

public class Ken_Skill : CharacterSkill
{
    public float SkillRecastTime = 10.0f;
    public override void PerformSkill()
    {
        Debug.Log("ken skill");
        
    }
}