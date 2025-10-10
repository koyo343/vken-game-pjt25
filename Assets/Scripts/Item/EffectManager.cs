using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour
{
    [Header("操作対象のプレイヤー")]
    public PlayerController playerController; // インスペクターからプレイヤーを設定

    [Header("各種マネージャーへの参照")]
    public ScoreManager scoreManager;
    public Debugmode debugmode; 
    public SkillRecastManager skillrecastmanager;

    // --- 内部で使う変数 ---
    private float originalMoveSpeed;
    private float originalJumpForce;
    public Damage_player damage_player;
    private bool isDamageInvincible = false;  // ダメージ後の無敵

    void Start()
    {
        if (playerController != null)
        {
            // ゲーム開始時にプレイヤーの元のステータスを記録
            originalMoveSpeed = playerController.moveSpeed;
            originalJumpForce = playerController.jumpForce;

            damage_player = GetComponent<Damage_player>();

            if (damage_player != null)
            {
                Debug.Log("damage_playerコンポーネントを取得しました");
            }
        }
    }

    /*// ■■■ ダメージを受ける処理 ■■■
    public void TakeDamage(int damageScorePenalty)
    {
        // どちらかの無敵状態ならダメージを受けない
        if (damage_player.isShield || isDamageInvincible)
        {
            if (damage_player.isShield)
            {
                damage_player.isShield = false; // アイテム無敵は一度で消費
                Debug.Log("無敵効果でダメージを防いだ！");
            }
            return;
        }

        // ダメージ処理を実行
        if (scoreManager != null)
        {
            scoreManager.AddScore(-damageScorePenalty);
        }

        // ダメージ後の無敵時間コルーチンを開始
        StartCoroutine(DamageInvincibilityCoroutine());
    }*/

    // ■■■ アイテム効果を適用するメソッド群 ■■■
    public void ApplySpeedUp(float multiplier, float duration)
    {
        StartCoroutine(SpeedUpCoroutine(multiplier, duration));
    }

    public void ApplyJumpUp(float multiplier, float duration)
    {
        StartCoroutine(JumpUpCoroutine(multiplier, duration));
    }
    
    public void GrantInvincibility()
    {
        damage_player.isShield = true;
        Debug.Log("無敵アイテムを取得！");
    }

    public void SkillRecast(int Recasttime)
    {
        skillrecastmanager.currentRecastTime += Recasttime;
        Debug.Log("スキルを短縮しました");
    }

    public void AddScore(int amount)
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(amount);
        }
    }
    
    // --- コルーチン ---
    private IEnumerator SpeedUpCoroutine(float multiplier, float duration)
    {
        if (playerController == null) yield break;
        playerController.moveSpeed = originalMoveSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        playerController.moveSpeed = originalMoveSpeed;
    }

    private IEnumerator JumpUpCoroutine(float multiplier, float duration)
    {
        if (playerController == null) yield break;
        playerController.jumpForce = originalJumpForce * multiplier;
        yield return new WaitForSeconds(duration);
        playerController.jumpForce = originalJumpForce;
    }

    private void DamageInvincibilityCoroutine()
    {
        // シールド付与
        Debug.Log("シールド付与予定");
        damage_player.isShield = true;
        Debug.Log("シールドを付与");
    }

    /*// --- デバッグ用ダメージ機能 ---
    void Update()
    {
        DamageDebug();
    }

    private void DamageDebug()
    {
        if (debugmode != null && debugmode.isDebug == true)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                TakeDamage(500);
            }
        }
    }*/
}