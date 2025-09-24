// CharacterAttack.cs
using UnityEngine;
using System.Collections;

// 抽象クラスとして定義
public abstract class CharacterAttack : MonoBehaviour
{
    //道具オブジェクトの情報
    public GameObject toolObject;
    public float toolRotationTime = 1.0f;
    public bool isUsingTool = false;

    // 子クラスが必ず上書きする必要がある抽象メソッドを定義
    public abstract void PerformAttack();
}