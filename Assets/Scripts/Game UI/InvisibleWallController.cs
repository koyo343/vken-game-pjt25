using UnityEngine;

public class InvisibleWallController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform InvisibleWall_Left;//もちろん左端
    public Transform InvisibleWall_Right;//もちろん右端
    public Transform InvisibleWall_Up;//もちろん天井

    public Transform Player;

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.Log("Main Cameraが見つかりません");
            return;
        }

        SetWallPosition();
    }

    void Update()
    {
        //フレームごとにカメラの左端に合わせて壁の位置の更新
        SetWallPosition();
    }

    private void SetWallPosition()
    {
        // カメラの左端のワールド座標をViewportToWorldPointで取得
        Vector3 leftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, mainCamera.nearClipPlane));

        // カメラの右端のワールド座標をViewportToWorldPointで取得
        Vector3 rightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, mainCamera.nearClipPlane));

        // カメラの上端のワールド座標をViewportToWorldPointで取得
        Vector3 upEdge = mainCamera.ViewportToWorldPoint(new Vector3(0f, 1f, mainCamera.nearClipPlane));

        if (InvisibleWall_Left == null || InvisibleWall_Right == null || InvisibleWall_Up == null || Player == null)
        {
            Debug.Log("InvisibleWallまたはPlayerがセットされていません");
        }
        else
        {
            InvisibleWall_Left.position = new Vector3(leftEdge.x, Player.position.y, Player.position.z);
            InvisibleWall_Right.position = new Vector3(rightEdge.x, Player.position.y, Player.position.z);
            InvisibleWall_Up.position = new Vector3(Player.position.x, upEdge.y, Player.position.z);
        }
    }
}