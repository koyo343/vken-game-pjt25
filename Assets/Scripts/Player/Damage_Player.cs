using UnityEngine;

public class Damage_player : DamageBase
{
    public GameObject player;
    public ScoreDictionaryManager scoreDictionaryManager;
    
    public InvinsibleManager invinsibleManager;

    public override void Damage(int damage)
    {

        if (invinsibleManager != null && !invinsibleManager.isInvinsible)
        {
            StartCoroutine(invinsibleManager.Invinsible(2f));

            /*ここにスコア減算処理を記述*/

            base.Damage(damage);
            scoreDictionaryManager.StrToScore("HitEnemy");

            //ここのコメントも仮実装
            Debug.Log("スコアが100減少しました;;");
        }
        else if (invinsibleManager == null)
        {
            Debug.Log("invinsibleManagerが見つかりません");
        }
        else if (invinsibleManager.isInvinsible)
        {
            //デバッグコメント
            Debug.Log("無敵時間中のため被弾処理は実行されませんでした");
        }

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