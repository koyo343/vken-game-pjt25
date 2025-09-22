// AWSCredentials.cs

using UnityEngine;
using UnityEngine.UI;
public static class AWSCredentials
{
    public static string AccessKey;
    public static string SecretKey;
    public static string Region;
    private static bool isInitialized = false;

    public static void Initialize()
    {
        DotEnv.Load();
        if(!DatabaseSwitcher.isLocal || !isInitialized){
            if(string.IsNullOrEmpty(DotEnv.Get("A")) || string.IsNullOrEmpty(DotEnv.Get("S")) || string.IsNullOrEmpty(DotEnv.Get("R"))){
                Debug.LogError(".env do not has keys enough");
            } else {
                AccessKey = DotEnv.Get("A");
                SecretKey = DotEnv.Get("S");
                Region = DotEnv.Get("R");
                isInitialized = true;
                Debug.Log("AWSCredentials is initialized");
            }
        } else {
            if(isInitialized)
            {
                Debug.Log("AWSCredentials is already initialized");
            } else {
                Debug.Log("AWSCredentials is not initialized in Local Mode");
                isInitialized = false;
            }
        }    
    }

    public static void isInitializedSwitch()
    {
        isInitialized = false;
    }
}