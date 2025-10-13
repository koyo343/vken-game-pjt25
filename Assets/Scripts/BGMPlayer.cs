using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    // このスクリプトのインスタンス（実体）を、どこからでもアクセスできるように静的変数で保持する
    public static BGMPlayer instance { get; private set; }

    public AudioSource audioSource;

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
            Debug.Log("Generate instance");
        }
        else
        {
            //Destroy(gameObject);
            Debug.Log("instance is already exists");
            return;
        }

        // AudioSourceコンポーネントを取得
        GetAudioComponent();
    }

    /// <summary>
    /// 指定されたBGMを再生するメソッド
    /// </summary>
    /// <param name="bgmClip">再生したいAudioClip</param>
    public void PlayBGM(AudioClip bgmClip)
    {
        Debug.Log("PlayBGM is called.");
        GetAudioComponent();
        
        // 再生するBGMがnull、または現在再生中のBGMと同じ場合は何もしない
        if (bgmClip == null || audioSource.clip == bgmClip)
        {
            Debug.Log($"bgmClip is null or already playing the same BGM: {bgmClip.name}");
            return;
        }

        // BGMをセットして再生
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.Play();
        Debug.Log($"Play BGM:{bgmClip.name}");
    }

    public void GetAudioComponent()
    {
        audioSource = GetComponent<AudioSource>();
    }


    /// <summary>
    /// BGMの再生を停止するメソッド
    /// </summary>
    public void StopBGM()
    {
        audioSource.Stop();
    }

    public static void CheckNullBGMInstance()
    {
        // シーン内に他にBGMPlayerインスタンスが存在しないかチェック
        if (instance == null)
        {
            // 新しいGameObjectを作成
            GameObject managerObject = new GameObject("BGMPlayer");
            DontDestroyOnLoad(managerObject);
            // スクリプトをアタッチしてInstanceを初期化
            managerObject.AddComponent<BGMPlayer>();
            Debug.Log("Generate BGMPlayer Instance");
        } else {
            GameObject managerObject = instance.gameObject;
            //managerObject.AddComponent<BGMPlayer>();
            Debug.Log("BGMPlayer Instance already exists");
            return;
        }
    }
}