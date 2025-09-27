using UnityEngine;

public class Damage_Boss : DamageEnemyBase
{
    public GameObject boss;

    //CameraControllerインスタンスの取得
    public CameraController cameraController;

    public override void Damage(int damage)
    {
        //とりあえずダメージ軽減処理の方向性で実装
        int bossdamage = (int)(damage * 0.1f);

        //親クラスの呼び出し
        base.Damage(bossdamage);

        //気持ち悪いデバッグログ
        Debug.Log("ボスは" + bossdamage + "を受けた(残り体力:" + health + ")");
    }

    protected override void Die()
    {
        /*ここに敵を倒した際のカウンターの呼び出し処理を追加*/
        if (boss != null)
        {
            /*ここにアニメーション追加かも？*/
            cameraController.BossFlag = true;
            Destroy(boss);
        }
    }
}