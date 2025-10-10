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
            //SE再生
            AudioClip goalAudioData = Resources.Load<AudioClip>("Materials/SE/ゲームクリア");
            SEManager seManager = FindObjectOfType<SEManager>();
            if (seManager == null) 
            {
                Debug.LogWarning("SEManagerが見つからないため、SEを再生できませんでした。");
            }
            if(goalAudioData == null)
            {
                Debug.Log("SEがロードされていません");
            } else
            {
            seManager.PlaySE(goalAudioData);
            Debug.Log($"SEを再生しました: {goalAudioData.name}");
            }
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