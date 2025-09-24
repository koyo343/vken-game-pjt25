// CharacterAttack.cs
using UnityEngine;
using System.Collections;

// 抽象クラスとして定義
public abstract class CharacterSkill : MonoBehaviour
{

    // 子クラスが必ず上書きする必要がある抽象メソッドを定義
    public abstract void PerformSkill();
}