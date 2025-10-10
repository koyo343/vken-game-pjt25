// CharacterSelectManager.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.EventSystems; // EventSystemを扱うために必要



public class CharacterSelectManager : MonoBehaviour
{
    // ゲームシーンに遷移するボタン
    public Button startButton;

    // ゲームシーンのシーン名
    public string gameSceneName = "Game_Scene";

    private Dictionary<int, string> characterNameDatas = new Dictionary<int, string>();

    public ObjectToggle objectToggle;
    public GameData_Manager GameData_Manager;
    public GameObject confirmationPanel; // 確認パネルの参照
    public GameObject NoButton; // Noボタンの参照
    public GameObject StartButton; // GameStartボタンの参照


    void Awake()
    {
        // ここで画像ファイルを辞書に登録
        // 🚨 必ずAssets/Resourcesフォルダに画像ファイルを配置してください 🚨
        characterNameDatas.Add(0, "ときのそら");
        characterNameDatas.Add(1, "剣持刀也");
        characterNameDatas.Add(2, "月ノ美兎");
        characterNameDatas.Add(3, "一ノ瀬うるは");
        characterNameDatas.Add(4, "キズナアイ");
    } 

    // ゲーム開始時に実行
    void Start()
    {
        startButton.onClick.AddListener(OnCharacterSelected);

        // ゲーム開始ボタンにメソッドを登録
        startButton.onClick.AddListener(OnGameStart);
        GameData_Manager.CheckNullInstance();
    }
    

    /// <summary>
    /// キャラクター選択ボタンが押されたときの処理
    /// </summary>
    public void OnCharacterSelected()
    {
        Debug.Log("OnCharacterSelected is called.");
        // 選択されたキャラクターの名前をGameData_Managerに格納
        if (GameData_Manager.Instance != null)
        {
            Debug.Log("GameData_Manager.Instance != null");
            for (int i = 0; i < objectToggle.characterDatas.Length; i++)
            {
                if (objectToggle.characterDatas[i].characterFlag == 1)
                {
                    Debug.Log("objectToggle.characterDatas[i].characterFlag == 1");
                    if (GameData_Manager.Instance != null)
                    {
                        Debug.Log("GameData_Manager.Instance != null");
                        if (characterNameDatas.ContainsKey(i))
                        {
                            string selectedCharacterName = characterNameDatas[i];
                            GameData_Manager.Instance.SetCharacter(selectedCharacterName);
                            Debug.Log($"キャラクターが保存されました: {selectedCharacterName}");
                        }
                        else
                        {
                            Debug.LogWarning($"キー {i} は存在しません。");
                        }
                    }
                    else
                    {
                        Debug.LogError("GameData_Manager.Instance == null");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("GameData_Manager.Instanceが初期化されていません！");
        }
    }
    /// <summary>
    /// ゲーム開始ボタンが押されたときの処理
    /// </summary>
    public void OnGameStart()
    {
        // 確認パネルを表示
        confirmationPanel.SetActive(true);

        // 一旦選択をクリア
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(NoButton);
    }

    public void OnConfirmStart()
    {
        // 確認パネルを非表示にしてゲームシーンに遷移
        confirmationPanel.SetActive(false);
        SceneManager.LoadScene(gameSceneName);
    }
    
    public void OnCancelStart()
    {
        // 確認パネルを非表示にして選択画面に戻る
        confirmationPanel.SetActive(false);
        // 一旦選択をクリア
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(StartButton);
    }
}