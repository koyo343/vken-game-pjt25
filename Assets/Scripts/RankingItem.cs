// RankingItem.cs
using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("RankingTable")]
public class RankingItem
{
    [DynamoDBHashKey] // これが正しく設定されているか確認
    public string PlayerID { get; set; }

    [DynamoDBProperty]
    public string PlayerName { get; set; }

    [DynamoDBProperty]
    public int Score { get; set; }

    [DynamoDBProperty]
    public string RankingCategory { get; set; }

    [DynamoDBVersion]
    public int? Version { get; set; }
}