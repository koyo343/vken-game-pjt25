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

    // Startは最初のフレーム更新の前に一度だけ呼ばれます
    void Awake()
    {
        // 辞書を初期化
        _scoreDictionary = new Dictionary<string, int>();

        // _ScoreData配列の要素を辞書に登録
        foreach (var data in _ScoreData)
        {
            // 同じキーが既に存在するか確認し、存在しない場合のみ追加
            if (!_scoreDictionary.ContainsKey(data.EventStr))
            {
                _scoreDictionary.Add(data.EventStr, data.Score);
            }
            else
            {
                Debug.LogWarning($"キー '{data.EventStr}' は既に存在するため、登録をスキップしました。");
            }
        }

        // テストのために登録内容を表示
        Debug.Log("辞書への登録が完了しました。");
        foreach (var item in _scoreDictionary)
        {
            Debug.Log($"キー: {item.Key}, 値: {item.Value}");
        }

        GameObject scoreManagerObject = GameObject.Find("ScoreManager"); //ScoreManagerを探す
        if (scoreManagerObject != null){
        //検索したGameObjectからScoreManagerコンポーネントを取得
        ScoreManager scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
        if (scoreManager != null){

        }else{
            Debug.LogError("ScoreManagerコンポーネントがGameObject 'ScoreManager'に見つかりません。");
        }
        }else{
            Debug.LogError("ゲームオブジェクト 'ScoreManager' が見つかりません。");
        }
    }

    void StrToScore(string Str){
        if(_scoreDictionary[Str] != NULL){
        scoreManager.AddScore(_scoreDictionary[Str]);
        }else{
            Debug.LogError("イベントが登録されていません");
        }
    }


}