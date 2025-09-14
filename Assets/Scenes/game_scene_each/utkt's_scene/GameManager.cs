using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 静的変数で、どのスクリプトからもアクセス可能
    public static bool isGameClear = false;

    // シングルトン化 (オプション)
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}