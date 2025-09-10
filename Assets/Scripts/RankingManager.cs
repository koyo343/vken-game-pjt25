using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

public class RankingManager : MonoBehaviour
{
    // ランキング表示用のUIコンポーネントをUnityエディタから設定
    public GameObject rankingContainer;
    public GameObject rankingEntryPrefab; // プレイヤー情報を表示するプレハブ
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
        // ボタンにメソッドを登録
        nextButton.onClick.AddListener(NextPage);
        previousButton.onClick.AddListener(PreviousPage);

        // ランキングデータをデータベースから取得開始
        LoadRankingDataFromDynamoDB();
    }

    /// <summary>
    /// DynamoDBからランキングデータを非同期で取得するメソッド
    /// </summary>
    private async void LoadRankingDataFromDynamoDB()
    {
        AmazonDynamoDBClient client = new AmazonDynamoDBClient();

        // GSIを使ってスコアの降順でクエリを実行
        var request = new QueryRequest
        {
            TableName = "RankingTable", // あなたのテーブル名に置き換えてください
            IndexName = "ScoreIndex",    // 作成したGSIの名前に置き換えてください
            KeyConditionExpression = "rankingCategory = :v_cat",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":v_cat", new AttributeValue { S = "allTime" } }
            },
            ScanIndexForward = false, // これが降順（高いスコアから）の鍵です
        };

        try
        {
            var response = await client.QueryAsync(request);

            rankingData.Clear(); // 既存データをクリア
            foreach (var item in response.Items)
            {
                rankingData.Add(new RankingEntry
                {
                    playerName = item["playerName"].S,
                    score = int.Parse(item["score"].N)
                });
            }

            // データの取得が完了したら、総ページ数を計算してUIを更新
            totalPages = Mathf.CeilToInt((float)rankingData.Count / entriesPerPage);
            UpdateUI();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"DynamoDBからのデータ取得に失敗しました: {e.Message}");
        }
    }

    /// <summary>
    /// UI（ランキング一覧、ページ表示、ボタンの状態）を更新する
    /// </summary>
    private void UpdateUI()
    {
        // 既存のランキングエントリをすべて削除
        foreach (Transform child in rankingContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // 現在のページに表示するデータを取得
        for (int i = 0; i < entriesPerPage; i++)
        {
            int dataIndex = currentOffset + i;
            if (dataIndex < rankingData.Count)
            {
                // プレイヤー情報プレハブをインスタンス化
                GameObject entryObject = Instantiate(rankingEntryPrefab, rankingContainer.transform);
                // 🚨 ここはプレハブ内のUIコンポーネントを操作するコードを記述 🚨
                // 例: 順位、名前、スコアを設定
                // entryObject.transform.Find("RankText").GetComponent<Text>().text = (dataIndex + 1).ToString();
                // entryObject.transform.Find("NameText").GetComponent<Text>().text = rankingData[dataIndex].playerName;
                // entryObject.transform.Find("ScoreText").GetComponent<Text>().text = rankingData[dataIndex].score.ToString();
            }
        }

        // ページ表示を更新
        int currentPage = (currentOffset / entriesPerPage) + 1;
        pageText.text = $"{currentPage} / {totalPages}";

        // ボタンの表示状態を制御
        previousButton.interactable = currentOffset > 0;
        nextButton.interactable = (currentOffset + entriesPerPage) < rankingData.Count;
    }

    // 「次へ」ボタンが押されたときの処理
    public void NextPage()
    {
        currentOffset = Mathf.Min(currentOffset + entriesPerPage, (rankingData.Count - 1) / entriesPerPage * entriesPerPage);
        UpdateUI();
    }

    // 「前へ」ボタンが押されたときの処理
    public void PreviousPage()
    {
        currentOffset = Mathf.Max(currentOffset - entriesPerPage, 0);
        UpdateUI();
    }
}