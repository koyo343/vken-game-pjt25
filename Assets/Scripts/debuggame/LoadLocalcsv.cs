using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使用する場合に必要
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Text;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.DynamoDBv2.Model;

public static class LoadingCSV
{
    private static bool isInitialized = false;
    //public static string FilePath { get; private set; };

    private static Dictionary<string, string[]> csvData = new Dictionary<string, string[]>();

    public static string csvfilePath = Path.Combine(Application.dataPath, "../SavedScoreLocal.csv");

    private const int SCORE_COLUMN_INDEX = 2;
    
    public static void Initialize()
    {
        if (!isInitialized || DatabaseSwitcher.isLocal)
        {
            LoadData();
            Debug.Log("LodingCSV is already initialized");
            return;
        }
        

        if (!File.Exists(csvfilePath))
        {
            Debug.Log("ファイルが存在しないため、初期データを書き込みます。");
            WriteInitialData();
        }

        LoadData();
        Debug.Log("LodingCSV is initialized");

        isInitialized = true;
    }

    public static void isInitializedSwitch()
    {
        isInitialized = false;
    }

    public static void LoadData()
    {
        csvData = new Dictionary<string, string[]>();
        string[] lines = File.ReadAllLines(csvfilePath, Encoding.UTF8);

        foreach (var line in lines.Skip(1))
        {
            var columns = line.Split(',');
            if (columns.Length > 0){
                csvData[columns[0]] = columns;
            }
        }
    }

    public static string[] GetRow(string key)
    {
        if (csvData.ContainsKey(key))
        {
            return csvData[key];
        }
        
        Debug.LogWarning($"キー '{key}' のデータが見つかりません。");
        return null;
    }

    public static void AddRow(string[] rowData)
    {
        string newLine = string.Join(",", rowData);
        File.AppendAllText(csvfilePath, "\n" + newLine, Encoding.UTF8);
        
        // 新しいデータを辞書にも追加
        if (rowData.Length > 0)
        {
            csvData[rowData[0]] = rowData;
        }
    }
    private static void WriteInitialData()
    {
        string header = "PlayerID, PlayerName, Score, RankingCategory";
        // File.WriteAllLines()が新しいファイルを自動で作成する
        File.WriteAllLines(csvfilePath, new[] { header}, Encoding.UTF8);
    }

    public static IEnumerable<string[]> GetRowsSortByScore()
    {
        var allRows = csvData.Values;

        var sortedRows = allRows.OrderByDescending(row =>
        {
            if (row.Length > SCORE_COLUMN_INDEX && int.TryParse(row[SCORE_COLUMN_INDEX], out int score))
            {
                return score;
            }
            return 0;
        });

        return sortedRows;
    }

    public static void DeleteFile()
    {
        if (File.Exists(csvfilePath))
        {
            File.Delete(csvfilePath);
            csvData.Clear();
            Debug.Log($"CSVファイルが削除されました: {csvfilePath}");
        }
        else
        {
            Debug.LogWarning("削除対象のファイルが見つかりませんでした。");
        }
    }

}