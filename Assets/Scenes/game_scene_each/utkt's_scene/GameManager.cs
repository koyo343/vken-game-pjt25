using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameClear = false; // staticでどこからでもアクセス可能にする

    public GameObject clearPanel;

    public void GameClear()
    {
        Debug.Log("ゲームクリア！");
        isGameClear = true; // ゲームクリア状態をtrueにする
        clearPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}