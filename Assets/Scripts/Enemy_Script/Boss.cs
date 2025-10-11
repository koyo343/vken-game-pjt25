using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    // === 状態管理 ===
    private enum BossState {Attacking, Charging, Cooldown }
    private BossState currentState = BossState.Cooldown;

    // === インスペクターで設定する項目 ===
    [Header("ターゲット (Player)")]
    public Transform player;

    [Header("移動・ジャンプ")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpDistance = 10f;

    [Header("攻撃全般")]
    public float coolDownTime = 3f;
    public float chargeTime = 1.5f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.5f;

    [Header("ノックバック設定")] //ノックバック力を設定
    public float knockbackHorizontalForce = 30f;
    public float knockbackVerticalForce = 20f;
    public float colliderDisableDuration = 0.1f;

    [Header("投射物と岩")]
    public GameObject projectilePrefab;
    public GameObject rockPrefab;
    public float launchSpeed = 15f;
    public int projectileCount = 5;
    public int RockCount = 5;
    public float RockCoolDown = 1.0f;
    public float rockDropHeight = 10f;

    [Header("視覚的フィードバック")]
    public SpriteRenderer sr;
    public Color chargeColor = Color.red;
    private Color normalColor;

    private Damage_player playerDamager;
    private Damage_Boss damage_boss;
    private EnemySE enemyAudioData;

    // プレイヤーのタグ名
    private const string PlayerTag = "Player";

    // === プライベート変数 ===
    private Rigidbody2D rb;
    private Collider2D bossCollider;
    private bool isGrounded = true;
    private float lastMoveDirectionX = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 必須コンポーネントの取得
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        damage_boss = GetComponent<Damage_Boss>();
        enemyAudioData = GetComponent<EnemySE>();
        bossCollider = GetComponent<Collider2D>();

        if (rb == null || sr == null || bossCollider == null)
        {
            Debug.LogError("Rigidbody2D, SpriteRenderer, または Collider2D コンポーネントがボスにアタッチされていません！");
            enabled = false;
            return;
        }

        if (damage_boss == null)
        {
            Debug.LogError("Damage_Bossコンポーネントがボス自身に見つかりません！");
        }

        // プレイヤーを検索し、コンポーネントを取得
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            FindAndGetPlayerComponent(playerObject);
        }
        else
        {
            Debug.LogError("Playerタグを持つオブジェクトが見つかりません。");
        }

        normalColor = sr.color;

        // 攻撃サイクル開始
        StartCoroutine(BossControlRoutine());
    }

    void Update()
    {
        //何もしない
    }

    void FixedUpdate()
    {
        // クールダウン中のみプレイヤーを追跡（水平移動の固定も兼ねる）
        if (currentState == BossState.Cooldown && player != null)
        {
            ChasePlayer();
        }
    }

    // === メイン制御コルーチン ===
    IEnumerator BossControlRoutine()
    {
        while (player != null)
        {
            // クールダウン
            currentState = BossState.Cooldown;
            sr.color = normalColor;
            rb.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(coolDownTime);

            // 攻撃選択
            int attackChosenNum = Random.Range(0, 4);

            currentState = BossState.Attacking;
            rb.linearVelocity = Vector2.zero;

            switch (attackChosenNum)
            {
                case 0: // 踏みつけ攻撃 (ジャンプからのフミツケ)
                    yield return TrampleAttack();
                    break;
                case 1: // 長方形投射物 (攻撃で相殺)
                    yield return ProjectileAttack();
                    break;
                case 2: // 岩落とし攻撃 (足場になる)
                    yield return RockDropAttack();
                    break;
                case 3: // 貯めからの突進
                    yield return DashAttack();
                    break;
            }
        }
    }

    // === 移動ロジック ===
    void ChasePlayer()
    {
        float targetX = player.position.x;
        float currentX = transform.position.x;

        // ★修正★ 常にPlayerがいる方向を計算し、それを移動方向に設定します。
        // X座標が近いか遠いかに関係なく、現在のPlayerの方向を追いかけ続けます。
        float directionToPlayer = Mathf.Sign(targetX - currentX);

        // X座標が異なる場合、またはX座標が同じだが直前の移動方向と逆を向いている場合のみ方向を更新
        if (Mathf.Abs(targetX - currentX) > 0.01f || lastMoveDirectionX != directionToPlayer)
        {
            lastMoveDirectionX = directionToPlayer;
        }

        // 【横押し出し防止】スクリプトで計算した速度を毎フレーム適用
        rb.linearVelocity = new Vector2(lastMoveDirectionX * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (rb != null && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // === 攻撃ロジック ===

    // 0. 踏みつけ攻撃 (ジャンプからのフミツケ)
    IEnumerator TrampleAttack()
    {
        Vector2 targetPos = player.position;
        float horizontalDistance = targetPos.x - transform.position.x;

        float gravityScale = rb.gravityScale > 0 ? rb.gravityScale : 1f;
        float effectiveGravity = Physics2D.gravity.magnitude * gravityScale;

        float flightTime = 2f * jumpForce / effectiveGravity;
        if (flightTime <= 0) flightTime = 0.01f;

        float horizontalVelocity = horizontalDistance / flightTime;

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(horizontalVelocity, jumpForce);
            isGrounded = false;

            // 5. 計算した滞空時間だけ待機 (着地を待つ)
            yield return new WaitForSeconds(flightTime);
        }

        // 6. 落下後に速度をリセット
        rb.linearVelocity = Vector2.zero;
    }

    // 1. 長方形投射物 (攻撃で相殺)
    IEnumerator ProjectileAttack()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 directionToTarget = (player.position - transform.position).normalized;
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();

            if (projRb != null)
            {
                projRb.linearVelocity = directionToTarget * launchSpeed;
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                proj.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }

            yield return new WaitForSeconds(2.0f / projectileCount);
        }
    }

    // 2. 岩落とし攻撃 (足場になる)
    IEnumerator RockDropAttack()
    {
        for (int i = 0; i < RockCount; i++)
        {
            Debug.Log("岩を生成");
            Vector3 dropPosition = new Vector3(player.position.x, player.position.y +rockDropHeight, transform.position.z);
            GameObject rock = Instantiate(rockPrefab, dropPosition, Quaternion.identity);
            yield return new WaitForSeconds(RockCoolDown);
        }
    }

    // 3. 貯めからの突進
    IEnumerator DashAttack()
    {
        // --- チャージ開始 ---
        currentState = BossState.Charging;
        sr.color = chargeColor;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(chargeTime);

        // --- 突進開始 ---
        currentState = BossState.Attacking;
        sr.color = normalColor;

        Vector2 dashDirection = (player.position - transform.position).normalized;
        float dashX = dashDirection.x;

        // 突進持続時間中、速度を毎フレーム固定して押し出しを防止
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            rb.linearVelocity = new Vector2(dashX * dashSpeed, rb.linearVelocity.y);
            yield return null;
        }

        // --- 突進終了 ---
        rb.linearVelocity = Vector2.zero;
        currentState = BossState.Cooldown;
    }

    // ★追加★ コライダーを遅延させて有効化するコルーチン
    IEnumerator ReEnableColliderAfterDelay()
    {
        // 設定された時間だけ待機
        yield return new WaitForSeconds(colliderDisableDuration);

        // Colliderを再有効化
        if (bossCollider != null)
        {
            bossCollider.enabled = true;
        }
    }

    // 接地判定と衝突処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面との接触判定
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.9f)
                {
                    isGrounded = true;
                    return;
                }
            }
        }

        // Playerに接触したとき (ヒットの瞬間)
        if (collision.gameObject.tag == "Player")
        {
            // 1. ダメージ処理
            if (playerDamager != null)
            {
                playerDamager.Damage(5);
            }
            Debug.Log("Playerに当たりました");
            // 2. Colliderを一時的に無効化し、コルーチンで再有効化 (押し出し防止)
            if (bossCollider != null)
            {
                bossCollider.enabled = false;
                StartCoroutine(ReEnableColliderAfterDelay());
            }
            // 3. ★★★ ノックバック処理 ★★★
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // PlayerがBossの左右どちらにいるか判定し、ノックバック方向を決定
                float knockbackDirection = Mathf.Sign(player.position.x - transform.position.x);

                // 力を計算 (垂直方向は常に上向き)
                Vector2 knockbackForce = new Vector2(
                    knockbackHorizontalForce * knockbackDirection,
                    knockbackVerticalForce
                );

                // 力を加える (ForceMode2D.Impulseで瞬発的な力を加える)
                playerRb.AddForce(knockbackForce, ForceMode2D.Impulse);

                Debug.Log("Playerをノックバックさせました！");
            }
        }
    }

    // 継続的な衝突処理（Y座標の押し込み防止含む）
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Playerと接触している場合のみ処理
        if (collision.gameObject.tag == "Player")
        {
            // Y軸押し込み防止ロジック:
            // BossがPlayerに乗ったときに、Y軸がフリーズするのを防ぐ
            if (isGrounded || Mathf.Abs(rb.linearVelocity.y) < 0.1f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            }
        }

        // Playerの攻撃に接触したとき
        if (collision.gameObject.tag == "PlayerAttack" || collision.gameObject.tag == "Bullet")
        {
            //SE再生
            SEManager seManager = FindObjectOfType<SEManager>();

            if (seManager != null)
            {
                if (enemyAudioData != null && enemyAudioData.EnemySound != null)
                {
                    seManager.PlaySE(enemyAudioData.EnemySound);
                    Debug.Log($"SEを再生しました: {enemyAudioData.EnemySound.name}");
                }
                else
                {
                    Debug.Log("EnemySE またはその中の AudioClip がアタッチされていません");
                }
            }
            else
            {
                Debug.LogWarning("SEManagerが見つからないため、SEを再生できませんでした。");
            }

            damage_boss.Damage(10);
            Debug.Log("Bossにダメージ");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 地面からの離脱判定
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    void FindAndGetPlayerComponent(GameObject playerObject)
    {
        playerDamager = playerObject.GetComponent<Damage_player>();

        if (playerDamager != null)
        {
            Debug.Log("PlayerオブジェクトとDamage_Playerコンポーネントを取得しました。");
        }
        else
        {
            Debug.LogError("PlayerオブジェクトにはDamage_Playerコンポーネントが見つかりません！");
        }
    }
}