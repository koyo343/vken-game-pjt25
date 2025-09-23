using UnityEngine;
using System.Collections;

public class Sora_Skill : CharacterSkill
{
    public override void PerformAttack()
    {
        Debug.Log("sora skill");
        //剣を前に出して突っ込む処理　キャラごと突っ込むので楽そう
    }
}