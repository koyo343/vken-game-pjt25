using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    // 入力フォームのコンポーネント
    public TMP_InputField playerIDInput;
    public TMP_InputField playerNameInput;
    public TMP_InputField scoreInput;
    public TMP_InputField playScoreInput;
    public TMP_InputField timeLeftsInput;
    public TMP_InputField timeScoreInput;
    
    // キャラクター画像を表示するImageコンポーネント
    public Image characterImage;
    
    // キャラクター選択ボタンと対応するSprite
    [System.Serializable]
    public class CharacterButtonData
    {
        public Button button;
        public string characterName;
        public Sprite characterSprite;
    }
    public CharacterButtonData[] characterButtons;
    
    // データを保存してリザルトシーンに移動するボタン
    public Button saveButton;

    void Start()
    {
        // ボタンにクリックイベントを登録
        foreach(var chara in characterButtons)
        {
            chara.button.onClick.AddListener(() => OnCharacterSelected(chara.characterName, chara.characterSprite));
        }
        saveButton.onClick.AddListener(OnSaveData);
        
        // 初期表示として、最初のキャラクターの画像をセット
        if (characterButtons.Length > 0)
        {
            OnCharacterSelected(characterButtons[0].characterName, characterButtons[0].characterSprite);
        }
    }

    /// <summary>
    /// キャラクター選択ボタンが押されたときの処理
    /// </summary>
    private void OnCharacterSelected(string characterName, Sprite characterSprite)
    {
        if (GameData_Manager.Instance != null)
        {
            GameData_Manager.Instance.SetCharacter(characterName);
        }
        else
        {
            Debug.LogError("GameData_Manager.Instanceが初期化されていません！");
        }
        
        if (characterImage != null)
        {
            characterImage.sprite = characterSprite;
        }
    }

    /// <summary>
    /// データを保存してシーンを移動するボタンが押されたときの処理
    /// </summary>
    private void OnSaveData()
    {
        // 入力フォームから値を取得
        string playerID = playerIDInput.text;
        string playerName = playerNameInput.text;
        int score = int.Parse(scoreInput.text);
        int playScore = int.Parse(playScoreInput.text);
        int timeLefts = int.Parse(timeLeftsInput.text);
        int timeScore = int.Parse(timeScoreInput.text);

        if (GameData_Manager.Instance != null)
        {
            GameData_Manager.Instance.SetPlayerResult(playerID, playerName, score);
            GameData_Manager.Instance.SetGameResult(playScore, timeLefts, timeScore);
        }
        else
        {
            Debug.LogError("GameData_Manager.Instanceが初期化されていません！");
            return;
        }

        SceneManager.LoadScene("Result_Scene");
    }
}