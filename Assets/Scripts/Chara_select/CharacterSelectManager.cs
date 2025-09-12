// CharacterSelectManager.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;



public class CharacterSelectManager : MonoBehaviour
{
    // 各キャラクター選択ボタンをInspectorで設定する配列
    public Button[] characterButtons;
    
    // ゲームシーンに遷移するボタン
    public Button startButton;

    // ゲームシーンのシーン名
    public string gameSceneName = "GameScene";

    private Dictionary<int, string> characterNameDatas = new Dictionary<int, string>();

    public ObjectToggle objectToggle;


    void Awake()
    {
        // ここで画像ファイルを辞書に登録
        // 🚨 必ずAssets/Resourcesフォルダに画像ファイルを配置してください 🚨
        characterNameDatas.Add(1, "ときのそら");
        characterNameDatas.Add(2, "剣持刀也");
        characterNameDatas.Add(3, "月ノ美兎");
        characterNameDatas.Add(4, "一ノ瀬うるは");
    } 

    // ゲーム開始時に実行
    void Start()
    {
        // 各ボタンにメソッドを登録
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int index = i;
            characterButtons[i].onClick.AddListener(() => OnCharacterSelected(index));
        }

        // ゲーム開始ボタンにメソッドを登録
        //startButton.onClick.AddListener(OnGameStart);
    }

    /// <summary>
    /// キャラクター選択ボタンが押されたときの処理
    /// </summary>
    public void OnCharacterSelected(int charIndex)
    {
        
        // 選択されたキャラクターの名前をGameData_Managerに格納
        if (GameData_Manager.Instance != null)
        {
            for (int i = 0; i < objectToggle.characterDatas.Length; i++)
            {
                if (objectToggle.characterDatas[i].characterFlag == 1)
                {
                    if (GameData_Manager.Instance != null)
                    {      
                        if (characterNameDatas.ContainsKey(i))
                            {
                                string selectedCharacterName = characterNameDatas[i];
                                GameData_Manager.Instance.SetCharacter(selectedCharacterName);
                                Debug.Log($"キャラクターが選択されました: {selectedCharacterName}");
                            }
                            else
                            {
                                Debug.LogWarning($"キー {i} は存在しません。");
                            }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("GameData_Manager.Instanceが初期化されていません！");
        }
    }
    /*
    /// <summary>
    /// ゲーム開始ボタンが押されたときの処理
    /// </summary>
    public void OnGameStart()
    {
        // ゲームシーンに遷移
        SceneManager.LoadScene(gameSceneName);
    }
    */
}