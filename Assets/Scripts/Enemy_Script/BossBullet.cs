using UnityEngine;

public class BossBullet : MonoBehaviour
{
    // 取得したいコンポーネントの型
    private Damage_player playerDamager;

    // プレイヤーのタグ名
    private const string PlayerTag = "Player";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 弾が長時間残らないように、念のため自動で削除する処理を追加すると良い
        Destroy(gameObject, 5f); // 5秒後に自動削除

        //"Player" タグを持つゲームオブジェクトをシーン全体から検索
        GameObject playerObject = GameObject.FindWithTag(PlayerTag);

        if (playerObject != null)
        {
            //そのゲームオブジェクトから Damage_Player コンポーネントを取得
            playerDamager = playerObject.GetComponent<Damage_player>();

            if (playerDamager != null)
            {
                Debug.Log("PlayerオブジェクトとDamage_Playerコンポーネントを取得しました。");
            }
            else
            {
                // プレイヤーオブジェクトは見つかったが、コンポーネントがアタッチされていない場合
                Debug.LogError("PlayerオブジェクトにはDamage_Playerコンポーネントが見つかりません！");
            }
        }
        else
        {
            // Playerタグを持つオブジェクトがシーンに見つからなかった場合
            Debug.LogError($"シーン内に '{PlayerTag}' タグを持つオブジェクトが見つかりません。");
        }
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
            //プレイヤーへのダメージ処理
            playerDamager.Damage(0);
            Destroy(gameObject); // 遅延なしで即時削除
            return; // 処理を終了
        }
    }
}