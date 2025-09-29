using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("衝突検知:" + other.GetComponent<DamageAble>());

        DamageAble damageable = other.GetComponent<DamageAble>();

        if (damageable != null)
        {
            Debug.Log("DamageAbleコンポーネント取得");
        }

        if (damageable != null)
        {
            Debug.Log("ダメージ処理の実行");
            damageable.Damage(0);
            Destroy(gameObject);
        }
    }
}