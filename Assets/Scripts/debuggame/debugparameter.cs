using UnityEngine;
using UnityEngine.UI;
using System;

public static class DebugParameter
{
    //public static DebugParameter Instance { get; private set; }

    public static bool ismasked = false;

    /*
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }


    }

    public static void CheckNullInstance()
    {
        if (Instance == null)
        {
            // 新しいGameObjectを作成
            GameObject managerObject = new GameObject("DebugParameter");
            DontDestroyOnLoad(managerObject);
            // スクリプトをアタッチしてInstanceを初期化
            managerObject.AddComponent<DebugParameter>();
            Debug.Log("Generate GameData Instance");
        } else {
            GameObject managerObject = Instance.gameObject;
            //managerObject.AddComponent<GameData_Manager>();
            Debug.Log("GameData Instance already exists");
            return;
        }
    }*/

    /*public static void getismasked()
    {
        return ismasked;
    }*/

    public static void togglemasked()
    {
        ismasked = !ismasked;
        Debug.Log($"ismasked : {ismasked}");
    }

}