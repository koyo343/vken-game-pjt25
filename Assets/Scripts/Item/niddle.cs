using UnityEngine;

public class niddle : MonoBehaviour
{
    private Damage_player playerDamager;
    private const string PlayerTag = "Player";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // プレイヤーコンポーネントの取得を試みる
        FindAndGetPlayerComponent();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Playerに接触したとき
        if (collision.gameObject.tag == "Player")
        {
            // ★修正点 2★: Nullチェックを追加
            if (playerDamager != null)
            {
                playerDamager.Damage(0);
            }
            Debug.Log("Playerに当たりました");
        }

    }

    void FindAndGetPlayerComponent()
    {
        // 1. "Player" タグを持つゲームオブジェクトをシーン全体から検索
        GameObject playerObject = GameObject.FindWithTag(PlayerTag);

        if (playerObject != null)
        {
            // 2. そのゲームオブジェクトから Damage_player コンポーネントを取得
            playerDamager = playerObject.GetComponent<Damage_player>();

            if (playerDamager != null)
            {
                Debug.Log("PlayerオブジェクトとDamage_Playerコンポーネントを取得しました。");
            }
            else
            {
                Debug.LogError("PlayerオブジェクトにはDamage_Playerコンポーネントが見つかりません！");
            }
        }
        else
        {
            Debug.LogError($"シーン内に '{PlayerTag}' タグを持つオブジェクトが見つかりません。");
        }
    }
}
