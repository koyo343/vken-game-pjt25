using UnityEngine;

public class Damage_holoxer : DamageBase
{
    public GameObject holoxer;

    public override void Damage(int damage)
    {

        //親クラスの呼び出し
        base.Damage(damage);

        //死亡判定
        if (GetHealth() <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        /*ここに敵を倒した際のカウンターの呼び出し処理を追加*/
        if (holoxer != null)
        {
            Destroy(holoxer);
            Debug.Log("holoxerを倒した");
        }
    }

    public int GetHealth()
    {
        return health;
    }
}