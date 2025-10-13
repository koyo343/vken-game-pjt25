using UnityEngine;
using System.Collections;

public class Uruha_Attack : CharacterAttack
{
    public override void PerformAttack()
    {
        if (!isUsingTool)
        {
            isUsingTool = true;
            StartCoroutine(LungeTool());
            Debug.Log("uruha attack");
        }
    }
    private IEnumerator LungeTool()
    {
        // ダッシュの速度と距離を設定
        float lungeDistance = 0.4f;  // 前方に突き出す距離
        float lungeDuration = 0.2f;  // 突き出すのにかかる時間 (片道)
        
        // 道具オブジェクトが割り当てられているか確認
        if (toolObject == null)
        {
            Debug.LogError("toolObjectが割り当てられていません");
            isUsingTool = false;
            yield break; // エラーなのでコルーチンを終了
        }

        toolObject.SetActive(true);

        // 開始位置と終了位置を計算
        Vector3 startPosition = toolObject.transform.localPosition;
        Vector3 targetPosition = startPosition;

        // 向きの判定を削除し、常にローカル座標のX軸プラス方向に移動させる
        // 親のlocalScale.xが-1なら、これだけで自動的に左に動く
        targetPosition.x += lungeDistance;

        // --- 前方への移動 ---
        float elapsedTime = 0f;
        while (elapsedTime < lungeDuration)
        {
            toolObject.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / lungeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        toolObject.transform.localPosition = targetPosition; // 確実に終了位置に

        // --- 元の位置に戻る ---
        elapsedTime = 0f;
        while (elapsedTime < lungeDuration)
        {
            toolObject.transform.localPosition = Vector3.Lerp(targetPosition, startPosition, (elapsedTime / lungeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        toolObject.transform.localPosition = startPosition; // 確実に開始位置に戻る

        // アニメーション終了後に道具を非表示に戻す
        toolObject.SetActive(false);
        isUsingTool = false;
    }
}