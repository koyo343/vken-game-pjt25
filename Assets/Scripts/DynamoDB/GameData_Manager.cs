// GameData_Manager.cs
using UnityEngine;
using System;

public class GameData_Manager : MonoBehaviour
{
    public static GameData_Manager Instance { get; private set; }

    public int currentScore { get; private set; }
    public string playerName { get; private set; }
    public string selectedCharacter { get; private set; }
    
    // playerIDを追加
    public string playerID { get; private set; }
    
    // 新しく追加する変数
    public int PlayScore { get; private set; }
    public int TotalTime { get; private set; }
    public int TimeScore { get; private set; }
    public int TotalScore { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
}