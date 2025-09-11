using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    // このスクリプトのインスタンス（実体）を、どこからでもアクセスできるように静的変数で保持する
    public static BGMPlayer instance;

    private AudioSource audioSource;

    // ゲームが開始される一番最初のタイミングで一度だけ呼ばれる
    void Awake()
    {
        // シーン内に他にBGMPlayerインスタンスが存在しないかチェック
        if (instance == null)
        {
            // 存在しない場合、このインスタンスを保持する
            instance = this;
            // シーンを移動してもこのオブジェクトが破壊されないようにする
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // すでにBGMPlayerインスタンスが存在する場合、このオブジェクトは不要なので破壊する
            Destroy(gameObject);
            return; // 処理を中断
        }

        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 指定されたBGMを再生するメソッド
    /// </summary>
    /// <param name="bgmClip">再生したいAudioClip</param>
    public void PlayBGM(AudioClip bgmClip)
    {
        // 再生するBGMがnull、または現在再生中のBGMと同じ場合は何もしない
        if (bgmClip == null || audioSource.clip == bgmClip)
        {
            return;
        }

        // BGMをセットして再生
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    /// <summary>
    /// BGMの再生を停止するメソッド
    /// </summary>
    public void StopBGM()
    {
        audioSource.Stop();
    }
}