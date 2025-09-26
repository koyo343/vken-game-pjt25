using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    // ダメージ量
    public int damageAmount = 1;

    // Trigger に何か他のオブジェクトが触れた時に呼び出されるメソッド
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 触れたオブジェクトのタグが "Player" かどうかをチェック
        if (other.CompareTag("Player"))
        {
            // Player の PlayerHealth スクリプトを取得
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // PlayerHealth スクリプトが存在すれば、ダメージを与える
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}