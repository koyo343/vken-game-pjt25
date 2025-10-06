using UnityEngine;


public class Badmark : MonoBehaviour
{
    // 取得したいコンポーネントの型
    private Damage_player playerDamager;

    // プレイヤーのタグ名
    private const string PlayerTag = "Player";
    void Start()
    {
        FindAndGetPlayerComponent();
        Destroy(gameObject, 4.0f);
    }
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 地面、プレイヤーの弾に接したら消滅
        /*if (other.CompareTag("Ground") )
        {
            Debug.Log("建物に当たりました");
            Destroy(gameObject); // 遅延なしで即時削除
            return; // 処理を終了
        }
        */
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

    void FindAndGetPlayerComponent()
    {
        // 1. "Player" タグを持つゲームオブジェクトをシーン全体から検索
        GameObject playerObject = GameObject.FindWithTag(PlayerTag);

        if (playerObject != null)
        {
            // 2. そのゲームオブジェクトから Damage_Player コンポーネントを取得
            playerDamager = playerObject.GetComponent<Damage_player>();

            if (playerDamager != null)
            {
                Debug.Log("PlayerオブジェクトとDamage_Playerコンポーネントを取得しました。");
                // これで、playerDamager.Damage(10); のようにコンポーネントのメソッドを呼び出せます。
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

    
}