using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // どのキーでゲームを終了させるか設定
    public KeyCode exitKey = KeyCode.Escape;

    void Update()
    {
        // 指定されたキーが押されたかチェック
        if (Input.GetKeyDown(exitKey))
        {
            // アプリケーションを終了させる
            QuitGame();
        }
    }

    // ゲーム終了用のメソッド
    public void QuitGame()
    {
        // Unityエディターで実行している場合は、再生を停止
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // ビルドされたゲームの場合はアプリケーションを終了
            Application.Quit();
        #endif
    }
}