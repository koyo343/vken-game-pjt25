using UnityEngine;
using UnityEngine.UI; // UIコンポーネントを使うために必要

public class ImageToggle : MonoBehaviour
{
    // 表示・非表示を切り替えたいImageコンポーネントをアタッチする変数
    public Image[]  targetImage;

    // ボタンのクリック時に呼び出すメソッド
    public void ToggleAllImage()
    {
        // targetImageが有効（表示）なら無効（非表示）に、無効なら有効にする

        foreach (Image img in targetImage)
        {
            if (img != null)
            {
                img.enabled = !img.enabled;
            }
        }
    }
}