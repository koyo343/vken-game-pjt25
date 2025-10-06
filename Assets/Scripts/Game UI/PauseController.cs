using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [Header("ポーズ画面のUI")]
    public GameObject PausePanel; // インスペクターからPausePanelをアタッチする

    public GameObject SettingsPanel; // インスペクターからSettingsPanelをアタッチする

    public Button Pausebutton;

    private bool isPaused = false; // ポーズ状態を管理するフラグ

    private bool issetting = false; // 設定画面を管理するフラグ

    void Awake()
    {
        Pausebutton.onClick.AddListener(CheckPause);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckPause();
        }
    }

    void CheckPause()
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

    void Start()
    {
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    // ■ ゲームをポーズする処理
    public void PauseGame()
    {
        // ポーズ画面をアクティブにする
        PausePanel.SetActive(true);
        // ゲーム内の時間を停止する
        Time.timeScale = 0f;
        // ポーズ状態フラグをtrueにする
        isPaused = true;
    }

    // ■ ゲームを再開する処理
    public void ResumeGame()
    {
        // ポーズ画面を非アクティブにする
        PausePanel.SetActive(false);
        // ゲーム内の時間を元に戻す
        Time.timeScale = 1f;
        // ポーズ状態フラグをfalseにする
        isPaused = false;
    }

    // ■ 設定画面に入る処理
    public void SettingGame()
    {
        // 設定画面をアクティブにする
        SettingsPanel.SetActive(true);
        // ポーズ画面を非アクティブにする
        PausePanel.SetActive(false);
        // 設定状態フラグをtrueにする
        issetting = true;
    }

    // ■ ポーズ画面に戻る処理
    public void ResumePause()
    {
        // 設定画面を非アクティブにする
        SettingsPanel.SetActive(false);
        // ポーズ画面を非アクティブにする
        PausePanel.SetActive(true);
        // 設定状態フラグをfalseにする
        issetting = false;
    }
}