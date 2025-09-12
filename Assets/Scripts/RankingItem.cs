using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("RankingTable")]
public class RankingItem
{
    // DynamoDBの属性名 "playerID" とマッピング
    [DynamoDBHashKey]
    [DynamoDBProperty("playerID")]
    public string PlayerID { get; set; }

    [DynamoDBProperty("playerName")]
    public string PlayerName { get; set; }

    [DynamoDBProperty("score")]
    public int Score { get; set; }

    [DynamoDBProperty("rankingCategory")]
    public string RankingCategory { get; set; }
}