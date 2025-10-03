// toolObjectをキャラクターの子オブジェクトとしてアタッチしてください。
using UnityEngine;
using System.Collections;

public class Sora_Skill : CharacterSkill
{
    public float SkillRecastTime = 10.0f;

    [Header("スキル設定")]
    public GameObject toolObject; // インスペクターから突進時に使う道具をアタッチ

    [Header("突進のパラメータ")]
    public float lungeDistance = 3.0f; // 突進する距離
    public float lungeDuration = 0.2f; // 突進にかかる時間

    public override void PerformSkill()
    {   
        Debug.Log("sora skill");
        StartCoroutine(DashWithTool());
    }

    private IEnumerator DashWithTool()
    {
        // 道具オブジェクトが設定されていなければエラーを出して終了
        if (toolObject == null)
        {
            Debug.LogError("toolObjectがアタッチされていません");
            yield break;
        }

        // try-finallyブロックで、処理の途中で中断されても必ず終了処理が呼ばれるようにする
        try
        {
            // 道具をアクティブにする
            toolObject.SetActive(true);

            // キャラクターの現在位置と目標位置を計算
            Vector3 startPosition = transform.position;
            // キャラクターの向き（右向きが正）に合わせて移動方向を決定
            Vector3 direction = transform.right * Mathf.Sign(transform.localScale.x);
            Vector3 targetPosition = startPosition + direction * lungeDistance;

            // 突進処理
            float elapsedTime = 0f;
            while (elapsedTime < lungeDuration)
            {
                // Lerpを使って現在位置から目標位置へ滑らかに移動
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / lungeDuration);

                elapsedTime += Time.deltaTime;
                yield return null; // 1フレーム待つ
            }

            // 確実に目標位置へ移動させる
            transform.position = targetPosition;
        }
        finally
        {
            // スキル終了処理
            toolObject.SetActive(false);
        }
    }
}