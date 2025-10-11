using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnlineViewer : MonoBehaviour
{
    public TextMeshProUGUI OnlineViewerText;

    public Image circleImage;

    void Start()
    {
        //circleImage = GetComponent<Image>();
        DisplayUpdate();
    }

    void DisplayUpdate()
    {
        if(DatabaseSwitcher.isServerUpload)
        {
            OnlineViewerText.text = "Online";
            circleImage.color = Color.green;
        } else {
            OnlineViewerText.text = "Offline";
            circleImage.color = Color.white;
        }
    }    
}   