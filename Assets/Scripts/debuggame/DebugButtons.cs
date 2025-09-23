using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使用する場合に必要
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Text;
using Amazon.DynamoDBv2;
using Amazon;
using Amazon.DynamoDBv2.Model;

public class debugButtons : MonoBehaviour
{
    public Button deleteData;
    public GameObject CautionPanel;
    public Button ConfirmButton;
    public Button CancelButton;



    void Start()
    {
        deleteData.onClick.AddListener(onClickdelete);
        CautionPanel.SetActive(false);
        ConfirmButton.onClick.AddListener(OnConfirm);
        CancelButton.onClick.AddListener(OnCancel);
    }

    void onClickdelete()
    {
        CautionPanel.SetActive(true);
    }

    void OnConfirm()
    {
        CautionPanel.SetActive(false);
        LoadingCSV.DeleteFile();
    }

    void OnCancel()
    {
        CautionPanel.SetActive(false);
    }

}