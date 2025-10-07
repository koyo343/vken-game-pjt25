using UnityEngine;
using System.Collections;//コルーチンを使用するため

public class InvinsibleManager : MonoBehaviour
{
    //無敵を付与されているか確認するため
    public bool isInvinsible;
    //アニメーターの情報を入れる変数
    public Animator animator;

    void Start()
    {
        //非無敵状態にセットする
        isInvinsible = false;
    }

    public IEnumerator Invinsible(float delaytime)
    {
        if (!isInvinsible)
        {
            //無敵付与
            isInvinsible = true;
            //アニメーターを無敵の状態にする
            animator.SetBool("isInvinsible", true);

            //コルーチンで指定秒数待機
            yield return new WaitForSeconds(delaytime);

            //無敵解除
            isInvinsible = false;
            //アニメーターの無敵状態を切る
            animator.SetBool("isInvinsible", false);
        }
    }
}