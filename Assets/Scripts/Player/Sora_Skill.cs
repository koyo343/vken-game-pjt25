using UnityEngine;
using System.Collections;

public class Sora_Skill : CharacterSkill
{

    //スキル設定
    public GameObject toolObject; // インスペクターから突進時に使う道具をアタッチ
    public bool isUsingTool = false; // 道具使用中かどうかのフラグ

    //突進のパラメータ
    public float lungeDistance = 15f; // 突進する距離
    public float lungeDuration = 0.5f; // 突進にかかる時間
    
    // 突進時の速度を計算するためのプロパティ
    private float lungeSpeed => lungeDistance / lungeDuration;

    // プレイヤーのRigidbody2DとPlayerControllerへの参照
    private Rigidbody2D rb;
    private PlayerController playerController;

    void Start()
    {
        // プレイヤーのコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody2Dコンポーネントが見つかりません。突進スキルは動作しません。");
        }
    }

    public override void PerformSkill()
    {
        if (playerController == null || rb == null) return;

        if (!isUsingTool)
        {
            isUsingTool = true;
            Debug.Log("sora skill");
            StartCoroutine(DashWithTool());
        }
    }

    private IEnumerator DashWithTool()
    {
        // 道具オブジェクトが設定されていなければエラーを出して終了
        if (toolObject == null)
        {
            Debug.LogError("toolObjectがアタッチされていません");
            isUsingTool = false;
            yield break;
        }

        // try-finallyブロックで、処理の途中で中断されても必ず終了処理が呼ばれるようにする
        try
        {
            // 突進中はPlayerControllerを無効化し、通常の移動・ジャンプを停止
            playerController.enabled = false;
            
            // 既存の速度をリセットし、重力を無効化（水平移動を安定させるため）
            rb.linearVelocity = Vector2.zero;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;

            // 突進方向と速度を計算
            float directionX = Mathf.Sign(transform.localScale.x); // プレイヤーの向き
            Vector2 dashVelocity = new Vector2(directionX * lungeSpeed, 0);

            // 開始位置を記録
            Vector3 startPosition = transform.position;
            // 突進開始時間を記録
            float startTime = Time.time; 

            // 武器の角度を調整
            float rotationZ = (directionX > 0) ? -90 : 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, rotationZ);
            toolObject.transform.rotation = targetRotation;

            // 道具をアクティブにする
            toolObject.SetActive(true);

            // Rigidbody2Dに突進速度を適用
            rb.linearVelocity = dashVelocity;

            // 突進が完了するまで待機
            // 物理フレーム（FixedUpdate）での移動のため、WaitForFixedUpdateを使用
            while (true)
            {
                // 経過時間で終了するか、最大移動距離に達したかチェック
                float distanceTraveled = Vector3.Distance(startPosition, transform.position);
                // 現在の経過時間を計算
                float elapsedTime = Time.time - startTime; 

                // **突進を停止する条件**
                // 1. 突進時間を超えた
                // 2. 目標距離に達した
                // 3. 速度がゼロになった（壁に衝突したことを意味する）
                if (elapsedTime >= lungeDuration || distanceTraveled >= lungeDistance || rb.velocity.x == 0)
                {
                    break;
                }

                yield return new WaitForFixedUpdate(); // FixedUpdateの実行を待つ
            }
            
            // 突進終了時に速度を強制的にゼロにする
            rb.linearVelocity = Vector2.zero;

        }
        finally
        {
            // スキル終了処理
            toolObject.SetActive(false);
            isUsingTool = false;
            
            // PlayerControllerを有効化
            if (playerController != null)
            {
                playerController.enabled = true;
            }

            // 重力を元に戻す
            if (rb != null)
            {
                // 突進前の重力スケールに戻す
                rb.gravityScale = 7f; // ここは適宜調整
            }
        }
    }
}