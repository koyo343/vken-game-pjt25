using UnityEngine;
using UnityEngine.Audio;

// このスクリプトにはAudioSourceが必須であることを示す
[RequireComponent(typeof(AudioSource))]
public class SEManager : MonoBehaviour
{
    // ゲーム全体で共有する唯一のインスタンス（実体）
    public static SEManager instance;

    private AudioSource audioSource;
    private AudioSource enemyaudioSource;

    public AudioMixer masterMixer;

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
        enemyaudioSource = gameObject.AddComponent<AudioSource>();

        if (masterMixer != null)
        {
            AudioMixerGroup[] groups = masterMixer.FindMatchingGroups("SE");

            if (groups.Length > 0)
            {
                enemyaudioSource.outputAudioMixerGroup = groups[0];
                Debug.Log("Audio Mixer Group 'SE' を AudioSource に設定しました。");
            }
            else
            {
                Debug.LogError("Audio Mixer Group 'SE' が見つかりません。名前を確認してください。");
            }
        }
        else
        {
            Debug.LogError("Audio Mixer ('GameMusicMixer') が見つかりません。Resourcesフォルダ内にあるか確認してください。");
        }  
    }

    /// <summary>
    /// 指定されたSEを再生する（どこからでも呼び出せる）
    /// </summary>
    /// <param name="clip">再生したいSEのAudioClip</param>
    public void PlaySE(AudioClip clip)
    {
        switch (clip.name)
        {
            case "敵やられ":
                if (enemyaudioSource.isPlaying)
                {
                    Debug.LogWarning($"敵やられSE ({clip.name}) は再生中です。スキップしました。");
                    break; 
                }
                enemyaudioSource.clip = clip; 
                enemyaudioSource.Play();
                Debug.Log($"SEを再生しましたwww: {clip.name}");
                break;

            case "カーソル音":
                audioSource.clip = clip;
                audioSource.time = 0.08f; 
                audioSource.Play(); 
                Debug.Log($"SEを0.08秒地点から開始しました: {clip.name}");
                break;

            default:
                audioSource.PlayOneShot(clip);
                Debug.Log($"SEを再生しました: {clip.name}");
                break;
        }
    }
}