using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenuScript : MonoBehaviour
{
    
    public GameObject inPausePanel;
    public GameObject inGamePanel;

    public GameObject targetPanel;

    private void switchPanel(GameObject currentPanel, GameObject nextPanel)
    {
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
    }

    public void navigationButton()
    {
        switchPanel(transform.parent.gameObject, targetPanel);
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
    }

    public void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            controllerActionButton();
        }
    }
}
