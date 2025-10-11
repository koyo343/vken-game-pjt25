using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使用する場合に必要
using System.Collections.Generic;
using System.Threading.Tasks;

public class playmodeButton : MonoBehaviour
{
    public Button PlaymodeButton;
    public TextMeshProUGUI PlaymodeText;

    void Start()
    {
        PlaymodeButton.onClick.AddListener(TogglePlaymode);
        Updatetext();
    }

    public void TogglePlaymode()
    {
        Playmode.togglePlaymode();
        Updatetext();
    }

    void Updatetext()
    {
        if (Playmode.isPlaymode)
        {
            PlaymodeText.text = "Playmode ON";
        }
        else
        {
            PlaymodeText.text = "Playmode OFF";
        }
    }
}