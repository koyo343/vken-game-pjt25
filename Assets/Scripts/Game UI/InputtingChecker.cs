using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // TMP_InputField を使う場合

public class MultiInputFocusChecker : MonoBehaviour
{
    // Inspectorから任意の数のInputFieldを設定
    public TMP_InputField[] targetInputFields; 
    // または public List<TMP_InputField> targetInputFields;

    /// <summary>
    /// 設定された InputField のいずれかにフォーカスが当たっているかを確認する
    /// </summary>
    /// <returns>いずれかの InputField が入力中であれば true</returns>
    public bool IsAnyInputFieldFocused()
    {
        // 現在 EventSystem で選択されている GameObject を取得
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        
        // selectedObject が null でないか確認
        if (selectedObject == null)
        {
            return false;
        }

        // targetInputFields の配列をループでチェック
        foreach (TMP_InputField inputField in targetInputFields)
        {
            // 設定された InputField の gameObject と、選択中のオブジェクトを比較
            if (inputField != null && selectedObject == inputField.gameObject)
            {
                // 一致するものが見つかった時点で、入力中と判断し true を返す
                return true;
            }
        }

        // 全ての InputField をチェックしても一致するものが見つからなかった
        return false;
    }
}