using UnityEngine;

public class BossSpowner : MonoBehaviour
{
    // インスペクターで設定する、アクティブにしたいGameObject
    public GameObject targetObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //当たったオブジェクトがプレイヤーのときのみ動作
        if (other.gameObject.CompareTag("Player"))
        {
            // targetObjectが設定されているか確認
            if (targetObject != null)
            {
                // オブジェクトをアクティブにする（有効化する）
                Debug.Log("ボスがスポーンしました");
                targetObject.SetActive(true);
                Destroy(gameObject); // スクリプトがアタッチされているオブジェクトを破棄
            }
        }
    }
}