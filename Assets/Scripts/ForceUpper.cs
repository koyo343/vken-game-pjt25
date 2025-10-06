using UnityEngine;

public class ForceUpper : MonoBehaviour
{
    [SerializeField] private float jumpForce = 20.0f;
    [SerializeField] private float normalThreshold = 0.9f; // 上向きと判定するしきい値 (0.9は上向き45度以内を意味)

    // OnTriggerEnter2D ではなく、OnCollisionEnter2D を使用
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // プレイヤーのタグをチェック
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーの Rigidbody2D を取得
            Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();

            // Rigidbody2Dがあるか確認
            if (rb2d != null)
            {
                // 衝突情報から接触点（接触面）の法線ベクトルを取得
                // 衝突が上向きであるかを確認するフラグ
                bool isLanding = false;

                // 衝突した全ての接触点をチェック
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    // 接触点の法線ベクトルを取得し、Vector2.upとの内積を計算
                    // 法線が完全に上向き（Vector2.up）なら内積は 1
                    // 法線が横向きなら内積は 0 に近い
                    if (Vector2.Dot(contact.normal, Vector2.up) > normalThreshold)
                    {
                        isLanding = true;
                        break; // 1つでも上からの衝突があればOK
                    }
                }

                // 上から乗ったと判定された場合のみ、力を加える
                if (isLanding)
                {
                    // 以前の縦方向の速度をリセット（連続ジャンプを防ぐため）
                    rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0f);
                    
                    // 上向きに力を加える
                    rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}