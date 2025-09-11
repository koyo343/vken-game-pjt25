using UnityEngine;
using UnityEngine.UI;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using System.Threading.Tasks;

public class ResultManager : MonoBehaviour
{
    // UIコンポーネントをUnityエディタからアタッチ
    public Text playerNameScoreText;
    public Text scoreDetailsText;

    // AWS DynamoDBへの接続情報
    private AmazonDynamoDBClient client;
    private DynamoDBContext context;

    void Start()
    {
        // AWS認証情報の初期化を必ず最初に行う
        AWSCredentials.Initialize();

        // UIコンポーネントが有効か確認
        if (playerNameScoreText == null)
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
        string playerName = GameData_Manager.Instance.playerName;
        int score = GameData_Manager.Instance.currentScore;

        // UIに表示
        playerNameScoreText.text = $"{playerName}：{score}";
        if (scoreDetailsText != null)
        {
            // ここでスコア詳細を計算・表示
            // 例: scoreDetailsText.text = $"スコア詳細：\n{GameData_Manager.Instance.someDetails}";
        }

        // スコアをDynamoDBに送信
        SaveScoreToDynamoDB(playerName, score);
    }
    
    /// <summary>
    /// スコアをDynamoDBに非同期で送信するメソッド
    /// 既存スコアより高い場合のみ更新する
    /// </summary>
    private async void SaveScoreToDynamoDB(string playerName, int newScore)
    {
        // プレイヤーIDの代わりに、プレイヤー名（または他のユニークな識別子）をプライマリーキーとして使用
        string playerId = playerName;

        try
        {
            // 既存のデータを取得する
            RankingItem existingItem = await context.LoadAsync<RankingItem>(playerId);
            
            // 既存データが存在しない、または新しいスコアが既存スコアより高い場合
            if (existingItem == null || newScore > existingItem.Score)
            {
                var newItem = new RankingItem
                {
                    PlayerID = playerId,
                    PlayerName = playerName,
                    Score = newScore,
                    RankingCategory = "allTime",
                    // オプティミスティックロックのためのバージョン管理
                    Version = existingItem?.Version
                };
                
                await context.SaveAsync(newItem);
                Debug.Log($"スコアが正常に更新されました: PlayerName={playerName}, NewScore={newScore}");
            }
            else
            {
                Debug.Log($"新しいスコア({newScore})は既存スコア({existingItem.Score})より低いため、更新しませんでした。");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"スコアの保存中にエラーが発生しました: {e.Message}");
        }
    }
}