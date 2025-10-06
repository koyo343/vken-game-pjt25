using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossClearManager : MonoBehaviour
{
    //public Damage_Boss Damage_Boss;
    public bool BossFlag = false;
    public string resultSceneName = "Result_Scene";
    public Button RetireButton;

    // シーン遷移を一度だけ実行するためのフラグ
    private bool isSceneTransitioning = false;

    void Start()
    {
        RetireButton.onClick.AddListener(OnRetire);
    }

    void Update()
    {
        // ボスが倒されてシーン遷移する処理
        if (/*Damage_Boss.BossFlag*/ BossFlag == true)
        {
            // シーン遷移を一度だけ実行
            isSceneTransitioning = true;
            Debug.Log("ボスが倒されました！リザルトシーンに遷移します。");

            SceneManager.LoadScene(resultSceneName);
        }
    }
    
    //リタイアボタンの処理
    public void OnRetire()
    {
        SceneManager.LoadScene(resultSceneName);
    }
}