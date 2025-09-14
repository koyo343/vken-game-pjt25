/*using UnityEngine;

public class MoveFloorController : Monobehavior
{
    private float minmove = 0f;
    private float maxmove; = 100f;
    private float moveSpeed = 1f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //変数にAnimatorの情報を取得して入れる
        animator = this.GetComponent<Animator>();
    }

    void Update
    {
         カメラが目指す新しい位置を計算
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

             プレイヤーが左に戻っても、カメラがminXPositionよりも左に動かないようにする
             Mathf.Maxを使って、targetPosition.xとminXPositionの大きい方を採用
            float CameraX = Mathf.Max(targetPosition.x, minXPosition);

            カメラの位置が上限、下限を割りそうになったら上限、下限で止める。
            そうでなければプレイヤー追従
            float CameraY = Mathf.Clamp(targetPosition.y, minYPosition, maxYPosition);

            // clampedXを使って新しい位置を再設定
            targetPosition = new Vector3(CameraX, CameraY, transform.position.z);

            // カメラの位置を徐々に目標位置に移動させる
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
    }
}*/