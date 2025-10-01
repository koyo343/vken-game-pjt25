using UnityEngine;
using System.Collections;//コルーチンを使用するため

public class InvinsibleManager : MonoBehaviour
{
    //無敵を付与されているか確認するため
    public bool isInvinsible;

    void Start()
    {
        isInvinsible = false;
    }

    public IEnumerator Invinsible(float delaytime)
    {
        if (!isInvinsible)
        {
            //無敵付与
            isInvinsible = true;

            //コルーチンで指定秒数待機
            yield return new WaitForSeconds(delaytime);

            //無敵解除
            isInvinsible = false;
        }
    }
}