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

    public RankingManager RankingManager;

    private float updatetime = 60.0f;
    private float lastUpdateTime = 0.0f;

    void Start()
    {
        AutoUpdaterButton.onClick.AddListener(ToggleAutoUpdater);
        UpdaterText.text = "AutoUpdating OFF";
    }

    void ToggleAutoUpdater()
    {
        isAutoUpdaterEnabled = !isAutoUpdaterEnabled;
        if(isAutoUpdaterEnabled){
            UpdaterText.text = "AutoUpdating ON";
            RankingManager.ReLoadRankingData();
            lastUpdateTime = Time.time;
            Debug.Log("AutoUpdater is enabled");
        } else {
            UpdaterText.text = "AutoUpdating OFF";
            Debug.Log("AutoUpdater is disabled");
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