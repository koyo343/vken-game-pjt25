using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面に接したら消滅
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.isKinematic = true; // 物理演算の影響を受けないようにする
            }
            // 適切な時間後に岩を消す処理を追加しても良い
            Destroy(gameObject, 0.1f); 
        }
    }
}