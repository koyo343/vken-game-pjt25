using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocalUI : MonoBehaviour
{
    string Localtext = "Now Displaying Local Ranking";
    string publictext = "Now Displaying Public Ranking";

    public TextMeshProUGUI RankingModeText;

    void Start()
    {
        if (DatabaseSwitcher.isLocal)
        {
            RankingModeText.text = Localtext;
        }
        else
        {
            RankingModeText.text = publictext;
        }
    }

    void Update()
    {
        if (!DatabaseSwitcher.LocalmodeisChenged)
        {
            return;
        }

        if (DatabaseSwitcher.isLocal)
        {
            RankingModeText.text = Localtext;
        }
        else
        {
            RankingModeText.text = publictext;
        }
        DatabaseSwitcher.LocalmodeisChenged = false;
    }
            
    
}