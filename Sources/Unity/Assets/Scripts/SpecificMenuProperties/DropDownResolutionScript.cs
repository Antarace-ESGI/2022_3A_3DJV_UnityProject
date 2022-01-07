using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DropDownResolutionScript : MonoBehaviour
{
    public void onResolutionScreen()
    {
        Resolution[] resolutions = Screen.resolutions;
        Resolution currentResolution = Screen.currentResolution;
        
        List<string> tmpL = new List<string>();
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            tmpL.Add(resolutions[i].ToString());
        }
        
        tmpL.Reverse();
        GetComponent<Dropdown>().AddOptions(tmpL);
    }

    public void Start()
    {
        onResolutionScreen();
    }
}
