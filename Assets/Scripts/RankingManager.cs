using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.DynamoDBv2.Model;

public class RankingManager : MonoBehaviour
{
    // UIコンポーネント
    public GameObject rankingContainer;
    public GameObject rankingEntryPrefab;
    public Button nextButton;
    public Button previousButton;
    public Text pageText;

    // 内部変数
    private List<RankingEntry> rankingData = new List<RankingEntry>();
    private int entriesPerPage = 10;
    private int currentOffset = 0;
    private int totalPages = 0;

    void Start()
    {
        // ボタンのクリックイベントを登録
        nextButton.onClick.AddListener(NextPage);
        previousButton.onClick.AddListener(PreviousPage);

        // DynamoDBからランキングデータを取得
        LoadRankingDataFromDynamoDB();
    }

    /// <summary>
    /// DynamoDBからランキングデータを非同期で取得
    /// </summary>
    private async void LoadRankingDataFromDynamoDB()
    {
        AmazonDynamoDBClient client = new AmazonDynamoDBClient(
            AWSCredentials.AccessKey,
            AWSCredentials.SecretKey,
            RegionEndpoint.APNortheast1 // あなたのリージョンに合わせる
        );

        var request = new QueryRequest
        {
            TableName = "RankingTable",
            IndexName = "ScoreIndex",
            KeyConditionExpression = "rankingCategory = :v_cat",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":v_cat", new AttributeValue { S = "allTime" } }
            },
            ScanIndexForward = false, // 降順ソート
        };

        try
        {
            var response = await client.QueryAsync(request);
            rankingData.Clear();
            foreach (var item in response.Items)
            {
                // データが存在しない場合のエラーを避けるためのチェック
                if (item.ContainsKey("playerName") && item.ContainsKey("score"))
                {
                    rankingData.Add(new RankingEntry
                    {
                        playerName = item["playerName"].S,
                        score = int.Parse(item["score"].N)
                    });
                }
            }

            totalPages = Mathf.CeilToInt((float)rankingData.Count / entriesPerPage);
            UpdateUI();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"DynamoDBからのデータ取得に失敗しました: {e.Message}");
        }
    }

    /// <summary>
    /// UI（ランキング一覧、ページ表示、ボタンの状態）を更新
    /// </summary>
    private void UpdateUI()
    {
        foreach (Transform child in rankingContainer.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < entriesPerPage; i++)
        {
            int dataIndex = currentOffset + i;
            if (dataIndex < rankingData.Count)
            {
                GameObject entryObject = Instantiate(rankingEntryPrefab, rankingContainer.transform);
                // ここでプレハブ内のUIに値を設定する
                // 例: entryObject.transform.Find("RankText").GetComponent<Text>().text = (dataIndex + 1).ToString();
            }
        }

        int currentPage = (currentOffset / entriesPerPage) + 1;
        pageText.text = $"{currentPage} / {totalPages}";
        previousButton.interactable = currentOffset > 0;
        nextButton.interactable = (currentOffset + entriesPerPage) < rankingData.Count;
    }

    // 「次へ」ボタンの処理
    public void NextPage()
    {
        currentOffset = Mathf.Min(currentOffset + entriesPerPage, (rankingData.Count - 1) / entriesPerPage * entriesPerPage);
        UpdateUI();
    }

    // 「前へ」ボタンの処理
    public void PreviousPage()
    {
        currentOffset = Mathf.Max(currentOffset - entriesPerPage, 0);
        UpdateUI();
    }
}