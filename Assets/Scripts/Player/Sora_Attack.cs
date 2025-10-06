using UnityEngine;
using System.Collections;

public class Sora_Attack : CharacterAttack
{
    public override void PerformAttack()
    {
        if (!isUsingTool)
        {
            isUsingTool = true;
            StartCoroutine(RotateTool());
            Debug.Log("sora attack");
        }
    }
    private IEnumerator RotateTool()
    {
        float elapsedTime = 0f;

        // 開始角度（現在のローカル回転）
        Quaternion startRotation = Quaternion.Euler(0, 0, 0);
        // 終了角度（Z軸を-90度回転させたクォータニオン）
        Quaternion targetRotation = Quaternion.Euler(0, 0, -90);

        while (elapsedTime < toolRotationTime)
        {
            // 開始角度から終了角度までを時間に応じて補間
            toolObject.transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, (elapsedTime / toolRotationTime));

            elapsedTime += Time.deltaTime;
            yield return null; // 次のフレームまで待機
        }

        // アニメーション終了後に道具を非表示に戻す
        toolObject.SetActive(false);
        isUsingTool = false;
    }
}