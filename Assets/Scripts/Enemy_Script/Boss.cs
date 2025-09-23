using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("ジャンプの距離")]
    public float jump_height;
    [Header("移動速度")]
    public float moveSpeed;
    private float CoolDownTime = 10;
    private int AttackChosenNum;
    
    // プレイヤーのTransformを格納する変数
    private Transform player;
    // Rigidbodyを格納する変数
    private Rigidbody rb;
    [Header("ジャンプの力")]
    public float jumpForce = 10f; // ジャンプの力を調整可能な変数として追加

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // "Player"タグが付いているゲームオブジェクトを検索してplayer変数に格納
        player = GameObject.FindWithTag("Player").transform;
        // 自身のRigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーに向けて移動
        // プレイヤーの方向を計算
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Y軸方向の移動はジャンプで制御

        // プレイヤーに向かって移動
        transform.position += direction * moveSpeed * Time.deltaTime;
        CoolDownTime -= Time.deltaTime; // クールダウンタイムを減らす

        if (CoolDownTime < 0)
        {
            //攻撃方法の選択
            AttackChosenNum = Random.Range(0, 3);
            CoolDownTime = 10; // クールダウンタイムをリセット

            switch (AttackChosenNum)
            {
                case 0:
                    //踏みつけ攻撃
                    Trample();
                    break;
                case 1:
                    //攻撃2
                    break;
                case 2:
                    //攻撃3
                    break;
                case 3:
                    //攻撃4
                    break;

            }
        }
    }

    //踏みつけ攻撃
    void Trample()
    {
        Jump();
    }

    void Jump()
    {
        // rbが存在するかチェックしてからAddForceを呼び出す
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}