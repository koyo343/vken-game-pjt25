// PNameInputManager.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PNameInputManager : MonoBehaviour
{
    // プレイヤー名を入力するためのInput Field
    public TMP_InputField playerNameInput;

    // データを保存して次のシーンに進むためのボタン
    public Button saveButton;

    void Start()
    {
        // ボタンにクリックイベントを登録
        saveButton.onClick.AddListener(OnSaveData);
    }

    /// <summary>
    /// プレイヤー名をGameData_Managerに保存し、次のシーンに遷移する
    /// </summary>
    private void OnSaveData()
    {
        // Input Fieldからプレイヤー名を取得
        string playerName = playerNameInput.text;

        // playerNameが空でないことを確認
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("プレイヤー名が入力されていません。");
            return;
        }

        // GameData_ManagerにplayerNameを格納
        if (GameData_Manager.Instance != null)
        {
            // playerIDは別のシーンで付与する前提なので、ここでは空のまま
            string dummyPlayerID = ""; 
            int dummyScore = 0;
            GameData_Manager.Instance.SetPlayerResult(dummyPlayerID, playerName, dummyScore);
            Debug.Log($"プレイヤー名を保存しました: {playerName}");
        }
        else
        {
            Debug.LogError("GameData_Manager.Instanceが初期化されていません！");
            return;
        }

        // 次のシーンに遷移
        // ここに次のゲームシーン名を指定してください
        SceneManager.LoadScene("Select_Chara_Scene"); 
    }
}