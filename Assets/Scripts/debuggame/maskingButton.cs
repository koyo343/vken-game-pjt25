using UnityEngine;
using UnityEngine.UI;
using System;

public class maskingObject : MonoBehaviour
{

    public Button ToggleButton;
    public GameObject[] targetGameObjects;

    public bool isMasked = false;

    void Start()
    {
        ToggleButton.onClick.AddListener(ToggleMask);
    }

    public void ToggleMask()
    {
        foreach (GameObject GameObject in targetGameObjects)
        {
            if(isMasked)
            {
                GameObject.gameObject.SetActive(true);
            }
            else
            {
                GameObject.gameObject.SetActive(false);
            }
        isMasked = !isMasked;
        }
    }
}