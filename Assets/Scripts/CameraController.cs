using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public float smoothing = 0.5f; // カメラの追従速度

    //カメラがこれ以上左に動かないようにする限界位置
    private float minXPosition;

    //カメラがこれ以上右に動かないようにする限界位置
    private float maxXPosition;

    //カメラがこれ以上上に動かないようにする限界位置
    private float maxYPosition;

    //カメラがこれ以上下に動かないようにする限界位置
    private float minYPosition;
    //セーブポイントの仮実装
    public Vector3 SavePoint;
    //落ちたかを判定するフラグ
    public bool FallFlag = false;
    //ボスフラグ
    public bool BossFlag = false;

    void Start()
    {
        // ゲーム開始時のカメラの左端、上限、下限の座標を設定してください
        //数字の後ろにfつけないと動きません
        minXPosition = -7012f;
        maxXPosition = 1000f;
        maxYPosition = 1500f;
        minYPosition = -2000f;

        //セーブポイントの初期化、第一引数をx、第二引数をy、第三引数をz座標とする。
        SavePoint = new Vector3(-3800f, 146f, 0f);

    }

    void LateUpdate()
    {
        if (player != null)
        {
            // カメラの位置をプレイヤーの位置に合わせる
            // カメラのZ座標は、元のZ座標を維持する

            // カメラが目指す新しい位置を計算
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            // プレイヤーが左に戻っても、カメラがminXPositionよりも左に動かないようにする
            // Mathf.Maxを使って、targetPosition.xとminXPositionの大きい方を採用
            float CameraX = Mathf.Clamp(targetPosition.x, minXPosition, maxXPosition);

            /*カメラの位置が上限、下限を割りそうになったら上限、下限で止める。
            そうでなければプレイヤー追従*/
            float CameraY = Mathf.Clamp(targetPosition.y, minYPosition, maxYPosition);

            // clampedXを使って新しい位置を再設定
            targetPosition = new Vector3(CameraX, CameraY, transform.position.z);

            // カメラの位置を徐々に目標位置に移動させる
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

            //minXPositionを更新できるか確認
            if (transform.position.x > minXPosition)
            {
                minXPosition = transform.position.x;
            }

            //ボスを倒した場合maxXPositionを更新
            if (BossFlag == true)
            {
                maxXPosition = 3000f;
            }
        }
    }

    void Update()
    {
        if (FallFlag)
        {
            // minXPositionをセーブポイントのX座標で上書き
            minXPosition = SavePoint.x;

            // 処理が完了したのでフラグをfalseに戻す
            FallFlag = false; 
            Debug.Log("Camera is reset.");
        }
    }
}