using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public static class DatabaseSwitcher
{
    public static bool isLocal = true;

    public void SwitchDatabase()
    {
        if (isLocal)
        {
            // プロジェクトのルートディレクトリにある.envファイルのパスを生成
            string filePath = Path.Combine(Application.dataPath, "../.env");
            
            // ★デバッグログ①：スクリプトが参照しようとしているパスを表示
            Debug.Log($"Attempting to load .env from: {filePath}");

            if (!File.Exists(filePath))
            {
                // ★デバッグログ②：ファイルが見つからない場合に警告を表示
                //Debug.LogWarning("'.env' file not found at the specified path.");
                Debug.Log("Database was not switched");
                isLoaded = true; // ロード済みとしてマークし、再試行を防ぐ
                return;
            }
            
            isLocal = false;

            Debug.Log("Database was switched to DynamoDB");
        }
        else
        {
            isLocal = true;
            Debug.Log("Database was switched to Local");
        }
    }
}
