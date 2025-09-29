using UnityEngine;

public class Damage_holoxer : DamageBase
{
    public GameObject holoxer;

    protected override void Die()
    {
        /*ここに敵を倒した際のカウンターの呼び出し処理を追加*/
        if (holoxer != null)
        {
            Destroy(holoxer);   
        }
    }
}