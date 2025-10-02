using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum ItemType
    {
        SpeedUp,
        JumpUp,
        ScoreUp,
        Invincible,
        Recast,
        SavePoints
    }

    [Header("アイテムの種類")]
    public ItemType type;

    [Header("効果の数値（アイテムタイプに合わせて設定）")]
    public float effectMagnitude = 1.5f; // 速度・ジャンプ用 (1.5 = 50%UP)
    public float effectDuration = 10f;   // 速度・ジャンプ用
    public int scoreValue = 100;         // スコアアップ用
    public int SkillRecast = 3;          // スキル時間短縮用

    //SEを再生するスクリプト
    private ScoreSE scoreaudio;

    private ItemSE itemaudio;

    void Awake()
    {
        itemaudio = GetComponent<ItemSE>();
        scoreaudio = GetComponent<ScoreSE>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // シーン内のEffectsManagerを探して取得する
            EffectManager effectsManager = FindObjectOfType<EffectManager>();
            if (effectsManager == null) return;
            // シーン内のCameraControllerを探して取得する 
            CameraController cameraController = FindObjectOfType<CameraController>();
            if (cameraController == null) return;

            // アイテムの種類によってeffectsManagerのメソッドを呼び分ける
            switch (type)
            {
                case ItemType.SpeedUp:
                    effectsManager.ApplySpeedUp(effectMagnitude, effectDuration);
                    PlayItemSE();
                    break;
                case ItemType.JumpUp:
                    effectsManager.ApplyJumpUp(effectMagnitude, effectDuration);
                    PlayItemSE();
                    break;
                case ItemType.ScoreUp:
                    effectsManager.AddScore(scoreValue);
                    PlayScoreSE();
                    break;
                case ItemType.Invincible:
                    effectsManager.GrantInvincibility();
                    PlayItemSE();
                    break;
                case ItemType.Recast:
                    effectsManager.SkillRecast(SkillRecast);
                    PlayItemSE();
                    break;
                case ItemType.SavePoints:
                    cameraController.SavePoint = transform.position; 
                    Debug.Log("セーブポイントを更新しました: ");
                    PlayItemSE();
                    break;
            }

            // アイテム自身を消す
            Destroy(gameObject);
        }
    }
    void PlayItemSE()
    {
        // 鳴らすべきSEクリップが存在するかチェック
        if (itemaudio != null && itemaudio.ItemSound != null)
        {
            // 💡 シーン内の SEManager インスタンスを探す（シングルトンであると仮定）
            SEManager seManager = FindObjectOfType<SEManager>();

            if (seManager != null)
            {
                // SEManagerに直接、このアイテムのSEクリップを渡して再生を依頼
                seManager.PlaySE(itemaudio.ItemSound);
                Debug.Log($"アイテムSEを再生しました: {itemaudio.ItemSound.name}");
            }
            else
            {
                Debug.LogWarning("SEManagerが見つからないため、SEを再生できませんでした。");
            }
        }
    }
    void PlayScoreSE()
    {
        // 鳴らすべきSEクリップが存在するかチェック
        if (scoreaudio != null && scoreaudio.ScoreSound != null)
        {
            // 💡 シーン内の SEManager インスタンスを探す（シングルトンであると仮定）
            SEManager seManager = FindObjectOfType<SEManager>();

            if (seManager != null)
            {
                // SEManagerに直接、このアイテムのSEクリップを渡して再生を依頼
                seManager.PlaySE(scoreaudio.ScoreSound);
                Debug.Log($"アイテムSEを再生しました: {scoreaudio.ScoreSound.name}");
            }
            else
            {
                Debug.LogWarning("SEManagerが見つからないため、SEを再生できませんでした。");
            }
        }
    }
}