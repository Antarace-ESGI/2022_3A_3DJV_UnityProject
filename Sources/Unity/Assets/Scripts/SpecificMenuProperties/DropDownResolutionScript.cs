using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownResolutionScript : MonoBehaviour
{
    public Dropdown dropdown;

    private void Start()
    {
        initResolution();
    }

    private void initResolution()
    {
        if (PlayerPrefs.HasKey("ScreenWidth") && PlayerPrefs.HasKey("ScreenHeight"))
        {
            Screen.SetResolution(PlayerPrefs.GetInt("ScreenWidth"),PlayerPrefs.GetInt("ScreenHeight"),true);
        }
        else
        {
            Screen.SetResolution(Screen.currentResolution.width,Screen.currentResolution.height,true);
        }
    }
    
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

    public void saveScreenSettings()
    {
        string res = dropdown.options[dropdown.value].text;
        string[] tmp = res.Split(char.Parse("x"));

        int width = int.Parse(tmp[0]);
        int height = int.Parse(tmp[1]);
        PlayerPrefs.SetInt("ScreenWidth", width);
        PlayerPrefs.SetInt("ScreenHeight", height);
        PlayerPrefs.Save();
    }

    private void updateScreenSetting()
    {
        string res = dropdown.options[dropdown.value].text;
        string[] tmp = res.Split(char.Parse("x"));

        int width = int.Parse(tmp[0]);
        int height = int.Parse(tmp[1]);
        Screen.SetResolution(width,height,true);
    }
    
    private void OnEnable()
    {
        onResolutionScreen();
        dropdown.onValueChanged.AddListener(delegate
        {
            updateScreenSetting();
        });
    }
    
    // -------- Test ------------ //

    // For testing purpose only

    private void currentResolution()
    {
        Debug.Log(Screen.currentResolution);
    }
    
}
