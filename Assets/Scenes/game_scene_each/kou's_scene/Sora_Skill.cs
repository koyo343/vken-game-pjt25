//toolObjectをアタッチしてね じゃないと動かんよ
using UnityEngine;
using System.Collections;

public class Sora_Skill : CharacterSkill
{
    public float SkillRecastTime = 10.0f;
    public GameObject toolObject;
    public float toolRotationTime = 1.0f;
    public bool isUsingTool = false;
    public override void PerformSkill()
    {
        Debug.Log("sora skill");
        isUsingTool = true;
        StartCoroutine(DashwithTool());
    }
    private IEnumerator DashwithTool()
    {
        // ダッシュの速度と距離を設定
        float lungeDistance = 0.4f;
        float lungeDuration = 0.2f;

        // 道具オブジェクトが割り当てられているか確認
        if (toolObject == null)
        {
            Debug.LogError("toolObjectが割り当てられていません");
            isUsingTool = false;
            yield break;
        }

        toolObject.SetActive(true);

        // キャラクターの開始位置と目標位置を計算
        Vector3 characterStartPosition = transform.position;
        Vector3 characterTargetPosition = transform.position;

        // 前方に突き出す方向を決定 (キャラクターの向きに合わせる)
        if (transform.localScale.x < 0)
        {
            characterTargetPosition.x -= lungeDistance;
        }
        else
        {
            characterTargetPosition.x += lungeDistance;
        }

        // --- 道具の開始位置と終了位置はそのまま ---
        Vector3 toolStartPosition = toolObject.transform.localPosition;
        Vector3 toolTargetPosition = toolStartPosition;

        if (transform.localScale.x < 0)
        {
            toolTargetPosition.x -= lungeDistance;
        }
        else
        {
            toolTargetPosition.x += lungeDistance;
        }


        // --- 前方への移動 ---
        float elapsedTime = 0f;
        while (elapsedTime < lungeDuration)
        {
            // 道具とキャラクターの両方を補間移動
            toolObject.transform.localPosition = Vector3.Lerp(toolStartPosition, toolTargetPosition, (elapsedTime / lungeDuration));
            transform.position = Vector3.Lerp(characterStartPosition, characterTargetPosition, (elapsedTime / lungeDuration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 確実に終了位置に
        toolObject.transform.localPosition = toolTargetPosition;
        transform.position = characterTargetPosition;

        // アニメーション終了後に道具を非表示に戻す
        toolObject.SetActive(false);
        isUsingTool = false;
    }
}