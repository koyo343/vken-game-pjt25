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

    public IEnumerator Invinsible(float delaytime)
    {
        if (!isInvinsible)
        {
            //無敵付与
            isInvinsible = true;

            if(delaytime <= 3f)
            {
                //アニメーターを無敵の状態にする
                blinker.BeginBlink();
            }

            //コルーチンで指定秒数待機
            yield return new WaitForSeconds(delaytime);

            //無敵解除
            isInvinsible = false;
            if (delaytime <= 3f)
            {
                //アニメーターの無敵状態を切る
                blinker.EndBlink();
            }
        }
    }
}
