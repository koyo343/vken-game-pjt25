using UnityEngine;
using System.Collections;

public class Uruha_Skill : CharacterSkill
{
    public float SkillRecastTime = 10.0f;
    public override void PerformSkill()
    {
        Debug.Log("uruha skill");
        //クソデカパンチを出す処理　できてると言っても過言じゃない
    }
}