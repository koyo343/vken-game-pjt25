using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreDebug : MonoBehaviour
{
    //ゲーム上にデバッグモードを表示する機能
     [Header("デバッグモード表示用UI")]
    public TextMeshProUGUI debugText; // 変数名をより分かりやすく

    [Header("デバッグモード有効時に表示する文字")]
    public string debugModeMessage = "DEBUG MODE";

    // 遷移先のシーン名
    public string targetSceneName;

    // 期待するキーの順番
    public KeyCode[] sequence = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.B };

    // 現在、シーケンスの何番目を待っているか
    private int currentSequenceIndex = 0;

    // 連続入力とみなす時間間隔
    public float timeOutSeconds = 1.5f;
    private float lastInputTime;

    //デバッグモードか否かの判定
    public bool mode = false;

    void Start()
    {
        if (debugText != null)
        {
            debugText.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        // タイムアウトのチェック
        if (Time.time - lastInputTime > timeOutSeconds && currentSequenceIndex > 0)
        {
            Debug.Log("タイムアウト！シーケンスがリセットされました。");
            currentSequenceIndex = 0;
        }

        // キーが押されたかチェック
        if (Input.anyKeyDown)
        {
            // どのキーが押されたか
            KeyCode pressedKey = GetPressedKey();

             // GetPressedKeyがマウスのボタンを返してきたら、何もせずに処理を中断する
            if (pressedKey == KeyCode.Mouse0 || pressedKey == KeyCode.Mouse1 || pressedKey == KeyCode.Mouse2){
                return; // このフレームの処理はここで終わり
            }
            // GetPressedKeyが何もキーを見つけられなかった場合も中断する
            if (pressedKey == KeyCode.None){
                return;
            }


            // 正しいキーが押されたか確認
            if (currentSequenceIndex < sequence.Length && pressedKey == sequence[currentSequenceIndex]){
                // 正しいキーが押されたので、次のインデックスへ進める
                currentSequenceIndex++;
                lastInputTime = Time.time;
                Debug.Log($"正しいキーが押されました: {pressedKey}。");

                // シーケンスが完了したかチェック
                if (currentSequenceIndex >= sequence.Length)
                {
                    Debug.Log("シーケンスが完了しました！");
                    currentSequenceIndex = 0; // リセット
                    mode = true;
                    if (debugText != null){
                        debugText.text = debugModeMessage;
                        debugText.gameObject.SetActive(true);
                        }
                }
            }else{
                // 誤ったキーが押された場合、シーケンスをリセット
                // ただし、もし最初のキーが押された直後に別のキーが押された場合、
                // インデックスを0にリセットする必要はない
                    if (currentSequenceIndex > 0 && pressedKey != sequence[currentSequenceIndex])
                    {
                        Debug.Log("誤ったキーが押されました。シーケンスをリセットします。");
                        currentSequenceIndex = 0;
                    }
                }
            }
        }
    // 押されたキーコードを取得するヘルパーメソッド
    private KeyCode GetPressedKey()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                return key;
            }
        }
        return KeyCode.None;
    }

}