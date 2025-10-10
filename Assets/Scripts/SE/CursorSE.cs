using UnityEngine;
using UnityEngine.EventSystems; // ISelectHandler を使うために必要

public class CursorSE : MonoBehaviour, ISelectHandler
{
    // Unityエディタから、このボタンで鳴らすSEを設定する
    public AudioClip CursorSound;

    // カーソルが合った時に呼ばれるメソッド
    public void OnPointerEnter()
    {
        // SEManagerを呼び出して、設定されたSEを再生してもらう
        SEManager.instance.PlaySE(CursorSound);
    }

    // キーボードで選択された時に呼び出される
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("OnSelectが呼ばれました");
        SEManager.instance.PlaySE(CursorSound);
    }

}