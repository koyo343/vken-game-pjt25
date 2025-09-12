using UnityEngine;

public class ShooterController : MonoBehaviour
{
    // 撃ち出す弾のプレハブをインスペクターから設定する
    public GameObject bulletPrefab;

    // 弾を発射する力
    public float shootForce = 10f;

    // アニメーターコンポーネント
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Fキーが押されたら
        if (Input.GetKeyDown(KeyCode.F))
        {
            // アニメーターの「Throw」トリガーを呼び出す
            animator.SetTrigger("Throw");
        }
    }

    // アニメーションイベントで呼び出される関数
    public void ShootBullet()
    {
        // 弾を生成する
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Bulletスクリプトが持つ速度を設定する
        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // ここでbulletSpeedを設定してもいいが、今回はBulletMove関数に任せる
        }

        // Rigidbody2Dを取得し、力を加える（BulletMoveを使うなら不要）
        // Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        // if (bulletRb != null)
        // {
        //     bulletRb.AddForce(transform.right * shootForce, ForceMode2D.Impulse);
        // }
    }
}