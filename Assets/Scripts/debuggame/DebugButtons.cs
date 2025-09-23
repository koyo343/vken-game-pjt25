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
    public GameObject deleteCautionPanel;
    public Button deleteConfirmButton;
    public Button deleteCancelButton;

    public Button TempDataInput;



    void Start()
    {
        deleteData.onClick.AddListener(onClickdelete);
        deleteCautionPanel.SetActive(false);
        deleteConfirmButton.onClick.AddListener(OnConfirm);
        deleteCancelButton.onClick.AddListener(OnCancel);

        TempDataInput.onClick.AddListener(OnTempDataInput);
    }

    void OnTempDataInput()
    {
        LoadingCSV.InputTemplateData();
    }

    void onClickdelete()
    {
        deleteCautionPanel.SetActive(true);
    }

    void OnConfirm()
    {
        deleteCautionPanel.SetActive(false);
        LoadingCSV.DeleteFile();
    }

    void OnCancel()
    {
        deleteCautionPanel.SetActive(false);
    }

}