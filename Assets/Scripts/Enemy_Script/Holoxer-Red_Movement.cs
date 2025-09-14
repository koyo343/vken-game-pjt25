using UnityEngine;

public class HoloxerRed_Movement : MonoBehaviour
{
    // 敵の移動速度
    [Header("移動速度")] public float moveSpeed = 5f;

    // Y軸の移動範囲
    [Header("Y軸の移動範囲")] public float moveRangeY = 2f;

    // Y軸の往復速度
    [Header("Y軸の往復速度")] public float frequencyY = 1f;

    // プレイヤーのTransformを格納する変数
    private Transform playerTransform;

    // 敵の初期位置を格納する変数
    private Vector3 initialPosition;

    void Start()
    {
        // シーン内の「Player」タグを持つゲームオブジェクトを見つける
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            // プレイヤーのTransformを取得
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure it has the 'Player' tag.");
        }

        // 敵の初期位置を記録
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // プレイヤーと敵のX軸の位置差を計算
            float xDirection = playerTransform.position.x - transform.position.x;
            
            // X軸の新しい位置を計算
            float newX = transform.position.x;
            if (Mathf.Abs(xDirection) > 0.1f)
            {
                newX += Mathf.Sign(xDirection) * moveSpeed * Time.deltaTime;
            }

            // 三角関数を使ったY軸方向の往復移動
            float newY = initialPosition.y + Mathf.Sin(Time.time * frequencyY) * moveRangeY;
            
            // 新しい位置を適用
            transform.position = new Vector3(newX, newY, transform.position.z);
        }
    }
}