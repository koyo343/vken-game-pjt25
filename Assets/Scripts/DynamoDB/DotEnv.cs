using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// .envファイルを読み込み、環境変数を取得するためのヘルパークラス。
/// </summary>
public static class DotEnv
{
    private static Dictionary<string, string> envVariables = new Dictionary<string, string>();
    private static bool isLoaded = false;

    /// <summary>
    /// .envファイルをロードします。複数回呼び出されても一度しか実行されません。
    /// </summary>
    public static void Load()
    {
        if (isLoaded)
        {
            return;
        }

        // プロジェクトのルートディレクトリにある.envファイルのパスを生成
        string filePath = Path.Combine(Application.dataPath, "../.env");
        
        // ★デバッグログ①：スクリプトが参照しようとしているパスを表示
        Debug.Log($"Attempting to load .env from: {filePath}");

        if (!File.Exists(filePath))
        {
            // ★デバッグログ②：ファイルが見つからない場合に警告を表示
            Debug.LogWarning("'.env' file not found at the specified path.");
            isLoaded = true; // ロード済みとしてマークし、再試行を防ぐ
            return;
        }

        // ★デバッグログ③：ファイルが見つかったことを表示
        Debug.Log(".env file found. Reading lines.");

        // ファイルの各行を読み込み、キーと値を解析
        foreach (var line in File.ReadAllLines(filePath))
        {
            // ★デバッグログ④：各行の内容を表示
            Debug.Log($"Reading line: {line}");

            // コメント行 (#で始まる行) と空行をスキップ
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
            {
                continue;
            }

            int separatorIndex = line.IndexOf("=");
            if (separatorIndex == -1)
            {
                // ★デバッグログ⑤：=がない不正な行を警告
                Debug.LogWarning($"Skipping malformed line in .env: {line}");
                continue;
            }

            string key = line.Substring(0, separatorIndex).Trim();
            string value = line.Substring(separatorIndex + 1).Trim().Trim('"');

            if (!envVariables.ContainsKey(key))
            {
                envVariables.Add(key, value);
            }
        }
        isLoaded = true;
        
        // ★デバッグログ⑥：読み込まれたキーの数と内容を表示
        Debug.Log($"Loaded {envVariables.Count} key-value pairs from .env.");
        foreach (var entry in envVariables)
        {
            Debug.Log($"Key: {entry.Key}, Value: {entry.Value}");
        }
    }

    /// <summary>
    /// ロードされた環境変数の値を取得します。
    /// </summary>
    public static string Get(string key)
    {
        // ここでのnullチェックは、ArgumentNullExceptionを防ぐ
        if (string.IsNullOrEmpty(key))
        {
            Debug.LogError("Key cannot be null or empty.");
            return null;
        }

        if (envVariables.ContainsKey(key))
        {
            return envVariables[key];
        }

        Debug.LogWarning($"Key '{key}' not found in loaded environment variables.");
        return null;
    }
}