using UnityEngine;

public class InvisibleWallController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform InvisibleWall;

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

        Debug.Log("カメラの左端のX座標: " + leftEdge.x);

        if (InvisibleWall == null || Player == null)
        {
            Debug.Log("InvisibleWallまたはPlayerがセットされていません");
        }
        else
        {
            InvisibleWall.position = new Vector3(leftEdge.x, Player.position.y, Player.position.z);
        }
    }
}