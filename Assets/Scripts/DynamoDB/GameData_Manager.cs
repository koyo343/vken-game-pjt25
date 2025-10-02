// GameData_Manager.cs
using UnityEngine;
using System;

public class GameData_Manager : MonoBehaviour
{
    public static GameData_Manager Instance { get; private set; }

    public int currentScore { get; private set; } = 0;
    public string playerName { get; private set; } = "dummyName";
    public string selectedCharacter { get; private set; } = "ときのそら";
    
    // playerIDを追加
    public string playerID { get; private set; } = "dummyID";
    
    // 新しく追加する変数
    public int PlayScore { get; private set; } = 0;
    public int TotalTime { get; private set; } = 0;
    public int TimeScore { get; private set; } = 0;
    public int TotalScore { get; private set; } = 0;
    public int TotalKill { get; private set; } = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }


    }

    public static void CheckNullInstance()
    {
        if (Instance == null)
        {
            // 新しいGameObjectを作成
            GameObject managerObject = new GameObject("GameData_Manager");
            DontDestroyOnLoad(managerObject);
            // スクリプトをアタッチしてInstanceを初期化
            managerObject.AddComponent<GameData_Manager>();
            Debug.Log("Generate GameData Instance");
        } else {
            GameObject managerObject = Instance.gameObject;
            managerObject.AddComponent<GameData_Manager>();
            Debug.Log("GameData Instance already exists");
            return;
        }
    }

    public void InitializePlayerID()
    {
        // 常に新しいGUIDを生成して格納
        playerID = Guid.NewGuid().ToString();
        Debug.Log($"新しいplayerIDを生成しました: {playerID}");
    }

    // playerIDも受け取るように修正
    public void SetPlayerResult(string id, string name, int score)
    {
        playerID = id;
        playerName = name;
        currentScore = score;
    }

    public void SetCharacter(string characterName)
    {
        selectedCharacter = characterName;
    }

    // 新しく追加するメソッド
    public void SetGameResult(int playScore, int totalTime, int timeScore)
    {
        PlayScore = playScore;
        TotalTime = totalTime;
        TimeScore = timeScore;
        TotalScore = PlayScore + TimeScore;
    }

    public void SetTotalKill(int totalKill)
    {
        TotalKill = totalKill;
    }


    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void InitializeAllData()
    {
        currentScore = 0;
        playerName = "dummyName";
        selectedCharacter = "ときのそら";
        playerID = "dummyID";
        PlayScore = 0;
        TotalTime = 0;
        TimeScore = 0;
        TotalScore = 0;
        TotalKill = 0;
    }

    public void AddKillCount()
    {
        TotalKill++;
    }
}