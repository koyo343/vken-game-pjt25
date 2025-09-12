using UnityEngine;

public class ObjectToggle : MonoBehaviour
{
    // 表示・非表示を切り替えたいGameObjectをアタッチする配列
    public GameObject[] targetGameObject;

    // ボタンのクリック時に呼び出すメソッド
    public void ToggleAllGameObject()
    {
        // 配列内のすべてのGameObjectをループ処理
        foreach (GameObject obj in targetGameObject)
        {
            if (obj != null)
            {
                // GameObjectの表示・非表示を切り替える
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
}