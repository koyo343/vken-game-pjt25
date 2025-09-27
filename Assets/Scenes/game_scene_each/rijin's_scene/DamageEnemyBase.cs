using UnityEngine;

public interface DamageAble
{
    void Damage(int amount);
}

//敵の初期設定
public abstract class DamageEnemyBase : MonoBehaviour, DamageAble
{
    //敵体力の初期設定(inspectorからも編集可能)
    [SerializeField] protected int health = 100;

    //子クラスで上書きする可能性があるので
    public virtual void Damage(int damage)
    {
        //damage分の体力を減少
        health -= damage;
        //ここはデバッグログなので消してもよい
        Debug.Log("敵がダメージを受けました！");

        if (health <= 0)
        {
            Die();
        }
    }

    //これは絶対に子クラスで実装しようね
    protected abstract void Die();
}