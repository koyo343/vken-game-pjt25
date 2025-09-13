using System.Collections.Generic;
using UnityEngine;

// インスペクターで表示・編集可能にするための属性
[System.Serializable]
public class EventToScoreData
{
    public string EventStr = "";
    public int Score = 100;
}

public class ScoreDictionaryManager : MonoBehaviour
{
    [Header("スコア一覧")]
    [SerializeField]
    private EventToScoreData[] _ScoreData;

    // 登録された辞書本体
    private Dictionary<string, int> _scoreDictionary;

    // ScoreManagerクラスのインスタンスを保持するフィールド
    // StrToScoreメソッドからアクセス可能にするために必要
    private ScoreManager _scoreManager;

    void Awake()
    {
        // 辞書を初期化
        _scoreDictionary = new Dictionary<string, int>();

        // _ScoreData配列の要素を辞書に登録
        foreach (var data in _ScoreData)
        {
            if (!_scoreDictionary.ContainsKey(data.EventStr))
            {
                _scoreDictionary.Add(data.EventStr, data.Score);
            }
            else
            {
                Debug.LogWarning($"キー '{data.EventStr}' は既に存在するため、登録をスキップしました。");
            }
        }

        // ScoreManagerオブジェクトとコンポーネントを検索してフィールドに格納
        GameObject scoreManagerObject = GameObject.Find("ScoreManager");
        if (scoreManagerObject != null)
        {
            _scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
            if (_scoreManager == null)
            {
                Debug.LogError("ScoreManagerコンポーネントがGameObject 'ScoreManager'に見つかりません。");
            }
        }
        else
        {
            Debug.LogError("ゲームオブジェクト 'ScoreManager' が見つかりません。");
        }
    }
    /*
        // イベント名（文字列）を渡してスコアを加算するメソッド
        public void StrToScore(string eventName)
        {
            // 辞書にキーが存在するかチェック
            if (_scoreDictionary.ContainsKey(eventName))
            {
                // スコアを取得
                int scoreToAdd = _scoreDictionary[eventName];

                // ScoreManagerのScoreAddメソッドを呼び出す
                if (_scoreManager != null)
                {
                    _scoreManager.AddScore(eventName, scoreToAdd);
                }
            }
            else
            {
                Debug.LogError($"イベント '{eventName}' は辞書に登録されていません。");
            }
        }
        */
}