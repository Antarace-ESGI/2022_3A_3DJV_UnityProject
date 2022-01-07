using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownResolutionScript : MonoBehaviour
{
    public Dropdown dropdown;
    
    private void onResolutionScreen()
    {
        Resolution[] resolutions = Screen.resolutions;
        Resolution currentResolution = Screen.currentResolution;
        
        List<string> tmpL = new List<string>();
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            tmpL.Add(resolutions[i].ToString());
        }
        
        tmpL.Reverse();
        dropdown.AddOptions(tmpL);
    }

    public void saveScreenSetting()
    {
        //Screen.SetResolution(1680,1050,true);
    }

    void Start()
    {
        onResolutionScreen();

        dropdown.onValueChanged.AddListener(delegate {
            saveScreenSetting();
        });
    }
    
}
