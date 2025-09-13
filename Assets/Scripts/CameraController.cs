using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float smoothing = 0.5f; // カメラの追従速度

    // カメラがこれ以上左に動かないようにする限界位置
    private float minXPosition;

    //カメラがこれ以上上に動かないようにする限界位置
    private float maxYPosition;

    //カメラがこれ以上下に動かないようにする限界位置
    private float minYPosition;

    //カメラの最終的な位置
    private float CameraPosition;

    void Start()
    {
        // ゲーム開始時のカメラの左端、上限、下限の座標を設定してください
        //数字の後ろにfつけないと動きません
        minXPosition = -7012f;
        maxYPosition = 1000f;
        minYPosition = 0f;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // カメラの位置をプレイヤーの位置に合わせる
            //ゲーム統合の際は680fの部分をplayer.position.yにしてy軸カメラ追従して高さを調整してください
            // カメラのZ座標は、元のZ座標を維持する

            // カメラが目指す新しい位置を計算
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            // プレイヤーが左に戻っても、カメラがminXPositionよりも左に動かないようにする
            // Mathf.Maxを使って、targetPosition.xとminXPositionの大きい方を採用
            float clampedX = Mathf.Max(targetPosition.x, minXPosition);

            // clampedXを使って新しい位置を再設定
            targetPosition = new Vector3(clampedX, CameraPosition, transform.position.z);

            /*カメラの位置が上限、下限を割りそうになったら上限、下限で止める。
            そうでなければプレイヤー追従*/
            CameraPosition = Mathf.Clamp(targetPosition.y, minYPosition, maxYPosition);

            // カメラの位置を徐々に目標位置に移動させる
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

            //minXPositionを更新できるか確認
            if (transform.position.x > minXPosition)
            {
                minXPosition = transform.position.x;
            }
        }
    }
}