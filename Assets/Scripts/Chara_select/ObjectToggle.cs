using UnityEngine;

public class ObjectToggle : MonoBehaviour
{
    // 各キャラクターに対応するGameObjectをInspectorで設定する配列
    public GameObject[] characterGameObjects;

    // キャラクターのデータを格納する構造体
    public struct CharacterData
    {
        public string characterName;
        public int characterFlag;
    }

    public CharacterData[] characterDatas;

    // ゲーム開始時に実行
    void Start()
    {
        characterDatas = new CharacterData[4];

        characterDatas[0].characterName = "Sora";
        characterDatas[0].characterFlag = 0;

        characterDatas[1].characterName = "Toya";
        characterDatas[1].characterFlag = 0;

        characterDatas[2].characterName = "Mito";
        characterDatas[2].characterFlag = 0;

        characterDatas[3].characterName = "Uruha";
        characterDatas[3].characterFlag = 0;
    }

    // 新しく表示するキャラクターのインデックスを受け取るメソッド
    public void ToggleObject(int newCharIndex)
    {
        // 現在フラグが1になっているキャラクターの表示を非表示にする
        for (int i = 0; i < characterDatas.Length; i++)
        {
            if (characterDatas[i].characterFlag == 1)
            {
                // 対応するGameObjectを非表示にする
                if (characterGameObjects[i] != null)
                {
                    characterGameObjects[i].SetActive(false);
                }
                Debug.Log($"キャラクターが選択されました: {characterDatas[i].characterName}");

                // フラグを0に戻す
                characterDatas[i].characterFlag = 0;
            }
        }

        // 新しく選択されたキャラクターの表示を有効にし、フラグを1にする
        // 指定されたインデックスが範囲内であることを確認
        if (newCharIndex >= 0 && newCharIndex < characterGameObjects.Length)
        {
            // 対応するGameObjectを表示する
            if (characterGameObjects[newCharIndex] != null)
            {
                characterGameObjects[newCharIndex].SetActive(true);
            }

            // フラグを1に設定
            characterDatas[newCharIndex].characterFlag = 1;
        }
    }
}