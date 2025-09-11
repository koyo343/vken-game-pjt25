using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使用する場合に必要
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.DynamoDBv2.Model;

// RankingEntryクラスは別のファイルで定義してください
// public class RankingEntry { public string playerName; public int score; }

public class RankingManager : MonoBehaviour
{
    // UIコンポーネントをUnityエディタからアタッチ
    public GameObject rankingContainer;
    public GameObject rankingEntryPrefab;
    public Button nextButton;
    public Button previousButton;
    public TextMeshProUGUI pageText;

    // 内部変数
    private List<RankingEntry> rankingData = new List<RankingEntry>();
    private int entriesPerPage = 10;
    private int currentOffset = 0;
    private int totalPages = 0;

    void Start()
    {
        AWSCredentials.Initialize();

        // ボタンのクリックイベントを登録
        nextButton.onClick.AddListener(NextPage);
        previousButton.onClick.AddListener(PreviousPage);

        // DynamoDBからランキングデータを非同期で取得
        LoadRankingDataFromDynamoDB();
    }

    /// <summary>
    /// DynamoDBからランキングデータを非同期で取得し、UIに反映します。
    /// </summary>
    private async void LoadRankingDataFromDynamoDB()
    {
        AmazonDynamoDBClient client = new AmazonDynamoDBClient(
            AWSCredentials.AccessKey,
            AWSCredentials.SecretKey,
            RegionEndpoint.GetBySystemName(AWSCredentials.Region)
        );

        // GSIを使ってスコアの降順でクエリを実行するリクエストを作成
        var request = new QueryRequest
        {
            TableName = "RankingTable",
            IndexName = "ScoreIndex",
            KeyConditionExpression = "rankingCategory = :v_cat",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":v_cat", new AttributeValue { S = "allTime" } }
            },
            // スコアの高い順（降順）にソート
            ScanIndexForward = false,
        };

        try
        {
            // 非同期でクエリを実行し、レスポンスを待つ
            var response = await client.QueryAsync(request);
            rankingData.Clear();

            foreach (var item in response.Items)
            {
                // アイテムからplayerNameとscoreを読み取り、リストに追加
                if (item.ContainsKey("playerName") && item.ContainsKey("score"))
                {
                    rankingData.Add(new RankingEntry
                    {
                        playerName = item["playerName"].S,
                        score = int.Parse(item["score"].N)
                    });
                }
            }
            Debug.Log("取得したランキングデータの件数: " + rankingData.Count);

            // データを取得後、総ページ数を計算しUIを更新
            totalPages = Mathf.CeilToInt((float)rankingData.Count / entriesPerPage);
            UpdateUI();
        }
        catch (System.Exception e)
        {
            // エラーが発生した場合、コンソールにログを出力
            Debug.LogError($"DynamoDBからのデータ取得に失敗しました: {e.Message}");
        }
    }

    /// <summary>
    /// 現在のオフセットに基づいてランキングUIを更新します。
    /// </summary>
    private void UpdateUI()
    {
        // 既存のランキングエントリをすべて削除
        // プレイモード中のみ破棄処理を実行する
        if (Application.isPlaying)
        {
            foreach (Transform child in rankingContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }

        // 現在のオフセットから、10人分のUIを動的に生成
        for (int i = 0; i < entriesPerPage; i++)
        {
            int dataIndex = currentOffset + i;
            if (dataIndex < rankingData.Count)
            {
                // デバッグログでデータの正しさを確認
                Debug.Log("データ確認: " + "PlayerName = " + rankingData[dataIndex].playerName + ", Score = " + rankingData[dataIndex].score);
                
                // プレハブをインスタンス化し、親を設定
                GameObject entryObject = Instantiate(rankingEntryPrefab, rankingContainer.transform);

                // プレハブ内のUI要素にデータを設定する
                TextMeshProUGUI rankText = entryObject.transform.Find("RankText").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI nameText = entryObject.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI scoreText = entryObject.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();

                if (rankText != null) {
                    rankText.text = (dataIndex + 1).ToString();
                }
                if (nameText != null) {
                    nameText.text = rankingData[dataIndex].playerName;
                }
                if (scoreText != null) {
                    scoreText.text = rankingData[dataIndex].score.ToString();
                }
            }
        }

        // ページ表示テキストとボタンの状態を更新
        int currentPage = (currentOffset / entriesPerPage) + 1;
        pageText.text = $"{currentPage} / {totalPages}";
        previousButton.interactable = currentOffset > 0;
        nextButton.interactable = (currentOffset + entriesPerPage) < rankingData.Count;
    }
    
    /// <summary>
    /// 「次へ」ボタンが押されたときの処理。次のページに移動します。
    /// </summary>
    public void NextPage()
    {
        currentOffset += entriesPerPage;
        // 最終ページを超えないようにオフセットを調整
        if (currentOffset >= rankingData.Count)
        {
            currentOffset = Mathf.Max(0, rankingData.Count - entriesPerPage);
        }
        UpdateUI();
    }
    
    /// <summary>
    /// 「前へ」ボタンが押されたときの処理。前のページに移動します。
    /// </summary>
    public void PreviousPage()
    {
        currentOffset = Mathf.Max(currentOffset - entriesPerPage, 0);
        UpdateUI();
    }
}