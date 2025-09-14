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

        playerNameInput.onEndEdit.AddListener(OnEndEditAction);
    }

    private void OnEndEditAction(string text)
    {
        // Enterキーが押されたことを確認
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log($"InputField中でEnterキーが押されました");
            OnSaveData();
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            // どのキーが押されたか
            KeyCode pressedKey = GetPressedKey();
            

             // GetPressedKeyがマウスのボタンを返してきたら、何もせずに処理を中断する
            if (pressedKey == KeyCode.Mouse0 || pressedKey == KeyCode.Mouse1 || pressedKey == KeyCode.Mouse2){
                return; // このフレームの処理はここで終わり
            }
            // GetPressedKeyが何もキーを見つけられなかった場合も中断する
            if (pressedKey == KeyCode.None){
                return;
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log($"Enterキーが押されました: {pressedKey}");
                OnSaveData();
            }
            else
            {
                Debug.Log($"Enterキー以外が押されました: {pressedKey}");
            }
        }
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

    private KeyCode GetPressedKey()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                return key;
            }
        }
        return KeyCode.None;
    }
}