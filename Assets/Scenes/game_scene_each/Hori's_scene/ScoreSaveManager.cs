using UnityEngine;

public class ScoreSaveManager : MonoBehaviour
{
    // このスクリプトは、TimerController.csと同じGameObjectにアタッチしてください

    // TimerControllerへの参照
    public TimerControllerforScore TimerControllerforScore;
    public ScoreManager scoreManager; 

    // このメソッドは、ゲームオーバー時に外部から呼ばれます
    public void OnGameOver()
    {
        // ScoreManagerからスコアを取得
        int playScore = scoreManager.currentScore;
        
        // TimerControllerから時間を取得
        float currentTime = TimerControllerforScore.GetCurrentTime();
        float totaltime = TimerControllerforScore.GetTotalTime();

        // TimerScoreを計算: 残り時間 * 10
        int timerScore = Mathf.Max(0, (int)currentTime * 10);
        
        // GameData_Managerにデータを格納
        if (GameData_Manager.Instance != null)
        {
            GameData_Manager.Instance.SetGameResult(playScore, (int)totaltime, timerScore);
        }
        else
        {
            Debug.LogError("GameData_Manager.Instanceが初期化されていません！");
        }
    }
}