// 必要なライブラリを読み込む
using UnityEngine;
using TMPro; // TextMesh Pro を使うために必要

public class TimerController : MonoBehaviour
{
    // --- Unityエディタから設定する項目 ---
    [Header("タイマーのテキストUI")]
    public TextMeshProUGUI timerText; // 画面に時間を表示するテキスト

    [Header("タイマーの初期値（秒）")]
    public float initialTime = 999f; // スタート時の時間

    // --- 内部で使う変数 ---
    private float currentTime;      // 残り時間を管理する変数
    private float timeUpCounter;    // 0秒になった後のカウントアップ用変数
    private bool isTimeUp = false;  // 時間切れになったかどうかを判定するフラグ

    // ■ ゲーム開始時に一度だけ呼ばれる処理
    void Start()
    {
        // 残り時間を初期値でセット
        currentTime = initialTime;
        
        // テキストの色を初期化（白）
        timerText.color = Color.white;
        
        // 最初の表示を更新
        UpdateTimerDisplay();
    }

    // ■ 毎フレーム（1秒間に何十回も）呼ばれる処理
    void Update()
    {
        // まだ時間切れになっていない場合
        if (!isTimeUp)
        {
            // 残り時間を減らしていく
            currentTime -= Time.deltaTime;

            // もし残り時間が0以下になったら
            if (currentTime <= 1f)
            {
                // ピッタリ0秒で止める
                currentTime = 0f;
                // 時間切れフラグを立てる
                isTimeUp = true;
                // テキストの色を赤に変える
                timerText.color = Color.red;
            }
        }
        // 時間切れになった後
        else
        {
            // カウントアップ用の変数を増やしていく
            timeUpCounter += Time.deltaTime;
        }

        // 毎フレーム、画面の表示を更新する
        UpdateTimerDisplay();
    }
    
    // ■ 画面表示を更新する専門の処理
    void UpdateTimerDisplay()
    {
        float displayTime; // 画面に表示する時間

        // 時間切れ前か後かで、表示する時間を切り替える
        if (!isTimeUp)
        {
            displayTime = currentTime;
        }
        else
        {
            displayTime = timeUpCounter;
        }
        
        // 小数点以下を切り捨てて整数にする
        int seconds = (int)displayTime;

        // "D3"という書式設定で、数値を3桁のゼロ埋め文字列に変換する (例: 5 -> "005")
        timerText.text = seconds.ToString("D3");
    }
}