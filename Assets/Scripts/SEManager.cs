using UnityEngine;

// このスクリプトにはAudioSourceが必須であることを示す
[RequireComponent(typeof(AudioSource))]
public class SEManager : MonoBehaviour
{
    // ゲーム全体で共有する唯一のインスタンス（実体）
    public static SEManager instance;

    private AudioSource audioSource;

    void Awake()
    {
        // シングルトン（ゲーム内にただ一つだけ存在する）の実装
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // シーンを移動しても破壊されないようにする
        }
        else
        {
            Destroy(gameObject); // すでに存在する場合は自分を破壊
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 指定されたSEを再生する（どこからでも呼び出せる）
    /// </summary>
    /// <param name="clip">再生したいSEのAudioClip</param>
    public void PlaySE(AudioClip clip)
    {
        // PlayOneShotを使うと、再生中のSEを中断せずに新しいSEを重ねて鳴らせる
        audioSource.PlayOneShot(clip);
    }
}