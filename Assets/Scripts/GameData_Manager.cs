// GameData_Manager.cs
using UnityEngine;

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
    public int TimeLefts { get; private set; }
    public int TimeScore { get; private set; }

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
    public void SetGameResult(int playScore, int timeLefts, int timeScore)
    {
        PlayScore = playScore;
        TimeLefts = timeLefts;
        TimeScore = timeScore;
    }
}