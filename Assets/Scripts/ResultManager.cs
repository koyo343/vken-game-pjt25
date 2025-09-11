using UnityEngine;
using UnityEngine.UI;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using System.Threading.Tasks;
using TMPro;
using System.Collections.Generic;

public class ResultManager : MonoBehaviour
{
    // UIコンポーネントをUnityエディタからアタッチ
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI totalScoreText;
    
    public TextMeshProUGUI playScoreText;
    public TextMeshProUGUI timeLeftsText;
    public TextMeshProUGUI timeScoreText;
    
    public Image characterImage;
    
    // キャラクター名と対応する画像を紐付ける辞書
    private Dictionary<string, Sprite> characterSprites = new Dictionary<string, Sprite>();

    // AWS DynamoDBへの接続情報
    private AmazonDynamoDBClient client;
    private DynamoDBContext context;

    void Awake()
    {
        // ここで画像ファイルを辞書に登録
        // 🚨 必ずAssets/Resourcesフォルダに画像ファイルを配置してください 🚨
        characterSprites.Add("ときのそら", Resources.Load<Sprite>("Materials/Chara/temp_tokino.jpg"));
        characterSprites.Add("剣持刀也", Resources.Load<Sprite>("Materials/Chara/temp_kenmochi.jpg"));
        characterSprites.Add("月ノ美兎", Resources.Load<Sprite>("Materials/Chara/temp_tsukino.jpg"));
        characterSprites.Add("一ノ瀬うるは", Resources.Load<Sprite>("Materials/Chara/temp_ichinose.jpg"));
    }

    void Start()
    {
        // AWS認証情報の初期化を必ず最初に行う
        AWSCredentials.Initialize();

        // UIコンポーネントが有効か確認
        if (playerNameText == null || totalScoreText == null || playScoreText == null || timeLeftsText == null || timeScoreText == null || characterImage == null)
        {
            Debug.LogError("リザルト画面のUIコンポーネントがアタッチされていません！");
            return;
        }

        // AWSクライアントの初期化
        client = new AmazonDynamoDBClient(
            AWSCredentials.AccessKey,
            AWSCredentials.SecretKey,
            RegionEndpoint.GetBySystemName(AWSCredentials.Region)
        );
        DynamoDBContextConfig config = new DynamoDBContextConfig();
        context = new DynamoDBContext(client, config);

        // GameData_Managerから結果を取得
        string playerID = GameData_Manager.Instance.playerID;
        string playerName = GameData_Manager.Instance.playerName;
        int totalScore = GameData_Manager.Instance.currentScore;
        int playScore = GameData_Manager.Instance.PlayScore;
        int timeLefts = GameData_Manager.Instance.TimeLefts;
        int timeScore = GameData_Manager.Instance.TimeScore;
        string selectedCharacter = GameData_Manager.Instance.selectedCharacter;

        // UIに表示
        playerNameText.text = "PlayerName: " + playerName;
        totalScoreText.text = "Total Score: " + totalScore.ToString();
        playScoreText.text = "Play Score: " + playScore.ToString();
        timeLeftsText.text = "Time Lefts: " + timeLefts.ToString() + "s";
        timeScoreText.text = "Time Score: " + timeScore.ToString();

        // キャラクター画像を変更
        if (characterSprites.ContainsKey(selectedCharacter))
        {
            characterImage.sprite = characterSprites[selectedCharacter];
        }
        else
        {
            Debug.LogWarning("選択されたキャラクターの画像が見つかりません: " + selectedCharacter);
        }

        // スコアをDynamoDBに送信
        SaveScoreToDynamoDB(playerID, playerName, totalScore);
    }
    
    /// <summary>
    /// スコアをDynamoDBに非同期で送信するメソッド
    /// 既存スコアより高い場合のみ更新する
    /// </summary>
    private async void SaveScoreToDynamoDB(string playerID, string playerName, int newScore)
    {
        string rankingCategory = "allTime";
        
        var item = new RankingItem
        {
            PlayerID = playerID,
            PlayerName = playerName,
            Score = newScore,
            RankingCategory = rankingCategory
        };
        
        try
        {
            await context.SaveAsync(item);
            Debug.Log($"スコアが正常に保存されました: PlayerID={playerID}, PlayerName={playerName}, Score={newScore}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"DynamoDBへのスコア送信に失敗しました: {e.Message}");
        }
    }
}