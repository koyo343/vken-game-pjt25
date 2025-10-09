using UnityEngine;
using System.Collections; //コルーチンを使用するため

public class InvinsibleManager : MonoBehaviour
{
    //無敵を付与されているか確認するため
    public bool isInvinsible;
    private SpriteRendererBlinker blinker;

    void Start()
    {
        //非無敵状態にセットする
        isInvinsible = false;
        blinker = GetComponent<SpriteRendererBlinker>();
    }

    void Update()
    {
        //Debug.Log(isInvinsible);
    }

    public IEnumerator Invinsible(float delaytime)
    {
        if (!isInvinsible)
        {
            //無敵付与
            isInvinsible = true;
            //アニメーターを無敵の状態にする
            blinker.BeginBlink();
            Debug.Log("被弾無敵開始");

            //コルーチンで指定秒数待機
            yield return new WaitForSeconds(delaytime);

            Debug.Log("被弾無敵owata");
            //無敵解除
            isInvinsible = false;
            //アニメーターの無敵状態を切る
            blinker.EndBlink();
        }
    }
    //スキルによる無敵時間コルーチン(点滅なし)
    public IEnumerator InvinsibleNoBlink(float delay)
    {
        if (!isInvinsible)
        {
            //無敵付与
            isInvinsible = true;
            Debug.LogWarning("スキルで無敵状態になった1");

            //コルーチンで指定秒数待機
            yield return new WaitForSeconds(delay);
            Debug.LogWarning("無敵状態owata");

            //無敵解除
            isInvinsible = false;
        }
    }
}
