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
    public GameObject CautionPanel;
    public Button ConfirmButton;
    public Button CancelButton;

    public string LocalmodeMessage = "Local Mode";
    public string PublicmodeMessage = "Public Mode";

    void Start()
    {
        // ボタンにクリックイベントを登録
        SwitchButton.onClick.AddListener(onClicking);
        ConfirmButton.onClick.AddListener(OnConfirm);
        CancelButton.onClick.AddListener(OnCancel);

        CautionPanel.SetActive(false);

        
        // 初期表示を設定
        UpdateLocalModeText();
    }

    private void onClicking()
    {
        Debug.Log("onClicking is called");
        
        // データベースモードを切り替える
        if (!DatabaseSwitcher.isLocal)
        {
            DatabaseSwitcher.SwitchDatabase();
            // テキストを一度だけ更新
            UpdateLocalModeText();
        }
        else
        {
            CautionPanel.SetActive(true);
        }

        
    }

    private void OnConfirm()
    {
        Debug.Log("OnConfirm is called");
        DatabaseSwitcher.SwitchDatabase();
        CautionPanel.SetActive(false);
        // テキストを一度だけ更新
        UpdateLocalModeText();
    }

    private void OnCancel()
    {
        Debug.Log("OnCancel is called");
        CautionPanel.SetActive(false);
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
    }
}