using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public CameraController cameracontroller;
    Vector3 myPosition;

    void Start()
    {
        // カメラオブジェクトを見つけ、CameraController コンポーネントを取得する
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            cameracontroller = mainCamera.GetComponent<CameraController>();
        }

        // NullReferenceExceptionを防ぐための最終チェック
        if (cameracontroller == null)
        {
            Debug.LogError("SavePoint: CameraControllerが見つかりません。CameraControllerがMain Cameraにアタッチされているか確認してください。");
            enabled = false;
        }
    }

    void Update()
    {
        // 何もなし
    }

    // ★★★ 修正済み: 衝突検出のための引数を追加 ★★★
    void OnTriggerEnter2D(Collider2D other)
    {
        // ボス/敵の弾などとの接触で誤作動しないように、Playerタグを持つオブジェクトか確認
        if (!other.CompareTag("Player"))
        {
            return;
        }
        
        // StartでCameraControllerが見つからなかった場合のためにチェック
        if (cameracontroller == null)
        {
            Debug.LogError("SavePoint: CameraControllerがNullです。セーブできません。");
            return;
        }
        
        myPosition = transform.position;
        
        cameracontroller.SavePoint.x = myPosition.x;
        cameracontroller.SavePoint.y = myPosition.y; 
        cameracontroller.SavePoint.z = myPosition.z; 

        // 2Dゲームでは通常Zは0ですが、念のためログ表示を修正
        Debug.Log("セーブポイントを(" + cameracontroller.SavePoint.x + "," + cameracontroller.SavePoint.y + ")に更新しました");
    }
}