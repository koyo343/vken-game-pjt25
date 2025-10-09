using UnityEngine;

public class CursorSE:MonoBehaviour
{
    // Unityエディタから、このボタンで鳴らすSEを設定する
    public AudioClip CursorSound;

    // カーソルが合った時に呼ばれるメソッド
    public void OnPointerEnter()
    {
        // SEManagerを呼び出して、設定されたSEを再生してもらう
        SEManager.instance.PlaySE(CursorSound);
    }
}