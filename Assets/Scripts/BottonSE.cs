using UnityEngine;

public class ButtonSE : MonoBehaviour
{
    // Unityエディタから、このボタンで鳴らすSEを設定する
    public AudioClip clickSound;

    // ボタンがクリックされた時に呼ばれるメソッド
    public void OnClick()
    {
        // SEManagerを呼び出して、設定されたSEを再生してもらう
        SEManager.instance.PlaySE(clickSound);
    }
}