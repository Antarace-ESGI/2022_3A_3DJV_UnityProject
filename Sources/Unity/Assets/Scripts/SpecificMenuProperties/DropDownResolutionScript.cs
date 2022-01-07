using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownResolutionScript : MonoBehaviour
{
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
        GetComponent<Dropdown>().AddOptions(tmpL);
    }
    

    void Start()
    {
        onResolutionScreen();

        GetComponent<Dropdown>().onValueChanged.AddListener(delegate {
            Debug.Log(GetComponent<Dropdown>().value);
        });
    }
    
}
