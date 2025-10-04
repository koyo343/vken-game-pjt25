using UnityEngine;
using System.Collections; // コルーチンのために必要

public class Boss : MonoBehaviour
{
    // === 状態管理 ===
    private enum BossState { Idle, Attacking, Charging, Cooldown }
    private BossState currentState = BossState.Idle;

    // === インスペクターで設定する項目 ===
    [Header("ターゲット (Player)")]
    public Transform player;

    [Header("移動・ジャンプ")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float jumpDistance = 10f; // 踏みつけでジャンプする水平方向の目標距離

    [Header("攻撃全般")]
    public float coolDownTime = 3f; // 攻撃間のクールダウン
    public float chargeTime = 1.5f; // 突進前のチャージ時間
    public float dashSpeed = 20f;   // 突進速度
    public float dashDuration = 0.5f; // 突進持続時間

    [Header("投射物と岩")]
    public GameObject projectilePrefab; // 攻撃で相殺できる長方形投射物
    public GameObject rockPrefab;       // 天井から落とす岩 (足場になる)
    public float launchSpeed = 15f;
    public int projectileCount = 5; // 投射物の数
    public float rockDropHeight = 10f; // 岩を落とす天井の高さ

    [Header("視覚的フィードバック")]
    public SpriteRenderer sr;
    public Color chargeColor = Color.red;
    private Color normalColor;

    // === プライベート変数 ===
    private Rigidbody2D rb;
    private bool isGrounded = true; // 接地判定は別途実装が必要です（ここでは簡略化）

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 必須コンポーネントの取得
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (rb == null || sr == null)
        {
            Debug.LogError("Rigidbody2D または SpriteRenderer コンポーネントがボスにアタッチされていません！");
            enabled = false;
            return;
        }

        // プレイヤーを検索
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Playerタグを持つオブジェクトが見つかりません。");
        }

        normalColor = sr.color;
        // 攻撃サイクル開始
        StartCoroutine(BossControlRoutine());
    }

    // Updateは移動とデバッグのみに使用
    void Update()
    {
        // 待機中（Idle）のみプレイヤーを追跡
        if (currentState == BossState.Idle && player != null)
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
            int attackChosenNum = Random.Range(1, 1); // 0, 1, 2, 3のいずれか

            currentState = BossState.Attacking;
            rb.linearVelocity = Vector2.zero; // 攻撃中は移動停止

            switch (attackChosenNum)
            {
                case 0: // 踏みつけ攻撃 (ジャンプからのフミツケ)
                    yield return StartCoroutine(TrampleAttack());
                    break;
                case 1: // 長方形投射物 (攻撃で相殺)
                    yield return StartCoroutine(ProjectileAttack());
                    break;
                case 2: // 岩落とし攻撃 (足場になる)
                    yield return StartCoroutine(RockDropAttack());
                    break;
                case 3: // 貯めからの突進
                    yield return StartCoroutine(DashAttack());
                    break;
            }
        }
    }

    // === 移動ロジック ===
    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Y軸方向の移動は行わない

        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (rb != null && isGrounded)
        {
            // 強制的に接地判定をfalseにするロジックを別途組み込む必要があります
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // === 攻撃ロジック ===

    // 0. 踏みつけ攻撃 (ジャンプからのフミツケ)
    IEnumerator TrampleAttack()
    {
        // ターゲットの水平位置を計算
        Vector2 targetPos = player.position;
        float horizontalDistance = targetPos.x - transform.position.x;
        
        // ターゲット方向へジャンプするための水平速度を計算（簡略化）
        float horizontalVelocity = horizontalDistance / (2 * jumpForce / Physics2D.gravity.magnitude);
        
        // ジャンプ
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(horizontalVelocity, jumpForce);
            // 接地判定ロジックがtrueに戻るまで待機
            // ここでは簡略化のため、一定時間待機で代用
            yield return new WaitForSeconds(1.5f); // ジャンプして落ちるまでの時間
        }
    }

    // 1. 長方形投射物 (攻撃で相殺)
    IEnumerator ProjectileAttack()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 directionToTarget = (player.position - transform.position).normalized;
            
            // 投射物生成
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            
            if (projRb != null)
            {
                // 投射物を発射
                projRb.linearVelocity = directionToTarget * launchSpeed;
                // 弾の角度設定（2D的な回転）
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                proj.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
            
            // 少し間隔を空けて連射
            yield return new WaitForSeconds(1.0f);
        }
    }

    // 2. 岩落とし攻撃 (足場になる)
    IEnumerator RockDropAttack()
    {
        // プレイヤーの真上、指定された高さの位置
        Vector3 dropPosition = new Vector3(player.position.x, rockDropHeight, transform.position.z);
        
        // 岩を生成 (岩のPrefabにはCollider2D, Rigidbody2D, そして"Ground"タグが必要です)
        GameObject rock = Instantiate(rockPrefab, dropPosition, Quaternion.identity);
        
        // 岩は落下し、着地後はTagにより足場として機能する
        // (岩のスクリプトで着地後にRigidBody2DのBodyTypeをStaticに変更するなどの処理が必要です)
        yield return null; // 1フレーム待機
    }

    // 3. 貯めからの突進
    IEnumerator DashAttack()
    {
        // --- チャージ開始 ---
        currentState = BossState.Charging;
        sr.color = chargeColor;
        rb.linearVelocity = Vector2.zero; // 完全停止

        // チャージ時間待機
        yield return new WaitForSeconds(chargeTime);

        // --- 突進開始 ---
        currentState = BossState.Attacking;
        sr.color = normalColor;

        // 突進方向を設定
        Vector2 dashDirection = (player.position - transform.position).normalized;
        dashDirection.y = 0; // 水平方向のみに突進

        // 突進実行
        rb.linearVelocity = dashDirection * dashSpeed;

        // 突進持続時間待機a
        yield return new WaitForSeconds(dashDuration);

        // --- 突進終了 ---
        rb.linearVelocity = Vector2.zero;
        currentState = BossState.Cooldown; // 次のクールダウンへ移行
    }

    // 接地判定のロジックをここに実装する必要があります (例: OnCollisionEnter2D, Physics2D.OverlapCircleAllなど)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 突進攻撃の衝突処理
        if (currentState == BossState.Attacking && collision.gameObject.CompareTag("Player"))
        {
            // 突進によるダメージ処理
        }
        
        // 地面との接触判定（簡略化）
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) // "Ground"レイヤーを仮定
        {
             isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 地面からの離脱判定（簡略化）
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
             isGrounded = false;
        }
    }
}