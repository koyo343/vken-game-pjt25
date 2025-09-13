// AWSCredentials.cs
public static class AWSCredentials
{
    public static string AccessKey;
    public static string SecretKey;
    public static string Region;

    public static void Initialize()
    {
        DotEnv.Load();
        AccessKey = DotEnv.Get("A");
        SecretKey = DotEnv.Get("S");
        Region = DotEnv.Get("R");
    }
}