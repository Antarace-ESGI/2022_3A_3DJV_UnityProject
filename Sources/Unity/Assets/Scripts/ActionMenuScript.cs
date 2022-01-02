using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class ActionMenuScript : MonoBehaviour
{
    
    public GameObject inPausePanel;
    public GameObject inGamePanel;

    private bool activatePause = false;

    private void switchPanel(GameObject currentPanel, GameObject nextPanel)
    {
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
    }
    
    public void enablePause()
    {
        Time.timeScale = 0.0f;
        switchPanel(inGamePanel,inPausePanel);
    }
    public void disablePause()
    {
        Time.timeScale = 1.0f;
        switchPanel(inPausePanel,inGamePanel);
    }
    public void exitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
    
    private void controllerActionButton()
    {
        if (Input.GetButtonDown("Start"))
        {
            if (inPausePanel.activeInHierarchy)
            {
                disablePause();
            }
            else
            {
                enablePause();
            }
            
        }
        /*
        if (Input.GetAxis("Vertical") > 0)
        {
            
        }
        else
        {
            
        }*/
    }

    public void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            controllerActionButton();
        }
    }
}
