using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class LocalSwitch : MonoBehaviour
{
    public Button SwitchButton;
    public TextMeshProUGUI LocalmodeText;
    public RankingManager manager;

    public string LocalmodeMessage = "Local Mode";
    public string PublicmodeMessage = "Public Mode";

    void Start()
    {
        // ボタンにクリックイベントを登録
        SwitchButton.onClick.AddListener(onClicking);
        
        // 初期表示を設定
        UpdateLocalModeText();
    }

    private void onClicking()
    {
        Debug.Log("onClicking is called");
        
        // データベースモードを切り替える
        DatabaseSwitcher.SwitchDatabase();

        // テキストを一度だけ更新
        UpdateLocalModeText();
    }

    private void UpdateLocalModeText()
    {
        if (DatabaseSwitcher.isLocal)
        {
            LocalmodeText.text = LocalmodeMessage;
        }
        else
        {
            LocalmodeText.text = PublicmodeMessage;
        }
        manager.ReLoadRankingData();
    }
}