using UnityEngine;

public class GameClear : MonoBehaviour
{
    public GameObject clearScreenPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーのRigidbody2Dコンポーネントを取得
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            
            // Rigidbody2Dが存在する場合のみ処理を実行
            if (playerRb != null)
            {
                // プレイヤーの速度を強制的にゼロにする
                playerRb.linearVelocity = Vector2.zero;
                
                // 重力の影響もゼロにする（任意）
                // これにより、坂道や空中で止まった際に滑り落ちるのを防ぐ
                playerRb.gravityScale = 0;
            }

            // GameManagerのisGameClearフラグをtrueに設定
            GameManager.isGameClear = true;

            // ゲーム時間を一時停止
            Time.timeScale = 0f;

            // クリア画面を表示
            if (clearScreenPanel != null)
            {
                clearScreenPanel.SetActive(true);
            }
        }
    }
}