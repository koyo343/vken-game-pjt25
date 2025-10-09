using UnityEngine;

public class Holoxer : MonoBehaviour
{
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity; // この変数はPhysics2Dを使う限り不要
    public ScoreDictionaryManager scoreDictionaryManager;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;

    // Damage_Playerコンポーネント (Playerのダメージ処理スクリプト)
    private Damage_player playerDamager;
    private const string PlayerTag = "Player";

    void Awake()
    {
        // プレイヤーコンポーネントの取得を試みる
        FindAndGetPlayerComponent();
    }

    void Start()
    {
        // Rigidbody2DやSpriteRendererはStartで取得
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // コンポーネント取得失敗時のスクリプト無効化（安全対策）
        if (rb == null || sr == null)
        {
            Debug.LogError("Holoxer: Rigidbody2D/SpriteRenderer コンポーネントが必要です。");
            enabled = false;
        }
        if (scoreDictionaryManager == null)
        {
            scoreDictionaryManager = FindObjectOfType<ScoreDictionaryManager>();
            if (scoreDictionaryManager == null)
            {
                Debug.LogWarning("Holoxer: ScoreDictionaryManagerがシーンに見つかりません。スコア処理をスキップします。");
            }
        }
    }

    void FixedUpdate()
    {
        if (sr != null && sr.isVisible)
        {
            // ★修正点 1★: Y軸速度を維持して自然な落下を実現
            int xVector = -1;
            transform.localScale = new Vector3(8, 8, 1);

            // X軸に移動速度を設定し、Y軸には既存の速度（重力による落下）を維持
            rb.linearVelocity = new Vector2(xVector * speed, rb.linearVelocity.y);
        }
        else if (rb != null)
        {
            // 画面に映っていないときに物理演算を中止
            rb.Sleep();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Playerに接触したとき
        if (collision.gameObject.tag == "Player")
        {
            // ★修正点 2★: Nullチェックを追加
            if (playerDamager != null)
            {
                playerDamager.Damage(0);
            }
            Debug.Log("Playerに当たりました");
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Playerの攻撃に接触したとき
        if (collision.gameObject.tag == "PlayerAttack" || collision.gameObject.tag == "Bullet")
        {
            //SE再生
            EnemySE enemyAudioData = GetComponent<EnemySE>();
            SEManager seManager = FindObjectOfType<SEManager>();
            if (seManager != null) 
            {
                seManager.PlaySE(enemyAudioData.EnemySound);
                Debug.Log($"SEを再生しました: {enemyAudioData.EnemySound.name}");
            }
            if(enemyAudioData.EnemySound == null)
            {
                Debug.Log("SEがアタッチされていません");
            }
            else
            {
                Debug.LogWarning("SEManagerが見つからないため、SEを再生できませんでした。");
            }

            // 敵（Holoxer自身）を破壊
            Destroy(gameObject);
            scoreDictionaryManager.StrToScore("BeatHoloxer");
            Debug.Log("Holoxerにダメージ");

            // プレイヤーの弾も消したい場合は、ここで相手の弾も破壊するロジックを追加
            // Destroy(collision.gameObject); 
        }
    }

    void FindAndGetPlayerComponent()
    {
        // 1. "Player" タグを持つゲームオブジェクトをシーン全体から検索
        GameObject playerObject = GameObject.FindWithTag(PlayerTag);

        if (playerObject != null)
        {
            // 2. そのゲームオブジェクトから Damage_player コンポーネントを取得
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
        else
        {
            Debug.LogError($"シーン内に '{PlayerTag}' タグを持つオブジェクトが見つかりません。");
        }
    }
}