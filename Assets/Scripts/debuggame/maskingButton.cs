using UnityEngine;
using UnityEngine.UI;
using System;

public class ToggleExit : MonoBehaviour
{

    public Button ToggleButton;
    //複数用
    //public GameObject[] targetGameObjects;

    //一つ用
    public GameObject targetGameObject;


    //public DebugParameter debugParameter;

    void Start()
    {
        ToggleButton.onClick.AddListener(ToggleMask);
        MaskUpdate();
    }

    public void ToggleMask()
    {

        DebugParameter.toggleExit();
        MaskUpdate();
    }
    
    void MaskUpdate()
    {   
        /*
        foreach (GameObject GameObject in targetGameObjects)
        {
            if (DebugParameter.ExitisActive)
            {
                GameObject.gameObject.SetActive(true);
            }
            else
            {
                GameObject.gameObject.SetActive(false);
            }
        }*/

        targetGameObject.SetActive(DebugParameter.ExitisActive);
        Debug.Log($"ExitMask is {DebugParameter.ExitisActive}");
    }
}