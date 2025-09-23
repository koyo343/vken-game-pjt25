using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class debug_scene_selector_manager : MonoBehaviour
{
    // プレイヤー名を入力するためのInput Field
    public TMP_InputField SceneNameInput;

    // データを保存して次のシーンに進むためのボタン
    public Button moveSceneButton;
    public GameData_Manager GameData_Manager;
    


    void Start()
    {
        // ボタンにクリックイベントを登録
        moveSceneButton.onClick.AddListener(DebugSceneMover);

        
        GameData_Manager.CheckNullInstance();
    }

    private void DebugSceneMover()
    {
        // Input Fieldからプレイヤー名を取得
        string SceneName = SceneNameInput.text;

        // playerNameが空でないことを確認
        if (string.IsNullOrEmpty(SceneName))
        {
            Debug.LogWarning("シーン名が入力されていません。");
            return;
        }

        if (SceneUtility.GetBuildIndexByScenePath(SceneName) != -1)
        {
            Debug.Log($"Move {SceneName} from debug_scene_selector");
            SceneManager.LoadScene(SceneName);
        }
        else
        {
            Debug.LogError($"シーン '{SceneName}' はビルド設定に登録されていません。");
        }
    }

}
