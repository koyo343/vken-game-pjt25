// AWSCredentials.cs
public static class AWSCredentials
{
    public static string AccessKey;
    public static string SecretKey;
    public static string Region;

    public static void Initialize()
    {
        DotEnv.Load();
        if(!isLocal){
            if(string.IsNullOrEmpty(DotEnv.Get("A")) || string.IsNullOrEmpty(DotEnv.Get("S")) || string.IsNullOrEmpty(DotEnv.Get("R"))){
                Debug.LogError(".env do not has keys");
            } else {
                AccessKey = DotEnv.Get("A");
                SecretKey = DotEnv.Get("S");
                Region = DotEnv.Get("R");
                Debug.Log("AWSCredentials is initialized");
            }
        } else {
            Debug.Log("AWSCredentials is not initialized in Local Mode");
        }

    }
}