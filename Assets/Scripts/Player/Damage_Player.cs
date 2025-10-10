using UnityEngine;

public class Damage_player : DamageBase
{
    public GameObject player;
    public ScoreDictionaryManager scoreDictionaryManager;

    public InvinsibleManager invinsibleManager;

    //シールドアイテムの効果中か否か
    public bool isShield;

    void Start()
    {
        isShield = false;
        Debug.Log("isShield is false");
    }

    public override void Damage(int damage)
    {
        //シールドを付与されているか
        if (isShield)
        {
            Debug.Log("シールドダメージ");

            //ここにシールドSEを追加
            CharactorSE charactorAudioData = GetComponent<CharactorSE>();
            SEManager seManager = FindObjectOfType<SEManager>();
            if (seManager != null) 
            {
                seManager.PlaySE(charactorAudioData.ShieldSound);
                Debug.Log($"ダメージSEを再生しました: {charactorAudioData.ShieldSound.name}");
            }
            if(charactorAudioData.ShieldSound == null)
            {
                Debug.Log("SEがアタッチされていません");
            }
            else
            {
                Debug.LogWarning("SEManagerが見つからないため、SEを再生できませんでした。");
            }

            base.Damage(0);
            
            //シールドを無効にする
            isShield = false;
        } else if (invinsibleManager != null && !invinsibleManager.isInvinsible) //InvinsibleManagerがあることとfalseであることを確認
        {
            //SE再生
            CharactorSE charactorAudioData = GetComponent<CharactorSE>();
            SEManager seManager = FindObjectOfType<SEManager>();
            if (seManager != null) 
            {
                seManager.PlaySE(charactorAudioData.DamageSound);
                Debug.Log($"ダメージSEを再生しました: {charactorAudioData.DamageSound.name}");
            }
            if(charactorAudioData.DamageSound == null)
            {
                Debug.Log("SEがアタッチされていません");
            }
            else
            {
                Debug.LogWarning("SEManagerが見つからないため、SEを再生できませんでした。");
            }

            StartCoroutine(invinsibleManager.Invinsible(2f));

            /*ここにスコア減算処理を記述*/

            base.Damage(damage);
            scoreDictionaryManager.StrToScore("HitEnemy");
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
