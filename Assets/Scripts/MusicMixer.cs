using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicMixer : MonoBehaviour
{
    // 司令塔と、それぞれのスライダーを接続する
    public AudioMixer audioMixer;
    public Slider masterSlider; // ALLスライダー用の変数を追加
    public Slider bgmSlider;
    public Slider seSlider;

    void Start()
    {
        // --- ALL(Master)の設定を追加 ---
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume_Setting", 1f);
        SetMasterVolume(masterSlider.value);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);

        // BGMの設定
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume_Setting", 1f);
        SetBgmVolume(bgmSlider.value);
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        
        // SEの設定
        seSlider.value = PlayerPrefs.GetFloat("SEVolume_Setting", 1f);
        SetSeVolume(seSlider.value);
        seSlider.onValueChanged.AddListener(SetSeVolume);
    }

    // --- ALL(Master)の音量を変更するメソッドを追加 ---
    public void SetMasterVolume(float volume)
    {
        float db = volume <= 0.001f ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("MasterVolume", db); // MasterVolumeという名前のつまみを操作
        PlayerPrefs.SetFloat("MasterVolume_Setting", volume);
    }

    // BGMの音量を変更するメソッド
    public void SetBgmVolume(float volume)
    {
        float db = volume <= 0.001f ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("BGMVolume", db);
        PlayerPrefs.SetFloat("BGMVolume_Setting", volume);
    }
    
    // SEの音量を変更するメソッド
    public void SetSeVolume(float volume)
    {
        float db = volume <= 0.001f ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("SEVolume", db);
        PlayerPrefs.SetFloat("SEVolume_Setting", volume);
    }
}