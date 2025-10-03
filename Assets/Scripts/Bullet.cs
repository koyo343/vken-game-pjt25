using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 弾のスピード
    /// </summary>
    public float bulletSpeed = 10f;

    /// <summary>
    /// 爆発エフェクトのプレハブ
    /// </summary>
    public GameObject explosionPrefab;

    /// <summary>
    /// 敵に当たっても消えないか（貫通するか）
    /// </summary>
    public bool isPenetrating = false;

    private void Update()
    {
        // 弾を前方に移動させる
        BulletMove();
        // 画面外に出たかチェックする
        OffScreen();
    }

    /// <summary>

    /// 弾をプレイヤーの向く方向に移動させる
    /// </summary>
    private void BulletMove()
    {
        // transform.rightはオブジェクトの右方向を示すベクトル
        transform.position += transform.right * bulletSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 弾が一定の距離を超えたら消す
    /// </summary>
    private void OffScreen()
    {
        // 画面の右端（X座標10f）を超えたら消滅
        if (transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
        // 画面の左端（X座標-10f）を超えたら消滅
        else if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 他のオブジェクトとぶつかったら実行される
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ぶつかったオブジェクトが「Enemy」タグを持っているか確認
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // プレイヤーのスコアを加算
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                //player.AddScore(1);
            }

            // 爆発エフェクトを生成
            Instantiate(explosionPrefab, collision.transform.position, transform.rotation);
            
            // ぶつかった敵とこの弾自身を消す
            Destroy(collision.gameObject);
            
             // 貫通しない弾（isPenetratingがfalse）の場合だけ、弾自身を消す
            if (!isPenetrating)
            {
                Destroy(gameObject);
            }
        }
    }
}