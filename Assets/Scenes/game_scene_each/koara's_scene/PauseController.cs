using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("ポーズ画面のUI")]
    public GameObject pausePanel; // インスペクターからPausePanelをアタッチする

    private bool isPaused = false; // ポーズ状態を管理するフラグ

    void Update()
    {
        // Escキーが押されたら、ポーズ状態を切り替える
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // もしポーズ中なら、ゲームを再開する
                ResumeGame();
            }
            else
            {
                // もしポーズ中でないなら、ゲームをポーズする
                PauseGame();
            }
        }
    }

    // ■ ゲームをポーズする処理
    public void PauseGame()
    {
        // ポーズ画面をアクティブにする
        pausePanel.SetActive(true);
        // ゲーム内の時間を停止する
        Time.timeScale = 0f;
        // ポーズ状態フラグをtrueにする
        isPaused = true;
    }

    // ■ ゲームを再開する処理
    public void ResumeGame()
    {
        // ポーズ画面を非アクティブにする
        pausePanel.SetActive(false);
        // ゲーム内の時間を元に戻す
        Time.timeScale = 1f;
        // ポーズ状態フラグをfalseにする
        isPaused = false;
    }
}