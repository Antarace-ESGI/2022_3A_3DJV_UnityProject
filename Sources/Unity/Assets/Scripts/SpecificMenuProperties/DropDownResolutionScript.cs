using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownResolutionScript : MonoBehaviour
{
    public Dropdown dropdown;
    
    private void onResolutionScreen()
    {
        Resolution[] resolutions = Screen.resolutions;
        List<string> tmpL = new List<string>();
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            tmpL.Add($"{resolutions[i].width}x{resolutions[i].height}");
        }
        
        tmpL.Reverse();
        dropdown.AddOptions(tmpL);
    }

    public void saveScreenSetting()
    {
        string res = dropdown.options[dropdown.value].text;
        string[] tmp = res.Split(char.Parse("x"));

        int width = int.Parse(tmp[0]);
        int height = int.Parse(tmp[1]);
        Screen.SetResolution(width,height,true);
    }

    void Start()
    {
        Screen.fullScreen = !Screen.fullScreen;
        onResolutionScreen();
    }
    
}
