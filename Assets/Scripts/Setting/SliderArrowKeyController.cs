using UnityEngine;
using UnityEngine.UI; // Sliderを扱うために必要
using UnityEngine.EventSystems; // EventSystemを扱うために必要

// このスクリプトがアタッチされたオブジェクトにSliderコンポーネントを必須にする
[RequireComponent(typeof(Slider))]
public class SliderArrowKeyController : MonoBehaviour
{
    // Inspectorビューから調整可能な、キー入力1回あたりのスライダーの値の変化量
    [Tooltip("矢印キーを一回押したときにスライダーの値をどれだけ変更するか")]
    [SerializeField]
    private float step = 0.025f;

    private Slider slider;

    void Start()
    {
        // このゲームオブジェクトにアタッチされているSliderコンポーネントを取得
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        // EventSystemによって現在選択されているUIオブジェクトが、
        // このスクリプトがアタッチされているゲームオブジェクトでなければ、何もしない
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            return;
        }

        // 左矢印キーが押された瞬間を検知
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            // スライダーの値をstep分だけ減らす
            slider.value -= step;

            //SE再生
            CursorSE cursorAudioData = GetComponent<CursorSE>();
            SEManager seManager = FindObjectOfType<SEManager>();
            if (seManager != null) 
            {
                seManager.PlaySE(cursorAudioData.CursorSound);
                Debug.Log($"SEを再生しました: {cursorAudioData.CursorSound.name}");
            }
            if(cursorAudioData.CursorSound == null)
            {
                Debug.Log("SEがアタッチされていません");
            }
            else
            {
                Debug.LogWarning("SEManagerが見つからないため、SEを再生できませんでした。");
            }
        }

        // 右矢印キーが押された瞬間を検知
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            // スライダーの値をstep分だけ増やす
            slider.value += step;

            //SE再生
            CursorSE cursorAudioData = GetComponent<CursorSE>();
            SEManager seManager = FindObjectOfType<SEManager>();
            if (seManager != null) 
            {
                seManager.PlaySE(cursorAudioData.CursorSound);
                Debug.Log($"SEを再生しました: {cursorAudioData.CursorSound.name}");
            }
            if(cursorAudioData.CursorSound == null)
            {
                Debug.Log("SEがアタッチされていません");
            }
            else
            {
                Debug.LogWarning("SEManagerが見つからないため、SEを再生できませんでした。");
            }
        }
    }
}