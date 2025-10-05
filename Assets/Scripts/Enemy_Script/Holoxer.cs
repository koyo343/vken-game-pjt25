using UnityEngine;

public class Holoxer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    private Rigidbody2D rb = null; //Rigidbody2D制御用変数
    private SpriteRenderer sr = null; //カメラに映ったときに動くようにする変数

    // 取得したいコンポーネントの型
    private Damage_player playerDamager;

    // プレイヤーのタグ名
    private const string PlayerTag = "Player";

    void Awake()
    {
        FindAndGetPlayerComponent();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (sr.isVisible)//画面に映っているときにのみ実行する
        {
            int xVector = -1;
            transform.localScale = new Vector3(8, 8, 1);
            rb.linearVelocity = new Vector2(xVector * speed, -gravity);
        }
        else
        {
            rb.Sleep();//画面に映っていないときに物理演算を中止
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Playerに接触したとき
        if (collision.gameObject.tag == "Player")
        {
            /*
            ここにPlayerがあたったときのスコア処理
            */
            playerDamager.Damage(0);
            Debug.Log("Playerに当たりました");

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

