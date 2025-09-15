using UnityEngine;

public class damageParticleSystem : MonoBehaviour
{
    // インスペクターから設定できるよう、ParticleSystem型の変数を宣言
    private ParticleSystem damageParticle;

    void Start()
    {
        // 自分自身（このスクリプトがアタッチされているオブジェクト）の
        // ParticleSystemコンポーネントを取得して変数に格納
        damageParticle = GetComponent<ParticleSystem>();
    }

    // 他のCollider 2Dと物理的に衝突した時に呼び出されるメソッド
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手のタグが "Enemy" だったら
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // パーティクルの再生を開始
            damageParticle.Play();
        }
    }

    // 衝突していたCollider 2Dから離れた時に呼び出されるメソッド
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 離れた相手のタグが "Enemy" だったら
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // パーティクルの再生を停止
            damageParticle.Stop();
        }
    }
}