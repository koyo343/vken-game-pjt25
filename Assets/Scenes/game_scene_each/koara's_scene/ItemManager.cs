using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum ItemType
    {
        SpeedUp,
        JumpUp,
        ScoreUp,
        Invincible
    }

    [Header("アイテムの種類")]
    public ItemType type;

    [Header("効果の数値（アイテムタイプに合わせて設定）")]
    public float effectMagnitude = 1.5f; // 速度・ジャンプ用 (1.5 = 50%UP)
    public float effectDuration = 10f;   // 速度・ジャンプ用
    public int scoreValue = 100;         // スコアアップ用

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // シーン内のEffectsManagerを探して取得する
            EffectManager effectsManager = FindObjectOfType<EffectManager>();
            if (effectsManager == null) return;

            // アイテムの種類によってeffectsManagerのメソッドを呼び分ける
            switch (type)
            {
                case ItemType.SpeedUp:
                    effectsManager.ApplySpeedUp(effectMagnitude, effectDuration);
                    break;
                case ItemType.JumpUp:
                    effectsManager.ApplyJumpUp(effectMagnitude, effectDuration);
                    break;
                case ItemType.ScoreUp:
                    effectsManager.AddScore(scoreValue);
                    break;
                case ItemType.Invincible:
                    effectsManager.GrantInvincibility();
                    break;
            }

            // アイテム自身を消す
            Destroy(gameObject);
        }
    }
}