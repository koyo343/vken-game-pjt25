// CharacterAttack.cs
using UnityEngine;
using System.Collections;

// なんでこんなとこ見てるんだよ　作業しろ
public abstract class CharacterSkill : MonoBehaviour
{

    // 子クラスが必ず上書きする必要がある抽象メソッドを定義
    public float SkillRecastTime;
    public abstract void PerformSkill();
}