using System.Collections;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    // アニメーターへの参照
    private Animator animator;

    void Start()
    {
        // プレイヤーにアタッチされているAnimatorコンポーネントを取得
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager.isGameClear)
        {
            // スキルのトリガー（Eキー）
            if (Input.GetKeyDown(KeyCode.E))
            {
                // スキルアニメーションを開始
                StartCoroutine(SkillRoutine());
            }
        }
    }

    // スキルのアニメーション再生を管理するコルーチン
    private IEnumerator SkillRoutine()
    {
        // スキルアニメーションの開始
        animator.SetBool("isSkill", true);

        // 0.5秒間待機
        yield return new WaitForSeconds(0.5f);

        // スキルアニメーションの終了
        animator.SetBool("isSkill", false);
    }
}