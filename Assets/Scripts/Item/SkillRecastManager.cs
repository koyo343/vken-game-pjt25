using UnityEngine;
using UnityEngine.UI; // UIコンポーネントを扱うために必要

public class SkillRecastManager : MonoBehaviour
{
    [Header("リキャストUI（Filled Image）")]
    public Image skillGauge;

    [Header("スキルの設定")]
    public float recastTime = 10f; // スキルが再使用可能になるまでの時間

    // --- 外部のスクリプトから参照する変数 ---
    [Tooltip("スキルが使用可能かどうか")]
    public bool IsSkillReady { get; private set; } = false;

    // --- 内部で使う変数 ---
    public float currentRecastTime = 0f; // 現在のリキャスト時間

    void Start()
    {
        // ゲーム開始時はスキルを使えない状態にする
        skillGauge.fillAmount = 0;
    }

    void Update()
    {
        // スキルがまだ使用可能でない場合、リキャスト時間を進める
        if (!IsSkillReady)
        {
            currentRecastTime += Time.deltaTime;
            skillGauge.fillAmount = currentRecastTime / recastTime; // UIを更新

            // リキャスト時間が完了したら、スキルを使用可能にする
            if (currentRecastTime >= recastTime)
            {
                IsSkillReady = true;
                skillGauge.fillAmount = 1; // ゲージを満タンにする
                Debug.Log("スキル使用可能！");
            }
        }
        
        // Fキーが押され、かつスキルが使用可能な場合
        if (Input.GetKeyDown(KeyCode.F) && IsSkillReady)
        {
            UseSkill();
        }
    }

    // スキルを使用する処理 
    public void UseSkill()
    {
        // スキルが使用可能でなければ何もしない
        if (!IsSkillReady)
        {
            Debug.Log("スキルはまだ使えません。");
            return;
        }

        Debug.Log("スキル発動！");

        // スキルを使用したので、リキャスト状態に戻す
        IsSkillReady = false;
        currentRecastTime = 0f;
        skillGauge.fillAmount = 0; // ゲージを空にする
    }
}