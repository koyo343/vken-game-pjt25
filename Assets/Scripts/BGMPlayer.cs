using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    public static BGMPlayer instance { get; private set; }

    public AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GetAudioComponent();
    }

    /// <summary>
    /// 指定されたBGMを再生するメソッド
    /// </summary>
    public void PlayBGM(AudioClip bgmClip)
    {
        Debug.Log("PlayBGM is called.");
        GetAudioComponent();
        
        if (bgmClip == null || audioSource.clip == bgmClip)
        {
            Debug.Log($"bgmClip is null or already playing the same BGM.");
            return;
        }

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
        if (instance == null)
        {
            GameObject managerObject = new GameObject("BGMPlayer");
            DontDestroyOnLoad(managerObject);
            managerObject.AddComponent<BGMPlayer>();
            Debug.Log("Generate BGMPlayer Instance");
        } else {
            GameObject managerObject = instance.gameObject;
            managerObject.AddComponent<BGMPlayer>();
            Debug.Log("BGMPlayer Instance already exists");
            return;
        }
    }
}