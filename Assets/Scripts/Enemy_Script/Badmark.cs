using UnityEngine;


public class Badmark : MonoBehaviour
{
    // 取得したいコンポーネントの型
    private Damage_player playerDamager;

    // プレイヤーのタグ名
    private const string PlayerTag = "Player";
    void Start()
    {

    }
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手にGroundタグが付いているとき
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Groundに当たりました");
            // 0.01秒後に消える
            Destroy(gameObject, 0.01f);
        }

        // 衝突した相手にPlayerタグが付いているとき
        if (collision.gameObject.tag == "Player")
        {
            /*
            ここにPlayerがあたったときのスコア処理
            */

            Debug.Log("Playerに当たりました");
            Destroy(gameObject);
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