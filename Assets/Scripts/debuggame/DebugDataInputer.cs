using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class DebugDataInputer : MonoBehaviour
{
    //public TMP_InputField playerIDInput; 要らないので
    public TMP_InputField playerNameInput;
    public TMP_InputField playScoreInput;
    public TMP_InputField timeLeftsInput;
    public TMP_InputField currentTimeInput;

    public Button saveButton;
    public GameData_Manager GameData_Manager;

    //public InputManager InputManager;

    public Image characterImage;
    public string selectedCharacter;

    // キャラクター名と対応する画像を紐付ける辞書
    private Dictionary<string, Sprite> characterSprites = new Dictionary<string, Sprite>();

    [System.Serializable]
    public class CharacterButtonData
    {
        public Button button;
        public string characterName;
        //public Sprite characterSprite;
    }
    public CharacterButtonData[] characterButtons;

    void Awake()
    {
        // ここで画像ファイルを辞書に登録
        // 🚨 必ずAssets/Resourcesフォルダに画像ファイルを配置してください 🚨
        characterSprites.Add("ときのそら", Resources.Load<Sprite>("Materials/Chara/sora/TokinoSora_stand"));
        characterSprites.Add("剣持刀也", Resources.Load<Sprite>("Materials/Chara/ken/KenmochiToya_stand"));
        characterSprites.Add("月ノ美兎", Resources.Load<Sprite>("Materials/Chara/tsukino/TsukinoMito_stand"));
        characterSprites.Add("一ノ瀬うるは", Resources.Load<Sprite>("Materials/Chara/uruha/Ichinose_stand"));
    }

    void Start()
    {
        // ボタンにクリックイベントを登録
        saveButton.onClick.AddListener(OnSaveInput);
        GameData_Manager.CheckNullInstance();

        //string selectedCharacter;
        string selectedCharacter = GameData_Manager.Instance.selectedCharacter;

        if(selectedCharacter == "dummyChara")
        {
            selectedCharacter = "ときのそら";
        } else {
            selectedCharacter = GameData_Manager.Instance.selectedCharacter;
        }

        foreach(var chara in characterButtons)
        {
            chara.button.onClick.AddListener(() => OnCharacterSelected(chara.characterName));
        }

        if (characterSprites.ContainsKey(selectedCharacter))
        {
            Debug.Log($"Loading sprite for: {selectedCharacter}. Sprite is null: {characterSprites[selectedCharacter] == null}");
            characterImage.sprite = characterSprites[selectedCharacter];
        }

        if (characterSprites.ContainsKey(selectedCharacter))
        {
            characterImage.sprite = characterSprites[selectedCharacter];
        }
        else
        {
            Debug.LogWarning("選択されたキャラクターの画像が見つかりません: " + selectedCharacter);
        }
    }

    private void OnSaveInput()
    {
        /*
        string playerName = playerNameInput.text;
        string charaName = characterNameInput.text;
        int playerName = playerNameInput.text;
        string playerName = playerNameInput.text;
        string playerName = playerNameInput.text;
        */

        if (characterSprites.ContainsKey(selectedCharacter))
        {
            Debug.Log($"Loading sprite for: {selectedCharacter}. Sprite is null: {characterSprites[selectedCharacter] == null}");
            characterImage.sprite = characterSprites[selectedCharacter];
        }

        if (characterSprites.ContainsKey(selectedCharacter))
        {
            characterImage.sprite = characterSprites[selectedCharacter];
        }
        else
        {
            Debug.LogWarning("選択されたキャラクターの画像が見つかりません: " + selectedCharacter);
        }

        // 入力フォームから値を取得
        string dummyPlayerID = ""; 
        string playerName = playerNameInput.text;
        int score = 0;
        int playScore = int.Parse(playScoreInput.text);
        int timeLefts = int.Parse(timeLeftsInput.text);
        int timeScore = int.Parse(timeLeftsInput.text) * 10;

        if (GameData_Manager.Instance != null)
        {
            GameData_Manager.Instance.SetPlayerResult(dummyPlayerID, playerName, score);
            GameData_Manager.Instance.SetGameResult(playScore, timeLefts, timeScore);
        }
        else
        {
            Debug.LogError("GameData_Manager.Instanceが初期化されていません！");
            return;
        }
    }

    /*
    private void SaveDebugInput();
    {
        // 入力フォームから値を取得
        string dummyPlayerID = ""; 
        string playerName = playerNameInput.text;
        int score = int.Parse(scoreInput.text);
        int playScore = int.Parse(playScoreInput.text);
        int timeLefts = int.Parse(timeLeftsInput.text);
        int timeScore = int.Parse(timeScoreInput.text);

        if (GameData_Manager.Instance != null)
        {
            GameData_Manager.Instance.SetPlayerResult(dummyPlayerID, playerName, score);
            GameData_Manager.Instance.SetGameResult(playScore, timeLefts, timeScore);
        }
        else
        {
            Debug.LogError("GameData_Manager.Instanceが初期化されていません！");
            return;
        }
    }
    */

    private void OnCharacterSelected(string characterName)
    {
        if (GameData_Manager.Instance != null)
        {
            GameData_Manager.Instance.SetCharacter(characterName);
        }
        else
        {
            Debug.LogError("GameData_Manager.Instanceが初期化されていません！");
        }

        Sprite characterSprite = characterSprites[characterName];
        
        if (characterImage != null)
        {
            characterImage.sprite = characterSprite;
        }
    }
}