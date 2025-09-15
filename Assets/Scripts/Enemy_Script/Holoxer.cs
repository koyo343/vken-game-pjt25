using UnityEngine;

public class Holoxer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    private Rigidbody2D rb = null; //Rigidbody2D制御用変数
    private SpriteRenderer sr = null; //カメラに映ったときに動くようにする変数


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
            transform.localScale = new Vector3(1, 1, 1);
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

            Debug.Log("Playerに当たりました");
            
        }
    }
}

