using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.IO;

public static class DatabaseSwitcher
{
    public static bool isLocal = true;

    public static bool isServerUpload = false;

    public static bool LocalmodeisChenged = false;

    public static void SwitchDatabase()
    {
        Debug.Log("SwitchDatabase is called");
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
                //isLoaded = true; // ロード済みとしてマークし、再試行を防ぐ
                return;
            }

            

            AWSCredentials.Initialize();
            LoadingCSV.isInitializedSwitch();

            if (!AWSCredentials.ServerConnected)
            {
                Debug.Log("Server is not connected.");
                return;
            }

            LocalmodeisChenged = true;


            isLocal = false;

            Debug.Log("Focus Database was switched to DynamoDB");
        }
        else
        {
            isLocal = true;
            isServerUpload = false;

            LoadingCSV.Initialize();
            AWSCredentials.isInitializedSwitch();

            LocalmodeisChenged = true;

            Debug.Log("Focus Database was switched to Local");
        }
    }
    
    public static void SwitchServerUpload()
    {
        Debug.Log("SwitchServerUpload is called");

        AWSCredentials.Initialize();


        if (!isServerUpload)
        {
            if (AWSCredentials.ServerConnected)
            {
                isServerUpload = true;
                Debug.Log("Datas Uploading to Server");
            }
            else
            {
                Debug.Log("Server is not connected.");
            }
        }
        else
        {
            isServerUpload = false;
            Debug.Log("Datas Uploading to Local only");
        }
    }
}
