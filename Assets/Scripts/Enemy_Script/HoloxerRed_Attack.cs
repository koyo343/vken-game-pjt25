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

    private float launchTimer;
    private bool isVisible = false;
    private Transform playerTransform;

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
}