using UnityEngine;
using System.Collections;

public class Mito_Skill : CharacterSkill
{
    [Header("スキル効果のパラメータ")]
    public float effectDuration = 5.0f;     // スキル効果の持続時間（秒）
    public float speedMultiplier = 1.75f;    // スピードの倍率
    public float jumpMultiplier = 1.5f;     // ジャンプ力の倍率
    public float invincibilityDuration = 5.0f; // 無敵時間の長さ

    // EffectManagerへの参照を保持する変数
    private EffectManager effectManager;

    void Start()
    {
        // このスクリプトがアタッチされているキャラクターが持つEffectManagerを取得する
        // PlayerControllerやEffectManagerを持つオブジェクトを探して取得します。
        // もしPlayerに直接アタッチされているなら GetComponent<EffectManager>() に変更する
        effectManager = FindObjectOfType<EffectManager>();

        if (effectManager == null)
        {
            Debug.LogError("EffectManagerが見つかりません");
        }
    }

    public override void PerformSkill()
    {
        Debug.Log("mito skill");

        // EffectManagerの参照がなければ何もしない
        if (effectManager == null)
        {
            Debug.LogWarning("スキル効果を発動できません");
            return;
        }

        // --- EffectManagerの機能を呼び出す ---

        // 1. スピードアップを適用
        effectManager.ApplySpeedUp(speedMultiplier, effectDuration);

        // 2. ジャンプ力アップを適用
        effectManager.ApplyJumpUp(jumpMultiplier, effectDuration);

        // 3. ダメージ無敵を適用（ステップ1で追加したメソッドを呼び出す）
        effectManager.ApplyInvincibility(invincibilityDuration);
    }
}