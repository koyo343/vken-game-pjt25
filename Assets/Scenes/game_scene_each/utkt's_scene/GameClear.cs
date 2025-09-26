using UnityEngine;

public class GameClear : MonoBehaviour
{
    public GameObject clearScreenPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManagerのisGameClearフラグをtrueに設定
            GameManager.isGameClear = true;

            Time.timeScale = 0f; // ゲームを一時停止

            // クリア画面を表示
            if (clearScreenPanel != null)
            {
                clearScreenPanel.SetActive(true);
            }
        }
    }
}