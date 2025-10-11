using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使用する場合に必要
using System.Collections.Generic;
using System.Threading.Tasks;

public static class Playmode
{
    public static bool isPlaymode = false;

    public static void togglePlaymode()
    {
        if (!isPlaymode)
        {
            if (DebugParameter.ExitisActive)
            {
                if (!DatabaseSwitcher.isServerUpload)
                {

                    DatabaseSwitcher.SwitchServerUpload();
                }
                DebugParameter.toggleExit();

            }
            else
            {
                if (!DatabaseSwitcher.isServerUpload)
                {

                    DatabaseSwitcher.SwitchServerUpload();
                }
            }
            isPlaymode = false;
        
        } else
        {
            if (DebugParameter.ExitisActive)
            {
                if (DatabaseSwitcher.isServerUpload)
                {

                    DatabaseSwitcher.SwitchServerUpload();
                }

            }
            else
            {
                if (!DatabaseSwitcher.isServerUpload)
                {

                    DatabaseSwitcher.SwitchServerUpload();
                }
                DebugParameter.toggleExit();
            }
            isPlaymode = true;
        }


        Debug.Log($"Playmode is {isPlaymode}");
    }

}

