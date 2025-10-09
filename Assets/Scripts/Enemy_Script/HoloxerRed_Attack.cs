using UnityEngine;

public class HoloxerRed_Attack : MonoBehaviour
{
    // 発射速度
    [Header("発射速度")]
    public float launchSpeed = 10f;

    // 落下速度（重力）の調整
    [Header("落下速度（重力）の調整")]
    [Tooltip("Rigidbody2DのGravity Scaleに影響します。")]
    public float gravityScale = 1f;

    // 発射角度の調整
    [Header("発射角度 (度)")]
    [Range(0, 90)]
    public float launchAngle = 45f;
    // 発射間隔
    [Header("発射間隔")]
    [Tooltip("次の弾を発射するまでの時間（秒）")]
    public float launchInterval = 2f;

    [Header("投擲物")]
    public GameObject badmarkPrefab;

    // 投擲物の出現位置を調整するオフセット
    [Header("出現位置のオフセット")]
    public float spawnOffset = 1.0f;
    public ScoreDictionaryManager scoreDictionaryManager;

    private float launchTimer;
    private bool isVisible = false;
    private Transform playerTransform;
    private Damage_player playerDamager;
    private const string PlayerTag = "Player";


    void Start()
    {
        launchTimer = launchInterval;

        // プレイヤーのTransformを取得
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure it has the 'Player' tag.");
        }

        if (scoreDictionaryManager == null)
        {
            scoreDictionaryManager = FindObjectOfType<ScoreDictionaryManager>();
            if (scoreDictionaryManager == null)
            {
                Debug.LogWarning("Holoxer: ScoreDictionaryManagerがシーンに見つかりません。スコア処理をスキップします。");
            }
        }

        FindAndGetPlayerComponent();
    }

    void Update()
    {
        // カメラ内に入っている場合のみ実行
        if (isVisible)
        {
            launchTimer -= Time.deltaTime;

            if (launchTimer <= 0)
            {
                // プレイヤーが存在する場合のみ発射
                if (playerTransform != null)
                {
                    LaunchBallet();
                }
                launchTimer = launchInterval;
            }
        }
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }

    void OnBecameInvisible()
    {
        isVisible = false;
        launchTimer = launchInterval;
    }

    void LaunchBallet()
    {
        // プレイヤーと敵のX軸の位置差を計算
        float xDistance = playerTransform.position.x - transform.position.x;

        // プレイヤーの方向ベクトルを計算
        Vector2 spawnDirection = Vector2.right; // デフォルトは右
        if (xDistance < 0)
        {
            spawnDirection = Vector2.left; // プレイヤーが左にいる場合は左向き
        }

        // 出現位置を計算
        Vector3 spawnPosition = transform.position + (Vector3)spawnDirection * spawnOffset;

        // badmarkを生成
        GameObject badmarkInstance = Instantiate(badmarkPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = badmarkInstance.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = gravityScale;

            float angleInRadians = launchAngle * Mathf.Deg2Rad;

            Vector2 launchVelocity = new Vector2(
                Mathf.Cos(angleInRadians) * launchSpeed,
                Mathf.Sin(angleInRadians) * launchSpeed
            );

            // プレイヤーが左にいる場合、X方向の速度を反転させる
            if (xDistance < 0)
            {
                launchVelocity.x *= -1;
            }

            rb.linearVelocity = launchVelocity;
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
            // 2. そのゲームオブジェクトから Damage_Player コンポーネントを取得
            playerDamager = playerObject.GetComponent<Damage_player>();

            if (playerDamager != null)
            {
                Debug.Log("PlayerオブジェクトとDamage_Playerコンポーネントを取得しました。");
                // これで、playerDamager.Damage(10); のようにコンポーネントのメソッドを呼び出せます。
            }
            else
            {
                // プレイヤーオブジェクトは見つかったが、コンポーネントがアタッチされていない場合
                Debug.LogError("PlayerオブジェクトにはDamage_Playerコンポーネントが見つかりません！");
            }
        }
        else
        {
            // Playerタグを持つオブジェクトがシーンに見つからなかった場合
            Debug.LogError($"シーン内に '{PlayerTag}' タグを持つオブジェクトが見つかりません。");
        }
    }
}