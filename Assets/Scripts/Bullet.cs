using Unity.VisualScripting;
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
        //OffScreen();
    }

    /// <summary>

    /// 弾をプレイヤーの向く方向に移動させる
    /// </summary>
    private void BulletMove()
    {
        // transform.rightはオブジェクトの右方向を示すベクトル
        transform.position += transform.right * bulletSpeed * Time.deltaTime;
    }

    //カメラから外れたらBulletを消す
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 他のオブジェクトとぶつかったら実行される
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ぶつかったオブジェクトが「Enemy」タグを持っているか確認
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageAble damageable = collision.gameObject.GetComponent<DamageAble>();

            //ダメージ処理
            if (damageable != null)
            {
                Debug.Log("DamageAbleコンポーネント取得");
                
                // 貫通しない弾（isPenetratingがfalse）の場合だけ、弾自身を消す
                if (!isPenetrating)
                {
                    //タガメ
                    damageable.Damage(100);
                    Destroy(gameObject);
                }
                else
                {
                    //うるぱんち
                    damageable.Damage(50);
                }
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            // 爆発エフェクトを生成
            //Instantiate(explosionPrefab, collision.transform.position, transform.rotation);

            // 地面に当たったら弾を消す
            Destroy(gameObject);
        }
    }
}