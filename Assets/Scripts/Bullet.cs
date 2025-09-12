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

    private void Update()
    {
        // 弾を前方に移動させる
        BulletMove();
        // 画面外に出たかチェックする
        OffScreen();
    }

    /// <summary>
    /// 弾を右方向に移動させる
    /// </summary>
    private void BulletMove()
    {
        // Vector3.rightは(1, 0, 0)を意味し、右方向を示す
        transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
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
                player.AddScore(1);
            }

            // 爆発エフェクトを生成
            Instantiate(explosionPrefab, collision.transform.position, transform.rotation);
            
            // ぶつかった敵とこの弾自身を消す
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}