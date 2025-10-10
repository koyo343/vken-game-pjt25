using UnityEngine;
using UnityEngine.UI;
using System;

public class maskingObject : MonoBehaviour
{

    public Button ToggleButton;
    public GameObject[] targetGameObjects;

    //public DebugParameter debugParameter;

    void Start()
    {
        ToggleButton.onClick.AddListener(ToggleMask);
        MaskUpdate();
    }

    public void ToggleMask()
    {

        DebugParameter.togglemasked();
        MaskUpdate();
    }
    
    void MaskUpdate()
    {
        foreach (GameObject GameObject in targetGameObjects)
        {
            if (!DebugParameter.ismasked)
            {
                GameObject.gameObject.SetActive(true);
            }
            else
            {
                GameObject.gameObject.SetActive(false);
            }
            Debug.Log($"object mask is {DebugParameter.ismasked}");
        }
    }
}