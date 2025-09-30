using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    // Trigger に何か他のオブジェクトが触れた時に呼び出されるメソッド
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageAble damageable = other.GetComponent<DamageAble>();

        if (damageable != null)
        {
            Debug.Log("DamageAbleコンポーネント取得");
        }
        if (damageable != null)
        {
            Debug.Log("ダメージ処理の実行");
            damageable.Damage(0);
        }
    }
}