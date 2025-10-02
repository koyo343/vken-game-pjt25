using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // Unityエディタからタイトル画面のBGMを直接設定するための変数

    public BGMPlayer BGMPlayer;

    public AudioClip BGMtitle;

    void Start()
    {
        BGMPlayer.CheckNullBGMInstance();
        // BGM係を呼び出して、設定されたBGMを再生してもらう
        BGMPlayer.instance.PlayBGM(BGMtitle);
    }

    public void callPlayBGM()
    {
        BGMPlayer.instance.PlayBGM(BGMtitle);
    }

    public void updateBGMtitle(AudioClip bgmClip)
    {
        BGMtitle = bgmClip;
    }
}