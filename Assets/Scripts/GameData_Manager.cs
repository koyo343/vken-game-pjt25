// GameData_Manager.cs
using UnityEngine;

public class GameData_Manager : MonoBehaviour
{
    public static GameData_Manager Instance { get; private set; }

    public int currentScore { get; private set; }
    public string playerName { get; private set; }

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

    public void SetPlayerResult(string name, int score)
    {
        playerName = name;
        currentScore = score;
    }
}