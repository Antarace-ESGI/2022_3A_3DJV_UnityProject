using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionMenuScript : MonoBehaviour
{
    
    public GameObject currentPanel;
    public GameObject nextPanel;

    public int dest = 0;
    
    private void CurrentPanelClose()
    {
        currentPanel.SetActive(false);
    }

    private void CurrentPanelOpen()
    {
        currentPanel.SetActive(true);
    }

    private void NextPanelOpen()
    {
        nextPanel.SetActive(true);
    }

    private void NextPanelClose()
    {
        nextPanel.SetActive(false);
    }

    public void navigationButton()
    {
        CurrentPanelClose();
        NextPanelOpen();
    }
    
    public void enablePause()
    {
        Time.timeScale = 0.0f;
        CurrentPanelClose();
        NextPanelOpen();
    }
    public void disablePause()
    {
        Time.timeScale = 1.0f;
        CurrentPanelClose();
        NextPanelOpen();
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
        GameObject inGamePanel = currentPanel;
        GameObject inPausePanel = nextPanel;
        if (Input.GetButtonDown("Start") && inGamePanel != null && inPausePanel != null)
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
    public void changeScene()
    {
        SceneManager.LoadScene(dest);
    }

    public void changeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    
    public void changeSceneWithIndex()
    {
        TrackArrayScript brain = FindObjectOfType<TrackArrayScript>();
        if (brain != null)
            SceneManager.LoadScene(brain.GetIndexOfTrack());
    }

    public void LoadAdditiveScene()
    {
        GameObject inGamePanel = currentPanel;
        TrackArrayScript brain = FindObjectOfType<TrackArrayScript>();
        if (brain)
        {
            brain.SetIndex(brain.GetIndex()+1);
            int i = brain.GetIndexOfTrack();
            if(i == 0)
                gameObject.SetActive(false);
            else
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
                inGamePanel.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
            // Default redirect to menu
            changeScene(3);
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
