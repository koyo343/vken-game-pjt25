using UnityEngine;

public class Damage_player : DamageBase
{
    public GameObject player;

    public override void Damage(int damage)
    {

        /*ここにスコア減算処理を記述*/

        base.Damage(damage);

        Debug.Log("スコアが100減少しました;;");
    }


    /*仕様としてDie()は必ず記述しなければならないため実装しているが
    敵側の当たり判定でdamage(0)としているのでここは形だけの実装*/
    protected override void Die()
    {
        /*ここに敵を倒した際のカウンターの呼び出し処理を追加*/
        if (player != null)
        {
            Destroy(player);
        }
    }
}