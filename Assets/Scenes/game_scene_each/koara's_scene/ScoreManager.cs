using UnityEngine;
using TMPro; // TextMesh Pro を使うために必要

public class ScoreManager : MonoBehaviour
{
    [Header("スコア表示用のテキストUI")]
    public TextMeshProUGUI scoreText; // インスペクターからScoreTextをアタッチ
    public ScoreManager scoreManager;       // ScoreManagerへの参照
    public Debugmode debugmode; // Debugmodeへの参照

    public int currentScore = 0; // 現在のスコアを管理する変数

    // ■ ゲーム開始時に一度だけ呼ばれる処理
    void Start()
    {
        // 最初のスコア表示を更新する
        UpdateScoreDisplay();
    }

    // ■ 毎フレーム呼ばれる処理
    void Update()
    {
        AddScoreDebug();//デバッグ用

        //　後々、敵を倒した時などのスコア追加で実装
    }

    // ■ スコアを加算するための処理
    public void AddScore(int amount)
    {
        currentScore += amount; // スコアを加算
        if(currentScore < 0){
            currentScore = 0;
        }
        UpdateScoreDisplay();    // 表示を更新
    }

    public void AddScoreDebug(){
        if (scoreManager != null && debugmode != null && debugmode.mode){
            if(Input.GetKeyDown(KeyCode.P)){
                AddScore(100);
                Debug.Log("スコアを100追加しました！");
            }else if(Input.GetKeyDown(KeyCode.O)){
                AddScore(-100);
                Debug.Log("スコアを100減らしました！");
            }
        }
    }

    // ■ 画面のスコア表示を更新する処理
    void UpdateScoreDisplay()
    {
        // テキストを更新する
        scoreText.text = "Score: " + currentScore.ToString();
    }
}