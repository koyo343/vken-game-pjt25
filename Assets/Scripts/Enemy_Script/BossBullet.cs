using UnityEngine;

public class BossBullet : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 弾が長時間残らないように、念のため自動で削除する処理を追加すると良い
        Destroy(gameObject, 5f); // 5秒後に自動削除
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Is TriggerがONの場合、このメソッドで接触を検出します
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 地面、壁、プレイヤーの弾に接したら消滅
        if (other.CompareTag("Ground") || other.CompareTag("Wall") || other.CompareTag("Bullet"))
        {
            Debug.Log("建物に当たりました");
            Destroy(gameObject); // 遅延なしで即時削除
            return; // 処理を終了
        }

        // プレイヤーへの処理
        if (other.CompareTag("Player"))
        {
            Debug.Log("Playerに当たりました");
            /*
            プレイヤーへのダメージ処理
            */
            Destroy(gameObject); // 遅延なしで即時削除
            return; // 処理を終了
        }
    }
}