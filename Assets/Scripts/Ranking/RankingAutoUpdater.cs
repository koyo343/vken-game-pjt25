using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使用する場合に必要
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.DynamoDBv2.Model;

public class AutoUpdaterToggle : MonoBehaviour
{
    public Button AutoUpdaterButton;
    private bool isAutoUpdaterEnabled = false;
    public TextMeshProUGUI UpdaterText;

    public TextMeshProUGUI UpdatingUIText;

    public RankingManager RankingManager;

    private float updatetime = 60.0f;
    private float lastUpdateTime = 0.0f;

    void Start()
    {
        AutoUpdaterButton.onClick.AddListener(ToggleAutoUpdater);
        UpdatingUIText.gameObject.SetActive(isAutoUpdaterEnabled);
        UpdaterText.text = "AutoUpdating OFF";
        UpdatingUIText.text = "Updating...";
    }

    void ToggleAutoUpdater()
    {
        isAutoUpdaterEnabled = !isAutoUpdaterEnabled;
        if(isAutoUpdaterEnabled){
            UpdaterText.text = "AutoUpdating ON";
            RankingManager.ReLoadRankingData();
            lastUpdateTime = Time.time;
            Debug.Log("AutoUpdater is enabled");
            UpdatingUIText.gameObject.SetActive(true);
        } else {
            UpdaterText.text = "AutoUpdating OFF";
            Debug.Log("AutoUpdater is disabled");
            UpdatingUIText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isAutoUpdaterEnabled)
        {
            if (Time.time - lastUpdateTime >= updatetime)
            {
                RankingManager.ReLoadRankingData();
                lastUpdateTime = Time.time;
                Debug.Log($"UpDated");
            }
        }
    }
}