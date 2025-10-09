using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossClearManager : MonoBehaviour
{
    public CameraController cameraController;
    //public string resultSceneName = "Result_Scene";
    public Button RetireButton;

    public ScoreSaveManager ScoreSaveManager;


    // シーン遷移を一度だけ実行するためのフラグ
    private bool isSceneTransitioning = false;

    void Start()
    {
        RetireButton.onClick.AddListener(OnRetire);
    }

    void Update()
    {
        // ボスが倒されてシーン遷移する処理
        if (cameraController.BossFlag && !isSceneTransitioning)
        {
            // シーン遷移を一度だけ実行
            isSceneTransitioning = true;
            Debug.Log("ボスが倒されました！リザルトシーンに遷移します。");

            //SceneManager.LoadScene(resultSceneName);
            ScoreSaveManager.OnGameOver();
        }
    }

    //リタイアボタンの処理
    public void OnRetire()
    {
        //SceneManager.LoadScene(resultSceneName);
        ScoreSaveManager.OnGameOver();
    }
}