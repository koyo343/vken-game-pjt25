using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject clearPanel; // クリア画面のパネル

    // ゲームクリアの判定と演出を行う関数
    public void GameClear()
    {
        Debug.Log("ゲームクリア！");
        clearPanel.SetActive(true);
        Time.timeScale = 0f; // ゲームを一時停止する
    }
}