// CharacterSelectManager.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;



public class CharacterSelectManager : MonoBehaviour
{
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
        characterNameDatas.Add(0, "ときのそら");
        characterNameDatas.Add(1, "剣持刀也");
        characterNameDatas.Add(2, "月ノ美兎");
        characterNameDatas.Add(3, "一ノ瀬うるは");
    } 

    // ゲーム開始時に実行
    void Start()
    {
        startButton.onClick.AddListener(OnCharacterSelected);

        // ゲーム開始ボタンにメソッドを登録
        //startButton.onClick.AddListener(OnGameStart);
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