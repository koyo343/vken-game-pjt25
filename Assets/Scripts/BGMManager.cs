using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // Unityエディタからタイトル画面のBGMを直接設定するための変数
    public AudioClip titleBGM;

    void Start()
    {
        // BGM係を呼び出して、設定されたBGMを再生してもらう
        BGMPlayer.instance.PlayBGM(titleBGM);
    }
}