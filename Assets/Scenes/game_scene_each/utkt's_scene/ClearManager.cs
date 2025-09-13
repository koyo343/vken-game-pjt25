using UnityEngine;

public class ClearManager : MonoBehaviour
{
    // トリガーとの接触を検出
    void OnTriggerEnter2D(Collider2D other)
    {
        // 接触したオブジェクトのタグが"Goal"であるかを確認
        if (other.gameObject.CompareTag("Goal"))
        {
            // GameManagerを見つけて、GameClear関数を呼び出す
            FindObjectOfType<GameManager>().GameClear();
        }
    }
}