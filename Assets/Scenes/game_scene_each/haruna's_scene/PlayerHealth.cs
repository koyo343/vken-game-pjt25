using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // 現在のHP
    public int currentHealth = 3;

    // ダメージを受けるメソッド
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("ダメージを受けました！ 現在のHP: " + currentHealth);

        // HPが0以下になったら...
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("ゲームオーバー！");
        // ゲームオーバー処理（例：ゲームオブジェクトを非アクティブにするなど）
        gameObject.SetActive(false);
    }
}