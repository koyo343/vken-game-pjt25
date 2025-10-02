using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public BGMPlayer BGMPlayer;

    public AudioClip BGMtitle;
    
    /// <summary>
    /// BGMの種類を増やすための配列 (Inspectorから設定)
    /// </summary>
    public AudioClip[] AdditionalBGMList; 

    void Start()
    {
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

    /// <summary>
    /// BGM Listのインデックスを指定してBGMを再生します。
    /// インデックス0はBGMtitle、インデックス1以降はAdditionalBGMListを参照します。
    /// </summary>
    /// <param name="index">再生したいBGMのインデックス（0から始まる番号）</param>
    public void PlayAdditionalBGMByIndex(int index)
    {
        if (BGMPlayer.instance == null)
        {
            Debug.LogError("BGMPlayerインスタンスが見つかりません。");
            return;
        }

        if (index == 0)
        {
            BGMPlayer.instance.PlayBGM(BGMtitle);
        }
        else if (index > 0 && index - 1 < AdditionalBGMList.Length)
        {
            BGMPlayer.instance.PlayBGM(AdditionalBGMList[index - 1]);
            Debug.Log($"Play Additional BGM Index: {index}");
        }
        else
        {
            Debug.LogWarning($"無効なBGMインデックスが指定されました: {index}");
        }
    }
}