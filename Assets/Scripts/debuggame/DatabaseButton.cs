using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class LocalSwitch : MonoBehaviour
{
    public Button SwitchButton;

    public Button UPSwitchButton;

    public TextMeshProUGUI LocalmodeText;

    public TextMeshProUGUI UploadmodeText;

    public GameObject CautionPanel;
    public Button ConfirmButton;
    public Button CancelButton;

    public string LocalmodeMessage = "Local Mode";
    public string PublicmodeMessage = "Public Mode";

    public string PublicUPmodeMessage = "Public Upload Mode";
    public string LocalUPmodeMessage = "Local Upload Mode";


    void Start()
    {
        // ボタンにクリックイベントを登録
        if (SwitchButton != null)
        {
            SwitchButton.onClick.AddListener(localbutton);
        }
        if (UPSwitchButton != null)
        {
            UPSwitchButton.onClick.AddListener(onClicking);
        }
        if (ConfirmButton != null)
        {
            ConfirmButton.onClick.AddListener(OnConfirm);
        }
        if (CancelButton != null)
        {
            CancelButton.onClick.AddListener(OnCancel);
        }


        CautionPanel.SetActive(false);


        // 初期表示を設定
        UpdateLocalModeText();
        UpdateUploadModeText();
    }

    private void localbutton()
    {
        if (DatabaseSwitcher.isLocal)
        {
            DatabaseSwitcher.SwitchDatabase();
            if (DatabaseSwitcher.isLocal)
            {
                return;
            }
            UpdateLocalModeText();
        }
        else
        {
            DatabaseSwitcher.SwitchDatabase();
            UpdateLocalModeText();
        }
    }


    private void onClicking()
    {
        Debug.Log("onClicking is called");

        // データベースモードを切り替える
        if (DatabaseSwitcher.isServerUpload)
        {
            DatabaseSwitcher.SwitchServerUpload();
            
            // テキストを一度だけ更新
            UpdateUploadModeText();
        }
        else
        {
            CautionPanel.SetActive(true);
        }


    }

    private void OnConfirm()
    {
        Debug.Log("OnConfirm is called");
        DatabaseSwitcher.SwitchServerUpload();
        
        CautionPanel.SetActive(false);
        if (!DatabaseSwitcher.isServerUpload)
        {
            return;
        }
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
    private void UpdateUploadModeText()
    {
        if (DatabaseSwitcher.isServerUpload)
        {
            UploadmodeText.text = PublicUPmodeMessage;
        }
        else
        {
            UploadmodeText.text = LocalUPmodeMessage;
        }
    }
}